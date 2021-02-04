using System;
using System.IO;
using FluentAssertions;
using NUnit.Framework;
using VH.Core.Configuration.AKSKeyVaultFileProvider;

namespace VH.Core.Configuration.UnitTests.AKSKeyVaultFileProvider.TheAksKeyVaultSecretFileProvider._GetFileInfo
{
    public class when_given_file_path_that_exists
    {
        private AksKeyVaultSecretFileProvider _sut;
        private string _fileName;

        [SetUp]
        public void SetUp()
        {
            var rootFolder = TempFolderFactory.CreateExistingTempFolder();
            _sut = new AksKeyVaultSecretFileProvider(rootFolder);

            _fileName = Guid.NewGuid()
                .ToString("N")
                .Substring(0, 5);

            File.WriteAllText(Path.Combine(rootFolder, _fileName), "bigfoot is real");
        }

        [Test]
        public void should_return_instance_of_AksKeyVaultSecretFileInfo()
        {
            var contents = _sut.GetFileInfo(_fileName);
            contents.Should().BeOfType<AksKeyVaultSecretFileInfo>();
            contents.Exists.Should().BeTrue();
        }
    }
}
