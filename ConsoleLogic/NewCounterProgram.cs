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
        Home Home { get; set; }
        HomeController HomeController { get; set; }

        public NewCounterProgram()
        {
            Home = new Home();
            HomeController = new HomeController(Home);
        }

        public void Start()
        {
            StartTariff(); //если тарифы не появляются в БД, то раскомментировать
            CreateHome();
        }

        private void CreateHome()
        {
            GetName();

            if (!HomeController.CheckUserInSystem())
            {
                GetAdress();
                CheckResidentsCount();
                CheckCounters();
            }

            SelectAction();
        }
    }
}
