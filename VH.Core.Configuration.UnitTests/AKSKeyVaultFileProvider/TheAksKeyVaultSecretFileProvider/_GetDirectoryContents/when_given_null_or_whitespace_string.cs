using FluentAssertions;
using Microsoft.Extensions.FileProviders;
using NUnit.Framework;
using VH.Core.Configuration.AKSKeyVaultFileProvider;

namespace VH.Core.Configuration.UnitTests.AKSKeyVaultFileProvider.TheAksKeyVaultSecretFileProvider._GetDirectoryContents
{
    public class when_given_null_or_whitespace_string
    {
        private AksKeyVaultSecretFileProvider _sut;

        [SetUp]
        public void SetUp()
        {
            var rootFolder = TempFolderFactory.CreateExistingTempFolder();
            _sut = new AksKeyVaultSecretFileProvider(rootFolder);
        }

        [TestCase("")]
        [TestCase(" ")]
        [TestCase(null)]
        public void should_return_NotFoundDirectoryContents_Singleton(string input)
        {
            var contents = _sut.GetDirectoryContents(input);
            contents.Should().BeSameAs(NotFoundDirectoryContents.Singleton);
            contents.Exists.Should().BeFalse();
        }
    }
}
