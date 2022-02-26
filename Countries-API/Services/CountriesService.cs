using Countries_API.Data;
using Countries_API.Data.Models;
using Countries_API.Data.ViewModels;
using Countries_API.Helpers;
using CountryInfoService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Countries_API.Services
{
    public class CountriesService
    {
        private AppDbContext _dbContext;

        public CountriesService(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void PopulateCountries()
        {
            try
            {
                var client = new CountryInfoServiceSoapTypeClient(CountryInfoServiceSoapTypeClient.EndpointConfiguration.CountryInfoServiceSoap);

                //Get all countries 
                var response = client.FullCountryInfoAllCountries();

                //Maps the response to Country object
                var countries = Mappers.CountryMapper.MapCountries(response);

                foreach (var country in countries)
                {
                    //The country is already added to database, just update it
                    if (_dbContext.Countries.FirstOrDefault(x => x.ISOCode == country.ISOCode) != null)
                    {
                        var countryToBeUpdated = _dbContext.Countries.FirstOrDefault(x => x.ISOCode == country.ISOCode);

                        countryToBeUpdated.CapitalCity = country.CapitalCity;
                        countryToBeUpdated.ContinentCode = country.ContinentCode;
                        countryToBeUpdated.CountryFlag = country.CountryFlag;
                        countryToBeUpdated.Languages = country.Languages;
                        countryToBeUpdated.Name = country.Name;
                    }
                    else
                    {
                        _dbContext.Countries.Add(country);
                    }

                    _dbContext.SaveChanges();
                }
            }
            catch(Exception ex)
            {
                throw ex;
            }
            
        }
        public FileVM GetCountryFlag(string countryIsoCode)
        {
            var flagResponse = new FileVM();

            var fileUrl = _dbContext.Countries.FirstOrDefault(x => x.ISOCode == countryIsoCode).CountryFlag;

            var flagFileBytes = CountriesHelper.DownloadImage("ImageData", countryIsoCode, fileUrl);

            CountriesHelper.CreateFileVMResponse(countryIsoCode, flagFileBytes, out flagResponse); 

            return flagResponse;
        }
    }

}
