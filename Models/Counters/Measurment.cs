using System;

namespace ERCTest.Models.Counters
{
    public class Measurment : ICloneable
    {
        public int Id { get; set; }

        public decimal AmountOfConsumption { get; set; }
        public DateTime CheckTime { get; set; }
        public int CountOfResident { get; set; }

        public object Clone()
        {
            return this.MemberwiseClone();
        }
    }
}