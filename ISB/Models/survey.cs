using System.ComponentModel.DataAnnotations;

namespace ISB.Models
{
    public class survey
    {
        [Key]
        public int id { get; set; }
       public int emp_id { get; set; }
        public employee employee_details { get; set; }
        public string subject { get; set; }
        public string descriptions { get; set; }
    
    }
}
