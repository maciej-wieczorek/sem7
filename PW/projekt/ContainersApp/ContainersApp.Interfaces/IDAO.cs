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
        IEnumerable<IContainer> GetAllContainers();
        IEnumerable<IContainer> GetContainersByName(string name);
        void AddContainer(IContainer container);
        IContainer? GetContainer(int containerID);
        void UpdateContainer(IContainer container);
		void DeleteContainer(int containerID);

        IEnumerable<IProducer> GetAllProducers();
		void AddProducer(IProducer producer);
        IProducer? GetProducer(int producerID);
		void UpdateProducer(IProducer producer);
		void DeleteProducer(int producerID);
    }
}
