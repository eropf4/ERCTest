using System;

namespace ERCTest.Models.Counters
{
    public class Measurment
    {
        public int Id { get; set; }

        public double AmountOfConsumption { get; set; }
        public DateTime CheckTime { get; set; }
        public int countOfResident { get; set; }
    }
}