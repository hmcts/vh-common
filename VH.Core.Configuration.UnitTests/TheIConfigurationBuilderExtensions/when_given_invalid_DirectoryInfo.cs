using System;
using System.IO;
using FluentAssertions;
using Microsoft.Extensions.Configuration;
using Moq;
using NUnit.Framework;

namespace VH.Core.Configuration.UnitTests.TheIConfigurationBuilderExtensions
{
    public class when_given_invalid_DirectoryInfo
    {
        [Test]
        public void should_not_throw_any_exceptions()
        {
            var randomString = Guid.NewGuid().ToString("N");
            var configBuilder = new Mock<IConfigurationBuilder>();
            var action = new Action(() =>
            {
                var di = new DirectoryInfo($"c:\\{randomString}");
                configBuilder.Object.AddAksKeyVaultSecretProvider(di);
            });

            action.Should().NotThrow();
        }
    }
}
