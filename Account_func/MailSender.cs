
using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;
using TheaterLaakBackend;

public class MailSender
{

    public void sendMail(string emailadres, string text, string subject)
    {
        var mailMessage = new MimeMessage();
        mailMessage.From.Add(new MailboxAddress("Theater Laak", "WubWubTunes@gmail.com"));
        mailMessage.To.Add(new MailboxAddress("Account", "theaterlaak123@gmail.com"));

        mailMessage.Subject = subject;
        mailMessage.Body = new TextPart("plain")
        {
            Text = text
        };

        using (var smtpClient = new SmtpClient())
        {
            smtpClient.Connect("smtp.gmail.com", 587, SecureSocketOptions.StartTls);
            smtpClient.Authenticate("WubWubTunes@gmail.com", "ptbnksiopfeezrvt");
            smtpClient.Send(mailMessage);
            smtpClient.Disconnect(true);
        }
    }

}

//TODO: Translate to English
