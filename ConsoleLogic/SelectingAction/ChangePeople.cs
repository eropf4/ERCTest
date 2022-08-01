using ERCTest.Library;
using ERCTest.Models.Counters;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ERCTest
{
    partial class NewCounterProgram
    {
        private void ChangePeople()
        {
            Console.Clear();

            Console.WriteLine("ВВедите новое количество человек");
            HomeController.SetResidentCount(int.Parse(Console.ReadLine()));
            Console.Clear();
            SelectAction();
        }
    }
}
