using System.ComponentModel.DataAnnotations;

namespace ISB.Models
{
    public class Issues_Challenges
    {
        [Key]
        public int id { get; set; }
        public string Open_Issue { get; set; }
        public string Issue_Severity { get; set; }
        public string Resolution_Status { get; set; }
    }
}
