using System.Security.Cryptography;
using System.Text;
using TheaterLaakBackend.Controllers;
public class HashPWs
{

    private readonly TheaterDbContext _context;
    public  string Sha256(string password)
    {
        using (var sha256 = SHA256.Create())
        {
            var salt = GenerateSalt();
            var bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password + salt));
            var passwordHash = BitConverter.ToString(bytes).Replace("-", "").ToLower();
            return passwordHash + ":" + salt;
        }
    }

       public string Sha256(string password , string salt)
    {
        using (var sha256 = SHA256.Create())
        {
            var bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password + salt));
            var passwordHash = BitConverter.ToString(bytes).Replace("-", "").ToLower();
            return passwordHash + ":" + salt;
        }
    }



    private  string GenerateSalt()
    {
        var saltBytes = new byte[16];
        using (var rng = RandomNumberGenerator.Create())
        {
            rng.GetBytes(saltBytes);
        }
        return Convert.ToBase64String(saltBytes);
    }
}