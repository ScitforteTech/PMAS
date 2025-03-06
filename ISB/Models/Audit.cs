using System.ComponentModel.DataAnnotations;

namespace ISB.Models
{
    public class Audit
    {
        [Key]
        public int id { get; set; }
        public string Tittle { get; set; }
        public int project_id { get; set; }
        public project projectDetails { get; set; }
        public int task_id { get; set; }
        public int Type_id { get; set; }
        public AuditType typesDetails { get; set; }
        public int assignedauditor { get; set; }
        public employee empDetails { get; set; }
        public string? AuditScope { get; set; }
        public string? AuditObjectives { get; set; }
        public  int Priority_id { get; set; }

        public auditpriority priorityDetails { get; set; }
        public string Due_Date { get; set; }
        public string? Comments { get; set; }
        public string? Attachment { get; set; }
    }
}
