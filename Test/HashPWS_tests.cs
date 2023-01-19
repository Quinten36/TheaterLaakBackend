  using Xunit;
using TheaterLaakBackend.Models;
namespace TheaterLaakBackend.Controllers;
public class HashPWS_tests
{
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
}