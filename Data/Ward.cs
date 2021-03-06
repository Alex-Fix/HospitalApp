﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Data
{
    public class Ward
    {
        public int Id { get; set; }
        public int WardNumber { get; set; }
        public int ComfotId { get; set; }
        public Comfort Comfort
        {
            get { return (Comfort)ComfotId; }
            set { ComfotId = (int)value; }
        }

        public int NumberOfPaces { get; set; }
        private ICollection<Admission> _admissions;
        public virtual ICollection<Admission> Admissions
        {
            get { return _admissions ?? (_admissions = new List<Admission>()); }
            set { _admissions = value; }
        }

        [NotMapped]
        public string ShortInfo
        {
            get { return $"Номер палати: {WardNumber}\tКількість місць: {NumberOfPaces}\tРівень комфорту: {Comfort.ToString()}"; }
        }

    }
    public enum Comfort
    {
        Econom,
        Business
    }
}
