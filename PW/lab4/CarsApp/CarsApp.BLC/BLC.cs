using CarsApp.Interfaces;
using System.Reflection;
namespace CarsApp.BLC
{
    // business logic component
    // make this a singleton
    public class BLC
    {
        private IDAO dao;

        public BLC(string libraryName)
        {
            Assembly assembly = Assembly.UnsafeLoadFrom(libraryName);
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

        public IEnumerable<IProducer> GetProducers() => dao.GetAllProducers();
        public IEnumerable<ICar> GetCars() => dao.GetAllCars();
    }
}