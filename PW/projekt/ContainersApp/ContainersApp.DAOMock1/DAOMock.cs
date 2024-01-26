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

        public IEnumerable<IContainer> GetAllContainers()
        {
            return containers;
        }
        public IEnumerable<IContainer> GetContainersByName(string name)
        {
            return containers.Where(container => container.Name == name).ToList();
        }
        public void AddContainer(IContainer container)
        {
            var newId = containers.Max(obj => obj.Id) + 1;
            var newContainer = new BO.Container
            {
                Id = newId,
                Name = container.Name,
                Producer = (BO.Producer)container.Producer,
                ProductionYear = container.ProductionYear,
                Type = container.Type,
            };
            containers.Add(newContainer);
        }
        public IContainer? GetContainer(int containerID)
        {
            return containers.FirstOrDefault(obj => obj.Id == containerID);
        }
        public void UpdateContainer(IContainer container)
        {
            var existingContainer = GetContainer(container.Id);
            if (existingContainer != null)
            {
                existingContainer.Name = container.Name;
                existingContainer.Producer = container.Producer;
                existingContainer.ProductionYear = container.ProductionYear;
                existingContainer.Type = container.Type;
            }
        }
        public void DeleteContainer(int containerID)
        {
            var container = GetContainer(containerID);
            if (container != null)
            {
                containers.Remove(container);
            }
        }
        public IEnumerable<IProducer> GetAllProducers()
        {
            return producers;
        }
        public void AddProducer(IProducer producer)
        {
            var newId = producers.Max(obj => obj.Id) + 1;
            var newProducer = new BO.Producer
            {
                Id = newId,
                Name = producer.Name,
                Address = producer.Address
            };
            producers.Add(newProducer);
        }
        public IProducer? GetProducer(int producerID)
        {
            return producers.FirstOrDefault(obj => obj.Id == producerID);
        }
        public void UpdateProducer(IProducer producer)
        {
            var existingProducer = GetProducer(producer.Id);
            if (existingProducer != null)
            {
                existingProducer.Name = producer.Name;
                existingProducer.Address = producer.Address;
            }
        }
        public void DeleteProducer(int producerID)
        {
            var producer = GetProducer(producerID);
            if (producer != null)
            {
                containers.RemoveAll(c => c.Producer == producer);
                producers.Remove(producer);
            }
        }
    }
}
