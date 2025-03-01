namespace Infrastructure.Options;

public class RedisOptions
{
    public static readonly string ConfigName = "RedisOptions";
    public bool RedisEnabled { get; init; }
    public string? Host { get; init; }
    public int Port { get; init; }
    public bool RedisSsl { get; init; }
    public int RedisDatabase { get; init; }
    public int RedisSyncTimeout { get; init; }
    public bool RedisAbortOnConnectFail { get; init; }
    public string? Password { get; init; }
}