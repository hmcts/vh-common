using System;
using System.IO;
using FluentAssertions;
using NUnit.Framework;
using VH.Core.Configuration.AKSKeyVaultFileProvider;

namespace VH.Core.Configuration.UnitTests.AKSKeyVaultFileProvider.TheAksKeyVaultSecretFileProvider._Constructor
{
    public class when_directory_does_not_exist
    {
        [Test]
        public void should_throw_DirectoryNotFoundException()
        {
            var rootFolder = TempFolderFactory.CreateNonExistingTempFolder();
            var action = new Action(() => new AksKeyVaultSecretFileProvider(rootFolder));
            action.Should().Throw<DirectoryNotFoundException>();
        }
    }
}
