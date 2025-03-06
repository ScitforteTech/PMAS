using System.ComponentModel.DataAnnotations;

namespace ISB.Models
{
    public class Audit_Conclusion
    {
        [Key]
        public int id { get; set; }
        public int rating_id { get; set; }
        public Audit_Rating _Rating { get; set; }
        public string Summary_Findings { get; set; }
        public string Date { get; set; }

    }
}
