using System.ComponentModel.DataAnnotations;

namespace ISB.Models
{
    public class onepage_plan
    {
        [Key]
        public int id { get; set; }
        public int parent_Plan { get; set; }

        public string name { get; set; }
        public int team { get; set; }
        public team team_details { get; set; }

        public string timeframe { get; set; }
        public int type { get; set; }
        public string description { get; set; }


    }
}
