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
        private void EditCounters()
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

        private bool GetConsoleAnswer(string counterName)
        {
            Console.WriteLine("Есть ли у вас счетчик " + counterName + "? (да/нет)");
            return Console.ReadLine() == "да";
        }
    }
}
