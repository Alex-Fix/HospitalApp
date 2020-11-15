﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data
{
    public class Doctor
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string MiddleName { get; set; }
        public string Specialization { get; set; }
        public string Phone { get; set; }
        public DateTime DateOfBirth { get; set; }
        public ICollection<Admission> Admissions { get; set; }

    }
}

