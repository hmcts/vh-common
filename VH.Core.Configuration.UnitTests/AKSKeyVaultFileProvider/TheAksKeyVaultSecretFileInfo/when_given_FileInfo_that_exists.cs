using System;
using System.IO;
using FluentAssertions;
using NUnit.Framework;
using VH.Core.Configuration.AKSKeyVaultFileProvider;

namespace VH.Core.Configuration.UnitTests.AKSKeyVaultFileProvider.TheAksKeyVaultSecretFileInfo
{
    public class when_given_FileInfo_that_exists
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
            File.WriteAllText(filePath, "aliens live among us");

            _fileInfo = new FileInfo(filePath);

            _sut = new AksKeyVaultSecretFileInfo(_fileInfo);
        }

        [Test]
        public void should_map_FileInfo_correctly()
        {
            _sut.Exists.Should().Be(_fileInfo.Exists);
            _sut.Length.Should().Be(_fileInfo.Length);
            _sut.PhysicalPath.Should().Be(_fileInfo.FullName);
            _sut.Name.Should().Be(_fileInfo.Name);
            _sut.LastModified.Should().Be(_fileInfo.LastWriteTimeUtc);
            _sut.IsDirectory.Should().BeFalse();
        }

        [Test]
        public void should_replace_dashes_in_Name_with_underscores()
        {
            var sut = new AksKeyVaultSecretFileInfo(new FileInfo("--file--name--"));
            sut.Name.Should().Be("__file__name__");
        }

        [Test]
        public void should_return_stream_upon_calling_CreateReadStream()
        {
            using (var stream = _sut.CreateReadStream())
            {
                stream.Should().NotBeNull();
            }


            //var action = new Action(() => _sut.CreateReadStream());
            //action.Should().Throw<InvalidOperationException>();
        }
    }
}
