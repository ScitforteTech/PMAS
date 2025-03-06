using System.ComponentModel.DataAnnotations;

namespace ISB.Models
{
    public class ComplianceStandards
    {
        [Key]
        public int id { get; set; }
        public int compliacne_id { get; set; }
        public compliance_status status { get; set; }
        public string Findings { get; set; }

        public string Evidence { get; set; }

    }
}
