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
                new BO.Producer() {ID = 1, Name = "Producer1"},
                new BO.Producer() {ID = 2, Name = "Producer2"}
            };

            containers = new List<IContainer>()
            {
                new BO.Container() {ID = 1, Producer = producers[0], Name = "Container1", ProductionYear = 2020, Type = Core.ContainerType.BundedStorage },
                new BO.Container() {ID = 2, Producer = producers[0], Name = "Container2", ProductionYear = 2022, Type = Core.ContainerType.OpenTop },
                new BO.Container() {ID = 3, Producer = producers[1], Name = "Container3", ProductionYear = 2023, Type = Core.ContainerType.Classic }
            };
        }
        public IContainer CreateNewContainer()
        {
            return new BO.Container();
        }

        public IProducer CreateNewProducer()
        {
            return new BO.Producer();
        }

        public IEnumerable<IContainer> GetAllContainers()
        {
            return containers;
        }

        public IEnumerable<IProducer> GetAllProducers()
        {
            return producers;
        }
    }
}
