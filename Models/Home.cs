using ERCTest.Models.Counters;
using System;
using System.Collections.Generic;
using System.Text;

namespace ERCTest
{
    public class Home
    {
        //public Home()
        //{
        //    EEDevice = new EECounter();
        //    GVSDevice = new GVSCounter();
        //    HVSDevice = new HVSCounter(); 
        //} не работает нормально с БД (из-за того, что при инициализации создает
        //  новый объект класса и обнуляет все свойства,где прописаны счетчики )

        public int Id { get; set; }
        public string OwnerName { get; set; }
        public string City { get; set; }
        public string Street { get; set; }
        public int HomeNumber { get; set; }
        public int RoomNumber { get; set; }

        public EECounterDay EEDeviceDay { get; set; }
        public EECounterNight EEDeviceNight { get; set; }
        public GVSCounter GVSDevice { get; set; }
        public HVSCounter HVSDevice { get; set; }


        public GVSTECounter GVSTECounter { get; set; }

        public int ResidientsCount { get; set; }

        public List<ICounter> GetCounters()
        {
            return new List<ICounter> {  GVSDevice, HVSDevice, EEDeviceDay, EEDeviceNight};
        }
    }
}

