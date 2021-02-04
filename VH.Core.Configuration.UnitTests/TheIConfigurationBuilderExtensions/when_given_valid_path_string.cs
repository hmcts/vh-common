using System;
using System.IO;
using System.Linq;
using FluentAssertions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.KeyPerFile;
using Moq;
using NUnit.Framework;

namespace VH.Core.Configuration.UnitTests.TheIConfigurationBuilderExtensions
{
    public class when_given_valid_path_string
    {
        public DirectoryInfo GenerateTempFolder()
        {
            var path = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString("N"));
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            return new DirectoryInfo(path);
        }

        [Test]
        public void should_not_throw_any_exceptions()
        {
            // arrange
            var tempFolderInfo = GenerateTempFolder();
            var configBuilder = new Mock<IConfigurationBuilder>();

            // act
            var action = new Action(() =>
            {
                configBuilder.Object.AddAksKeyVaultSecretProvider(tempFolderInfo.FullName);
            });

            // assert
            action.Should().NotThrow();
        }

        [TestCase(0)]
        [TestCase(1)]
        [TestCase(2)]
        [TestCase(10)]
        public void should_add_key_per_file_for_each_file_in_folder(int numberOfFiles)
        {
            // arrange
            var tempFolderInfo = GenerateTempFolder();

            for (var i = 0; i < numberOfFiles; i++)
            {
                var key = $"secret key {i:D2}";
                var value = $"secret value {i:D2}";
                var filePath = Path.Combine(tempFolderInfo.FullName, key);
                File.WriteAllText(filePath, value);
            }

            // act
            var configRoot = new ConfigurationBuilder()
                .AddAksKeyVaultSecretProvider(tempFolderInfo.FullName)
                .Build();

            // assert
            configRoot.Providers.Count().Should().Be(1);

            var keyPerFileConfigurationProvider = configRoot.Providers.First();
            keyPerFileConfigurationProvider.Should().BeOfType<KeyPerFileConfigurationProvider>();

            for (var i = 0; i < numberOfFiles; i++)
            {
                keyPerFileConfigurationProvider.TryGet($"secret key {i:D2}", out var value);
                value.Should().Be($"secret value {i:D2}");
            }
        }
    }
}
