using ContainersApp.Interfaces;

namespace ContainersApp.DAOSql
{
    public class DAOSql : IDAO
    {
        private DataContext _dbContext;

        public DAOSql()
        {
            _dbContext = new DataContext();
        }
        public IEnumerable<IContainer> GetAllContainers()
        {
            return _dbContext.containers.ToList();
        }
        public void AddContainer(IContainer container)
        {
            var newId = (_dbContext.containers.Max(c => (int?)c.Id) ?? 0) + 1;
            var newContainer = new BO.Container
            {
                Id = newId,
                Name = container.Name,
                Producer = (BO.Producer)container.Producer,
                ProductionYear = container.ProductionYear,
                Type = container.Type,
            };

            _dbContext.containers.Add(newContainer);
            _dbContext.SaveChanges();
        }
        public IContainer? GetContainer(int containerID)
        {
            return _dbContext.containers.FirstOrDefault(c => c.Id == containerID);
        }

        public void UpdateContainer(IContainer container)
        {
            var existingContainer = _dbContext.containers.Find(container.Id);
            if (existingContainer != null)
            {
                _dbContext.Entry(existingContainer).CurrentValues.SetValues(container);
                _dbContext.SaveChanges();
            }
        }

        public void DeleteContainer(int containerID)
        {
            var container = _dbContext.containers.Find(containerID);
            if (container != null)
            {
                _dbContext.containers.Remove(container);
                _dbContext.SaveChanges();
            }
        }

        public IEnumerable<IProducer> GetAllProducers()
        {
            return _dbContext.producers.ToList();
        }
        public void AddProducer(IProducer producer)
        {
            _dbContext.producers.Add((BO.Producer)producer);
            _dbContext.SaveChanges();
        }
        public IProducer? GetProducer(int producerID)
        {
            return _dbContext.producers.FirstOrDefault(p => p.Id == producerID);
        }
        public void UpdateProducer(IProducer producer)
        {
            var existingProducer = _dbContext.producers.Find(producer.Id);
            if (existingProducer != null)
            {
                _dbContext.Entry(existingProducer).CurrentValues.SetValues(producer);
                _dbContext.SaveChanges();
            }
        }

        public void DeleteProducer(int producerID)
        {
            var producer = _dbContext.producers.Find(producerID);
            if (producer != null)
            {
                _dbContext.producers.Remove(producer);
                _dbContext.SaveChanges();
            }
        }
    }
}