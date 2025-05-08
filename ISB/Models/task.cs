using System.ComponentModel.DataAnnotations;

namespace ISB.Models
{
    public class task
    {
        [Key]
        public int task_id { get; set; }

        public string name { get; set; }
        public string description { get; set; }
        public string created_date { get; set; }
        public string updated_date { get; set; }
        public string? due_date { get; set; }
        public string? document { get; set; }
        /*No1*/
        public project project_data { get; set; }
        public int? project_ { get; set; }
        /*No2*/
        public employee emp_data { get; set; }
        public int employee_ids { get; set; }
        /*No3*/
        public task_priority _proprity { get; set; }
        public int task_priority { get; set; }
        /*No4*/
        public int task_statuss { get; set; }
        public task_status _status { get; set; }
        /*No 5*/
        public int task_types { get; set; }
        public task_type _types { get; set; }
        /*No 6*/
        public int plan_id { get; set; }
        public List<sub_task> subtasks_details { get; set; }
    }
}
