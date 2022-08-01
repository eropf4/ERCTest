using ERCTest.Models.Counters;
using System;
using System.Collections.Generic;
using System.Text;

namespace ERCTest
{
    partial class NewCounterProgram
    {
        private void StartTariff()
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
    }
}
