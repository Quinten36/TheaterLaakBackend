using TheaterLaakBackend.Contexts;
using TheaterLaakBackend.Controllers;

public class VerificatieCodeGenerator
{


    private readonly TheaterDbContext _context;

    public VerificatieCodeGenerator(TheaterDbContext context)
    {
        _context = context;
    }

    public int getVerificatieCode()
    {
        Random random = new Random();
        return random.Next(10000, 99999);
    }


    public async void sendVertificatie(string id, string email)
    {
        int VerificatieCode = getVerificatieCode();

        var existingValidation = _context.Verificaties.FirstOrDefault((v => v.AccountID == id));
        if (existingValidation != null)
        {
            _context.Verificaties.Remove(existingValidation);
            await _context.SaveChangesAsync();
        }

        await _context.Verificaties.AddAsync(new Validation { AccountID = id, ValidationCode = VerificatieCode, VerificationCodeSendDate = DateTime.Now });
        await _context.SaveChangesAsync();

        MailSender MS = new MailSender();
        MS.sendMail(email, "Uw verificatie code = " + VerificatieCode, "Uw verificatie code = " + VerificatieCode);
    }

}


