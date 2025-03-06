using System.ComponentModel.DataAnnotations;

namespace ISB.Models
{
    public class client
    {
        [Key]
      public  int id { get; set; }
        public string name{ get; set; }
        public string email { get; set; }
        public string number { get; set; }
        public string? address { get; set; }
        public List<project> pro_details { get; set; }
    }
}
