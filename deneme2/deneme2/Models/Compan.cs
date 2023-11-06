using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace deneme2.Models
{
    public class Compan
    {
        [Key]
        public string SirketAdi { get; set; }
        public string SirketAdresi { get; set; }
        public string SirketTelefon { get; set; }
        public string LogoPath { get; set; }
    }
}
