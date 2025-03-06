using System.ComponentModel.DataAnnotations;

namespace ISB.Models
{
    public class Companies
    {
        [Key]
        public int id { get; set; }

        public string name { get; set; }
        public string website { get; set; }
        public string address { get; set; }
        public string city { get; set; }
        public string country { get; set; }
       
        public int status { set; get; }
        public string Phonenumber { get; set; }

        public string modules { set; get; }
        public string logo { get; set; }

        public string user_name { get; set; }
        public string email { get; set; }
        public string dateCurrent { get; set; }
        public string user_invitations { get; set; }
    }
}
