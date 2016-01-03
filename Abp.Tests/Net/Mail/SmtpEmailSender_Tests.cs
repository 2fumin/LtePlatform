using Abp.Net.Mail.Smtp;
using NSubstitute;
using Xunit;

namespace Abp.Tests.Net.Mail
{
    public class SmtpEmailSender_Tests
    {
        private readonly SmtpEmailSender _smtpEmailSender;

        public SmtpEmailSender_Tests()
        {
            var configuration = Substitute.For<ISmtpEmailSenderConfiguration>();

            configuration.DefaultFromAddress.Returns("smtp.189.cn");
            configuration.DefaultFromDisplayName.Returns("Ouyang Hui");

            configuration.Host.Returns("smtp.189.cn");
            configuration.Port.Returns(25);

            //configuration.Domain.Returns("...");
            configuration.UserName.Returns("ouyh18");
            configuration.Password.Returns("md@287965");

            //configuration.EnableSsl.Returns(false);
            //configuration.UseDefaultCredentials.Returns(false);

            _smtpEmailSender = new SmtpEmailSender(configuration);
        }

        [Fact] //Need to set configuration before executing this test
        public void Test_Send_Email()
        {
            _smtpEmailSender.Send(
                "ouyh18@189.cn",
                "ouyh19@189.cn",
                "Test email", 
                "An email body",
                true
                );
        }
    }
}
