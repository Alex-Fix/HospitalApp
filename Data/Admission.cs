using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data
{
    public class Admission
    {
        public int Id { get; set; }
        public int? PatientId { get; set; }
        public virtual Patient Patient { get; set; }
        public DateTime DateOfReceipt { get; set; }
        public DateTime DischargeDate { get; set; }
        public string Diagnosis { get; set; }
        public int? WardId { get; set; }
        public virtual Ward Ward { get; set; }
        public int? DoctorId { get; set; }
        public virtual Doctor Doctor { get; set; }
        public virtual ICollection<Medicine> Medicines { get; set; }

        public Admission()
        {
            Medicines = new List<Medicine>();
        }
    }
}
