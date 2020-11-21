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
        private ICollection<Medicine> _medisines;

        public virtual ICollection<Medicine> Medicines
        {
            get { return _medisines ?? (_medisines = new List<Medicine>()); }
            set { _medisines = value; }
        }

        
    }
}
