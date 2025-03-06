using System.ComponentModel.DataAnnotations;

namespace ISB.Models
{
    public class employee
    {
        [Key]
        public int id { get; set; }
        public string salutation { get; set; }
        public string name { get; set; }
        public string email { get; set; }
        public string iamge { get; set; }
  
        public string mobile { get; set; }
        public string Dob { get; set; }
        public string gender { get; set; }
     
        public string joining_date { get; set; }
        public string address { get; set; }
        public string about { get; set; }
        public string password { get; set; }
        public int login_allowed { get; set; }
        public int email_Notification { get; set; }
        public string emplyoment_type { get; set; }
         public string marital_status { get; set; }
      
        public int role_id { get; set; }
        public int status{ get; set; }
        public roles roles_details { get; set; }
        public int depart_id { get; set; }
        public depatement department_details { get; set; }
        public int designationid { get; set; }
        public designation designation_details { get; set; }

        public List<survey> survey_details { get; set; }
        public List<project> project_details { get; set; }

        public List<task> tasks_details { get; set; }
        public List<sub_task> subtasks_details { get; set; }
        public List<Audit> audit_details { get; set; }

    }
}
