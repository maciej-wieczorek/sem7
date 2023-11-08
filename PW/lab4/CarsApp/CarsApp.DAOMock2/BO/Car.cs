using CarsApp.Core;
using CarsApp.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarsApp.DAOMock2.BO
{
    public class Car : ICar
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public IProducer Producer { get; set; }
        public int ProductionYear { get; set; }
        public TransmissionType Transmission { get; set; }
    }
}
