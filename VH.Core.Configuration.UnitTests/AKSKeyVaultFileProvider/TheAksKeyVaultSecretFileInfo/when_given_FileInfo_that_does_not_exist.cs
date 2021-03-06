using System;
using System.IO;
using FluentAssertions;
using NUnit.Framework;
using VH.Core.Configuration.AKSKeyVaultFileProvider;

namespace VH.Core.Configuration.UnitTests.AKSKeyVaultFileProvider.TheAksKeyVaultSecretFileInfo
{
    public class when_given_FileInfo_that_does_not_exist
    {
        private AksKeyVaultSecretFileInfo _sut;
        private FileInfo _fileInfo;

        [SetUp]
        public void SetUp()
        {
            var rootFolder = TempFolderFactory.CreateExistingTempFolder();
            var fileName = Guid.NewGuid()
                .ToString("N")
                .Substring(0, 5);

            var filePath = Path.Combine(rootFolder, fileName);
            _fileInfo = new FileInfo(filePath);

            _sut = new AksKeyVaultSecretFileInfo(_fileInfo);
        }

        [Test]
        public void should_map_FileInfo_correctly()
        {
            _sut.Exists.Should().Be(_fileInfo.Exists);

            var action = new Action(() =>
            {
                var x = _sut.Length;
            });
            action.Should().Throw<FileNotFoundException>();

            _sut.PhysicalPath.Should().Be(_fileInfo.FullName);
            _sut.Name.Should().Be(_fileInfo.Name);
            _sut.LastModified.Should().Be(_fileInfo.LastWriteTimeUtc);
            _sut.IsDirectory.Should().BeFalse();
        }

        [Test]
        public void should_throw_FileNotFoundException_upon_calling_CreateReadStream()
        {
            var action = new Action(() => _sut.CreateReadStream());
            action.Should().Throw<FileNotFoundException>();
        }
    }
}
