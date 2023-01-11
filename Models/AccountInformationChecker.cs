using System.Text.RegularExpressions;
namespace TheaterLaakBackend;
public class AccountInformationChecker
{


    //Kijkt voor patronen in het wachtwoord bijvoorbeeld ABCABC returned true . ABCGFD false


    public string PasswordCheck(string username, string password)
    {
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
        string[] top10 = System.IO.File.ReadAllLines("Top10MeestGebruikteWachtwoorden.txt");
        return top10.Contains(RemoveSpecialCharacters(password).ToLower());
    }
    public bool CheckForRepeatingCharacters(string password)
    {
        string pattern = @"(.+)\1+";
        return Regex.IsMatch(password, pattern);
    }

       public  Boolean woordenboekCheck(string password)
    {
        string[] woordenlijst = System.IO.File.ReadAllLines("Woordenlijst.txt");
            
        return woordenlijst.Contains(RemoveSpecialCharacters(password.ToLower()));
    }
}