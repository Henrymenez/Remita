using Remita.Cache.Configuration;
using Remita.Services.Utility;

namespace Remita.Configuration;

public class RemitaApiConfig
{
    public string ConnectionString { get; set; } = null!;
    public RedisConfig Redis { get; set; } = null!;
    public JwtConfig Jwt { get; set; } = null!;
}
