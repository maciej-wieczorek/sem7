using Microsoft.EntityFrameworkCore;

namespace AssignmentsSharing.Models
{
    [Index(nameof(Label), IsUnique = true)]
    public class Issue
    {
        public Guid Id { get; set; }
        public string Label { get; set; }
        public string Description { get; set; }
        public List<Assignment> Assignments { get; set; } = new List<Assignment>();
        public List<Developer> Developers { get; set; } = new List<Developer>();

    }
}
