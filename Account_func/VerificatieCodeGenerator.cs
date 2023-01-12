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


    public async void sendVertificatie(int id, string email)
    {
        int VerificatieCode = getVerificatieCode();
        await _context.Veritficaties.AddAsync(new Veritficatie { AccountID = id, VerificatieCode = VerificatieCode, DatumTijdVertificatieCodeVerzonden = DateTime.Now });
        await _context.SaveChangesAsync();
        MailSender MS = new MailSender();
        MS.sendMail(email , "Uw verificatie code = " + VerificatieCode, "Uw verificatie code = " + VerificatieCode);
    }
}