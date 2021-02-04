using FluentAssertions;
using Microsoft.Extensions.FileProviders;
using NUnit.Framework;
using VH.Core.Configuration.AKSKeyVaultFileProvider;

namespace VH.Core.Configuration.UnitTests.AKSKeyVaultFileProvider.TheAksKeyVaultSecretFileProvider._GetDirectoryContents
{
    public class when_given_the_root_folder_path
    {
        private AksKeyVaultSecretFileProvider _sut;
        private string _rootFolder;

        [SetUp]
        public void SetUp()
        {
            _rootFolder = TempFolderFactory.CreateExistingTempFolder();
            _sut = new AksKeyVaultSecretFileProvider(_rootFolder);
        }

        [Test]
        public void should_return_NotFoundDirectoryContents_Singleton()
        {
            var contents = _sut.GetDirectoryContents(_rootFolder);
            contents.Should().BeSameAs(NotFoundDirectoryContents.Singleton);
            contents.Exists.Should().BeFalse();
        }
    }
}
