using ContainersApp.Core;
using ContainersApp.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContainersApp.DAOMock1.BO
{
    public class Container : IContainer
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public IProducer Producer { get; set; }
        public int ProductionYear { get; set; }
        public ContainerType Type { get; set; }
    }
}
