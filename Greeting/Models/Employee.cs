using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Greeting.Models
{
    public class Employee
    {
        public int Id { get; set; } = 1;
        public string Email { get; set; } = "f0rest@sk.com";
        public string Name { get; set; } = "f0rest";
        public string Password { get; set; } = "f0rest@123";

    }
}
