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
        public List<LanguageVM> GetLanguagesByContinent(string continentCode)
        {
            List<LanguageVM> languagesByContinent = new List<LanguageVM>();
            
            //See if response is already cached
            bool isCached = _cache.TryGetValue("LanguagesByContinent", out languagesByContinent);

            if (!isCached) //If response is not cached, get the response and cache it
            {
                languagesByContinent = _continentsService.GetLanguagesByContinent(continentCode);
                var cacheEntryOptions = new MemoryCacheEntryOptions().SetSlidingExpiration(TimeSpan.FromSeconds(40));
                _cache.Set("LanguagesByContinent", languagesByContinent, cacheEntryOptions);
            }

            return languagesByContinent;
        }
    }
}
