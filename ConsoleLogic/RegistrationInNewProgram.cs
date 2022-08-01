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
        private void GetName()
        {
            Console.WriteLine("Пожалуйста, представтесь!");
            HomeController.SetOwnerName(Console.ReadLine());
            Console.Clear();
        }

        private void GetAdress()
        {
            Console.WriteLine("Введите адресс через пробел (Город Улица Дом Квартира)!");
            HomeController.SetAdress(Console.ReadLine());
            Console.Clear();
        }


        private void CheckCounters()
        {
            var counters = new Dictionary<string, List<ICounter>> {
                { "ХВС", new List<ICounter> {{new HVSCounter() }, new GVSTECounter() } },
                { "ГВС", new List<ICounter> {new GVSCounter() } },
                { "ЭЭ", new List<ICounter> {{new EECounterDay() }, {new EECounterNight()}}}};

            var countersList = new List<ICounter>();

            foreach (var counter in counters.Keys.ToList())
            {
                Console.WriteLine("Есть ли у вас счетчик " + counter + "? (да/нет)");
                var answer = Console.ReadLine().ToLower() == "да";
                foreach (var counterEnum in counters[counter])
                {
                    counterEnum.HasInHome = answer;
                    countersList.Add(counterEnum);
                }
            }

            HomeController.SetCounters(countersList);
            HomeController.SaveNewHomeInDB();

            Console.Clear();
        }

        private void CheckResidentsCount()
        {
            Console.WriteLine("Сколько человек проживает в квартире/доме?");
            HomeController.SetResidentCount(int.Parse(Console.ReadLine()));
            Console.Clear();
        }
    }
}
