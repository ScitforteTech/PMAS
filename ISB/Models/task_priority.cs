using System.ComponentModel.DataAnnotations;

namespace ISB.Models
{
    public class task_priority
    {
        [Key]
        public int id { get; set; }
        public string name { get; set; }
        public string colour { get; set; }
        public int status { get; set; }
        public List<task> tasks_details { get; set; }
    }
}
