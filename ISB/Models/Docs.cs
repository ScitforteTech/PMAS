using System.ComponentModel.DataAnnotations;

namespace ISB.Models
{
    public class Docs
    {
        [Key]
        public int id { get; set; }
        public int user_id { get; set; }
         public string Tittle { get; set; }
        public string Desciprtion { get; set; }

    }
}
