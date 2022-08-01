using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ERCTest.Models.Counters
{
    public class GVSTECounter : ICounter
    {
        public GVSTECounter()
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

        public string Name { get; } = "ГВСТЭ";

    }
}

