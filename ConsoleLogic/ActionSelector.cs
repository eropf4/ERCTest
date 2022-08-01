using ERCTest.Models.Counters;
using System;
using System.Collections.Generic;
using System.Text;

namespace ERCTest
{
    partial class NewCounterProgram
    {
        private void SelectAction()
        {
            Console.WriteLine("Добро пожаловать," + HomeController.GetOwnerName() + ".Выберите действие (1-4):" + "\n" +
                "[1] Поменять количество человек проживающих в доме/квартире." + "\n" +
                "[2] Подать данные прибора учета." + "\n" +
                "[3] Посмотреть начисления" + "\n" +
                "[4] Изменить наличие приборов" + "\n" +
                "[5] Выйти.");
            var select = int.Parse(Console.ReadLine());

            switch (select)
            {
                case 1:
                    ChangePeople();
                    break;
                case 2:
                    SetMesurments();
                    break;
                case 3:
                    GetMesurments();
                    break;
                case 4:
                    EditCounters();
                    break;
                default:
                    Environment.Exit(1);
                    break;
            }
        }
    }
}
