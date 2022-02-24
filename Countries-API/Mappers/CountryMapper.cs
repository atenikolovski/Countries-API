using Countries_API.Data.Models;
using CountryInfoService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Countries_API.Mappers
{
    public class CountryMapper
    {
        public static List<Country> MapCountries(tCountryInfo[] countries)
        {
            List<Country> mappedCountries = new List<Country>();

            foreach (var countryInfo in countries)
            {
                #region Map Language
                List<Language> languages = new List<Language>();

                if (countryInfo.Languages != null && countryInfo.Languages.Length > 0)
                {
                    foreach (var languageCountry in countryInfo.Languages)
                    {
                        var language = new Language
                        {
                            IsoCode = languageCountry.sISOCode,
                            Name = languageCountry.sName
                        };

                        languages.Add(language);
                    }
                }
                #endregion

                var country = new Country
                {
                    ISOCode = countryInfo.sISOCode,
                    Name = countryInfo.sName,
                    CapitalCity = countryInfo.sCapitalCity,
                    ContinentCode = countryInfo.sContinentCode,
                    CountryFlag = countryInfo.sCountryFlag,
                    Languages = languages
                };

                mappedCountries.Add(country);
            }

            return mappedCountries;           
        }
    }
}
