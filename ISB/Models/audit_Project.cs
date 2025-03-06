namespace ISB.Models
{
    public class audit_Project
    {
        public int  id { get; set; }
        public int project_id { get; set; }
        public project project { get; set; }
        public string start_date { get; set; }
        public string end_date { get; set; }
        public int project_Status { get; set; }
        public project_status projectStatus { get; set; }
        public int buget_id { get; set; }
        public BudgetAdherence budgetdetails { get; set; }
        public int schedule_id { get; set; }
        public ScheduleAdherence scheduledetails { get; set; }
        public string? kips { get; set; } 
    }
}
