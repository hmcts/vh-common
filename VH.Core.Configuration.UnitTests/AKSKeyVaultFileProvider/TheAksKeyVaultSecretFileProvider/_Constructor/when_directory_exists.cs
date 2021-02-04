using System;
using FluentAssertions;
using NUnit.Framework;
using VH.Core.Configuration.AKSKeyVaultFileProvider;

namespace VH.Core.Configuration.UnitTests.AKSKeyVaultFileProvider.TheAksKeyVaultSecretFileProvider._Constructor
{
    public class when_directory_exists
    {
        [Test]
        public void should_not_throw_any_Exception()
        {
            var rootFolder = TempFolderFactory.CreateExistingTempFolder();
            var action = new Action(() => new AksKeyVaultSecretFileProvider(rootFolder));
            action.Should().NotThrow();
        }
    }
}
