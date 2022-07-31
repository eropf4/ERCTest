using System;
using System.Collections.Generic;
using System.Text;

namespace ERCTest.Models.Counters
{
    public interface ICounter
    {
        int Id { get; set; }

        Measurment LastAmountOfConsumpiton { get; }
        List<Measurment> Measurments { get; set; }
        public bool HasInHome { get; set; }
        public string Name { get; }
        public Tariff GetTariff();
    }
}
