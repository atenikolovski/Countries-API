using Countries_API.Data;
using Countries_API.Data.Models;
using Countries_API.Data.ViewModels;
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

        public ContinentsService(AppDbContext dbContext)
        {
            _dbContext = dbContext;
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
                return languagesByContinent;
            }
            else
            {
                throw new KeyNotFoundException("Entered continent code does not exists.")
            }

        }
    }
}
