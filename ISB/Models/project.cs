using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

namespace ISB.Models
{
    public class project
    {
        [Key]
        public int Project_id { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public string start_date { get; set; }
        public string end_date { get; set; }
        public string? document { get; set; }
        public int team_id { get; set; }
         public employee employee_details { get; set; }
        public int client_id { get; set; }
        public client client_data { get; set; }
         public int project_statuss { get; set; }
        public project_status status_details { get; set; }
        public int project_priority { get; set; }
        public project_priority priority_details { get; set; }
        public int project_types { get; set; }
        public int plan_id { get; set; }
        public project_type types_details { get; set; }
        
        public List<task> tasks_details { get; set; }
        public List<Audit> audit_details { get; set; }
        //public List<audit_Project> audit_projectdetails { get; set; }
        public ICollection<audit_Project> audit_projectdetails { get; set; } 
    }
}
