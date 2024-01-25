using ContainersApp.Core;
using ContainersApp.Interfaces;

namespace ContainersWebApp.Models
{
    public class Container : IContainer
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int ProductionYear { get; set; }
        public ContainerType Type { get; set; }
        public IProducer? Producer { get; set; } = null;
        public int ProducerId { get; set; }
    }
}
