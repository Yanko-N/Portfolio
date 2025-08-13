namespace Persistence.Models
{
    public class PersonalInfo
    {
        public int Id { get; set; }
        public Image ProfileImage { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string WorkTitle { get; set; }
        public string EndPointCV { get; set; }
        public string Email { get; set; }
        public string CellphoneNumber { get; set; }
        public string Address { get; set; }
    }
}
