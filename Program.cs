using ERCTest.Library;
using ERCTest.Models.Counters;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ERCTest
{
    class Program
    {
        static void Main()
        {
            var newProgram = new NewCounterProgram();
            newProgram.Start();
        }
    }
}
