namespace Portofolio.Models
{
	public class ResumeModel
	{
		public string Name { get; set; }
		public string Description { get; set; }
		public string Role { get; set; }
		public string Number { get; set; }
		public string Email { get; set; }
		public string Adress { get; set; }
		public List<string> Links { get; set; }

		public List<Education> Educations { get; set; }
		public List<Skill> Skills { get; set; }
		public List<Work> Works { get; set; }
		public List<Language> Languages { get; set; }
	}
}
