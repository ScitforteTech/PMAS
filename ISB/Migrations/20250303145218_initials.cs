using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ISB.Migrations
{
    /// <inheritdoc />
    public partial class initials : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "tbl_auditpriority",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    colour = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    status = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_auditpriority", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "tbl_auditTypes",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    colour = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    status = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_auditTypes", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "tbl_bugetaherence",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    colour = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    status = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_bugetaherence", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "tbl_clients",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    number = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    address = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_clients", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "tbl_CommunicationEffectiveness",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    colour = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    status = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_CommunicationEffectiveness", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "tbl_companies",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    website = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    address = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    city = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    country = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    status = table.Column<int>(type: "int", nullable: false),
                    Phonenumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    modules = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    logo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    user_name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    dateCurrent = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    user_invitations = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_companies", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "tbl_departments",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    department_name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_departments", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "tbl_emptypes",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    colour = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    status = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_emptypes", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "tbl_impact_risk",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    risk_name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    scoring = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_impact_risk", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "tbl_likedhood_risk",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    risk_name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    scoring = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_likedhood_risk", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "tbl_maritalstatus",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    colour = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    status = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_maritalstatus", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "tbl_project_types",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    project_typeName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    colour = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    status = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_project_types", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "tbl_projectpriority",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    colour = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    status = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_projectpriority", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "tbl_projectStatus",
                columns: table => new
                {
                    status_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name_status = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    status = table.Column<int>(type: "int", nullable: false),
                    colour = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_projectStatus", x => x.status_id);
                });

            migrationBuilder.CreateTable(
                name: "tbl_risk_types",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    color = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    status = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_risk_types", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "tbl_risks",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Risk_factor = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    risk_category = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    impact_risk = table.Column<int>(type: "int", nullable: false),
                    likelihood_risk = table.Column<int>(type: "int", nullable: false),
                    total_scoring = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_risks", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "tbl_roles",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    roles_name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_roles", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "tbl_ScheduleAdherence",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    colour = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    status = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_ScheduleAdherence", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "tbl_StakeholderEngagement",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    colour = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    status = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_StakeholderEngagement", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "tbl_taks_file",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    file_name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    date = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    task_ids = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_taks_file", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "tbl_task_status",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    colour = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    status = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_task_status", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "tbl_task_types",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    colour = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    status = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_task_types", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "tbl_taskpriority",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    colour = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    status = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_taskpriority", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "tbl_teams",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    team_name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    team_des = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    team_leader = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    team_member1 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    team_member2 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    team_member3 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    team_member4 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    team_member5 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    team_member6 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    status = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_teams", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "tbl_designations",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    designation_name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    colour_code = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    depart_id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_designations", x => x.id);
                    table.ForeignKey(
                        name: "FK_tbl_designations_tbl_departments_depart_id",
                        column: x => x.depart_id,
                        principalTable: "tbl_departments",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "tbl_employee",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    salutation = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    iamge = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    mobile = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Dob = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    gender = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    joining_date = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    address = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    about = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    login_allowed = table.Column<int>(type: "int", nullable: false),
                    email_Notification = table.Column<int>(type: "int", nullable: false),
                    emplyoment_type = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    marital_status = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    role_id = table.Column<int>(type: "int", nullable: false),
                    status = table.Column<int>(type: "int", nullable: false),
                    depart_id = table.Column<int>(type: "int", nullable: false),
                    designationid = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_employee", x => x.id);
                    table.ForeignKey(
                        name: "FK_tbl_employee_tbl_departments_depart_id",
                        column: x => x.depart_id,
                        principalTable: "tbl_departments",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_tbl_employee_tbl_designations_designationid",
                        column: x => x.designationid,
                        principalTable: "tbl_designations",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_tbl_employee_tbl_roles_role_id",
                        column: x => x.role_id,
                        principalTable: "tbl_roles",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "tbl_projects",
                columns: table => new
                {
                    Project_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    start_date = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    end_date = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    document = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    team_id = table.Column<int>(type: "int", nullable: false),
                    client_id = table.Column<int>(type: "int", nullable: false),
                    project_statuss = table.Column<int>(type: "int", nullable: false),
                    project_priority = table.Column<int>(type: "int", nullable: false),
                    project_types = table.Column<int>(type: "int", nullable: false),
                    teamid = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_projects", x => x.Project_id);
                    table.ForeignKey(
                        name: "FK_tbl_projects_tbl_clients_client_id",
                        column: x => x.client_id,
                        principalTable: "tbl_clients",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_tbl_projects_tbl_employee_team_id",
                        column: x => x.team_id,
                        principalTable: "tbl_employee",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_tbl_projects_tbl_projectStatus_project_statuss",
                        column: x => x.project_statuss,
                        principalTable: "tbl_projectStatus",
                        principalColumn: "status_id");
                    table.ForeignKey(
                        name: "FK_tbl_projects_tbl_project_types_project_types",
                        column: x => x.project_types,
                        principalTable: "tbl_project_types",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_tbl_projects_tbl_projectpriority_project_priority",
                        column: x => x.project_priority,
                        principalTable: "tbl_projectpriority",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_tbl_projects_tbl_teams_teamid",
                        column: x => x.teamid,
                        principalTable: "tbl_teams",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "tbl_survey",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    emp_id = table.Column<int>(type: "int", nullable: false),
                    subject = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    descriptions = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_survey", x => x.id);
                    table.ForeignKey(
                        name: "FK_tbl_survey_tbl_employee_emp_id",
                        column: x => x.emp_id,
                        principalTable: "tbl_employee",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "tbl_audit",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Tittle = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    project_id = table.Column<int>(type: "int", nullable: false),
                    task_id = table.Column<int>(type: "int", nullable: false),
                    Type_id = table.Column<int>(type: "int", nullable: false),
                    assignedauditor = table.Column<int>(type: "int", nullable: false),
                    AuditScope = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AuditObjectives = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Priority_id = table.Column<int>(type: "int", nullable: false),
                    Due_Date = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Comments = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Attachment = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_audit", x => x.id);
                    table.ForeignKey(
                        name: "FK_tbl_audit_tbl_auditTypes_Type_id",
                        column: x => x.Type_id,
                        principalTable: "tbl_auditTypes",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_tbl_audit_tbl_auditpriority_Priority_id",
                        column: x => x.Priority_id,
                        principalTable: "tbl_auditpriority",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_tbl_audit_tbl_employee_assignedauditor",
                        column: x => x.assignedauditor,
                        principalTable: "tbl_employee",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_tbl_audit_tbl_projects_project_id",
                        column: x => x.project_id,
                        principalTable: "tbl_projects",
                        principalColumn: "Project_id");
                });

            migrationBuilder.CreateTable(
                name: "tbl_auditProject",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    project_id = table.Column<int>(type: "int", nullable: false),
                    start_date = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    end_date = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    project_Status = table.Column<int>(type: "int", nullable: false),
                    buget_id = table.Column<int>(type: "int", nullable: false),
                    schedule_id = table.Column<int>(type: "int", nullable: false),
                    kips = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_auditProject", x => x.id);
                    table.ForeignKey(
                        name: "FK_tbl_auditProject_tbl_ScheduleAdherence_schedule_id",
                        column: x => x.schedule_id,
                        principalTable: "tbl_ScheduleAdherence",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_tbl_auditProject_tbl_bugetaherence_buget_id",
                        column: x => x.buget_id,
                        principalTable: "tbl_bugetaherence",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_tbl_auditProject_tbl_projectStatus_project_Status",
                        column: x => x.project_Status,
                        principalTable: "tbl_projectStatus",
                        principalColumn: "status_id");
                    table.ForeignKey(
                        name: "FK_tbl_auditProject_tbl_projects_project_id",
                        column: x => x.project_id,
                        principalTable: "tbl_projects",
                        principalColumn: "Project_id");
                });

            migrationBuilder.CreateTable(
                name: "tbl_tasks",
                columns: table => new
                {
                    task_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    created_date = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    updated_date = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    due_date = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    document = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    project_ = table.Column<int>(type: "int", nullable: true),
                    employee_ids = table.Column<int>(type: "int", nullable: false),
                    task_priority = table.Column<int>(type: "int", nullable: false),
                    task_statuss = table.Column<int>(type: "int", nullable: false),
                    task_types = table.Column<int>(type: "int", nullable: false),
                    riskid = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_tasks", x => x.task_id);
                    table.ForeignKey(
                        name: "FK_tbl_tasks_tbl_employee_employee_ids",
                        column: x => x.employee_ids,
                        principalTable: "tbl_employee",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_tbl_tasks_tbl_projects_project_",
                        column: x => x.project_,
                        principalTable: "tbl_projects",
                        principalColumn: "Project_id");
                    table.ForeignKey(
                        name: "FK_tbl_tasks_tbl_risks_riskid",
                        column: x => x.riskid,
                        principalTable: "tbl_risks",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_tbl_tasks_tbl_task_status_task_statuss",
                        column: x => x.task_statuss,
                        principalTable: "tbl_task_status",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_tbl_tasks_tbl_task_types_task_types",
                        column: x => x.task_types,
                        principalTable: "tbl_task_types",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_tbl_tasks_tbl_taskpriority_task_priority",
                        column: x => x.task_priority,
                        principalTable: "tbl_taskpriority",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "tbl_subtask",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    tittle = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    discription = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    files_upload = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    start_date = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    due_date = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    emp_id = table.Column<int>(type: "int", nullable: false),
                    risk_id = table.Column<int>(type: "int", nullable: false),
                    task_id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_subtask", x => x.id);
                    table.ForeignKey(
                        name: "FK_tbl_subtask_tbl_employee_emp_id",
                        column: x => x.emp_id,
                        principalTable: "tbl_employee",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_tbl_subtask_tbl_risks_risk_id",
                        column: x => x.risk_id,
                        principalTable: "tbl_risks",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_tbl_subtask_tbl_tasks_task_id",
                        column: x => x.task_id,
                        principalTable: "tbl_tasks",
                        principalColumn: "task_id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_tbl_audit_assignedauditor",
                table: "tbl_audit",
                column: "assignedauditor");

            migrationBuilder.CreateIndex(
                name: "IX_tbl_audit_Priority_id",
                table: "tbl_audit",
                column: "Priority_id");

            migrationBuilder.CreateIndex(
                name: "IX_tbl_audit_project_id",
                table: "tbl_audit",
                column: "project_id");

            migrationBuilder.CreateIndex(
                name: "IX_tbl_audit_Type_id",
                table: "tbl_audit",
                column: "Type_id");

            migrationBuilder.CreateIndex(
                name: "IX_tbl_auditProject_buget_id",
                table: "tbl_auditProject",
                column: "buget_id");

            migrationBuilder.CreateIndex(
                name: "IX_tbl_auditProject_project_id",
                table: "tbl_auditProject",
                column: "project_id");

            migrationBuilder.CreateIndex(
                name: "IX_tbl_auditProject_project_Status",
                table: "tbl_auditProject",
                column: "project_Status");

            migrationBuilder.CreateIndex(
                name: "IX_tbl_auditProject_schedule_id",
                table: "tbl_auditProject",
                column: "schedule_id");

            migrationBuilder.CreateIndex(
                name: "IX_tbl_designations_depart_id",
                table: "tbl_designations",
                column: "depart_id");

            migrationBuilder.CreateIndex(
                name: "IX_tbl_employee_depart_id",
                table: "tbl_employee",
                column: "depart_id");

            migrationBuilder.CreateIndex(
                name: "IX_tbl_employee_designationid",
                table: "tbl_employee",
                column: "designationid");

            migrationBuilder.CreateIndex(
                name: "IX_tbl_employee_role_id",
                table: "tbl_employee",
                column: "role_id");

            migrationBuilder.CreateIndex(
                name: "IX_tbl_projects_client_id",
                table: "tbl_projects",
                column: "client_id");

            migrationBuilder.CreateIndex(
                name: "IX_tbl_projects_project_priority",
                table: "tbl_projects",
                column: "project_priority");

            migrationBuilder.CreateIndex(
                name: "IX_tbl_projects_project_statuss",
                table: "tbl_projects",
                column: "project_statuss");

            migrationBuilder.CreateIndex(
                name: "IX_tbl_projects_project_types",
                table: "tbl_projects",
                column: "project_types");

            migrationBuilder.CreateIndex(
                name: "IX_tbl_projects_team_id",
                table: "tbl_projects",
                column: "team_id");

            migrationBuilder.CreateIndex(
                name: "IX_tbl_projects_teamid",
                table: "tbl_projects",
                column: "teamid");

            migrationBuilder.CreateIndex(
                name: "IX_tbl_subtask_emp_id",
                table: "tbl_subtask",
                column: "emp_id");

            migrationBuilder.CreateIndex(
                name: "IX_tbl_subtask_risk_id",
                table: "tbl_subtask",
                column: "risk_id");

            migrationBuilder.CreateIndex(
                name: "IX_tbl_subtask_task_id",
                table: "tbl_subtask",
                column: "task_id");

            migrationBuilder.CreateIndex(
                name: "IX_tbl_survey_emp_id",
                table: "tbl_survey",
                column: "emp_id");

            migrationBuilder.CreateIndex(
                name: "IX_tbl_tasks_employee_ids",
                table: "tbl_tasks",
                column: "employee_ids");

            migrationBuilder.CreateIndex(
                name: "IX_tbl_tasks_project_",
                table: "tbl_tasks",
                column: "project_");

            migrationBuilder.CreateIndex(
                name: "IX_tbl_tasks_riskid",
                table: "tbl_tasks",
                column: "riskid");

            migrationBuilder.CreateIndex(
                name: "IX_tbl_tasks_task_priority",
                table: "tbl_tasks",
                column: "task_priority");

            migrationBuilder.CreateIndex(
                name: "IX_tbl_tasks_task_statuss",
                table: "tbl_tasks",
                column: "task_statuss");

            migrationBuilder.CreateIndex(
                name: "IX_tbl_tasks_task_types",
                table: "tbl_tasks",
                column: "task_types");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tbl_audit");

            migrationBuilder.DropTable(
                name: "tbl_auditProject");

            migrationBuilder.DropTable(
                name: "tbl_CommunicationEffectiveness");

            migrationBuilder.DropTable(
                name: "tbl_companies");

            migrationBuilder.DropTable(
                name: "tbl_emptypes");

            migrationBuilder.DropTable(
                name: "tbl_impact_risk");

            migrationBuilder.DropTable(
                name: "tbl_likedhood_risk");

            migrationBuilder.DropTable(
                name: "tbl_maritalstatus");

            migrationBuilder.DropTable(
                name: "tbl_risk_types");

            migrationBuilder.DropTable(
                name: "tbl_StakeholderEngagement");

            migrationBuilder.DropTable(
                name: "tbl_subtask");

            migrationBuilder.DropTable(
                name: "tbl_survey");

            migrationBuilder.DropTable(
                name: "tbl_taks_file");

            migrationBuilder.DropTable(
                name: "tbl_auditTypes");

            migrationBuilder.DropTable(
                name: "tbl_auditpriority");

            migrationBuilder.DropTable(
                name: "tbl_ScheduleAdherence");

            migrationBuilder.DropTable(
                name: "tbl_bugetaherence");

            migrationBuilder.DropTable(
                name: "tbl_tasks");

            migrationBuilder.DropTable(
                name: "tbl_projects");

            migrationBuilder.DropTable(
                name: "tbl_risks");

            migrationBuilder.DropTable(
                name: "tbl_task_status");

            migrationBuilder.DropTable(
                name: "tbl_task_types");

            migrationBuilder.DropTable(
                name: "tbl_taskpriority");

            migrationBuilder.DropTable(
                name: "tbl_clients");

            migrationBuilder.DropTable(
                name: "tbl_employee");

            migrationBuilder.DropTable(
                name: "tbl_projectStatus");

            migrationBuilder.DropTable(
                name: "tbl_project_types");

            migrationBuilder.DropTable(
                name: "tbl_projectpriority");

            migrationBuilder.DropTable(
                name: "tbl_teams");

            migrationBuilder.DropTable(
                name: "tbl_designations");

            migrationBuilder.DropTable(
                name: "tbl_roles");

            migrationBuilder.DropTable(
                name: "tbl_departments");
        }
    }
}
