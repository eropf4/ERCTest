﻿using ERCTest.Library;
using ERCTest.Models.Counters;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ERCTest
{
    class Program
    {
        static Home Home { get; set; }
        static HomeController HomeController { get; set; }

        static void Main()
        {
            Home = new Home();
            HomeController = new HomeController(Home);
            //StartTariff(); если тарифы не появляются в БД, то раскомментировать
            CreateHome();
        }

        private static void CreateHome()
        {
            GetName();

            if (!HomeController.CheckUserInSystem())
            {
                GetAdress();
                CheckResidentsCount();
                CheckCounters();
            }

            SelectAction();
        }

        private static void GetName()
        {
            Console.WriteLine("Пожалуйста, представтесь!");
            HomeController.SetOwnerName(Console.ReadLine());
            Console.Clear();
        }

        private static void GetAdress()
        {
            Console.WriteLine("Введите адресс через пробел (Город Улица Дом Квартира)!");
            HomeController.SetAdress(Console.ReadLine());
            Console.Clear();
        }


        private static void CheckCounters()
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
            HomeController.SaveChangesInDB();

            Console.Clear();
        }

        private static void CheckResidentsCount()
        {
            Console.WriteLine("Сколько человек проживает в квартире/доме?");
            HomeController.SetResidentCount (int.Parse(Console.ReadLine()));
            Console.Clear();
        }

        private static void StartTariff()
        {
            using (var db = new ERContext())
            {
                var tariffsList = new List<Tariff>() {
                    new Tariff() { ServiceName = "ХВС", TariffPrice = 35.78m, TariffWithoutCouner = 4.85m, UnitOfMeasurment = "м3"  },
                    new Tariff() { ServiceName = "ГВС", TariffPrice = 158.98m, TariffWithoutCouner = 4.01m, UnitOfMeasurment = "м3" },
                    new Tariff() { ServiceName = "ГВСТЭ", TariffPrice = 998.69m, TariffWithoutCouner = 0.05349m, UnitOfMeasurment = "Гкал" },
                    new Tariff() { ServiceName = "ЭЭНочь", TariffPrice = 2.31m, TariffWithoutCouner = 82, UnitOfMeasurment =  "квт*ч"},
                    new Tariff() { ServiceName = "ЭЭДень", TariffPrice = 4.9m, TariffWithoutCouner = 82, UnitOfMeasurment =  "квт*ч"} };
           
                db.Tariffs.AddRange(tariffsList);
                db.SaveChanges();
            }
        }

        private static void SelectAction()
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

        private static void ChangePeople()
        {
            Console.Clear();

            Console.WriteLine("ВВедите новое количество человек");
            HomeController.SetResidentCount(int.Parse(Console.ReadLine()));
            Console.Clear();
            SelectAction();
        }

        private static void SetMesurments() 
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
                    new Measurment() { AmountOfConsumption = decimal.Parse(currentAmount), CheckTime = time,
                        CountOfResident = HomeController.CurrentHome.ResidientsCount});
                }
                else
                {
                    HomeController.SetMesurments(counterInHome,
                        new Measurment() { AmountOfConsumption = -1, CheckTime = DateTime.Now,
                            CountOfResident = HomeController.CurrentHome.ResidientsCount });

                    Console.WriteLine("Спасибо, мы подали ваши показания в расчете на "
                        + HomeController.CurrentHome.ResidientsCount + " количество человек.");
                    Console.ReadLine();
                }

                Console.Clear();
            }

            Console.Clear();
            SelectAction();
        }

        private static void EditCounters()
        {
            Console.Clear();

            var listCountersInHome = HomeController.CurrentHome.GetCounters();
            using (var db = new ERContext())
            {
                var dbHomes = db.Homes.
                    Include(x => x.HVSDevice).
                    Include(x => x.GVSDevice).
                    Include(x => x.EEDeviceDay).
                    Include(x => x.EEDeviceNight).
                    FirstOrDefault(x => x.Id == HomeController.CurrentHome.Id);

                var eeDeviceInHome = false;
                var chekedEEDevice = false;

                foreach (var counter in listCountersInHome)
                {
                    var answer = false;


                    if (counter is EECounterDay || counter is EECounterNight)
                    {
                        if (!chekedEEDevice)
                        {
                            answer = GetConsoleAnswer("ЭЭ");
                            chekedEEDevice = true;
                            eeDeviceInHome = answer;
                        }    
                        else
                        {
                            answer = eeDeviceInHome;
                        }
                    }
                    else
                        answer = GetConsoleAnswer(counter.Name);


                    counter.HasInHome = answer;
                    dbHomes.GetCounters().FirstOrDefault(x => x.Name == counter.Name).HasInHome = answer;
                    db.SaveChanges();
                }
            }

            Console.Clear();
            SelectAction();
        }

        private static bool GetConsoleAnswer(string counterName)
        {
            Console.WriteLine("Есть ли у вас счетчик " + counterName + "? (да/нет)");
            return Console.ReadLine() == "да";
        }

        private static void GetMesurments()
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
