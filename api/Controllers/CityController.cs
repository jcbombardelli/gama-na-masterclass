using MasterclassDapper.Models;
using MasterclassDapper.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace MasterclassDapper.Controller
{

  [Route("api/cities")]
  [ApiController]
  public class CityController : ControllerBase {


    private readonly CityRepository cityRepository;
    public CityController(IConfiguration configuration) 
    { 
      cityRepository = new CityRepository(configuration);
    }

    [HttpGet]
    public async Task<IActionResult> GetCities()
    {
      try
      {
        var cities = cityRepository.FindAll();
        return Ok(cities);
      }
      catch (System.Exception ex)
      {
        return this.StatusCode(StatusCodes.Status500InternalServerError, $"Banco de dados falhou {ex.Message}");
      }
    }

    [HttpPost] 
    public async Task<IActionResult> PostCity([FromBody] City city)
    {
      try
      {
        if (city == null) return BadRequest();
        cityRepository.Add(city);
        return Created($"/api/cities/{city.Id}", city);
      }
      catch (System.Exception ex)
      {
        return this.StatusCode(StatusCodes.Status500InternalServerError, $"Banco de dados falhou {ex.Message}");
      }
    }


    [HttpGet("{id}")]
    public async Task<IActionResult> GetCity(int id)
    {
      try
      {
        var city = cityRepository.FindByID(id);
        if (city == null) return NotFound();
        return Ok(city);
      }
      catch (System.Exception ex)
      {
        return this.StatusCode(StatusCodes.Status500InternalServerError, $"Banco de dados falhou {ex.Message}");
      }
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteCity(int id)
    {
      try
      {
        var city = cityRepository.FindByID(id);
        if (city == null) return NotFound();
        cityRepository.Remove(id);
        return Ok($"City {city.Name} deleted");
      }
      catch (System.Exception ex)
      {
        return this.StatusCode(StatusCodes.Status500InternalServerError, $"Banco de dados falhou {ex.Message}");
      }
    }


  }
}

