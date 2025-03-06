using System.ComponentModel.DataAnnotations;

namespace ISB.Models
{
    public class project_type
    {
        [Key]
        public int id { get; set; }
        public string project_typeName { get; set; }
        public string colour { get; set; }
        public int status { get; set; }
        public List<project> project_details { get; set; }
    }
}
