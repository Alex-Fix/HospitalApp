using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data
{
    public class Medicine
    {
        public int Id { get; set; }
        public string Sku { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string Indication { get; set; }
        public int CountryId { get; set; }
        public Country Country { get; set; }
        public ICollection<Admission> Admissions { get; set; }
        
    }
}
