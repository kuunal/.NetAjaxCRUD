﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ModelLayer
{
    public class Employee
    {
        public int Id { get; set; } 
        public string Email { get; set; } 
        public string Name { get; set; } 
        public string Password { get; set; } 

        public string Address { get; set; }
        public long PhoneNumber { get; set; }
    }
}
