
using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;


public class VerificiatieMail{

            public void  VerstuurVerificatieMail(string emailAccount)
        {
            var mailMessage = new MimeMessage();
            mailMessage.From.Add(new MailboxAddress("Theater Laak", "WubWubTunes@gmail.com"));
            mailMessage.To.Add(new MailboxAddress("Account",emailAccount));
            mailMessage.Subject = "Account verificatie";
            mailMessage.Body = new TextPart("plain")
            {
                Text = "Hello"
            };

         using (var smtpClient = new SmtpClient())
            {
                smtpClient.Connect("smtp.gmail.com", 587, SecureSocketOptions.StartTls);
                smtpClient.Authenticate("WubWubTunes@gmail.com", "ptbnksiopfeezrvt");
                smtpClient.Send(mailMessage);
                Console.WriteLine(mailMessage);
                smtpClient.Disconnect(true);
            }             
        }
        
}