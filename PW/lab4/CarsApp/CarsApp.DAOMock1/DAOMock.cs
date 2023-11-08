using CarsApp.Interfaces;

namespace CarsApp.DAOMock1
{
    public class DAOMock : IDAO
    {
        private List<IProducer> producers;
        private List<ICar> cars;

        public DAOMock()
        {
            producers = new List<IProducer>()
            {
                new BO.Producer() {ID = 1, Name = "Audi"},
                new BO.Producer() {ID = 2, Name= "Mazda"}
            };

            cars = new List<ICar>()
            {
                new BO.Car() {ID = 1, Producer = producers[0], Name = "Q7", ProductionYear = 2020, Transmission = Core.TransmissionType.Automatic},
                new BO.Car() {ID = 2, Producer = producers[0], Name = "RS3", ProductionYear = 2022, Transmission = Core.TransmissionType.Automatic},
                new BO.Car() {ID = 3, Producer = producers[1], Name = "CX-60", ProductionYear = 2023, Transmission = Core.TransmissionType.Manual}
            };
        }
        public ICar CreateNewCar()
        {
            return new BO.Car();
        }

        public IProducer CreateNewProducer()
        {
            return new BO.Producer();
        }

        public IEnumerable<ICar> GetAllCars()
        {
            return cars;
        }

        public IEnumerable<IProducer> GetAllProducers()
        {
            return producers;
        }
    }
}