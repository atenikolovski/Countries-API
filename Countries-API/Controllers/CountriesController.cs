using Countries_API.Data.ViewModels;
using Countries_API.Services;
using CountryInfoService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
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
        private CountriesService _countriesService;

        private readonly ILogger<CountriesController> _logger;

        public CountriesController(CountriesService countriesService, ILogger<CountriesController> logger)
        {
            _countriesService = countriesService;
            _logger = logger;
        }

        [HttpPost("populate")]
        public IActionResult Populate()
        {
            try
            {
                _logger.LogInformation("Called countries/populate.");

                var countries = _countriesService.PopulateCountries();

                _logger.LogInformation("Finished countries/populate.");


                return Created(nameof(Populate), countries);
            }
            catch(Exception ex)
            {
                _logger.LogError(ex.Message + "Exception: " + ex.StackTrace);
                return BadRequest(ex.Message + "Exception: " + ex.StackTrace);               
            }
        }

        [HttpGet("{isoCode}/flag")]
        public IActionResult GetCountryFlag(string isoCode)
        {
            try
            {
                _logger.LogInformation("Called countries/{isoCode}/flag.");

                var flagFile = _countriesService.GetCountryFlag(isoCode);

                _logger.LogInformation("Finished countries/{isoCode}/flag.");


                return Ok(flagFile);
            }
            catch(FileNotFoundException ex)
            {
                _logger.LogWarning(ex.Message);

                return NotFound(ex.Message);
            }
            catch(Exception ex)
            {
                _logger.LogError(ex.Message + "at: " + ex.StackTrace);

                return BadRequest(ex.Message + "at: " + ex.StackTrace);
            }

        }
    }
}
