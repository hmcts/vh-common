using System;
using FluentAssertions;
using Microsoft.Extensions.Configuration;
using Moq;
using NUnit.Framework;

namespace VH.Core.Configuration.UnitTests.TheIConfigurationBuilderExtensions
{
    public class when_given_invalid_path_string
    {
        [Test]
        public void should_not_throw_any_exceptions()
        {
            var randomString = Guid.NewGuid().ToString("N");
            var configBuilder = new Mock<IConfigurationBuilder>();
            var action = new Action(() =>
            {
                configBuilder.Object.AddAksKeyVaultSecretProvider($"c:\\{randomString}");
            });

            action.Should().NotThrow();
        }
    }
}
