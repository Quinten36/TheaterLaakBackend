using TechTalk.SpecFlow;
using Xunit;

namespace TheaterLaak
{
    [Binding]
    public class SendMailSteps
    {
        private readonly MailSender _mailSender;
        private readonly ScenarioContext _scenarioContext;

        public bool result;
        public SendMailSteps(ScenarioContext scenarioContext)
        {
            _scenarioContext = scenarioContext;
            _mailSender = new MailSender();
        }

        [Given("the email address \"(.*)\" and subject \"(.*)\" and text \"(.*)\"")]

        public void GivenTheEmailAddressAndSubjectAndText(string email, string subject, string text)
        {
            _scenarioContext["email"] = email;
            _scenarioContext["subject"] = subject;
            _scenarioContext["text"] = text;
        }

        [When("the sendMail method is called with the given email address, subject, and text")]
        public void WhenTheSendMailMethodIsCalledWithTheGivenEmailAddressSubjectAndText()
        {
            string email = _scenarioContext.Get<string>("email");
            string subject = _scenarioContext.Get<string>("subject");
            string text = _scenarioContext.Get<string>("text");
            result = _mailSender.sendMail(email, text, subject);
        }

        [Then("a message should have been sent to the user and the method returned true")]
        public void ThenAMessageShouldBeSentToWithTheSubjectAndTheText()
        {           
            Assert.True(result);
        }

    }
}