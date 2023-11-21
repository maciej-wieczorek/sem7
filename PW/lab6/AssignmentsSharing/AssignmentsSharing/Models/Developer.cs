namespace AssignmentsSharing.Models
{
    public class Developer
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Pseudonym { get; set; }
        public List<Assignment> Assignments { get; set; } = new List<Assignment>();
        public List<Issue> Issues { get; set; } = new List<Issue>();
    }
}
