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
        private void GetMesurments()
        {
            Console.Clear();

            Console.WriteLine("Ваши начисления:");
            var allMeasurmentsDic = HomeController.GetAllMeasurmentsDic();
            var totalSum = 0m;

            foreach (var counter in allMeasurmentsDic.Keys)
            {
                Console.WriteLine(counter.Name + "\n");
                var totalCounterSum = 0m;

                foreach (var measurmentAndSum in allMeasurmentsDic[counter])
                {
                    Console.WriteLine("     Дата подачи показаний:" + measurmentAndSum.Key.CheckTime);
                    if (measurmentAndSum.Key.AmountOfConsumption == -1)
                        Console.WriteLine("     Сумма составлена с расчетом на " + measurmentAndSum.Key.CountOfResident + " человек.");
                    else
                        Console.WriteLine("     Количество потребления:" + measurmentAndSum.Key.AmountOfConsumption);
                    totalCounterSum += measurmentAndSum.Value;
                    Console.WriteLine("     Cумма за месяц:" + measurmentAndSum.Value + "\n\n");
                }
                Console.WriteLine("         Итоговая сумма за все месяцы:" + totalCounterSum + "\n");
                totalSum += totalCounterSum;
            }

            Console.WriteLine("                 Итоговая сумма за все месяцы по всем счетчикам:" + totalSum);

            Console.ReadLine();
            Console.Clear();
            SelectAction();
        }
    }
}
