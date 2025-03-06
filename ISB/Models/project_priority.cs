using System.ComponentModel.DataAnnotations;

namespace ISB.Models
{
    public class project_priority
    {
        [Key]
        public int id { get; set; }
        public string name { get; set; }
        public string colour { get; set; }
        public int status { get; set; }
        public List<project> project_details { get; set; }
    }
}
