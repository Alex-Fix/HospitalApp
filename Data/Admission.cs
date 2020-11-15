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
        public int PatientId { get; set; }
        public Patient Patient { get; set; }
        public DateTime DateOfReceipt { get; set; }
        public DateTime DischargeDate { get; set; }
        public string Diagnosis { get; set; }
        public int WardId { get; set; }
        public Ward Ward { get; set; }
        public int DoctorId { get; set; }
        public Doctor Doctor { get; set; }
        public ICollection<Medicine> Medicines { get; set; }

    }
}
