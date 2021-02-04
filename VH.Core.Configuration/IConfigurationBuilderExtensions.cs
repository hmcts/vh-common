using System.IO;
using Microsoft.Extensions.Configuration;
using VH.Core.Configuration.AKSKeyVaultFileProvider;

namespace VH.Core.Configuration
{
    public static class IConfigurationBuilderExtensions
    {
        /// <summary>
        /// <para>
        /// Adds Key-Per-File support for AKS.
        /// </para>
        /// <para>
        /// See: https://docs.microsoft.com/en-us/aspnet/core/fundamentals/configuration/?view=aspnetcore-3.1#key-per-file-configuration-provider
        /// </para>
        /// </summary>
        /// <param name="configurationBuilder"></param>
        /// <param name="pathToDirectoryContainingAppSecrets">
        /// <para>
        /// The path to the secrets folder; Usually in the format <c>/mnt/secrets/vh-{APPNAME}</c>, where <c>{APPNAME}</c> is the name of the application in lowercase letters.
        /// </para>
        /// <para>
        /// NOTE: The directory should contain one or more files, with one secret in each file, as per the Microsoft specification (see link in this method's summary).
        /// </para>
        /// </param>
        /// <returns></returns>
        public static IConfigurationBuilder AddAksKeyVaultSecretProvider(
            this IConfigurationBuilder configurationBuilder,
            string pathToDirectoryContainingAppSecrets)
        {
            if (Directory.Exists(pathToDirectoryContainingAppSecrets))
            {
                configurationBuilder.AddKeyPerFile(k =>
                {
                    k.FileProvider = new AksKeyVaultSecretFileProvider(pathToDirectoryContainingAppSecrets);
                    k.Optional = true;
                });
            }

            return configurationBuilder;
        }

        /// <summary>
        /// <para>
        /// Adds Key-Per-File support for AKS.
        /// </para>
        /// <para>
        /// See: https://docs.microsoft.com/en-us/aspnet/core/fundamentals/configuration/?view=aspnetcore-3.1#key-per-file-configuration-provider
        /// </para>
        /// </summary>
        /// <param name="configurationBuilder"></param>
        /// <param name="directoryInfoContainingAppSecrets">
        /// <para>
        /// The path to the secrets folder; Usually in the format <c>/mnt/secrets/vh-{APPNAME}</c>, where <c>{APPNAME}</c> is the name of the application in lowercase letters.
        /// </para>
        /// <para>
        /// NOTE: The directory should contain one or more files, with one secret in each file, as per the Microsoft specification (see link in this method's summary).
        /// </para>
        /// </param>
        /// <returns></returns>
        public static IConfigurationBuilder AddAksKeyVaultSecretProvider(
            this IConfigurationBuilder configurationBuilder,
            DirectoryInfo directoryInfoContainingAppSecrets)
        {
            if (directoryInfoContainingAppSecrets.Exists)
            {
                configurationBuilder.AddKeyPerFile(k =>
                {
                    k.FileProvider = new AksKeyVaultSecretFileProvider(directoryInfoContainingAppSecrets.FullName);
                    k.Optional = true;
                });
            }

            return configurationBuilder;
        }
    }
}
