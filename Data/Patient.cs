using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Data
{
    public class Patient
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string MiddleName { get; set; }
        public string Address { get; set; }
        public string InsurancePolicy { get; set; }
        public DateTime DateOfBirth { get; set; }
        private ICollection<Admission> _admissions;

        public virtual ICollection<Admission> Admissions
        {
            get { return _admissions ?? (_admissions = new List<Admission>()); }
            set { _admissions = value; }
        }

        [NotMapped]
        public string FullName
        {
            get { return $"{LastName} {FirstName} {MiddleName}"; }
        }

        [NotMapped]
        public string ShortInfo
        {
            get { return $"ПІБ: {FullName}\tСтраховий поліс: {InsurancePolicy}"; }
        }

    }
}
