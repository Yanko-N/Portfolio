namespace Persistence.Models
{
    public class Education
    {
        public int Id { get; set; }
        public int SchoolId { get; set; }
        public School School { get; set; }

        public int CourseId { get; set; }
        public virtual Course? Course { get; set; } 

        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; } // Null if is the current education
        public virtual ICollection<Skill> Skills { get; set; } = new List<Skill>();
    }
}
