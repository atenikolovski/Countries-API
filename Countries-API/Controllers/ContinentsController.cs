using Countries_API.Data.Models;
using Countries_API.Data.ViewModels;
using Countries_API.Services;
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
    public class ContinentsController : ControllerBase
    {
        public ContinentsService _continentsService;

        public ContinentsController(ContinentsService continentsService)
        {
            _continentsService = continentsService;
        }

        [HttpGet("{continentCode}/languages")]
        public List<LanguageVM> GetLanguagesByContinent(string continentCode)
        {
            var a = _continentsService.GetLanguagesByContinent(continentCode);
            return a;
        }
    }
}
