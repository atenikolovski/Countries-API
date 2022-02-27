using Countries_API.Data;
using Countries_API.Data.Models;
using Countries_API.Data.ViewModels;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Countries_API.Services
{
    public class ContinentsService
    {
        private AppDbContext _dbContext;
        private readonly ILogger<ContinentsService> _logger;

        public ContinentsService(AppDbContext dbContext, ILogger<ContinentsService> logger)
        {
            _dbContext = dbContext;
            _logger = logger;
        }

        public List<LanguageVM> GetLanguagesByContinent(string continentCode)
        {
            var languagesByContinent = (from c in _dbContext.Countries
                     join l in _dbContext.Languages
                     on c.Id equals l.CountryId
                     where c.ContinentCode == continentCode
                     select new LanguageVM { IsoCode = l.IsoCode, Name = l.Name}).Distinct().ToList();

            if(languagesByContinent != null && languagesByContinent.Count > 0)
            {
                _logger.LogInformation("Getting Languages for continentCode: " + continentCode);

                return languagesByContinent;
            }
            else
            {
                throw new KeyNotFoundException("Continent with the entered continent code does not exists.");
            }

        }
    }
}
