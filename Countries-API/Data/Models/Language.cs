using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Countries_API.Data.Models
{
    public class Language
    {
        public int Id { get; set; }
        public string IsoCode { get; set; }
        public string Name { get; set; }
    }
}
