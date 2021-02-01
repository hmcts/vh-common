using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using System.IO;
using VH.Core.Configuration.AKSKeyVaultFileProvider;

namespace VH.Core.Configuration
{
    public static class ConfigurationExtensions
    {
        public static IHostBuilder AddAksKeyVaultSecretProvider(this IHostBuilder hostBuilder)
        {
            return hostBuilder.ConfigureAppConfiguration((_, configuration) =>
            {
                var path = "/mnt/secrets/vh-infra-core/";
                if (Directory.Exists(path))
                {
                    configuration.AddKeyPerFile(k =>
                    {
                        k.FileProvider = new AksKeyVaultSecretFileProvider(path);
                        k.Optional = true;
                    });
                }
            });
        }
    }
}
