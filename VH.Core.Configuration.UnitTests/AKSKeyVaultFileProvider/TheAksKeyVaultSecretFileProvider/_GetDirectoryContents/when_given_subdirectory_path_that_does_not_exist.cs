using FluentAssertions;
using Microsoft.Extensions.FileProviders;
using NUnit.Framework;
using VH.Core.Configuration.AKSKeyVaultFileProvider;

namespace VH.Core.Configuration.UnitTests.AKSKeyVaultFileProvider.TheAksKeyVaultSecretFileProvider._GetDirectoryContents
{
    public class when_given_subdirectory_path_that_does_not_exist
    {
        private AksKeyVaultSecretFileProvider _sut;

        [SetUp]
        public void SetUp()
        {
            var rootFolder = TempFolderFactory.CreateExistingTempFolder();
            _sut = new AksKeyVaultSecretFileProvider(rootFolder);
        }

        [Test]
        public void should_return_NotFoundDirectoryContents_Singleton()
        {
            var contents = _sut.GetDirectoryContents("i_dont_exist");
            contents.Should().BeSameAs(NotFoundDirectoryContents.Singleton);
            contents.Exists.Should().BeFalse();
        }
    }
}
