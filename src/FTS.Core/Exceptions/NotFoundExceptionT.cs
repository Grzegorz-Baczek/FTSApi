namespace FTS.Core.Exceptions;

public sealed class NotFoundException<T>(object key) : NotFoundException(typeof(T).Name, key) where T : class;
