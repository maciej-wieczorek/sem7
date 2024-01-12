using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContainersApp.Interfaces
{
    public interface IProducer
    {
        int ID { get; set; }
        string Name { get; set; }
        string Address { get; set; }
    }
}
