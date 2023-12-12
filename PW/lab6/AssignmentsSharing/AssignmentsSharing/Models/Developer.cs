using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace AssignmentsSharing.Models
{
    [Index(nameof(Pseudonym), IsUnique = true)]
    public class Developer : IdentityUser<Guid>
	{
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public override Guid Id { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Pseudonym { get; set; }
        public List<Assignment> Assignments { get; set; } = new List<Assignment>();
        public List<Issue> Issues { get; set; } = new List<Issue>();
    }
}
