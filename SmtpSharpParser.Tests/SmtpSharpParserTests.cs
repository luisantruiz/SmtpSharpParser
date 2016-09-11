using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Net.Mail;

namespace SmtpSharpParser.Tests
{
    [TestClass]
    public class SmtpSharpParserTests
    {
        [TestMethod]
        public void ShouldWork()
        {
            var values = "from=emailaddress@smtp.com;userName=user@smtp.com;password=password;deliveryMethod=Network;deliveryFormat=SevenBit;"
                + "host=mail.smtp.com;enableSsl=true;port=25;defaultCredentials=true;";
            var smtpSection = SmtpSharpParser.Parse(values);

            Assert.AreEqual(smtpSection.From, "emailaddress@smtp.com");
            Assert.AreEqual(smtpSection.Network.UserName, "user@smtp.com");
            Assert.AreEqual(smtpSection.Network.Password, "password");
            Assert.AreEqual(smtpSection.DeliveryMethod, SmtpDeliveryMethod.Network);
            Assert.AreEqual(smtpSection.Network.Host, "mail.smtp.com");
            Assert.AreEqual(smtpSection.Network.EnableSsl, true);
            Assert.AreEqual(smtpSection.Network.Port, 25);
            Assert.AreEqual(smtpSection.DeliveryFormat, SmtpDeliveryFormat.SevenBit);
            Assert.AreEqual(smtpSection.Network.DefaultCredentials, true);
        }

        [TestMethod]
        public void ShouldWorkWithCustomSplitCharacter()
        {
            var values = "from=emailaddress@smtp.com,userName=user@smtp.com,password=password,deliveryMethod=Network,deliveryFormat=SevenBit,"
                + "host=mail.smtp.com,enableSsl=true,port=25,defaultCredentials=true,";
            var smtpSection = SmtpSharpParser.Parse(values, ",");

            Assert.AreEqual(smtpSection.From, "emailaddress@smtp.com");
            Assert.AreEqual(smtpSection.Network.UserName, "user@smtp.com");
            Assert.AreEqual(smtpSection.Network.Password, "password");
            Assert.AreEqual(smtpSection.DeliveryMethod, SmtpDeliveryMethod.Network);
            Assert.AreEqual(smtpSection.Network.Host, "mail.smtp.com");
            Assert.AreEqual(smtpSection.Network.EnableSsl, true);
            Assert.AreEqual(smtpSection.Network.Port, 25);
            Assert.AreEqual(smtpSection.DeliveryFormat, SmtpDeliveryFormat.SevenBit);
            Assert.AreEqual(smtpSection.Network.DefaultCredentials, true);
        }
    }
}
