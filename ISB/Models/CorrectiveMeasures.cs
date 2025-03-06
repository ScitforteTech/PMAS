using System.ComponentModel.DataAnnotations;

namespace ISB.Models
{
    public class CorrectiveMeasures
    {
        [Key]
        public int id { get; set; }
        public string ImprovementSuggestions { get; set; }
        public int priority_id { get; set; }
        public CorrectiveMeasures_priority priority { get; set; }

    }
}
