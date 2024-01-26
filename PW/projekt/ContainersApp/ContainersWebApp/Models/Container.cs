using ContainersApp.Core;
using ContainersApp.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace ContainersWebApp.Models
{
    public class Container : IContainer
    {
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        [Required]
        [Range(1900, 2100)]
        public int ProductionYear { get; set; }

        [Required]
        public ContainerType Type { get; set; }

        public IProducer? Producer { get; set; } = null;
        
        [Required]
        public int ProducerId { get; set; }
    }
}
