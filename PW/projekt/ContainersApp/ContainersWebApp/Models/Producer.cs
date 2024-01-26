using ContainersApp.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace ContainersWebApp.Models
{
    public class Producer : IProducer
    {
        public int Id { get; set; }
        
        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        [Required]
        [StringLength(100)]
        public string Address { get; set; }
    }
}
