using System.ComponentModel.DataAnnotations;

namespace ISB.Models
{
    public class designation
    {
        [Key]
        public int id { get; set; }
        public string designation_name { get; set; }
        public string colour_code { get; set; }

        public int  depart_id { get; set; }
         public depatement  depart_detail { get; set; }
        public List<employee> employees_data { get; set; }
    }
}
