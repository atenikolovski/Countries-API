using Countries_API.Data.Models;
using Countries_API.Data.ViewModels;
using Countries_API.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
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
        private IMemoryCache _cache;
        public ContinentsService _continentsService;
        private readonly ILogger<ContinentsController> _logger;

        public ContinentsController(IMemoryCache cache, ContinentsService continentsService, ILogger<ContinentsController> logger)
        {
            _cache = cache;
            _continentsService = continentsService;
            _logger = logger;
        }

        [HttpGet("{continentCode}/languages")]
        public IActionResult GetLanguagesByContinent(string continentCode)
        {
            try
            {
                _logger.LogInformation("Called continents/{continentCode}/languages.");

                List<LanguageVM> languagesByContinent;

                //See if response is already cached
                bool isCached = _cache.TryGetValue("LanguagesByContinent" + continentCode, out languagesByContinent);

                if (!isCached) //If response is not cached, get the response and cache it
                {
                    languagesByContinent = _continentsService.GetLanguagesByContinent(continentCode);
                    var cacheEntryOptions = new MemoryCacheEntryOptions().SetSlidingExpiration(TimeSpan.FromSeconds(40));
                    _cache.Set("LanguagesByContinent" + continentCode, languagesByContinent, cacheEntryOptions);
                }

                _logger.LogInformation("Finished continents/{continentCode}/languages.");

                return Ok(languagesByContinent);
                
            }
            catch(KeyNotFoundException ex)
            {
                _logger.LogWarning(ex.Message);

                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message + "at: " + ex.StackTrace);

                return BadRequest(ex.Message + "at: " + ex.StackTrace);
            }

        }
    }
}
