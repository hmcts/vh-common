using FluentAssertions;
using NUnit.Framework;
using VH.Core.Configuration.AKSKeyVaultFileProvider;

namespace VH.Core.Configuration.UnitTests.AKSKeyVaultFileProvider.TheAksKeyVaultSecretFileProvider._GetFileInfo
{
    public class when_given_file_path_that_does_not_exist
    {
        private AksKeyVaultSecretFileProvider _sut;

        [SetUp]
        public void SetUp()
        {
            var rootFolder = TempFolderFactory.CreateExistingTempFolder();
            _sut = new AksKeyVaultSecretFileProvider(rootFolder);
        }

        [Test]
        public void should_return_instance_of_AksKeyVaultSecretFileInfo()
        {
            var contents = _sut.GetFileInfo("i_dont_exist.txt");
            contents.Should().BeOfType<AksKeyVaultSecretFileInfo>();
            contents.Exists.Should().BeFalse();
        }
    }
}
