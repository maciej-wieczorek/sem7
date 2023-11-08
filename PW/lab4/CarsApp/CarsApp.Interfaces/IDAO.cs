using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarsApp.Interfaces
{
    // data access object
    public interface IDAO
    {
        IEnumerable<IProducer> GetAllProducers();
        IEnumerable<ICar> GetAllCars();
        IProducer CreateNewProducer();
        ICar CreateNewCar();
    }
}
