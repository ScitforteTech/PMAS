using System.ComponentModel.DataAnnotations;

namespace ISB.Models
{
    public class depatement
    {
        [Key]
        public int id { get; set; }
        public string department_name { get; set; }
        public List<designation> designation_data { get; set; }
        public List<employee> employees { get; set; }
    }
}
