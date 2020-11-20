using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data
{
    public class Country
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<Medicine> Medicines { get; set; }

        public Country()
        {
            Medicines = new List<Medicine>();
        }
    }
}
