using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Models
{
    public class Project
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string DemoUrl { get; set; }
        public string SourceCodeUrl { get; set; }

        public int ProjectCategoryId { get; set; }
        public virtual ProjectCategory? Category { get; set; }

        public ICollection<Skill> Skills { get; set; } = new List<Skill>();
        public ICollection<Image> Images { get; set; } = new List<Image>();
    }
}
