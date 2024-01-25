using ContainersApp.Interfaces;
using System.Reflection;
using Microsoft.Extensions.Configuration;

namespace ContainersApp.BLC
{
    // business logic component
    public class BLC
    {
        private IDAO dao;

        public BLC(IConfiguration confguration)
        {
            Assembly assembly = Assembly.UnsafeLoadFrom(confguration.GetSection("DAOLibrary")["Path"]);
            Type typeToCreate = null;

            foreach (Type type in assembly.GetTypes())
            {
                if (type.IsAssignableTo(typeof(IDAO)))
                {
                    typeToCreate = type;
                    break;
                }
            }

            dao = (IDAO)Activator.CreateInstance(typeToCreate, null);
        }


        public IEnumerable<IContainer> GetAllContainers() => dao.GetAllContainers();
        public void AddContainer(IContainer container) => dao.AddContainer(container);
        public IContainer? GetContainer(int containerID) => dao.GetContainer(containerID);
        public void UpdateContainer(IContainer container) => dao.UpdateContainer(container);
		public void DeleteContainer(int containerID) => dao.DeleteContainer(containerID);

        public IEnumerable<IProducer> GetAllProducers() => dao.GetAllProducers();
		public void AddProducer(IProducer producer) => dao.AddProducer(producer);
        public IProducer? GetProducer(int producerID) => dao.GetProducer(producerID);
		public void UpdateProducer(IProducer producer) => dao.UpdateProducer(producer);
		public void DeleteProducer(int producerID) => dao.DeleteProducer(producerID);
    }
}
