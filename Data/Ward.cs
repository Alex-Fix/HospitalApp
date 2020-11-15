﻿using System;
using System.Collections.Generic;
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
        public Comfort Comfot { get; set; }
        public int NumberOfPaces { get; set; }
        public ICollection<Admission> Admissions { get; set; }

    }
    public enum Comfort
    {
        Econom,
        Business
    }
}