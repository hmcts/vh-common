using System;
using FluentAssertions;
using NUnit.Framework;
using VH.Core.Configuration.AKSKeyVaultFileProvider;

namespace VH.Core.Configuration.UnitTests.AKSKeyVaultFileProvider.TheAksKeyVaultSecretFileProvider._Constructor
{
    public class when_given_null_or_whitespace_string
    {
        [TestCase("")]
        [TestCase(" ")]
        [TestCase(null)]
        public void should_throw_ArgumentException(string input)
        {
            var action = new Action(() => new AksKeyVaultSecretFileProvider(input));
            action.Should().Throw<ArgumentException>();
        }
    }
}
