using System;
using System.IO;
using FluentAssertions;
using NUnit.Framework;
using VH.Core.Configuration.AKSKeyVaultFileProvider;

namespace VH.Core.Configuration.UnitTests.AKSKeyVaultFileProvider.TheTheAksKeyVaultSecretDirectoryInfo
{
    public class when_given_DirectoryInfo_that_exists
    {
        private AksKeyVaultSecretDirectoryInfo _sut;
        private DirectoryInfo _directoryInfo;

        [SetUp]
        public void SetUp()
        {
            var existingTempFolder = TempFolderFactory.CreateExistingTempFolder();
            _directoryInfo = new DirectoryInfo(existingTempFolder);
            _sut = new AksKeyVaultSecretDirectoryInfo(_directoryInfo);
        }

        [Test]
        public void should_map_DirectoryInfo_correctly()
        {
            _sut.Exists.Should().Be(_directoryInfo.Exists);
            _sut.Length.Should().Be(-1);
            _sut.PhysicalPath.Should().Be(_directoryInfo.FullName);
            _sut.Name.Should().Be(_directoryInfo.Name);
            _sut.LastModified.Should().Be(_directoryInfo.LastWriteTimeUtc);
            _sut.IsDirectory.Should().BeTrue();
        }

        [Test]
        public void should_throw_InvalidOperationException_upon_calling_CreateReadStream()
        {
            var action = new Action(() => _sut.CreateReadStream());
            action.Should().Throw<InvalidOperationException>();
        }
    }
}
