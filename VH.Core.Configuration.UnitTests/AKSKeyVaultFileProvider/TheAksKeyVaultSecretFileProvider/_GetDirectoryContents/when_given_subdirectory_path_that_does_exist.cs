using System;
using System.IO;
using FluentAssertions;
using NUnit.Framework;
using VH.Core.Configuration.AKSKeyVaultFileProvider;

namespace VH.Core.Configuration.UnitTests.AKSKeyVaultFileProvider.TheAksKeyVaultSecretFileProvider._GetDirectoryContents
{
    public class when_given_subdirectory_path_that_does_exist
    {
        private AksKeyVaultSecretFileProvider _sut;
        private string _subDirectoryName;

        [SetUp]
        public void SetUp()
        {
            var rootFolder = TempFolderFactory.CreateExistingTempFolder();
            _sut = new AksKeyVaultSecretFileProvider(rootFolder);

            _subDirectoryName = Guid.NewGuid()
                .ToString("N")
                .Substring(0, 5);

            Directory.CreateDirectory(Path.Combine(rootFolder, _subDirectoryName));
        }

        [Test]
        public void should_return_instance_of_AksKeyVaultSecretDirectoryContents()
        {
            var contents = _sut.GetDirectoryContents(_subDirectoryName);
            contents.Should().BeOfType<AksKeyVaultSecretDirectoryContents>();
            contents.Exists.Should().BeTrue();
        }
    }
}
