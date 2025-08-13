using Domain.Classes.Values;

namespace Domain.Classes.Entities
{
    public sealed class Education
    {
        public int Id { get; }
        public School School { get; }
        public Course Course { get; }
        public DateRange Period { get; }
        public IReadOnlyCollection<Skill> Skills { get; }

        public Education(int id, School school, Course course, DateRange period, IEnumerable<Skill> skills)
        {
            if (school == null)
            {
                throw new ArgumentException("School should not be null.");
            }

            if (course == null)
            {
                throw new ArgumentException("Course should not be null.");
            }

            Id = id;
            School = school;
            Course = course;
            Period = period;
            Skills = skills?.ToList() ?? new List<Skill>();
        }
    }
}
