﻿using Greeting.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Greeting.Repositories
{
    public interface ILoginRepository<T>
    {
        Task<Employee> Login(T user);
        

    }
}