using Microsoft.EntityFrameworkCore;

namespace ISB.Models
{
    public class dbcontext : DbContext
    {
        public dbcontext(DbContextOptions<dbcontext> options) : base(options)
        {

        }
        public DbSet<Companies> tbl_companies { get; set; }
        public DbSet<roles> tbl_roles { get; set; }
        public DbSet<depatement> tbl_departments { get; set; }
        public DbSet<designation> tbl_designations { get; set; }

        public DbSet<employee> tbl_employee { get; set; }
        public DbSet<client> tbl_clients { get; set; }
        public DbSet<team> tbl_teams { get; set; }
        public DbSet<project> tbl_projects{ get; set; }
       
        public DbSet<survey> tbl_survey { get; set; }
        public DbSet<Likelihood_risk> tbl_likedhood_risk { get; set; }
        public DbSet<Impact_risk> tbl_impact_risk { get; set; }
        public DbSet<risk> tbl_risks { get; set; }
        public DbSet<project_status> tbl_projectStatus { get; set; }
        public DbSet<project_type> tbl_project_types { get; set; }
        public DbSet<task_type> tbl_task_types { get; set; }
        public DbSet<task_status> tbl_task_status { get; set; }
        public DbSet<project_priority> tbl_projectpriority { get; set; }
        public DbSet<task_priority> tbl_taskpriority { get; set; }
        public DbSet<task> tbl_tasks { get; set; }
        public DbSet<task_files> tbl_taks_file { get; set; }
        public DbSet<sub_task> tbl_subtask { get; set; }
        public DbSet<emp_type> tbl_emptypes { get; set; }
        public DbSet<maritalStatus> tbl_maritalstatus { get; set; }
     
        public DbSet<risk_type> tbl_risk_types { get; set; }
        public DbSet<AuditType> tbl_auditTypes { get; set; }
        public DbSet<auditpriority> tbl_auditpriority { get; set; }
        public DbSet<Audit> tbl_audit { get; set; }
        public DbSet<BudgetAdherence> tbl_bugetaherence { get; set; }
        public DbSet<ScheduleAdherence> tbl_ScheduleAdherence { get; set; }
        public DbSet<audit_Project> tbl_auditProject { get; set; }
        public DbSet<StakeholderEngagement> tbl_StakeholderEngagement { get; set; }
        public DbSet<Communication_Effectivenes> tbl_CommunicationEffectiveness { get; set; }
        public DbSet<riskmangament> tbl_riskmangament { get; set; }
        public DbSet<compliance_status> tbl_compliancestatus { get; set; }
        public DbSet<ComplianceStandards> tbl_complianceandStandards { get; set; }
        public DbSet<IssueSeverity> tbl_issueSeverity { get; set; }
        public DbSet<ResolutionStatus> tbl_ResolutionStatus { get; set; }
        public DbSet<Issues_Challenges> tbl_issue { get; set; }
        public DbSet<CorrectiveMeasures_priority> tbl_measurePrority { get; set; }
        public DbSet<CorrectiveMeasures> tbl_auditCorrectiveMeasures { get; set; }
        public DbSet<Audit_Rating> tbl_auditRating{ get; set; }
        public DbSet<Audit_Conclusion> tblaudit_Conclusions { get; set; }
        public DbSet<Docs> tbl_docs { get; set; }

        public DbSet<doc_favourite> tbl_favdocs { get; set; }
        public DbSet<plan_types> tbl_plantypes { get; set; }
        public DbSet<plan> tbl_plan { get; set; }
        public DbSet<plan_pillars> tbl_plan_Pillars { get; set; }
        public DbSet<onepage_plan> tbl_oneplan { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<designation>().
                HasOne(p => p.depart_detail).
                WithMany(d => d.designation_data).HasForeignKey(b => b.depart_id);

            modelBuilder.Entity<employee>().
                HasOne(p => p.department_details).
                WithMany(d => d.employees).HasForeignKey(b => b.depart_id).OnDelete(DeleteBehavior.Restrict); ;

            modelBuilder.Entity<employee>().
               HasOne(p => p.roles_details).
               WithMany(d => d.employees).HasForeignKey(b => b.role_id).OnDelete(DeleteBehavior.Cascade); ;


            modelBuilder.Entity<employee>().
           HasOne(p => p.designation_details).
           WithMany(d => d.employees_data).HasForeignKey(b => b.designationid).OnDelete(DeleteBehavior.Cascade); ;
           
            modelBuilder.Entity<project>().
              HasOne(p => p.client_data).
              WithMany(d => d.pro_details).HasForeignKey(b => b.client_id).OnDelete(DeleteBehavior.ClientSetNull);

            modelBuilder.Entity<project>().
           HasOne(p => p.priority_details).
           WithMany(d => d.project_details).HasForeignKey(b => b.project_priority).OnDelete(DeleteBehavior.ClientSetNull);

            modelBuilder.Entity<project>().
           HasOne(p => p.employee_details).
           WithMany(d => d.project_details).HasForeignKey(b => b.team_id).OnDelete(DeleteBehavior.ClientSetNull);
            modelBuilder.Entity<project>().
           HasOne(p => p.types_details).
           WithMany(d => d.project_details).HasForeignKey(b => b.project_types).OnDelete(DeleteBehavior.ClientSetNull);
            modelBuilder.Entity<project>().
         HasOne(p => p.status_details).
         WithMany(d => d.project_details).HasForeignKey(b => b.project_statuss).OnDelete(DeleteBehavior.ClientSetNull);
        
            modelBuilder.Entity<survey>().
            HasOne(p => p.employee_details).
            WithMany(d => d.survey_details).HasForeignKey(b => b.emp_id);
            modelBuilder.Entity<task>().
          HasOne(p => p.project_data).
          WithMany(d => d.tasks_details).HasForeignKey(b => b.project_).
          OnDelete(DeleteBehavior.ClientSetNull); 

            modelBuilder.Entity<task>().
       HasOne(p => p.emp_data).
       WithMany(d => d.tasks_details).HasForeignKey(b => b.employee_ids);
            modelBuilder.Entity<task>().
       HasOne(p => p._status).
       WithMany(d => d.tasks_details).HasForeignKey(b => b.task_statuss);
            modelBuilder.Entity<task>().
       HasOne(p => p._proprity).
       WithMany(d => d.tasks_details).HasForeignKey(b => b.task_priority);
            modelBuilder.Entity<task>().
       HasOne(p => p._types).
       WithMany(d => d.tasks_details).HasForeignKey(b => b.task_types);
          
            modelBuilder.Entity<sub_task>().
                    HasOne(p => p.emp_details).
WithMany(d => d.subtasks_details).HasForeignKey(b => b.emp_id).OnDelete(DeleteBehavior.ClientSetNull);
            modelBuilder.Entity<sub_task>().
                   HasOne(p => p.task_details).
WithMany(d => d.subtasks_details).HasForeignKey(b => b.task_id).OnDelete(DeleteBehavior.ClientSetNull);
            modelBuilder.Entity<sub_task>().
                 HasOne(p => p.risl_details).
WithMany(d => d.subtasks_details).HasForeignKey(b => b.risk_id).OnDelete(DeleteBehavior.ClientSetNull);
            modelBuilder.Entity<Audit>().
              HasOne(p => p.typesDetails).
WithMany(d => d.auditdetails).HasForeignKey(b => b.Type_id).OnDelete(DeleteBehavior.ClientSetNull);
            modelBuilder.Entity<Audit>().
        HasOne(p => p.priorityDetails).
WithMany(d => d.auditdetails).HasForeignKey(b => b.Priority_id).OnDelete(DeleteBehavior.ClientSetNull);
            modelBuilder.Entity<Audit>().
        HasOne(p => p.empDetails).
WithMany(d => d.audit_details).HasForeignKey(b => b.assignedauditor).OnDelete(DeleteBehavior.ClientSetNull);
            modelBuilder.Entity<Audit>().
              HasOne(p => p.projectDetails).
      WithMany(d => d.audit_details).HasForeignKey(b => b.project_id).OnDelete(DeleteBehavior.ClientSetNull);
            modelBuilder.Entity<audit_Project>().
     HasOne(p => p.budgetdetails).
WithMany(d => d.auditproject_details).HasForeignKey(b => b.buget_id).OnDelete(DeleteBehavior.ClientSetNull);
            modelBuilder.Entity<audit_Project>().
  HasOne(p => p.scheduledetails).
WithMany(d => d.auditproject_details).HasForeignKey(b => b.schedule_id).OnDelete(DeleteBehavior.ClientSetNull);
            modelBuilder.Entity<audit_Project>().
     HasOne(p => p.projectStatus).
   WithMany(d => d.auditproject_details).HasForeignKey(b => b.project_Status).OnDelete(DeleteBehavior.ClientSetNull);
            modelBuilder.Entity<audit_Project>().
    HasOne(p => p.project).
  WithMany(d => d.audit_projectdetails).HasForeignKey(b => b.project_id).OnDelete(DeleteBehavior.ClientSetNull);
            modelBuilder.Entity<riskmangament>().
     HasOne(p => p.Effectiveness_Engagement).
   WithMany(d => d.riskmangaments).HasForeignKey(b => b.Effectiveness_id).OnDelete(DeleteBehavior.ClientSetNull);
            modelBuilder.Entity<riskmangament>().
      HasOne(p => p.Stakeholder_Engagement).
    WithMany(d => d.riskmangaments).HasForeignKey(b => b.Stakeholder_id).OnDelete(DeleteBehavior.ClientSetNull);
            modelBuilder.Entity<ComplianceStandards>().
         HasOne(p => p.status).
       WithMany(d => d.standards).HasForeignKey(b => b.compliacne_id).OnDelete(DeleteBehavior.ClientSetNull);
            modelBuilder.Entity<CorrectiveMeasures>().
        HasOne(p => p.priority).
      WithMany(d => d.CorrectiveMeasures).HasForeignKey(b => b.priority_id).OnDelete(DeleteBehavior.ClientSetNull);
            modelBuilder.Entity<Audit_Conclusion>().
           HasOne(p => p._Rating).
         WithMany(d => d.conclusions).HasForeignKey(b => b.rating_id).OnDelete(DeleteBehavior.ClientSetNull);
            modelBuilder.Entity<plan>().
        HasOne(p => p.types).
      WithMany(d => d._plan).HasForeignKey(b => b.type_id).OnDelete(DeleteBehavior.ClientSetNull);
            modelBuilder.Entity<plan>().
        HasOne(p => p._team).
      WithMany(d => d._plan).HasForeignKey(b => b.team_id).OnDelete(DeleteBehavior.ClientSetNull);
            modelBuilder.Entity<onepage_plan>().
       HasOne(p => p.team_details).
     WithMany(d => d._oneplan).HasForeignKey(b => b.team).OnDelete(DeleteBehavior.ClientSetNull);
        }

    }
}

