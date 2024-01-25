using Microsoft.Extensions.Configuration;
using ContainersApp.Interfaces;

namespace ContainersApp.DAOMock1
{
    public class DAOMock : IDAO
    {
        private List<IProducer> producers;
        private List<IContainer> containers;

        public DAOMock()
        {
            producers = new List<IProducer>()
            {
                new BO.Producer() {Id = 1, Name = "Producer1"},
                new BO.Producer() {Id = 2, Name = "Producer2"}
            };

            containers = new List<IContainer>()
            {
                new BO.Container() {Id = 1, Producer = producers[0], Name = "Container1", ProductionYear = 2020, Type = Core.ContainerType.BundedStorage },
                new BO.Container() {Id = 2, Producer = producers[0], Name = "Container2", ProductionYear = 2022, Type = Core.ContainerType.OpenTop },
                new BO.Container() {Id = 3, Producer = producers[1], Name = "Container3", ProductionYear = 2023, Type = Core.ContainerType.Classic }
            };
        }

        public void AddContainer(IContainer container)
        {
            throw new NotImplementedException();
        }

        public void AddProducer(IProducer producer)
        {
            throw new NotImplementedException();
        }

        public IContainer CreateContainer(IContainer container)
        {
            throw new NotImplementedException();
        }

        public void DeleteContainer(int containerID)
        {
            throw new NotImplementedException();
        }

        public void DeleteProducer(int producerID)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<IContainer> GetAllContainers()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<IProducer> GetAllProducers()
        {
            throw new NotImplementedException();
        }

        public IContainer? GetContainer(int containerID)
        {
            throw new NotImplementedException();
        }

        public IProducer? GetProducer(int producerID)
        {
            throw new NotImplementedException();
        }

        public void UpdateContainer(IContainer container)
        {
            throw new NotImplementedException();
        }

        public void UpdateProducer(IProducer producer)
        {
            throw new NotImplementedException();
        }
    }
}
