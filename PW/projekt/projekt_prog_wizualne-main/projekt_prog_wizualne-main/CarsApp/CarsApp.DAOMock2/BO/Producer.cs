﻿using System;
using CarsApp.Interfaces;
namespace CarsApp.DAOMock2.BO
{
	public class Producer : IProducer
	{
        public int ID { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
    }
}

