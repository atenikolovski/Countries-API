using Countries_API.Data.ViewModels;
using Countries_API.Services;
using CountryInfoService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Countries_API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class CountriesController : ControllerBase
    {
        public CountriesService _countriesService;

        public CountriesController(CountriesService countriesService)
        {
            _countriesService = countriesService;
        }

        [HttpPost("populate")]
        public IActionResult Populate()
        {
            try
            {
                var countries = _countriesService.PopulateCountries();

                return Created(nameof(Populate), countries);
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message + "Exception: " + ex.StackTrace);
            }           
        }

        [HttpGet("{isoCode}/flag")]
        public IActionResult GetCountryFlag(string isoCode)
        {
            try
            {
                var flagFile = _countriesService.GetCountryFlag(isoCode);
                
                return Ok(flagFile);
            }
            catch(FileNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message + "at: " + ex.StackTrace);
            }

        }
    }
}
