using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Countries_API.Data.ViewModels
{
    public class FileVM
    {
        public string FileName { get; set; }
        public string FileBase64 { get; set; }
        public string Sha256 { get; set; }
    }
}
