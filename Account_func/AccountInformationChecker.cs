using System.Text.RegularExpressions;
using TheaterLaakBackend.Controllers;

namespace TheaterLaakBackend;
public class AccountInformationChecker
{

    private readonly TheaterDbContext _context;

     public AccountInformationChecker()
    {

    }

    public AccountInformationChecker(TheaterDbContext context)
    {
        _context = context;
    }

    public string BestaandeGebruikerCheck(string username, string email)
    {
        if (!checkEmailAdresDomainName(email))
        {
            return "Dit is geen geldig email adres.";
        }
        if (checkGebruikerAlBestaat(username))
        {
            return "De gebruikersnaam bestaat al verandere deze naar een nieuwe.";
        }
        if (checkEmailAlBestaat(email))
        {
            return "Dit email adres bestaat al verandere deze naar een nieuwe.";
        }
        return "Succes";
    }

    public string PasswordCheck(string username, string password)
    {
        if(passwordCheckEISEN(password)){
            return "Het wachtwoord voldoet niet aan de eisen. 1 hoofdletter 1 kleine letter 1 cijfer en 1 speciaalteken !#@$(%)@#$_";
        }
        if (CheckForSimilarUserNameAndPassword(username, password))
        {
            return "Het wachtwoord is hetzelfde als de gebruikersnaam verander deze";
        }
        if (CheckForTop10WachtwoordenLijst(password) || woordenboekCheck(password))
        {
            return "Het wachtwoord komt te vaak voor verander uw wachtwoord en gebruik geen bestaande woorden.";
        }
        if (CheckForRepeatingCharacters(password))
        {
            return "Het wachtwoord heeft een herhalend patroon verander dit naar een veiliger wachtwoord";
        }

        return "Succes";
    }

    public bool passwordCheckEISEN(string password)
{
    //De eisen zijn min 1 hoofdletter 1 kleine letter 1 speciaal teken en 1 teken tusse nde 8 en 32 tekens
    string passwordRegex = @"^(?=.*[a-zA-Z])(?=.*\d)(?=.*[!@#$%^&*()_+])[A-Za-z\d!@#$%^&*()_+]{7,32}$";
    if (Regex.IsMatch(password, passwordRegex))
    {
        return false;
    }
    return true;
}

    public string RemoveSpecialCharacters(string str)
    {
        return Regex.Replace(str, "[^a-zA-Z]+", "", RegexOptions.Compiled);
    }


    public bool CheckForSimilarUserNameAndPassword(string username, string password)
    {
        return username == password;
    }

    public bool CheckForTop10WachtwoordenLijst(string password)
    {
        string[] top10 = System.IO.File.ReadAllLines("Lijsten/Top10MeestGebruikteWachtwoorden.txt");
        return top10.Contains(RemoveSpecialCharacters(password).ToLower());
    }
    public bool CheckForRepeatingCharacters(string password)
    {
        string pattern = @"(.+)\1+";
        return Regex.IsMatch(password, pattern);
    }

    public Boolean woordenboekCheck(string password)
    {
        string[] woordenlijst = System.IO.File.ReadAllLines("Lijsten/Woordenlijst.txt");

        return woordenlijst.Contains(RemoveSpecialCharacters(password.ToLower()));
    }

    public bool checkGebruikerAlBestaat(string username)
    {
        return _context.Accounts.Any(u => u.Username == username);
    }
    public bool checkEmailAlBestaat(string email)
    {
        return _context.Accounts.Any(u => u.Email == email);
    }
    public bool checkEmailAdresDomainName(string email)
    {
        string[] emaildomainGebruiker = email.Split('@');
        string domain = emaildomainGebruiker[1];
        string[] domains = System.IO.File.ReadAllLines("Lijsten/validEmailDomainNames.txt");
        return domains.Contains(domain);
    }
}

//TODO: Translate to English
