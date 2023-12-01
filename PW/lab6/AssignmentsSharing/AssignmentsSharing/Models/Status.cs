namespace AssignmentsSharing.Models
{
    public class Status
    {
        public Guid Id { get; set; }
        public StatusType Type { get; set; }
        public DateTime OccuranceTime { get; set; }
        public Assignment? Assignment { get; set; }
    }
}
