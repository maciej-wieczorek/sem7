namespace AssignmentsSharing.Models
{
    public class Status
    {
        public enum StatusType { Open, WIP, Closed }
        public Guid Id { get; set; }
        public StatusType Type { get; set; }
        public DateTime OccuranceTime { get; set; }
        public Assignment Assignment { get; set; }
    }
}
