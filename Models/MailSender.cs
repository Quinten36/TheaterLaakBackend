
using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;


public class VerificiatieMail{

            public void MailSender(string emailadres , string text , string subject)
        {
            var mailMessage = new MimeMessage();
            mailMessage.From.Add(new MailboxAddress("Theater Laak", "WubWubTunes@gmail.com"));
            mailMessage.To.Add(new MailboxAddress("Account",emailadres));
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