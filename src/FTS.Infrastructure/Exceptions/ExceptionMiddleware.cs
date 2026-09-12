using FTS.Core.Exceptions;
using FluentValidation;
using Humanizer;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Data.SqlClient;

namespace FTS.Infrastructure.Exceptions;

internal sealed class ExceptionMiddleware : IMiddleware
{
    private readonly ILogger<ExceptionMiddleware> _logger;

    public ExceptionMiddleware(ILogger<ExceptionMiddleware> logger)
        => _logger = logger;

    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        try
        {
            await next(context);
        }
        catch (Exception exception)
        {
            await HandleAsync(exception, context);
        }
    }

    private async Task HandleAsync(Exception exception, HttpContext context)
    {
        if (exception is OperationCanceledException && context.RequestAborted.IsCancellationRequested)
            return;

        if (context.Response.HasStarted)
        {
            _logger.LogError(exception, "Exception after response started.");
            return;
        }

        var (statusCode, error) = Map(exception, context.TraceIdentifier);
        error = error with { StatusCode = statusCode };

        if (statusCode >= 500)
            _logger.LogError(exception, "{Code}", error.Code);
        else
            _logger.LogInformation("{Code}: {Reason}", error.Code, error.Reason);

        context.Response.StatusCode = statusCode;
        await context.Response.WriteAsJsonAsync(error);
    }

    private static (int StatusCode, Error Error) Map(Exception exception, string traceId) => exception switch
    {
        ValidationException validationException =>
            (StatusCodes.Status400BadRequest,
             new Error("validation", "Validation failed.", FieldErrors(validationException), traceId)),

        UnauthorizedException e => (StatusCodes.Status401Unauthorized, Custom(e, traceId)),
        ForbiddenException e => (StatusCodes.Status403Forbidden, Custom(e, traceId)),
        NotFoundException e =>
            (StatusCodes.Status404NotFound,
             new Error($"{e.Resource.Underscore()}_not_found", e.Message, null, traceId)),
        ConflictException e => (StatusCodes.Status409Conflict, Custom(e, traceId)),

        DomainException e => (StatusCodes.Status400BadRequest, Custom(e, traceId)),

        DbUpdateException { InnerException: SqlException { Number: 2601 or 2627 } } =>
            (StatusCodes.Status409Conflict,
             new Error("duplicate", "Resource already exists.", null, traceId)),

        CustomException e => (StatusCodes.Status400BadRequest, Custom(e, traceId)),
        _ => (StatusCodes.Status500InternalServerError,
              new Error("error", "There was an error.", null, traceId))
    };

    private static Error Custom(CustomException exception, string traceId)
        => new(exception.GetType().Name.Underscore().Replace("_exception", string.Empty),
            exception.Message, null, traceId);

    private static Dictionary<string, string[]> FieldErrors(ValidationException exception)
        => exception.Errors
            .GroupBy(error => error.PropertyName.Camelize())
            .ToDictionary(group => group.Key, group => group.Select(error => error.ErrorMessage).ToArray());
}

public sealed record Error(string Code, string Reason, Dictionary<string, string[]>? Errors, string TraceId, int StatusCode = 0);