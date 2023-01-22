
using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;

public class MailSender
{

    public bool sendMail(string emailadres, string text, string subject)
    {
        var mailMessage = new MimeMessage();
        mailMessage.From.Add(new MailboxAddress("Theater Laak", "WubWubTunes@gmail.com"));
        mailMessage.To.Add(new MailboxAddress("Account", "Theaterlaak123@gmail.com"));

        mailMessage.Subject = subject;
        mailMessage.Body = new TextPart("plain")
        {
            Text = text
        };

        using (var smtpClient = new SmtpClient())
        {
            smtpClient.Connect("smtp.gmail.com", 587, SecureSocketOptions.StartTls);
            smtpClient.Authenticate("WubWubTunes@gmail.com", "xwuutlbvawtktjte");
            
                try{
                smtpClient.Send(mailMessage);
                }
                catch(InvalidOperationException e){
                    return false;
                }
            
            
            smtpClient.Disconnect(true);
            return true;
        }
    }

}

//TODO: Translate to English
