using System.Text.RegularExpressions;
namespace TheaterLaakBackend;
public class PasswordChecker
{


    public string RemoveSpecialCharacters(string str)
    {
        return Regex.Replace(str, "[^a-zA-Z0-9_.]+", "", RegexOptions.Compiled);
    }
    Boolean woordenboekCheck(string password)
    {

        string[] woordenlijst = System.IO.File.ReadAllLines("Woordenlijst.txt");
        return woordenlijst.Contains(password);
    }

}