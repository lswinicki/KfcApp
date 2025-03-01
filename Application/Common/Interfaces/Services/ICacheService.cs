namespace Application.Common.Interfaces.Services;

public interface ICacheService
{
    Task SetAsync<T>(string key, T value, int durationHours, int durationMinutes, int durationSeconds, CancellationToken cancellationToken = default);
    Task<T> GetValueAsync<T>(string? key, CancellationToken cancellationToken = default);
}