using System.ComponentModel.DataAnnotations;

namespace ISB.Models
{
    public class task_files
    {
        [Key]
        public int Id { get; set; }
        public string file_name { get; set; }

        public string date { get; set; }
        public int task_ids { get; set; }
      

    }
}
