using System.ComponentModel.DataAnnotations;

namespace ISB.Models
{
    public class risk
    {
        [Key]
        public int id { get; set; }

        public string Risk_factor { get; set; }
        public string description { get; set; }
        public string risk_category { get; set; }

        public int impact_risk { get; set; }
        public int likelihood_risk { get; set; }
        public int total_scoring { get; set; }
        public List<task> tasks_details { get; set; }
        public List<sub_task> subtasks_details { get; set; }

    }
}
