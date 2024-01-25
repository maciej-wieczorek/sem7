using System;
using CarsApp.Core;
using CarsApp.Interfaces;
namespace CarsApp.DAOMock1.BO
{
    public class Medicine : IMedicine
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public IProducer Producer { get; set; }
        public double Price { get; set; }
        public MedicineType MedType { get; set; }
    }
}

