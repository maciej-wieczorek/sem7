using ContainersApp.Core;
using ContainersApp.Interfaces;

namespace ContainersApp.DAOSql.BO
{
    public class Container : IContainer
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public Producer Producer { get; set; }
        IProducer IContainer.Producer { get { return Producer; } set { Producer = (BO.Producer)value; } }
        public int ProductionYear { get; set; }
        public ContainerType Type { get; set; }
    }
}