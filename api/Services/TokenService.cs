using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using MasterclassDapper.Models;
using Microsoft.IdentityModel.Tokens;

namespace MasterclassDapper.Services
{

  public class TokenService
  {

    private readonly string _secret;
    public TokenService(IConfiguration configuration)
    {
      _secret = configuration.GetValue<string>("JWT:Secret");
    }

    public string GenerateToken(User user)
    {

      var tokenHandler = new JwtSecurityTokenHandler();
      var key = Encoding.ASCII.GetBytes(_secret);
      var tokenDescriptor = new SecurityTokenDescriptor
      {
        Subject = new ClaimsIdentity(new Claim[]
          {
            new Claim(ClaimTypes.Name, user.Name.ToString()),
            new Claim(ClaimTypes.Email, user.Email.ToString())
            //Podem ser acrescidos mais informações, @ClaimTypes
          }),
        Expires = DateTime.UtcNow.AddHours(2),
        SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
      };
      var token = tokenHandler.CreateToken(tokenDescriptor);
      return tokenHandler.WriteToken(token);
    }
  }

}