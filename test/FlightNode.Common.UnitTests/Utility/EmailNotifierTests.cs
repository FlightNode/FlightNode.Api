using FlightNode.Common.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Ploeh.AutoFixture;
using Moq;
using SendGrid;
using System.Net.Mail;

namespace FlightNode.Common.UnitTests.Utility
{
    public class EmailNotifierTests : BaseTester
    {
        public class EmailNotifierTss : EmailNotifier
        {
            public EmailNotifierTss(IEmailFactory factory) : base(factory)
            {
            }
        }

        public class SendAsync : BaseTester
        {
            [Fact]
            public void ConstructorRejectsNullArgument()
            {
                Assert.Throws<ArgumentNullException>(() => new EmailNotifier(null));
            }

            [Fact]
            public void RejectsNullArgument()
            {
                Assert.ThrowsAsync<ArgumentNullException>(() => new EmailNotifier(Fixture.Create<IEmailFactory>()).SendAsync(null));
            }

            const string to = "someone@somewhere.com";
            const string subject = "test";
            const string body = "body";
            const string fromEmail = "from@example.com";
            const string fromName = "Zeus";

            private NotificationModel CreateModel()
            {
                return new NotificationModel(to, subject, body)
                {
                    FromEmail = fromEmail,
                    FromName = fromName
                };
            }

            private EmailNotifier CreateSystem()
            {
                return Fixture.Create<EmailNotifierTss>();
            }

            private Mock<ISendGrid> FreezeSendGridMessage()
            {
                return Fixture.Freeze<Mock<ISendGrid>>();
            }

            private Mock<ITransport> FreeTransport()
            {
                return Fixture.Freeze<Mock<ITransport>>();
            }

            private Mock<IEmailFactory> FreezeSendGridFactory()
            {
                var factoryMock = Fixture.Freeze<Mock<IEmailFactory>>();

                factoryMock.Setup(x => x.CreateMessage())
                   .Returns(Fixture.Create<Mock<ISendGrid>>().Object);

                factoryMock.Setup(x => x.CreateTransport())
                    .Returns(Fixture.Create<Mock<ITransport>>().Object);

                return factoryMock;
            }

            [Fact]
            public void DeliversMailUsingValueOfTo()
            {
               var messageMock = RunMessageVerificationTest();

                // Assert
                messageMock.Verify(x => x.AddTo(to));
            }

            [Fact]
            public void DeliversMailUsingValueOfSubject()
            {
                var messageMock = RunMessageVerificationTest();

                // Assert
                messageMock.VerifySet(x => x.Subject = subject);
            }

            [Fact]
            public void DeliversMailUsingValueOfBody()
            {
                var messageMock = RunMessageVerificationTest();

                // Assert
                messageMock.VerifySet(x => x.Text = body);
            }

            [Fact]
            public void DeliversMailUsingValueOfFromName()
            {
                var messageMock = RunMessageVerificationTest();

                // Assert
                messageMock.VerifySet(x => x.From = It.Is<MailAddress>(y => y.DisplayName == fromName));
            }

            [Fact]
            public void DeliversMailUsingValueOfFromEmail()
            {
                var messageMock = RunMessageVerificationTest();

                // Assert
                messageMock.VerifySet(x => x.From = It.Is<MailAddress>(y => y.Address == fromEmail));
            }

            [Fact]
            public void DeliversMail()
            {
                // Arrange
                var model = CreateModel();

                var messageMock = FreezeSendGridMessage();
                var transportMock = FreeTransport();
                FreezeSendGridFactory();

                // Act
                CreateSystem().SendAsync(model);

                // Assert
                transportMock.Verify(x => x.DeliverAsync(It.Is<ISendGrid>(y => object.ReferenceEquals(messageMock.Object, y))));
            }

            private Mock<ISendGrid> RunMessageVerificationTest()
            {
                // Arrange
                var model = CreateModel();

                var messageMock = FreezeSendGridMessage();
                FreeTransport();
                FreezeSendGridFactory();

                // Act
                CreateSystem().SendAsync(model);
                return messageMock;
            }
        }
    }
}
