using FluentAssertions;
using Microsoft.Extensions.FileProviders;
using NUnit.Framework;
using VH.Core.Configuration.AKSKeyVaultFileProvider;

namespace VH.Core.Configuration.UnitTests.AKSKeyVaultFileProvider.TheAksKeyVaultSecretFileProvider._GetFileInfo
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
        public void should_return_NotFoundFileInfo_instance(string input)
        {
            var contents = _sut.GetFileInfo(input);
            contents.Should().BeOfType<NotFoundFileInfo>();
            contents.Exists.Should().BeFalse();
        }
    }
}
