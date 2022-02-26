using Countries_API.Data;
using Countries_API.Data.Models;
using Countries_API.Data.ViewModels;
using Countries_API.Helpers;
using CountryInfoService;
using System;
using System.Collections.Generic;
using System.IO;
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

            // Get the file extension
            var uri = new Uri(fileUrl);
            var uriWithoutQuery = uri.GetLeftPart(UriPartial.Path);
            var fileExtension = Path.GetExtension(uriWithoutQuery);

            //Check if file already exists on disk, if exists get it from the disk
            if (File.Exists("ImageData\\" + countryIsoCode + fileExtension))
            {
                var fileOnDisk = File.ReadAllBytes("ImageData\\" + countryIsoCode + fileExtension);
                CountriesHelper.CreateFileVMResponse(countryIsoCode, fileOnDisk, out flagResponse);
            }
            //If the file does not exist on the disk, download it and write it on the disk
            else
            {                
                var flagFileBytes = CountriesHelper.DownloadImageAndSaveOnDisk("ImageData", countryIsoCode, fileUrl, fileExtension);
                CountriesHelper.CreateFileVMResponse(countryIsoCode, flagFileBytes, out flagResponse);
            }

            return flagResponse;
        }
    }

}
