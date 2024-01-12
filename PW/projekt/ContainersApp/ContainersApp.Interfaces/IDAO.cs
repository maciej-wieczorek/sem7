using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContainersApp.Interfaces
{
    // data access object
    public interface IDAO
    {
        IEnumerable<IProducer> GetAllProducers();
        IEnumerable<IContainer> GetAllContainers();
        IProducer CreateNewProducer();
        IContainer CreateNewContainer();
    }
}
