using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

namespace ISB.Models
{
    public class team
    {
        [Key]
        public int id { get; set; }
     
        public string team_name { get; set; }
        public string team_des { get; set; }
        public string team_leader { get; set; }
        public string? team_member1 { get; set; }
        public string? team_member2 { get; set; }
        public string? team_member3 { get; set; }
        public string? team_member4 { get; set; }
        public string? team_member5 { get; set; }
        public string? team_member6 { get; set; }
        public int status { get; set; }

      public List<project> pro_details { get; set; }
        public List<plan> _plan { get; set; }
        public List<onepage_plan> _oneplan { get; set; }

    }
}
