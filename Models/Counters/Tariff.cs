using System;
using System.Collections.Generic;
using System.Text;

namespace ERCTest.Models.Counters
{
    public class Tariff
    {
        public int Id { get; set; }

        public string ServiceName { get; set; }
        public decimal TariffPrice { get; set; }
        public decimal TariffWithoutCouner { get; set; }
        public string UnitOfMeasurment { get; set; }
    }
}
