using System.ComponentModel.DataAnnotations;

namespace ISB.Models
{
    public class ScheduleAdherence
    {
        [Key]
        public int id { get; set; }
        public string name { get; set; }
        public string colour { get; set; }
        public int status { get; set; }
        public List<audit_Project> auditproject_details { get; set; }

    }
}
