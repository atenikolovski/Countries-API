using Countries_API.Data.ViewModels;
using Countries_API.Services;
using CountryInfoService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
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
        public void Populate()
        {
            _countriesService.PopulateCountries();
        }

        [HttpGet("{isoCode}/flag")]
        public FileVM GetCountryFlag(string isoCode)
        {
            var file = _countriesService.GetCountryFlag(isoCode);

            return file;
        }
    }
}
