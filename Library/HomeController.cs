using ERCTest.Models.Counters;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ERCTest.Library
{
    class HomeController
    {
        public Home CurrentHome { get; set; }

        public HomeController(Home Home)
        {
            CurrentHome = Home;
        }

        public void SetOwnerName(string nameString)
        {
            CurrentHome.OwnerName = nameString;
        }

        public string GetOwnerName()
        {
            if (CurrentHome.OwnerName != null)
                return CurrentHome.OwnerName;
            else return ("Имя пользователя не назначено");
        }

        public void SetAdress(string adressStirng) // адрессная строка в формате Город Улица Дом Квартира
        {
            var adressList = adressStirng.Split();

            CurrentHome.City = adressList[0];
            CurrentHome.Street = adressList[1];
            CurrentHome.HomeNumber = int.Parse(adressList[2]);
            CurrentHome.RoomNumber = int.Parse(adressList[3]);
        }

        public bool CheckUserInSystem()
        {
            using (var db = new ERContext())
            {
                var home = db.Homes
                    .Include(x => x.HVSDevice.Measurments)
                    .Include(x => x.GVSDevice.Measurments)
                    .Include(x => x.EEDeviceDay.Measurments)
                    .Include(x => x.EEDeviceNight.Measurments)
                    .Include(x => x.GVSTECounter.Measurments)
                    .FirstOrDefault(x => x.OwnerName == CurrentHome.OwnerName);
                if (home != null)
                {
                    CurrentHome = home;
                    return true;
                }

                return false;
            }
        }

        public void SetResidentCount(int newResidentCount)
        {
            CurrentHome.ResidientsCount = newResidentCount;

            using (var db = new ERContext())
            {
                var changingHome = db.Homes.FirstOrDefault(x => x.Id == CurrentHome.Id);

                if (changingHome != null)
                {
                    changingHome.ResidientsCount = newResidentCount;
                    db.SaveChanges();
                }
            }
        }

        public void SetCounters(List<ICounter> counters)
        {
            foreach (var counter in counters)
            {
                SetCounter(counter);
            }
        }

        public void SetCounter(ICounter counter)
        {
            if (counter != null)
            {
                switch (counter)
                {
                    case EECounterDay _:
                        CurrentHome.EEDeviceDay = (EECounterDay)counter;
                        break;
                    case HVSCounter _:
                        CurrentHome.HVSDevice = (HVSCounter)counter;
                        break;
                    case GVSCounter _:
                        CurrentHome.GVSDevice = (GVSCounter)counter;
                        break;
                    case EECounterNight _:
                        CurrentHome.EEDeviceNight = (EECounterNight)counter;
                        break;
                    case GVSTECounter _:
                        CurrentHome.GVSTECounter = (GVSTECounter)counter;
                        break;
                }
            }
        }

        public void SetMesurments(ICounter counter, Measurment measurment)
        {

            using (var db = new ERContext())
            {

                var changingHome = db.Homes
                    .Include(x => x.HVSDevice.Measurments)
                    .Include(x => x.GVSDevice.Measurments)
                    .Include(x => x.EEDeviceDay.Measurments)
                    .Include(x => x.EEDeviceNight.Measurments)
                    .FirstOrDefault(x => x.Id == CurrentHome.Id);

                switch (counter)
                {
                    case HVSCounter _:
                        changingHome.HVSDevice.Measurments.Add(measurment);
                        break;
                    case GVSCounter _:
                        changingHome.GVSDevice.Measurments.Add(measurment);
                        break;
                    case EECounterDay _:
                        changingHome.EEDeviceDay.Measurments.Add(measurment);
                        break;
                    case EECounterNight _:
                        changingHome.EEDeviceNight.Measurments.Add(measurment);
                        break;
                }

                counter.Measurments.Add(measurment);
                this.SetCounter(counter);

                db.SaveChanges();
            }
        }

        public Dictionary<ICounter, Dictionary<Measurment, decimal>> GetAllMeasurmentsDic()
        {
            var allMeasurmentsDic = new Dictionary<ICounter, Dictionary<Measurment, decimal>>();

            var counterList = CurrentHome.GetCounters();

            var lastSum = 0m;

            foreach (var counter in counterList)
            {
                allMeasurmentsDic[counter] = new Dictionary<Measurment, decimal>();

                foreach (var measurment in counter.Measurments)
                {
                    var tariff = counter.GetTariff();

                    if (measurment.AmountOfConsumption != -1)
                    {
                        var mounthSum = (tariff.TariffPrice * measurment.AmountOfConsumption) - lastSum;
                        allMeasurmentsDic[counter].Add(measurment, mounthSum);
                        lastSum += mounthSum;
                    }
                    else
                    {
                        lastSum = 0;
                        
                        if (counter is EECounterDay || counter is EECounterNight)
                            tariff.TariffPrice = 4.28m;

                        allMeasurmentsDic[counter].Add(measurment,
                            (tariff.TariffWithoutCouner * measurment.CountOfResident * tariff.TariffPrice));
                    }
                }

                lastSum = 0;
            }

            GetGVSTECounterMesurments(allMeasurmentsDic);

            return allMeasurmentsDic;
        }

        private void GetGVSTECounterMesurments(Dictionary<ICounter, Dictionary<Measurment, decimal>> allMeasurmentsDic)
        {
            var lastSum = 0m;
            allMeasurmentsDic[CurrentHome.GVSTECounter] = new Dictionary<Measurment, decimal>();
            var tariff = CurrentHome.GVSTECounter.GetTariff();
            var GVSTariff = CurrentHome.GVSDevice.GetTariff();


            foreach (var measurment in CurrentHome.GVSDevice.Measurments)
            {
                var GVSTEMesurment = (Measurment)measurment.Clone();


                if (GVSTEMesurment.AmountOfConsumption != -1)
                {
                    var mounthSum = (tariff.TariffPrice * GVSTEMesurment.AmountOfConsumption * tariff.TariffWithoutCouner) - lastSum;
                    GVSTEMesurment.AmountOfConsumption *= tariff.TariffWithoutCouner;
                    allMeasurmentsDic[CurrentHome.GVSTECounter].Add(GVSTEMesurment, mounthSum);
                    lastSum += mounthSum;
                }
                else
                {
                    lastSum = 0;

                    allMeasurmentsDic[CurrentHome.GVSTECounter].Add(GVSTEMesurment,
                        (tariff.TariffWithoutCouner * (GVSTEMesurment.CountOfResident * GVSTariff.TariffWithoutCouner )* tariff.TariffPrice));
                }
            }
        }

        public void SaveNewHomeInDB()
        {
            using (var bd = new ERContext())
            {
                    bd.Homes.Add(CurrentHome);
                    bd.SaveChangesAsync();
            }
        }
    }
}
