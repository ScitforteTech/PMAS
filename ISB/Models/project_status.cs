using System.ComponentModel.DataAnnotations;

namespace ISB.Models
{
    public class project_status
    {
        [Key]
        public int status_id { get; set; }
        public string name_status { get; set; }
        public int status { get; set; }
        public string? colour { get; set; }
        public List<project> project_details { get; set; }
        public List<audit_Project> auditproject_details { get; set; }
    }
}
