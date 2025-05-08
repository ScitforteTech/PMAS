using System.ComponentModel.DataAnnotations;

namespace ISB.Models
{
    public class doc_favourite
    {
        [Key]
        public int _id { get; set; }
        public int user_id { get; set; }
        public int doc_id { get; set; }
    }
}
