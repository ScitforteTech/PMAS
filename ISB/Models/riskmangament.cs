using System.ComponentModel.DataAnnotations;

namespace ISB.Models
{
    public class riskmangament
    {
        [Key]
        public int id { get; set; }
     public string   IdentifiedRisks { get; set; }
        public string Risk_Mitigation_Plans{ get; set; }
        public int Likelihood { get; set; }
        public string impact { get; set; }
        public string Risk_Scoring { get; set; }
        public int Stakeholder_id { get; set; }
        public StakeholderEngagement Stakeholder_Engagement { get; set; }
        public int Effectiveness_id { get; set; }
        public Communication_Effectivenes Effectiveness_Engagement { get; set; }

    }
}
