using System.ComponentModel.DataAnnotations;

namespace ISB.Models
{
    public class Audit_Rating
    {
        [Key]
        public int id { get; set; }
        public string name { get; set; }
        public string colour { get; set; }
        public int status { get; set; }
        public List<Audit_Conclusion> conclusions { get; set; }
    }
}
