using Xunit;
using TheaterLaakBackend.Models;
namespace TheaterLaakBackend.Controllers;
public class AccountInformationChecker_Tests
{

    private AccountInformationChecker AIC;



    public AccountInformationChecker_Tests()
    {
        //Setup
        AIC = new AccountInformationChecker();
    }

    [Fact]
    public void checkEmailAdresDomainName_Met_Goede_Email_Returns_True()
    {
        //Arrange 
        string email = "TestGebruiker@yahoo.com";

        //Act
        bool result = AIC.checkEmailAdresDomainName(email);

        //Assert
        Assert.True(result);

    }

    [Fact]
    public void checkEmailAdresDomainName_Met_Foute_Email_Returns_False()
    {
        //Arrange 
        string email = "TestGebruiker@randomnietbestaandeemail.com";

        //Act
        bool result = AIC.checkEmailAdresDomainName(email);

        //Assert
        Assert.False(result);

    }


    [Fact]
    public void woordenboekCheck_Bestaandwoord_returnsTrue()
    {

        // Arrange
        string bestaandWooord = "boek";

        // Act
        var result = AIC.woordenboekCheck(bestaandWooord);

        // Assert
        Assert.True(result);
    }

    [Fact]
    public void woordenboekCheck_NIET_Bestaandwoord_returnsFalse()
    {

        // Arrange
        string bestaandWooord = "sidfjwerwofj";

        // Act
        var result = AIC.woordenboekCheck(bestaandWooord);

        // Assert
        Assert.False(result);
    }

    [Fact]
    public void woordenboekCheck_WEL_Bestaandwoord_PLUSspecialeKarakters_returnsTrue()
    {

        // Arrange
        string bestaandWooord = "Boek123!#23@@!";

        // Act
        var result = AIC.woordenboekCheck(bestaandWooord);

        // Assert
        Assert.True(result);
    }

    [Fact]
    public void woordenboekCheck_NIET_Bestaandwoord_PLUSspecialeKarakters_returnsFalse()
    {

        // Arrange
        string bestaandWooord = "sidf!jwe#@!rwofj!@#";

        // Act
        var result = AIC.woordenboekCheck(bestaandWooord);

        // Assert
        Assert.False(result);
    }



    [Fact]
    public void CheckForRepeatingCharactersInPassword_SlechtWachtwoord_Letters_ReturnsTrue()
    {

        // Arrange
        string password = "ABCABC123!";

        // Act
        var result = AIC.CheckForRepeatingCharacters(password);

        // Assert
        Assert.True(result);
    }

    [Fact]
    public void CheckForRepeatingCharactersInPassword_GoedWachtwoord_ReturnsFalse()
    {

        // Arrange
        string password = "Aeack4df!";

        // Act
        var result = AIC.CheckForRepeatingCharacters(password);

        // Assert
        Assert.False(result);
    }


    [Fact]
    public void CheckForRepeatingCharactersInPassword_SlechtWachtwoord_Cijfers_ReturnsFalse()
    {

        // Arrange
        string password = "123123AB!";

        // Act
        var result = AIC.CheckForRepeatingCharacters(password);

        // Assert
        Assert.True(result);
    }

    [Fact]
    public void CheckForTop10WachtwoordenLijst_GoedWachtwoord_ReturnsFalse()
    {

        // Arrange
        string password = "Aeack4df!";

        // Act
        var result = AIC.CheckForRepeatingCharacters(password);

        // Assert
        Assert.False(result);
        // false omdat die dan niet in de lijst staat.
    }

    [Fact]
    public void CheckForTop10WachtwoordenLijst_SlechtWachtwoord_ReturnsTrue()
    {

        // Arrange
        string password = "wachtwoord1234!";

        // Act
        var result = AIC.CheckForRepeatingCharacters(password);

        // Assert
        Assert.True(result);
        //True omdat die dan wel in de lijst staat
    }


    [Fact]
    public void CheckForSimilarUserNameAndPassword_NaamEnWachtwoordGelijk_ReturnsTrue()
    {
        // Arrange
        string password = "testgebruiker";
        string username = "testgebruiker";
        // Act
        var result = AIC.CheckForSimilarUserNameAndPassword(username, password);
        // Assert
        Assert.True(result);
    }

    [Fact]
    public void CheckForSimilarUserNameAndPassword_NaamEnWachtwoordNIET_GELIJK_ReturnsFalse()
    {
        // Arrange
        string password = "Aeack4df!";
        string username = "testgebruiker";
        // Act
        var result = AIC.CheckForSimilarUserNameAndPassword(username, password);
        // Assert
        Assert.False(result);
    }

    [Fact]
    public void RemoveSpecialCharacters_WoordMetSpecialeKarakters_ReturnsStringzonder_SpecialeKarakters_EN_CIJFers()
    {
        // Arrange
        string woordMetSpecialeKarakters = "(*)&b!@#@#$(o12312ek";
        string expectedResult = "boek";
        // Act
        var result = AIC.RemoveSpecialCharacters(woordMetSpecialeKarakters);

        // Assert
        Assert.Equal(expectedResult, result);
    }

    [Fact]
    public void Sha256_ReturnsHash_PLUS_Salt_divided()
    {
        // Arrange  
        var password = "Hello World";
        HashPWs HashHelper = new HashPWs();

        // Act
        var result = HashHelper.Sha256(password);
        var parts = result.Split(':');
        var passwordHash = parts[0];
        var salt = parts[1];
        // Assert
        Assert.NotNull(result);
        Assert.Contains(':', result);

        Assert.NotEqual(password, passwordHash);
        Assert.NotEmpty(salt);
    }


    [Fact]
    public void Sha256_ReturnsHash_PLUS_Salt_divided123()
    {
        // Arrange  
        var password = "Aeack4df!";
        HashPWs HashHelper = new HashPWs();
        
        // Act              
        var result = HashHelper.Sha256(password , "BmBZ2Yt32/w9s0uzVpXUKw==");
        
        // Assert
        Assert.Equal(result , "c0e0b187dfbf68af8f3fadb275d8b84365865977053b82a63d73a649e1ea4a80:BmBZ2Yt32/w9s0uzVpXUKw==");
    }


    [Fact]
    public void test()
    {
        // Arrange  
        var password = "Aeack4df!";
        HashPWs HashHelper = new HashPWs();
        
        // Act              
        var result = HashHelper.generateNewPassword();
        
        // Assert
        Assert.Equal(result , password);
    }

}



