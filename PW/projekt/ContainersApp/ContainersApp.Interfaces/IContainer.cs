using ContainersApp.Core;

namespace ContainersApp.Interfaces
{
    public interface IContainer
    {
        int Id { get; set; }
        string Name { get; set; }
        IProducer Producer { get; set; }
        int ProductionYear { get; set; }
        ContainerType Type { get; set; }
    }
}
