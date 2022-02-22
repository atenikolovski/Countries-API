using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Countries_API.Data.Models
{
    public class Country
    {
        [Key]
        public string ISOCode { get; set; }
        public string Name { get; set; }
        public string CapitalCity { get; set; }
        public string ContinentCode { get; set; }
        public string CountryFlag { get; set; }
        public List<Language> Languages { get; set; }
    }
}
