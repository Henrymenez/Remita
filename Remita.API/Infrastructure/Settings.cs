using Remita.Cache.Configuration;
using Remita.Services.Utility;

namespace Remita.Api.Infrastructure
{
    public class Settings
    {
        public JwtConfig? JwtConfig { get; set; }
        public RedisConfig? RedisConfig { get; set; }
        //   public ElasticSearchConfig? ElasticSearch { get; set; }
        //  public ServiceBusSettings? MWBServiceBus { get; init; }
        //  public BlobSettings? MWBBlob { get; init; }

    }
}
