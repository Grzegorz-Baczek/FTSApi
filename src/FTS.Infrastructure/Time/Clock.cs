using FTS.Core.Abstractions;

namespace FTS.Infrastructure.Time;

internal sealed class Clock : IClock
{
    public DateTime Current() => DateTime.UtcNow;
}
