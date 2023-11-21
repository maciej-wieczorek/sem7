namespace AssignmentsSharing.Models
{
    public class Issue
    {
        public Guid Id { get; set; }
        public List<Assignment> Assignments { get; set; } = new List<Assignment>();
        public List<Developer> Developers { get; set; } = new List<Developer>();

    }
}
