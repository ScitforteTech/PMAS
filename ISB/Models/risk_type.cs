using System.ComponentModel.DataAnnotations;

namespace ISB.Models
{
    public class risk_type
    {
        [Key]
        public int id { get; set; }
        public string name { get; set; }
        public string color { get; set; }
        public int status { get; set; }

    }
}
