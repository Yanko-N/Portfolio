namespace Persistence.Models
{
    public class Work
    {
        public int Id { get; set; }
        public int EnterpriseId { get; set; }
        public virtual Enterprise? Enterprise { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; } // Null if is the current job
        public string Title { get; set; }
        public string Description { get; set; }

        public virtual ICollection<Skill> Skills { get; set; } = new List<Skill>();
    }
}
