using System.ComponentModel.DataAnnotations;

namespace ISB.Models
{
    public class plan
    {
        [Key]
        public int id { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public string time_frame { get; set; }

        public int type_id { get; set; }
        public plan_types types { get; set; }
        public int team_id { get; set; }
        public team _team { get; set; }


    }
}
