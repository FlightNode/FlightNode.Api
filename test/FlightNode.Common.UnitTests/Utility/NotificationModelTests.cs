using FlightNode.Common.Utility;
using FluentAssertions;
using System;
using Xunit;

namespace FlightNode.Common.UnitTests.Utility
{
    public class NotificationModelTests : BaseTester
    {
        public class Constructor : NotificationModelTests
        {
            [Fact]
            public void InitializesFromAddressBasedOnSettings()
            {
                var system = new NotificationModel("a", "b", "c");

                Assert.Equal(FlightNode.Common.Properties.Settings.Default.FromEmail, system.FromEmail);
            }

            [Fact]
            public void InitializesFromNameBasedOnSettings()
            {
                var system = new NotificationModel("a", "b", "c");

                Assert.Equal(FlightNode.Common.Properties.Settings.Default.FromName, system.FromName);
            }

            [Fact]
            public void AssignsTo()
            {
                var expected = "hi";
                var system = new NotificationModel(expected, "b", "c");

                Assert.Equal(expected, system.To);
            }

            [Fact]
            public void AssignsSubject()
            {
                var expected = "hi";
                var system = new NotificationModel("a",expected, "c");

                Assert.Equal(expected, system.Subject);
            }

            [Fact]
            public void AssignsBody()
            {
                var expected = "hi";
                var system = new NotificationModel("a", "b", expected);

                system.Body.Should().Be(expected);
            }

            [Fact]
            public void RejectsNullTo()
            {
                Assert.Throws<ArgumentNullException>(() => new NotificationModel(null, "a", "b"));
            }

            [Fact]
            public void RejectsNullSubject()
            {
                Assert.Throws<ArgumentNullException>(() => new NotificationModel("a", null, "b"));
            }

            [Fact]
            public void RejectsNullBody()
            {
                Assert.Throws<ArgumentNullException>(() => new NotificationModel("a", "b",null));
            }

            [Fact]
            public void RejectsEmptyTo()
            {
                Assert.Throws<ArgumentException>(() => new NotificationModel("   ", "a", "b"));
            }

            [Fact]
            public void RejectsEmptySubject()
            {
                Assert.Throws<ArgumentException>(() => new NotificationModel("a", "   ",  "b"));
            }

            [Fact]
            public void RejectsEmptyBody()
            {
                Assert.Throws<ArgumentException>(() => new NotificationModel("a", "b","   "));
            }
        }
    }
}
