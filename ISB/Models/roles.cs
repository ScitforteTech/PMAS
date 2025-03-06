using System.ComponentModel.DataAnnotations;

namespace ISB.Models
{
    public class roles
    {
        [Key]
        public int id { get; set; }
        public string roles_name { get; set; }
        public List<employee> employees { get; set; }
    }
}
