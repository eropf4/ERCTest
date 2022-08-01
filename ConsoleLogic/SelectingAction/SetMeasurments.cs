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
        private void SetMesurments()
        {
            Console.Clear();

            var counterListInHome = HomeController.CurrentHome.GetCounters();

            foreach (var counterInHome in counterListInHome)
            {
                var unitOfMeasurment = counterInHome.GetTariff().UnitOfMeasurment;
                Console.WriteLine(counterInHome.Name + "\n");
                if (counterInHome.HasInHome)
                {
                    Console.WriteLine("Количество " + unitOfMeasurment + " на данный момент");
                    var currentAmount = Console.ReadLine();

                    var time = DateTime.Now;

                    HomeController.SetMesurments(counterInHome,
                    new Measurment()
                    {
                        AmountOfConsumption = decimal.Parse(currentAmount),
                        CheckTime = time,
                        CountOfResident = HomeController.CurrentHome.ResidientsCount
                    });
                }
                else
                {
                    HomeController.SetMesurments(counterInHome,
                        new Measurment()
                        {
                            AmountOfConsumption = -1,
                            CheckTime = DateTime.Now,
                            CountOfResident = HomeController.CurrentHome.ResidientsCount
                        });

                    Console.WriteLine("Спасибо, мы подали ваши показания в расчете на "
                        + HomeController.CurrentHome.ResidientsCount + " количество человек.");
                    Console.ReadLine();
                }

                Console.Clear();
            }

            Console.Clear();
            SelectAction();
        }
    }
}
