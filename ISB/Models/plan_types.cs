using System.ComponentModel.DataAnnotations;

namespace ISB.Models
{
    public class plan_types
    {
        [Key]
        public int id { get; set; }
        public string name { get; set; }
        public string color { get; set; }
        public int status { get; set; }
        public List<plan> _plan { get; set; }
    }
}
