using System.ComponentModel.DataAnnotations;

namespace ISB.Models
{
    public class sub_task
    {
        [Key]
        public int id { get; set; }
        public string tittle { get; set; }
        public string discription { get; set; }
        public string? files_upload { get; set; }
        public string? start_date { get; set; }
        public string? due_date { get; set; }
        public int emp_id { get; set; }
        public employee emp_details { get; set; }
        public int risk_id { get; set; }
        public risk risl_details { get; set; }
        public int task_id { get; set; }
        public task task_details { get; set; }
    }
}
