using System.ComponentModel.DataAnnotations;

namespace ISB.Models
{
    public class Impact_risk
    {

        [Key]
        public int id { get; set; }
        public string risk_name { get; set; }
        public int scoring { get; set; }

    }
}
