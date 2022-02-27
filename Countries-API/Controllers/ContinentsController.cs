using Countries_API.Data.Models;
using Countries_API.Data.ViewModels;
using Countries_API.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
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

        public ContinentsController(IMemoryCache cache, ContinentsService continentsService)
        {
            _cache = cache;
            _continentsService = continentsService;
        }

        [HttpGet("{continentCode}/languages")]
        public IActionResult GetLanguagesByContinent(string continentCode)
        {
            try
            {
                List<LanguageVM> languagesByContinent;

                //See if response is already cached
                bool isCached = _cache.TryGetValue("LanguagesByContinent" + continentCode, out languagesByContinent);

                if (!isCached) //If response is not cached, get the response and cache it
                {
                    languagesByContinent = _continentsService.GetLanguagesByContinent(continentCode);
                    var cacheEntryOptions = new MemoryCacheEntryOptions().SetSlidingExpiration(TimeSpan.FromSeconds(40));
                    _cache.Set("LanguagesByContinent" + continentCode, languagesByContinent, cacheEntryOptions);
                }
                
                return Ok(languagesByContinent);
                
            }
            catch(KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message + "at: " + ex.StackTrace);
            }

        }
    }
}
