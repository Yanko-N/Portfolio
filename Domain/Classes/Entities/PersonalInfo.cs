using Domain.Classes.Values;

namespace Domain.Classes.Entities
{
    public class PersonalInfo
    {
        public int Id { get; set; }
        public Image ProfileImage { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string WorkTitle { get; set; }
        public string EndPointCV { get; set; }
        public Email Email { get; set; }
        public string CellphoneNumber { get; set; }

        public string Address { get; set; }

        public PersonalInfo(int id, Image profileImage,string name, string description, string workTitle,string endPointCV, Email email,
            string cellphoneNumber, string address)
        {
            if (string.IsNullOrWhiteSpace(name)) 
            { 
                throw new ArgumentException("Name is required."); 
            }

            if (email == null) 
            { 
                throw new ArgumentNullException(nameof(email), "Email is required.");
            }


            Id = id;
            Name = name.Trim();
            ProfileImage = profileImage;
            Description = description ?? string.Empty;
            WorkTitle = workTitle ?? string.Empty;
            EndPointCV = endPointCV ?? string.Empty;
            Email = email;
            CellphoneNumber = cellphoneNumber ?? string.Empty;
            Address = address ?? string.Empty;
        }
    }
}
