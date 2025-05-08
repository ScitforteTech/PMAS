namespace ISB.Models
{
    public class mainModel
    {
        public List<employee> logined_user { get; set; }
        public List<Companies> companies_details { get; set; }
        public Companies company_data { get; set; }
        public List<roles> role_detail { get; set; }
        public roles roles_data { get; set; }

        public List<depatement> department_details { get; set; }
        public depatement department_data { get; set; }
        public List<designation> design_details { get; set; }
        public designation design_data { get; set; }
        public List<employee> employee_details { get; set; }
        public employee employee_data { get; set; }
        public client client_data { get; set; }

        public List<client> client_details { get; set; }
        public List<team> team_details { get; set; }
        public team team_data { get; set; }
        public project project_data { get; set; }
        public List<project> project_details { get; set; }
        public List<task> task_details { get; set; }
        public task task_data { get; set; }
        public survey survey_data { get; set; }
        public List<survey> survey_details { get; set; }
        public List<Impact_risk> impact_Risks { get; set; }
        public Impact_risk impactrisk_data { get; set; }
        public List<Likelihood_risk> Likelihood_risk { get; set; }
        public Likelihood_risk likelihoodrisk_data { get; set; }
        public List<risk> risk_details { get; set; }
        public risk risk_data { get; set; }
        public List<project_status> pro_Statusdetail { get; set; }
        public project_status proStatus_data { get; set; }
        public  List<project_type> projects_types { get; set; }
        public project_type projects_typesData { get; set; }
        public List<task_type> task_typedetails { get; set; }
       
        public task_type task_typedata { get; set; }
        public task_status task_statusdata { get; set; }
        public List<task_status> task_statusdetails { get; set; }
        public List<project_priority> projects_prioritesDetails { get; set; }
        public project_priority projects_prioritesData { get; set; }
        public List<task_priority> task_prioritesDetails { get; set; }
        public task_priority task_prioritesData { get; set; }
        public List<task_files> files_details { get; set; }
        public List<sub_task> subTask_details { get; set; }
        public sub_task subTask_data { get; set; }
        public List<emp_type> emp_types_details { get; set; }
        public emp_type emp_types_data { get; set; }
        public List<maritalStatus> materialStatus_details { get; set; }
        public maritalStatus materialStatus_data { get; set; }
  
        public List<risk_type> Risktypes_details { get; set; }
        public risk_type Risktypes_data { get; set; }
        public List<AuditType>audittype { get; set; }
        public AuditType audittypedata { get; set; }
        public List<auditpriority> auditPriorityDetails { get; set; }
        public auditpriority auditPrioritydata { get; set; }
         public List<Audit> audit_datails { get; set; }
        public Audit audit_data { get; set; }
        public List<BudgetAdherence> buget_details { get; set; }
        public BudgetAdherence buget_data { get; set; }
        public List<ScheduleAdherence> scheduleAdherences { get; set; }
        public ScheduleAdherence scheduleAdherences_data { get; set; }

        public List<audit_Project> auditproject_details { get; set; }
        public audit_Project auditproject_data { get; set; }
        public List<StakeholderEngagement> StakeholderEngagement_details { get; set; }
        public StakeholderEngagement StakeholderEngagement_data { get; set; }
        public List<Communication_Effectivenes> Communication_Effectivenes_details { get; set; }
        public Communication_Effectivenes Communication_Effectivenes_data { get; set; }
        public List<riskmangament> riskmangament_details { get; set; }
        public riskmangament riskmangament_data { get; set; }
        public List<compliance_status> complianceStatus_details { get; set; }
        public compliance_status complianceStatus_data { get; set; }
        public List<ComplianceStandards> ComplianceStandards_details { get; set; }
        public ComplianceStandards ComplianceStandards_data { get; set; }
        public List<IssueSeverity> IssueSeverity_details { get; set; }
        public IssueSeverity IssueSeverity_data { get; set; }
        public List<ResolutionStatus> ResolutionStatus_details { get; set; }
        public ResolutionStatus ResolutionStatus_data { get; set; }
        public List<Issues_Challenges> issues_Detail { get; set; }
        public Issues_Challenges issues_Data { get; set; }
        public CorrectiveMeasures_priority correctiveMeasures_PriorityData { get; set; }
        public List< CorrectiveMeasures_priority> correctiveMeasures_PriorityDetails { get; set; }
        public CorrectiveMeasures correctiveMeasures_Data { get; set; }
        public List<CorrectiveMeasures> correctiveMeasures_Details { get; set; }
        public Audit_Rating Audit_Rating_Data { get; set; }
        public List<Audit_Rating> Audit_Rating_Details { get; set; }
        public Audit_Conclusion Audit_Conclusion_Data { get; set; }
        public List<Audit_Conclusion> Audit_Conclusion_Details { get; set; }
        public Docs Doc_Data { get; set; }
        public List<Docs> Docs_Details { get; set; }
        public plan_types planType_Data { get; set; }
        public List<plan_types> planType_Details { get; set; }
        public plan plan_data { get; set; }
        public List<plan> plan_Details { get; set; }
        public plan_pillars planPillar_data { get; set; }
        public List<plan_pillars> planPillar_Details { get; set; }
        public onepage_plan oneplan_data { get; set; }
        public List<onepage_plan> oneplan_Details { get; set; }

    }
}
