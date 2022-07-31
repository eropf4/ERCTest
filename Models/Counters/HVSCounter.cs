using ERCTest.Models.Counters;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ERCTest
{
    public class HVSCounter : ICounter
    {
        public HVSCounter()
        {
            Measurments = new List<Measurment>();
        }

        public int Id { get; set; }


        public Measurment LastAmountOfConsumpiton => Measurments.Last();
        public List<Measurment> Measurments { get; set; }
        public bool HasInHome { get; set; }

        public Tariff GetTariff()
        {
            using (var db = new ERContext())
            {
                var tarriff = db.Tariffs.FirstOrDefault(x => x.ServiceName == Name);
                return tarriff;
            }

        }

        public string Name { get; } = "ХВС";
    }
}
