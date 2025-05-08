using System.ComponentModel.DataAnnotations;

namespace ISB.Models
{
    public class plan_pillars
    {
        [Key]
        public int id { get; set; }
        public string name { get; set; }
        public string? description { get; set; }
        public int plan_id { get; set; }
    }
}
