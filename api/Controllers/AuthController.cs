using MasterclassDapper.Models;
using MasterclassDapper.Repositories;
using MasterclassDapper.Services;
using Microsoft.AspNetCore.Mvc;

namespace MasterclassDapper.Controller
{

  [Route("api/login")]
  [ApiController]
  public class AuthController : ControllerBase {

    private readonly TokenService tokenService;

    public AuthController(IConfiguration configuration){
      tokenService = new TokenService(configuration);
    }

    [HttpGet]
    public async Task<IActionResult> generateRandomToken(){
      var token = tokenService.GenerateToken(new User {
        Name = "JC Bombardelli",
        Email = "jc.bombardelli@gama.academy"
      });
      return Ok(token);
    }
  }
}