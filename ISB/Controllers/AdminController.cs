using System;
using System.Data;
using System.Net.Mail;
using System.Net.NetworkInformation;
using System.Reflection.Metadata;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Xml.Linq;

using ISB.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using SelectPdf;
using static System.Runtime.InteropServices.JavaScript.JSType;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace ISB.Controllers
{

    public class AdminController : Controller
    {
        private readonly dbcontext _con;
        private readonly IWebHostEnvironment _evn;

        public AdminController(dbcontext con, IWebHostEnvironment evn)
        {
            _con = con;
            _evn = evn;
        }
        public IActionResult Index()
        {
            var login = HttpContext.Session.GetString("user_session");
            if (login != null)
            {
                var logined_user = _con.tbl_employee.Where(p => p.id == int.Parse(login)).ToList();
                mainModel data = new mainModel()
                {
                    logined_user = logined_user,
                };
                return View(data);
            }
            else
            {
                return RedirectToAction("login_form");
            }


        }

        public IActionResult companyCreatetion()
        {
            var login = HttpContext.Session.GetString("user_session");
            if (login != null)
            {
                var logined_user = _con.tbl_employee.Where(p => p.id == int.Parse(login)).ToList();
                mainModel data = new mainModel()
                {
                    logined_user = logined_user,
                };
                return View(data);
            }
            else
            {
                return RedirectToAction("login_form");
            }
        }


        [HttpPost]
        public IActionResult companyCreatetion(Companies company, IFormFile logo)
        {
            var name_existing = _con.tbl_companies.FirstOrDefault(p => p.name == company.name);
            if (name_existing == null)
            {

                string file_extension = Path.GetExtension(logo.FileName);
                if (file_extension == ".png" || file_extension == ".jpg" || file_extension == ".jpeg")
                {
                    Random ran = new Random();
                    var currentDate = DateTime.Now;

                    string fileNAme = Path.GetFileName(logo.FileName);
                    var R_num = ran.Next(0000, 9999).ToString();
                    var name = fileNAme = R_num;
                    string file_name = name + file_extension;
                    string filepath = Path.Combine(_evn.WebRootPath, "compnanies_logo", file_name);
                    FileStream fs = new FileStream(filepath, FileMode.Create);
                    logo.CopyTo(fs);
                    company.logo = file_name;
                    company.dateCurrent = currentDate.ToString("yyyy-MM-dd");
                    _con.tbl_companies.Add(company);
                    _con.SaveChanges();
                    TempData["msg1"] = "The company is created now !";

                    return RedirectToAction("companyCreatetion");
                }
                else
                {
                    TempData["msg1"] = "This type of file is not supported !";
                    return RedirectToAction("companyCreatetion");
                }

            }
            else
            {
                TempData["msg1"] = "the name of company is already regsitered!";
                return RedirectToAction("companyCreatetion");
            }

        }

        [HttpGet]
        public IActionResult companies_detail(string search_text)
        {
            var login = HttpContext.Session.GetString("user_session");
            if (login != null)
            {
                var logined_user = _con.tbl_employee.Where(p => p.id == int.Parse(login)).ToList();
                List<Companies> company = new List<Companies>();
                if (string.IsNullOrEmpty(search_text))
                {
                    company = _con.tbl_companies.ToList();
                }

                else
                {
                    company = _con.tbl_companies.FromSqlInterpolated($"select * from tbl_companies where name like '%'+ {search_text}+ '%'").ToList();
                }
                if (company.Count == 0)
                {
                    TempData["msg"] = $"sorry the record is not found {search_text} ";
                }
                mainModel data = new mainModel() { companies_details = company, logined_user = logined_user, };
                return View(data);
            }
            else
            {
                return RedirectToAction("login_form");
            }
        }
        public IActionResult delete_comapny(int id)
        {
            var data = _con.tbl_companies.Find(id);
            _con.tbl_companies.Remove(data);
            _con.SaveChanges();
            return RedirectToAction("companies_detail");
        }
        public IActionResult companies_data(int id)
        {
            var login = HttpContext.Session.GetString("user_session");
            if (login != null)
            {
                var company = _con.tbl_companies.Where(p => p.id == id).ToList();
                var logined_user = _con.tbl_employee.Where(p => p.id == int.Parse(login)).ToList();
                mainModel data = new mainModel()
                {
                    companies_details = company,
                    logined_user = logined_user,
                };
                return View(data);
            }
            else
            {
                return RedirectToAction("login_form");
            }



        }


        public IActionResult status_comapny(int id)
        {
            var data = _con.tbl_companies.Find(id);
            if (data.status == 1)
            {
                data.status = 0;
            }
            else
            {
                data.status = 1;
            }
            _con.SaveChanges();
            return RedirectToAction("companies_detail");
        }
        public IActionResult update_comapny(int id)
        {
            var login = HttpContext.Session.GetString("user_session");
            if (login != null)
            {
                var company = _con.tbl_companies.Find(id);
                var logined_user = _con.tbl_employee.Where(p => p.id == int.Parse(login)).ToList();
                mainModel data = new mainModel()
                {
                    company_data = company,
                    logined_user = logined_user,
                };
                return View(data);
            }
            else
            {
                return RedirectToAction("login_form");
            }

        }
        [HttpPost]
        public IActionResult update_comapny(Companies company, IFormFile logo)
        {
            string file_extension = Path.GetExtension(logo.FileName);
            if (file_extension == ".png" || file_extension == ".jpg" || file_extension == ".jpeg")
            {
                Random ran = new Random();
                string fileNAme = Path.GetFileName(logo.FileName);
                var R_num = ran.Next(0000, 9999).ToString();
                var name = fileNAme = R_num;
                string file_name = name + file_extension;
                string filepath = Path.Combine(_evn.WebRootPath, "compnanies_logo", file_name);
                FileStream fs = new FileStream(filepath, FileMode.Create);
                logo.CopyTo(fs);
                company.modules = User.FindFirstValue(ClaimTypes.NameIdentifier); // Example for getting user ID

                company.logo = file_name;

                _con.tbl_companies.Update(company);
                _con.SaveChanges();
                TempData["msg"] = "The company is created now !";

                return RedirectToAction("companies_detail");
            }
            else
            {
                TempData["msg"] = "This type of file is not supported !";
                return RedirectToAction("companies_detail");
            }
        }
        [HttpGet]
        public IActionResult roles_details(string search_text)
        {
            var login = HttpContext.Session.GetString("user_session");
            if (login != null)
            {
                var logined_user = _con.tbl_employee.Where(p => p.id == int.Parse(login)).ToList();
                List<roles> roles = new List<roles>();
                if (string.IsNullOrEmpty(search_text))
                {
                    roles = _con.tbl_roles.ToList();
                }

                else
                {
                    roles = _con.tbl_roles.FromSqlInterpolated($"select * from tbl_roles where roles_name like '%'+ {search_text}+ '%'").ToList();
                }
                if (roles.Count == 0)
                {
                    TempData["msg"] = $"sorry the record is not found {search_text} ";
                }
                mainModel data = new mainModel()
                {
                    role_detail = roles,
                    logined_user = logined_user
                };
                return View(data);
            }
            else
            {
                return RedirectToAction("login_form");
            }
        }
        [HttpPost]
        public IActionResult add_role(roles role)
        {
            var role_exiting = _con.tbl_roles.FirstOrDefault(p => p.roles_name == role.roles_name);
            if (role_exiting == null)
            {
                _con.tbl_roles.Add(role);
                _con.SaveChanges();
                TempData["msg1"] = "The role is created successfully !";
                return RedirectToAction("roles_details");

            }
            else
            {

                TempData["msg1"] = "The role is already added!";
                return RedirectToAction("roles_details");
            }
        }
        public IActionResult delete_roles(int id)
        {
            var del = _con.tbl_roles.Find(id);
            _con.tbl_roles.Remove(del);
            _con.SaveChanges();
            TempData["msg1"] = "The role has been successfully deleted!";
            return RedirectToAction("roles_details");
        }
        public IActionResult update_roles(int id)
        {
            var login = HttpContext.Session.GetString("user_session");
            if (login != null)
            {
                var find_id = _con.tbl_roles.Find(id);
                var logined_user = _con.tbl_employee.Where(p => p.id == int.Parse(login)).ToList();
                mainModel data = new mainModel()
                {
                    roles_data = find_id,
                    logined_user = logined_user,
                };
                return View(data);
            }
            else
            {
                return RedirectToAction("login_form");
            }

        }
        [HttpPost]
        public IActionResult update_roles(roles role)
        {

            var role_exiting = _con.tbl_roles.FirstOrDefault(p => p.roles_name == role.roles_name);
            if (role_exiting == null)
            {
                _con.tbl_roles.Update(role);
                _con.SaveChanges();
                TempData["msg1"] = "The role is updated successfully !";
                return RedirectToAction("roles_details");

            }
            else
            {

                TempData["msg1"] = "The role is already added!";
                return RedirectToAction("roles_details");
            }
        }

        [HttpGet]
        public IActionResult departments_details(string search_text)
        {
            var login = HttpContext.Session.GetString("user_session");
            if (login != null)
            {
                var logined_user = _con.tbl_employee.Where(p => p.id == int.Parse(login)).ToList();
                List<depatement> depatements = new List<depatement>();
                if (string.IsNullOrEmpty(search_text))
                {
                    depatements = _con.tbl_departments.ToList();
                }

                else
                {
                    depatements = _con.tbl_departments.FromSqlInterpolated($"select * from tbl_departments where department_name like '%'+ {search_text}+ '%'").ToList();
                }
                if (depatements.Count == 0)
                {
                    TempData["msg"] = $"sorry the record is not found {search_text} ";
                }
                mainModel data = new mainModel() { department_details = depatements, logined_user = logined_user };
                return View(data);
            }
            else
            {
                return RedirectToAction("login_form");
            }
        }
        public IActionResult view_design(int id)
        {
            var login = HttpContext.Session.GetString("user_session");
            if (login != null)
            {
                var logined_user = _con.tbl_employee.Where(p => p.id == int.Parse(login)).ToList();
                var find_id = _con.tbl_designations.Where(p => p.depart_id == id).Include(p => p.depart_detail).ToList();
                if (find_id.Count == 0)
                {
                    TempData["msg"] = "there is no designation is appointed to this department";
                }
                mainModel data = new mainModel() { design_details = find_id, logined_user = logined_user };
                return View(data);

            }
            else
            {
                return RedirectToAction("login_form");
            }

        }
        public IActionResult update_design(int id)
        {
            var login = HttpContext.Session.GetString("user_session");
            if (login != null)
            {
                var logined_user = _con.tbl_employee.Where(p => p.id == int.Parse(login)).ToList();
                var find_id = _con.tbl_designations.Find(id);
                var department_list = _con.tbl_departments.ToList();
                mainModel data = new mainModel()
                {
                    design_data = find_id,
                    department_details = department_list,
                    logined_user = logined_user,
                };
                return View(data);
            }
            else
            {
                return RedirectToAction("login_form");
            }



        }
        [HttpPost]
        public IActionResult update_design(designation design)
        {
            _con.tbl_designations.Update(design);
            _con.SaveChanges();
            TempData["msg1"] = "The designation is update successfully !";
            return RedirectToAction("departments_details");
        }
        [HttpPost]
        public IActionResult add_department(depatement depart)
        {
            var depart_exiting = _con.tbl_departments.FirstOrDefault(p => p.department_name == depart.department_name);
            if (depart_exiting == null)
            {
                _con.tbl_departments.Add(depart);
                _con.SaveChanges();
                TempData["msg1"] = "The department is created successfully !";
                return RedirectToAction("departments_details");

            }
            else
            {

                TempData["msg1"] = "The department is already added!";
                return RedirectToAction("departments_details");
            }
        }
        public IActionResult delete_depart(int id)
        {
            var del = _con.tbl_departments.Find(id);
            _con.tbl_departments.Remove(del);
            _con.SaveChanges();
            TempData["msg1"] = "The department has been successfully deleted!";
            return RedirectToAction("departments_details");
        }
        public IActionResult delete_design(int id)
        {
            var del = _con.tbl_designations.Find(id);
            _con.tbl_designations.Remove(del);
            _con.SaveChanges();
            TempData["msg1"] = "The department has been successfully deleted!";
            return RedirectToAction("departments_details");
        }
        public IActionResult update_depart(int id)
        {
            var login = HttpContext.Session.GetString("user_session");
            if (login != null)
            {
                var find_id = _con.tbl_departments.Find(id);
                var logined_user = _con.tbl_employee.Where(p => p.id == int.Parse(login)).ToList();
                mainModel data = new mainModel()
                {
                    department_data = find_id,
                    logined_user = logined_user,
                };
                return View(data);
            }
            else
            {
                return RedirectToAction("login_form");
            }

        }
        [HttpPost]
        public IActionResult update_depart(depatement depart)
        {
            var depart_exiting = _con.tbl_departments.FirstOrDefault(p => p.department_name == depart.department_name);
            if (depart_exiting == null)
            {
                _con.tbl_departments.Update(depart);
                _con.SaveChanges();
                TempData["msg1"] = "The department is updated successfully !";
                return RedirectToAction("departments_details");

            }
            else
            {

                TempData["msg1"] = "The department is already added!";
                return RedirectToAction("departments_details");
            }
        }
        [HttpPost]
        public IActionResult add_designation(designation design)
        {
            var design_exiting = _con.tbl_designations.FirstOrDefault(p => p.designation_name == design.designation_name);
            if (design_exiting == null)
            {
                _con.tbl_designations.Add(design);
                _con.SaveChanges();
                TempData["msg1"] = "The designation is updated successfully !";
                return RedirectToAction("departments_details");

            }
            else
            {

                TempData["msg1"] = "The designation is already added!";
                return RedirectToAction("departments_details");
            }

        }




        [HttpGet]
        public IActionResult employee_details(string search_text)
        {


            var login = HttpContext.Session.GetString("user_session");
            if (login != null)
            {
                var logined_user = _con.tbl_employee.Where(p => p.id == int.Parse(login)).ToList();


                List<employee> emp_data = new List<employee>();
                if (string.IsNullOrEmpty(search_text))
                {
                    emp_data = _con.tbl_employee.ToList();
                }

                else
                {
                    emp_data = _con.tbl_employee.FromSqlInterpolated($"select * from tbl_employee where name like '%'+ {search_text}+ '%' or email like '%'+ {search_text}+ '%'").ToList();
                }
                if (emp_data.Count == 0)
                {
                    TempData["msg"] = $"sorry the record is not found {search_text} ";
                }
                var designation = _con.tbl_designations.ToList();
                var department = _con.tbl_departments.ToList();
                var roles = _con.tbl_roles.ToList();
                var material_status = _con.tbl_maritalstatus.Where(p => p.status == 1).ToList();
                var _type = _con.tbl_emptypes.Where(p => p.status == 1).ToList();

                mainModel data = new mainModel()
                {

                    emp_types_details = _type,
                    employee_details = emp_data,
                    role_detail = roles,
                    logined_user = logined_user,
                    materialStatus_details = material_status,
                    department_details = department,
                    design_details = designation,

                };
                return View(data);
            }
            else
            {
                return RedirectToAction("login_form");
            }
        }

        [HttpPost]
        public IActionResult add_emp(employee emp, IFormFile iamge, int length)
        {
            // Check if the email already exists
            var existingEmployee = _con.tbl_employee.FirstOrDefault(p => p.email == emp.email);

            if (existingEmployee == null)
            {
                // Validate the image file extension
                string fileExtension = Path.GetExtension(iamge.FileName);
                if (fileExtension == ".png" || fileExtension == ".jpg" || fileExtension == ".jpeg")
                {
                    // Generate a unique file name
                    string fileName = $"{Guid.NewGuid()}{fileExtension}"; // Use GUID for uniqueness
                    string filePath = Path.Combine(_evn.WebRootPath, "emp_images", fileName);

                    // Save the image file
                    using (FileStream fs = new FileStream(filePath, FileMode.Create))
                    {
                        fs.CopyTo(fs);
                    }
                    string pass = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890";
                    Random rd = new Random();
                    char[] mypass = new char[7];
                    for (int i = 0; i < 7; i++)
                    {
                        mypass[i] = pass[(int)(35 * rd.NextDouble())];
                    }
                    string mypassString = new string(mypass);
                    emp.password = mypassString;



                    emp.iamge = fileName; // Save the file name in the employee record
                    emp.status = 1;
                    // Add the employee to the database
                    _con.tbl_employee.Add(emp);
                    _con.SaveChanges();

                    TempData["msg1"] = "The employee has been successfully added!";
                    return RedirectToAction("EmployeeDetails");
                }
                else
                {
                    TempData["msg1"] = "This type of file is not supported!";
                    return RedirectToAction("EmployeeDetails");
                }
            }
            else
            {
                TempData["msg1"] = "An employee with this email already exists!";
                return RedirectToAction("EmployeeDetails");
            }
        }
        public IActionResult login_form()
        {
            return View();
        }
        [HttpPost]
        public IActionResult login_form(string logined_email, string logined_pass)
        {
            var user_logined = _con.tbl_employee.FirstOrDefault(e => e.email == logined_email);
            if (user_logined != null && user_logined.password == logined_pass)
            {
                if (user_logined.status == 1)
                {
                    HttpContext.Session.SetString("user_session", user_logined.id.ToString());
                    return RedirectToAction("Index");
                }
                else
                {
                    TempData["logined"] = "The account is block by the admin";
                    return RedirectToAction("login_form");
                }
            }
            else
            {
                TempData["logined"] = "The email or password is block by the admin";
                return RedirectToAction("login_form");
            }

        }

        public IActionResult emp_data(int id)
        {
            var login = HttpContext.Session.GetString("user_session");
            if (login != null)
            {
                var logined_user = _con.tbl_employee.Where(p => p.id == int.Parse(login)).ToList();
                var find_id = _con.tbl_employee.Where(p => p.id == id).ToList();
                var data_emp = _con.tbl_employee.FirstOrDefault(p => p.id == id);
                var roles_item = _con.tbl_roles.Where(p => p.id == data_emp.role_id).ToList();
                var depart_item = _con.tbl_departments.Where(p => p.id == data_emp.depart_id).ToList();
                var desgni_item = _con.tbl_designations.Where(p => p.id == data_emp.designationid).ToList();
                mainModel data = new mainModel()
                {
                    logined_user = logined_user,
                    employee_details = find_id,
                    role_detail = roles_item,
                    department_details = depart_item,
                    design_details = desgni_item

                };
                return View(data);
            }
            else
            {
                return RedirectToAction("login_form");
            }


        }
        public IActionResult status_emp(int id)
        {
            var data = _con.tbl_employee.Find(id);
            if (data.status == 1)
            {
                data.status = 0;
            }
            else
            {
                data.status = 1;
            }
            _con.SaveChanges();
            return RedirectToAction("employee_details");
        }
        public IActionResult delete_emp(int id)
        {
            var del = _con.tbl_employee.Find(id);
            _con.tbl_employee.Remove(del);
            _con.SaveChanges();
            TempData["msg1"] = "The employee has been successfully deleted!";
            return RedirectToAction("employee_details");
        }
        public IActionResult update_emp(int id)
        {
            var login = HttpContext.Session.GetString("user_session");
            if (login != null)
            {
                var logined_user = _con.tbl_employee.Where(p => p.id == int.Parse(login)).ToList();
                var find_id = _con.tbl_employee.Find(id);
                var data_emp = _con.tbl_employee.FirstOrDefault(p => p.id == id);
                var roles_item = _con.tbl_roles.ToList();
                var selected_role = _con.tbl_roles.FirstOrDefault(r => r.id == find_id.role_id);
                var depart_item = _con.tbl_departments.ToList();
                var selected_depart = _con.tbl_departments.FirstOrDefault(r => r.id == find_id.depart_id);
                var desgni_item = _con.tbl_designations.ToList();
                var selected_design = _con.tbl_designations.FirstOrDefault(r => r.id == find_id.designationid);
                var material_status = _con.tbl_maritalstatus.Where(p => p.status == 1).ToList();
                var _type = _con.tbl_emptypes.Where(p => p.status == 1).ToList();

                mainModel data = new mainModel()
                {
                    logined_user = logined_user,
                    design_data = selected_design,
                    emp_types_details = _type,
                    department_data = selected_depart,
                    roles_data = selected_role,

                    materialStatus_details = material_status,
                    employee_data = find_id,
                    role_detail = roles_item,
                    department_details = depart_item,
                    design_details = desgni_item

                };
                return View(data);
            }
            else
            {

                return RedirectToAction("login_form");
            }
        }
        [HttpPost]
        public IActionResult update_emp(employee emp, IFormFile iamge)
        {
            string file_extension = Path.GetExtension(iamge.FileName);
            if (file_extension == ".png" || file_extension == ".jpg" || file_extension == ".jpeg")
            {
                Random ran = new Random();
                string fileNAme = Path.GetFileName(iamge.FileName);
                var R_num = ran.Next(0000, 9999).ToString();
                var name = fileNAme = R_num;
                string file_name = name + file_extension;
                string filepath = Path.Combine(_evn.WebRootPath, "emp_images", file_name);
                FileStream fs = new FileStream(filepath, FileMode.Create);
                iamge.CopyTo(fs);
                //company.modules = User.FindFirstValue(ClaimTypes.NameIdentifier); // Example for getting user ID
                emp.status = 1;
                emp.iamge = file_name;

                _con.tbl_employee.Add(emp);
                _con.SaveChanges();
                TempData["msg1"] = "The employee is successfully updated now !";

                return RedirectToAction("employee_details");
            }
            else
            {
                TempData["msg1"] = "This type of file is not supported !";
                return RedirectToAction("employee_details");
            }
        }
        /*for the client*/
        [HttpGet]
        public IActionResult client_details(string search_text)
        {
            List<client> client_data = new List<client>();
            if (string.IsNullOrEmpty(search_text))
            {
                client_data = _con.tbl_clients.ToList();
            }

            else
            {
                client_data = _con.tbl_clients.FromSqlInterpolated($"select * from tbl_clients where name like '%'+ {search_text}+ '%' or email like '%'+ {search_text}+ '%'").ToList();
            }
            if (client_data.Count == 0)
            {
                TempData["msg"] = $"sorry the record is not found {search_text} ";
            }

            mainModel data = new mainModel()
            {
                client_details = client_data

            };
            return View(data);
        }
        [HttpPost]
        public IActionResult add_client(client _clinet)
        {
            var number_exisiting = _con.tbl_clients.FirstOrDefault(p => p.number == _clinet.number);
            if (number_exisiting == null)
            {
                _con.tbl_clients.Add(_clinet);
                _con.SaveChanges();
                TempData["msg1"] = "The client is successfully added ";
                return RedirectToAction("client_details");
            }
            else
            {
                TempData["msg1"] = "The client is  added  with this number " + " " + _clinet.number;
                return RedirectToAction("client_details");
            }


        }
        public IActionResult delete_client(int id)
        {
            var del = _con.tbl_clients.Find(id);
            _con.tbl_clients.Remove(del);
            _con.SaveChanges();
            TempData["msg1"] = "The Client has been successfully deleted!";
            return RedirectToAction("client_details");
        }
        public IActionResult update_client(int id)
        {
            var clintdata = _con.tbl_clients.Find(id);

            mainModel data = new mainModel()
            {
                client_data = clintdata

            };
            return View(data);
        }
        [HttpPost]
        public IActionResult update_client(client _client)
        {
            _con.tbl_clients.Update(_client);
            _con.SaveChanges();
            TempData["msg1"] = "The Client has been successfully updated!";
            return RedirectToAction("client_details");
        }
        [HttpGet]
        public IActionResult team_details(string search_text)
        {
            var login = HttpContext.Session.GetString("user_session");
            if (login != null)
            {
                var logined_user = _con.tbl_employee.Where(p => p.id == int.Parse(login)).ToList();


                List<team> team_data = new List<team>();
                if (string.IsNullOrEmpty(search_text))
                {
                    team_data = _con.tbl_teams.ToList();
                }

                else
                {
                    team_data = _con.tbl_teams.FromSqlInterpolated($"select * from tbl_teams where team_name like '%'+ {search_text}+ '%'").ToList();
                }
                if (team_data.Count == 0)
                {
                    TempData["msg"] = $"sorry the record is not found {search_text} ";
                }
                var employee = _con.tbl_employee.ToList();
                mainModel data = new mainModel()
                {
                    employee_details = employee,
                    team_details = team_data
                };
                return View(data);
            }
            else
            {
                return RedirectToAction("login_form");
            }
        }
        [HttpPost]
        public IActionResult add_team(team _team)
        {
            _con.tbl_teams.Add(_team);
            _con.SaveChanges();
            TempData["msg1"] = "The team has been successfully created!";
            return RedirectToAction("team_details");
        }
        public IActionResult delete_team(int id)
        {
            var del = _con.tbl_teams.Find(id);
            _con.tbl_teams.Remove(del);
            _con.SaveChanges();
            TempData["msg1"] = "The team has been successfully deleted!";
            return RedirectToAction("team_details");
        }
        public IActionResult update_teamstatus(int id)
        {

            var data = _con.tbl_teams.Find(id);
            if (data.status == 1)
            {
                data.status = 0;
            }
            else
            {
                data.status = 1;
            }
            _con.SaveChanges();
            TempData["msg1"] = "The status of team has been successfully updated!";
            return RedirectToAction("team_details");
        }
        public IActionResult update_team(int id)
        {
            var login = HttpContext.Session.GetString("user_session");
            if (login != null)
            {
                var logined_user = _con.tbl_employee.Where(p => p.id == int.Parse(login)).ToList();
                var teamdata = _con.tbl_teams.Find(id);
                var employee = _con.tbl_employee.ToList();
                mainModel data = new mainModel()
                {
                    team_data = teamdata,
                    employee_details = employee,
                };
                return View(data);
            }
            else
            {
                return RedirectToAction("login_form");
            }


        }
        [HttpPost]
        public IActionResult update_team(team _team)
        {
            _con.tbl_teams.Update(_team);
            _con.SaveChanges();
            TempData["msg1"] = "The  team has been successfully updated!";
            return RedirectToAction("team_details");
        }
        [HttpGet]
        public IActionResult project_details(string search_text)
        {
            var login = HttpContext.Session.GetString("user_session");
            if (login != null)
            {
                var logined_user = _con.tbl_employee.Where(p => p.id == int.Parse(login)).ToList();

                List<project> pro_data = new List<project>();
                if (string.IsNullOrEmpty(search_text))
                {
                    pro_data = _con.tbl_projects.Include(p => p.employee_details).Include(p => p.status_details).Where(p => p.plan_id == 0).ToList();
                }

                else
                {
                    pro_data = _con.tbl_projects.FromSqlInterpolated($"select * from tbl_teams where team_name like '%'+ {search_text}+ '%'").Include(p => p.status_details).Where(p => p.plan_id == 0).ToList();
                }
                if (pro_data.Count == 0)
                {
                    TempData["msg"] = $"sorry the record is not found {search_text} ";
                }
                var team_details = _con.tbl_teams.Where(p => p.status == 1).ToList();
                var emp_data = _con.tbl_employee.Where(p => p.status == 1).ToList();
                var client_details = _con.tbl_clients.ToList();
                var status = _con.tbl_projectStatus.Where(p => p.status == 1).ToList();
                var priority = _con.tbl_projectpriority.Where(p => p.status == 1).ToList();
                var type = _con.tbl_project_types.Where(p => p.status == 1).ToList();

                mainModel data = new mainModel()
                {
                    logined_user = logined_user,
                    projects_types = type,
                    projects_prioritesDetails = priority,
                    pro_Statusdetail = status,
                    project_details = pro_data,
                    employee_details = emp_data,
                    team_details = team_details,
                    client_details = client_details,
                };
                return View(data);
            }
            else
            {
                return RedirectToAction("login_form");
            }
        }
        [HttpPost]

        public IActionResult add_project(project pro, IFormFile document)
        {
            if (document != null) // Check if a file was actually uploaded
            {
                string file_extension = Path.GetExtension(document.FileName);
                if (file_extension == ".pdf" || file_extension == ".docx" || file_extension == ".txt")
                {
                    Random ran = new Random();
                    string fileNAme = Path.GetFileName(document.FileName);
                    var R_num = ran.Next(0000, 9999).ToString();
                    var name = fileNAme = R_num; // This line seems redundant; you're re-assigning fileNAme
                    string file_name = name + file_extension;
                    string filepath = Path.Combine(_evn.WebRootPath, "documents", file_name);
                    using (FileStream fs = new FileStream(filepath, FileMode.Create)) // Use 'using' for proper disposal
                    {
                        document.CopyTo(fs);
                    }

                    pro.document = file_name; // Store the path in the project object
                }
                else
                {
                    TempData["msg1"] = "This type of file is not supported !";
                    return RedirectToAction("project_details");
                }
            }
            // If document is null, the code will skip the file processing block.  pro object will be saved without file info.
            pro.plan_id = 0;
            _con.tbl_projects.Add(pro);
            _con.SaveChanges();
            TempData["msg1"] = "The project is successfully updated now !";

            return RedirectToAction("project_details");

        }
        public IActionResult delete_project(int id)
        {
            var data = _con.tbl_projects.Find(id);
            _con.tbl_projects.Remove(data);
            _con.SaveChanges();
            return RedirectToAction("project_details");
        }
        //public IActionResult update_prostatus(int id)
        //{


        //}
        public IActionResult project_status()
        {
            var login = HttpContext.Session.GetString("user_session");
            if (login != null)
            {
                var status = _con.tbl_projectStatus.ToList();
                var logined_user = _con.tbl_employee.Where(p => p.id == int.Parse(login)).ToList();
                mainModel data = new mainModel()
                {
                    pro_Statusdetail = status,
                    logined_user = logined_user,
                };
                return View(data);
            }
            else
            {
                return RedirectToAction("login_form");
            }




        }
        [HttpPost]
        public IActionResult add_projectStatus(project_status _status)
        {
            _con.tbl_projectStatus.Add(_status);

            _con.SaveChanges();
            TempData["msg1"] = "The project Status is successfully added!";
            return RedirectToAction("project_status");

        }
        public IActionResult delete_statusProj(int id)
        {
            var data = _con.tbl_projectStatus.Find(id);
            _con.tbl_projectStatus.Remove(data);
            _con.SaveChanges();
            return RedirectToAction("project_status");

        }
        public IActionResult update_statusProj(int id)
        {
            var login = HttpContext.Session.GetString("user_session");
            if (login != null)
            {
                var find_id = _con.tbl_projectStatus.Find(id);
                var logined_user = _con.tbl_employee.Where(p => p.id == int.Parse(login)).ToList();
                mainModel data = new mainModel()
                {
                    proStatus_data = find_id,
                    logined_user = logined_user,
                };
                return View(data);
            }
            else
            {
                return RedirectToAction("login_form");
            }

        }
        public IActionResult update_project(int id)
        {
            var login = HttpContext.Session.GetString("user_session");
            if (login != null)
            {
                var logined_user = _con.tbl_employee.Where(p => p.id == int.Parse(login)).ToList();


                var find_id = _con.tbl_projects.Find(id);
                var team_details = _con.tbl_teams.Where(p => p.status == 1).ToList();
                var emp_data = _con.tbl_employee.Where(p => p.status == 1).ToList();
                var client_details = _con.tbl_clients.ToList();
                var status = _con.tbl_projectStatus.Where(p => p.status == 1).ToList();
                var priority = _con.tbl_projectpriority.Where(p => p.status == 1).ToList();
                var type = _con.tbl_project_types.Where(p => p.status == 1).ToList();
                var client_name = _con.tbl_clients.FirstOrDefault(P => P.id == find_id.client_id);
                var type_name = _con.tbl_project_types.FirstOrDefault(P => P.id == find_id.project_types);
                var employe_name = _con.tbl_employee.FirstOrDefault(P => P.id == find_id.team_id);
                var priority_name = _con.tbl_projectpriority.FirstOrDefault(P => P.id == find_id.project_priority);
                var status_name = _con.tbl_projectStatus.FirstOrDefault(P => P.status_id == find_id.project_statuss);
                mainModel data = new mainModel()
                {
                    client_data = client_name,
                    projects_types = type,
                    projects_prioritesDetails = priority,
                    pro_Statusdetail = status,

                    employee_details = emp_data,
                    team_details = team_details,
                    client_details = client_details,
                    projects_typesData = type_name,
                    employee_data = employe_name,
                    projects_prioritesData = priority_name,
                    proStatus_data = status_name,
                    logined_user = logined_user,


                    project_data = find_id
                };
                return View(data);
            }
            else
            {
                return RedirectToAction("login_form");
            }
        }
        [HttpPost]
        public IActionResult update_project(project pro, IFormFile document)
        {
            if (document != null) // Check if a file was actually uploaded
            {
                string file_extension = Path.GetExtension(document.FileName);
                if (file_extension == ".pdf" || file_extension == ".docx" || file_extension == ".txt")
                {
                    Random ran = new Random();
                    string fileNAme = Path.GetFileName(document.FileName);
                    var R_num = ran.Next(0000, 9999).ToString();
                    var name = fileNAme = R_num; // This line seems redundant; you're re-assigning fileNAme
                    string file_name = name + file_extension;
                    string filepath = Path.Combine(_evn.WebRootPath, "documents", file_name);
                    using (FileStream fs = new FileStream(filepath, FileMode.Create)) // Use 'using' for proper disposal
                    {
                        document.CopyTo(fs);
                    }

                    pro.document = file_name; // Store the path in the project object
                }
                else
                {
                    TempData["msg1"] = "This type of file is not supported !";
                    return RedirectToAction("project_details");
                }
            }
            // If document is null, the code will skip the file processing block.  pro object will be saved without file info.
            pro.plan_id = 0;
            _con.tbl_projects.Update(pro);
            _con.SaveChanges();
            TempData["msg1"] = "The project is successfully updated now !";

            return RedirectToAction("project_details");

        }

        public IActionResult updatestatus_statusProj(int id)
        {

            var find_id = _con.tbl_projectStatus.Find(id);
            if (find_id.status == 1)
            {
                find_id.status = 0;

            }
            else
            {
                find_id.status = 1;
            }
            _con.SaveChanges();
            return RedirectToAction("project_status");

        }
        public IActionResult survey_details(string search_text)
        {

            List<survey> survey_data = new List<survey>();
            if (string.IsNullOrEmpty(search_text))
            {
                survey_data = _con.tbl_survey.Include(p => p.employee_details).ToList();
            }

            else
            {
                survey_data = _con.tbl_survey.FromSqlInterpolated($"select * from tbl_survey  where subject like '%'+ {search_text}+ '%'").Include(p => p.employee_details).ToList();
            }
            if (survey_data.Count == 0)
            {
                TempData["msg"] = $"sorry the record is not found {search_text} ";
            }

            var emp_details = _con.tbl_employee.ToList();
            mainModel data = new mainModel()
            {
                employee_details = emp_details,
                survey_details = survey_data
            };
            return View(data);
        }
        [HttpPost]
        public IActionResult add_survey(survey _survey)
        {
            _con.tbl_survey.Add(_survey);
            _con.SaveChanges();
            TempData["msg1"] = "The survey is successfully added!";
            return RedirectToAction("survey_details");
        }
        public IActionResult delete_survey(int id)
        {
            var del = _con.tbl_survey.Find(id);
            _con.tbl_survey.Remove(del);
            _con.SaveChanges();
            TempData["msg1"] = "The survey is successfully deleted!";
            return RedirectToAction("survey_details");
        }
        public IActionResult update_survey(int id)
        {
            var find_id = _con.tbl_survey.Find(id);
            var emp_details = _con.tbl_employee.ToList();
            var emp_data = _con.tbl_employee.Where(e => e.id == find_id.emp_id);
            var survey = _con.tbl_survey.Where(p => p.id == id).ToList();
            mainModel data = new mainModel()
            {
                employee_details = emp_details,
                survey_details = survey,
                survey_data = find_id
            };
            return View(data);
        }
        [HttpPost]
        public IActionResult update_survey(survey _survey)
        {
            _con.tbl_survey.Update(_survey);
            _con.SaveChanges();
            TempData["msg1"] = "The survey is successfully updated!";
            return RedirectToAction("survey_details");
        }

        [HttpGet]
        public IActionResult Impact_risk(string search_text)
        {
            var login = HttpContext.Session.GetString("user_session");
            if (login != null)
            {
                var logined_user = _con.tbl_employee.Where(p => p.id == int.Parse(login)).ToList();

                List<Impact_risk> Impact_risk_data = new List<Impact_risk>();
                if (string.IsNullOrEmpty(search_text))
                {
                    Impact_risk_data = _con.tbl_impact_risk.ToList();
                }

                else
                {
                    Impact_risk_data = _con.tbl_impact_risk.FromSqlInterpolated($"select * from tbl_impact_risk  where risk_name like '%'+ {search_text}+ '%'").ToList();
                }
                if (Impact_risk_data.Count == 0)
                {
                    TempData["msg"] = $"sorry the record is not found {search_text} ";
                }

                var emp_details = _con.tbl_employee.ToList();
                mainModel data = new mainModel()
                {
                    logined_user = logined_user,
                    impact_Risks = Impact_risk_data
                };
                return View(data);

            }
            else
            {

                return RedirectToAction("login_form");
            }
        }
        [HttpPost]
        public IActionResult add_impactRisk(Impact_risk _risk)
        {
            _con.tbl_impact_risk.Add(_risk);
            _con.SaveChanges();
            TempData["msg1"] = "The risk is successfully added!";
            return RedirectToAction("Impact_risk");
        }
        public IActionResult delete_impactRisk(int id)
        {
            var del = _con.tbl_impact_risk.Find(id);
            _con.tbl_impact_risk.Remove(del);
            _con.SaveChanges();
            TempData["msg1"] = "The risk is successfully deleted!";
            return RedirectToAction("Impact_risk");
        }
        public IActionResult update_ImpactRisk(int id)
        {
            var login = HttpContext.Session.GetString("user_session");
            if (login != null)
            {
                var find_id = _con.tbl_impact_risk.Find(id);
                var logined_user = _con.tbl_employee.Where(p => p.id == int.Parse(login)).ToList();
                mainModel data = new mainModel()
                {
                    impactrisk_data = find_id,
                    logined_user = logined_user,
                };
                return View(data);
            }
            else
            {
                return RedirectToAction("login_form");
            }



        }
        [HttpPost]
        public IActionResult update_ImpactRisk(Impact_risk _risk)
        {
            _con.tbl_impact_risk.Update(_risk);
            _con.SaveChanges();
            TempData["msg1"] = "The risk is successfully updated now!";
            return RedirectToAction("Impact_risk");
        }
        [HttpGet]
        public IActionResult likehood_risk(string search_text)
        {
            var login = HttpContext.Session.GetString("user_session");
            if (login != null)
            {
                var logined_user = _con.tbl_employee.Where(p => p.id == int.Parse(login)).ToList();


                List<Likelihood_risk> Likelihood_risk_data = new List<Likelihood_risk>();
                if (string.IsNullOrEmpty(search_text))
                {
                    Likelihood_risk_data = _con.tbl_likedhood_risk.ToList();
                }

                else
                {
                    Likelihood_risk_data = _con.tbl_likedhood_risk.FromSqlInterpolated($"select * from tbl_likedhood_risk  where risk_name like '%'+ {search_text}+ '%'").ToList();
                }
                if (Likelihood_risk_data.Count == 0)
                {
                    TempData["msg"] = $"sorry the record is not found {search_text} ";
                }

                var emp_details = _con.tbl_employee.ToList();
                mainModel data = new mainModel()
                {
                    logined_user = logined_user,
                    Likelihood_risk = Likelihood_risk_data
                };
                return View(data);
            }
            else
            {
                return RedirectToAction("login_form");
            }

        }
        [HttpPost]
        public IActionResult add_likelihoodRisk(Likelihood_risk _risk)
        {
            _con.tbl_likedhood_risk.Add(_risk);

            _con.SaveChanges();
            TempData["msg1"] = "The risk is successfully added!";
            return RedirectToAction("likehood_risk");
        }
        public IActionResult delete_likehoodRisk(int id)
        {
            var del = _con.tbl_likedhood_risk.Find(id);
            _con.tbl_likedhood_risk.Remove(del);
            _con.SaveChanges();
            TempData["msg1"] = "The risk is successfully deleted!";
            return RedirectToAction("likehood_risk");
        }
        public IActionResult update_likehoodRisk(int id)
        {
            var login = HttpContext.Session.GetString("user_session");
            if (login != null)
            {
                var find_id = _con.tbl_likedhood_risk.Find(id);
                var logined_user = _con.tbl_employee.Where(p => p.id == int.Parse(login)).ToList();
                mainModel data = new mainModel()
                {
                    likelihoodrisk_data = find_id,
                    logined_user = logined_user,
                };
                return View(data);
            }
            else
            {
                return RedirectToAction("login_form");
            }



        }
        [HttpPost]
        public IActionResult update_likehoodRisk(Likelihood_risk _risk)
        {
            _con.tbl_likedhood_risk.Update(_risk);
            _con.SaveChanges();
            TempData["msg1"] = "The risk is successfully updated now!";
            return RedirectToAction("likehood_risk");
        }
        [HttpGet]
        public IActionResult risk_details(string search_text)
        {

            var login = HttpContext.Session.GetString("user_session");
            if (login != null)
            {
                var logined_user = _con.tbl_employee.Where(p => p.id == int.Parse(login)).ToList();



                List<risk> _risk_data = new List<risk>();
                if (string.IsNullOrEmpty(search_text))
                {
                    _risk_data = _con.tbl_risks.ToList();
                }

                else
                {
                    _risk_data = _con.tbl_risks.FromSqlInterpolated($"select * from tbl_risks  where risk_name like '%'+ {search_text}+ '%'").ToList();
                }
                if (_risk_data.Count == 0)
                {
                    TempData["msg"] = $"sorry the record is not found {search_text} ";
                }

                var impact_risk = _con.tbl_impact_risk.ToList();
                var likelihood_liked = _con.tbl_likedhood_risk.ToList();
                var risktype = _con.tbl_risk_types.ToList();
                mainModel data = new mainModel()
                {
                    logined_user = logined_user,
                    risk_details = _risk_data,
                    impact_Risks = impact_risk,
                    Risktypes_details = risktype,
                    Likelihood_risk = likelihood_liked
                };
                return View(data);

            }
            else
            {
                return RedirectToAction("login_form");
            }
        }

        [HttpPost]
        public IActionResult add_risk(risk _risk)
        {
            _risk.total_scoring = _risk.impact_risk * _risk.likelihood_risk;

            _con.tbl_risks.Add(_risk);
            _con.SaveChanges();
            TempData["msg1"] = "The risk is successfully added now!";
            return RedirectToAction("risk_details");
        }
        public IActionResult delete_Risk(int id)
        {
            var del = _con.tbl_risks.Find(id);
            _con.tbl_risks.Remove(del);
            _con.SaveChanges();
            TempData["msg1"] = "The risk is successfully deleted!";
            return RedirectToAction("risk_details");
        }
        public IActionResult update_Risk(int id)
        {
            var login = HttpContext.Session.GetString("user_session");
            if (login != null)
            {
                var find_id = _con.tbl_risks.Find(id);
                var impact_risk = _con.tbl_impact_risk.ToList();
                var likelihood_liked = _con.tbl_likedhood_risk.ToList();
                mainModel data = new mainModel()
                {
                    risk_data = find_id,
                    impact_Risks = impact_risk,
                    Likelihood_risk = likelihood_liked
                };
                return View(data);
            }
            else
            {
                return RedirectToAction("login_form");
            }
        }
        [HttpPost]
        public IActionResult update_Risk(risk _risk)
        {
            _risk.total_scoring = _risk.impact_risk * _risk.likelihood_risk;
            _con.tbl_risks.Update(_risk);
            _con.SaveChanges();
            TempData["msg1"] = "The risk is successfully updated!";
            return RedirectToAction("risk_details");
        }
        public IActionResult project_type()
        {
            var login = HttpContext.Session.GetString("user_session");
            if (login != null)
            {
                var type = _con.tbl_project_types.ToList();
                var logined_user = _con.tbl_employee.Where(p => p.id == int.Parse(login)).ToList();
                mainModel data = new mainModel()
                {
                    projects_types = type,
                    logined_user = logined_user,
                };
                return View(data);
            }
            else
            {
                return RedirectToAction("login_form");
            }

        }
        [HttpPost]
        public IActionResult add_projectType(project_type _type)
        {
            _con.tbl_project_types.Add(_type);

            _con.SaveChanges();
            TempData["msg1"] = "The project type is successfully added!";
            return RedirectToAction("project_type");

        }
        public IActionResult delete_projecttype(int id)
        {
            var del = _con.tbl_project_types.Find(id);
            _con.tbl_project_types.Remove(del);
            _con.SaveChanges();
            TempData["msg1"] = "The project type is successfully deleted!";
            return RedirectToAction("project_type");
        }
        public IActionResult update_projecttype(int id)
        {
            var login = HttpContext.Session.GetString("user_session");
            if (login != null)
            {
                var find_id = _con.tbl_project_types.Find(id);
                var logined_user = _con.tbl_employee.Where(p => p.id == int.Parse(login)).ToList();
                mainModel data = new mainModel()
                {
                    projects_typesData = find_id,
                    logined_user = logined_user,
                };
                return View(data);
            }
            else
            {
                return RedirectToAction("login_form");
            }


        }
        [HttpPost]
        public IActionResult update_projecttype(project_type _type)
        {
            _con.tbl_project_types.Update(_type);
            _con.SaveChanges();
            return RedirectToAction("project_type");

        }
        public IActionResult project_typestatusUpdate(int id)
        {
            var find_id = _con.tbl_project_types.Find(id);
            if (find_id.status == 1)
            {
                find_id.status = 0;
            }
            else
            {
                find_id.status = 1;

            }
            _con.SaveChanges();
            return RedirectToAction("project_type");
        }


        public IActionResult task_type()
        {
            var login = HttpContext.Session.GetString("user_session");
            if (login != null)
            {
                var type = _con.tbl_task_types.ToList();
                var logined_user = _con.tbl_employee.Where(p => p.id == int.Parse(login)).ToList();
                mainModel data = new mainModel()
                {
                    task_typedetails = type,
                    logined_user = logined_user,
                };
                return View(data);
            }
            else
            {
                return RedirectToAction("login_form");
            }

        }
        public IActionResult task_typestatusUpdate(int id)
        {
            var find_id = _con.tbl_task_types.Find(id);
            if (find_id.status == 1)
            {
                find_id.status = 0;
            }
            else
            {
                find_id.status = 1;

            }
            _con.SaveChanges();
            return RedirectToAction("task_type");
        }

        [HttpPost]
        public IActionResult add_taskType(task_type _type)
        {
            _con.tbl_task_types.Add(_type);
            _con.SaveChanges();
            return RedirectToAction("task_type");
        }
        public IActionResult delete_tasktype(int id)
        {
            var del = _con.tbl_task_types.Find(id);
            _con.tbl_task_types.Remove(del);
            _con.SaveChanges();
            return RedirectToAction("task_type");
        }
        public IActionResult update_tasktype(int id)
        {
            var login = HttpContext.Session.GetString("user_session");
            if (login != null)
            {
                var find_id = _con.tbl_task_types.Find(id);
                var logined_user = _con.tbl_employee.Where(p => p.id == int.Parse(login)).ToList();
                mainModel data = new mainModel()
                {
                    task_typedata = find_id,
                    logined_user = logined_user,
                };
                return View(data);
            }
            else
            {
                return RedirectToAction("login_form");
            }


        }
        [HttpPost]
        public IActionResult update_tasktype(task_type _type)
        {
            _con.tbl_task_types.Update(_type);
            _con.SaveChanges();
            return RedirectToAction("task_type");

        }
        public IActionResult taskstatus()
        {
            var login = HttpContext.Session.GetString("user_session");
            if (login != null)
            {
                var status = _con.tbl_task_status.ToList();
                var logined_user = _con.tbl_employee.Where(p => p.id == int.Parse(login)).ToList();
                mainModel data = new mainModel()
                {
                    task_statusdetails = status,
                    logined_user = logined_user,
                };
                return View(data);
            }
            else
            {
                return RedirectToAction("login_form");
            }


        }
        [HttpPost]
        public IActionResult add_taskstatus(task_status _status)
        {
            _con.tbl_task_status.Add(_status);
            _con.SaveChanges();
            return RedirectToAction("taskstatus");
        }
        public IActionResult delete_taskststatus(int id)
        {
            var del = _con.tbl_task_status.Find(id);
            _con.tbl_task_status.Remove(del);
            _con.SaveChanges();
            return RedirectToAction("taskstatus");
        }
        public IActionResult update_taskstatus(int id)
        {
            var login = HttpContext.Session.GetString("user_session");
            if (login != null)
            {
                var find_id = _con.tbl_task_status.Find(id);
                var logined_user = _con.tbl_employee.Where(p => p.id == int.Parse(login)).ToList();
                mainModel data = new mainModel()
                {
                    task_statusdata = find_id,
                    logined_user = logined_user,
                };
                return View(data);
            }
            else
            {
                return RedirectToAction("login_form");
            }


        }
        [HttpPost]
        public IActionResult update_taskstatus(task_status _status)
        {
            _con.tbl_task_status.Update(_status);
            _con.SaveChanges();
            return RedirectToAction("taskstatus");
        }

        public IActionResult update_tasks_status(int id)
        {
            var find_id = _con.tbl_task_status.Find(id);
            if (find_id.status == 1)
            {
                find_id.status = 0;
            }
            else
            {
                find_id.status = 1;
            }
            _con.SaveChanges();
            return RedirectToAction("taskstatus");
        }
        public IActionResult project_priority()
        {
            var login = HttpContext.Session.GetString("user_session");
            if (login != null)
            {
                var priority = _con.tbl_projectpriority.ToList();
                var logined_user = _con.tbl_employee.Where(p => p.id == int.Parse(login)).ToList();
                mainModel data = new mainModel()
                {
                    projects_prioritesDetails = priority,
                    logined_user = logined_user,
                };
                return View(data);
            }
            else
            {
                return RedirectToAction("login_form");
            }


        }
        [HttpPost]
        public IActionResult addproject_priority(project_priority _priority)
        {
            _con.tbl_projectpriority.Add(_priority);
            _con.SaveChanges();
            return RedirectToAction("project_priority");
        }
        public IActionResult delete_projectpriority(int id)
        {
            var del = _con.tbl_projectpriority.Find(id);
            _con.tbl_projectpriority.Remove(del);
            _con.SaveChanges();
            return RedirectToAction("project_priority");
        }
        public IActionResult update_projectpriority(int id)
        {
            var login = HttpContext.Session.GetString("user_session");
            if (login != null)
            {
                var find_id = _con.tbl_projectpriority.Find(id);
                var logined_user = _con.tbl_employee.Where(p => p.id == int.Parse(login)).ToList();
                mainModel data = new mainModel()
                {
                    projects_prioritesData = find_id,
                    logined_user = logined_user,
                };
                return View(data);
            }
            else
            {
                return RedirectToAction("login_form");
            }


        }
        [HttpPost]
        public IActionResult update_projectpriority(project_priority _prority)
        {
            _con.tbl_projectpriority.Update(_prority);
            _con.SaveChanges();
            return RedirectToAction("project_priority");

        }
        public IActionResult projectPriorityStatus(int id)
        {
            var find_id = _con.tbl_projectpriority.Find(id);
            if (find_id.status == 1)
            {
                find_id.status = 0;
            }
            else
            {
                find_id.status = 1;
            }
            _con.SaveChanges();
            return RedirectToAction("project_priority");
        }
        public IActionResult task_priority()
        {
            var login = HttpContext.Session.GetString("user_session");
            if (login != null)

            {
                var task_priority = _con.tbl_taskpriority.ToList();
                var logined_user = _con.tbl_employee.Where(p => p.id == int.Parse(login)).ToList();
                mainModel data = new mainModel()
                {
                    logined_user = logined_user,
                    task_prioritesDetails = task_priority,
                };
                return View(data);
            }
            else
            {
                return RedirectToAction("login_form");
            }

        }
        [HttpPost]
        public IActionResult addtask_priority(task_priority _Priority)
        {
            _con.tbl_taskpriority.Add(_Priority);
            _con.SaveChanges();
            return RedirectToAction("task_priority");
        }
        public IActionResult delete_taskprority(int id)
        {
            var del = _con.tbl_taskpriority.Find(id);
            _con.tbl_taskpriority.Remove(del);
            _con.SaveChanges();
            return RedirectToAction("task_priority");
        }

        public IActionResult projecttaskprioritiesStatus(int id)
        {
            var find_id = _con.tbl_taskpriority.Find(id);
            if (find_id.status == 1)
            {
                find_id.status = 0;
            }
            else
            {
                find_id.status = 1;
            }
            _con.SaveChanges();
            return RedirectToAction("task_priority");
        }
        public IActionResult update_taskpriority(int id)
        {
            var login = HttpContext.Session.GetString("user_session");
            if (login != null)
            {
                var find_id = _con.tbl_taskpriority.Find(id);
                var logined_user = _con.tbl_employee.Where(p => p.id == int.Parse(login)).ToList();
                mainModel data = new mainModel()
                {
                    task_prioritesData = find_id,
                    logined_user = logined_user,
                };
                return View(data);
            }
            else
            {
                return RedirectToAction("login_form");
            }


        }
        [HttpPost]
        public IActionResult update_taskpriority(task_priority _task)
        {
            _con.tbl_taskpriority.Update(_task);
            _con.SaveChanges();
            return RedirectToAction("task_priority");

        }
        public IActionResult task_details(string search_text)
        {
            var login = HttpContext.Session.GetString("user_session");
            if (login != null)
            {
                var logined_user = _con.tbl_employee.Where(p => p.id == int.Parse(login)).ToList();


                List<task> task_data = new List<task>();
                if (string.IsNullOrEmpty(search_text))
                {
                    task_data = _con.tbl_tasks.Include(p => p.emp_data).Include(a => a._status).Where(p => p.plan_id == 0).ToList();
                }

                else
                {
                    task_data = _con.tbl_tasks.FromSqlInterpolated($"select * from tbl_project_task  where name like '%'+ {search_text}+ '%'").Include(p => p.emp_data).Include(a => a._status).Where(p => p.plan_id == 0).ToList();
                }
                if (task_data.Count == 0)
                {
                    TempData["msg"] = $"sorry the record is not found {search_text} ";
                }
                var team_details = _con.tbl_teams.Where(p => p.status == 1).ToList();
                var emp = _con.tbl_employee.Where(p => p.status == 1).ToList();
                var project_details = _con.tbl_projects.ToList();
                var _types = _con.tbl_task_types.Where(p => p.status == 1).ToList();
                var _status = _con.tbl_task_status.Where(p => p.status == 1).ToList();
                var _prority = _con.tbl_taskpriority.Where(p => p.status == 1).ToList();
                var _risk = _con.tbl_risks.ToList();
                mainModel data = new mainModel()
                {
                    logined_user = logined_user,
                    employee_details = emp,
                    risk_details = _risk,
                    task_details = task_data,
                    task_prioritesDetails = _prority,
                    task_typedetails = _types,
                    task_statusdetails = _status,
                    project_details = project_details,
                    team_details = team_details
                };
                return View(data);
            }

            else
            {
                return RedirectToAction("login_form");
            }
        }
        [HttpPost]
        public IActionResult add_Task(task _task, IFormFile document)
        {
            if (document != null) // Check if a file was actually uploaded
            {
                string file_extension = Path.GetExtension(document.FileName);
                if (file_extension == ".pdf" || file_extension == ".docx" || file_extension == ".txt")
                {
                    Random ran = new Random();
                    string fileNAme = Path.GetFileName(document.FileName);
                    var R_num = ran.Next(0000, 9999).ToString();
                    var name = fileNAme = R_num; // This line seems redundant; you're re-assigning fileNAme
                    string file_name = name + file_extension;
                    string filepath = Path.Combine(_evn.WebRootPath, "documents", file_name);
                    using (FileStream fs = new FileStream(filepath, FileMode.Create)) // Use 'using' for proper disposal
                    {
                        document.CopyTo(fs);
                    }

                    _task.document = file_name;
                    // Store the path in the project object
                }
                else
                {
                    TempData["msg1"] = "This type of file is not supported !";
                    return RedirectToAction("task_details");
                }
            }
            // If document is null, the code will skip the file processing block.  pro object will be saved without file info.
            _task.created_date = DateTime.Now.ToString();
            _task.updated_date = DateTime.Now.ToString();
            _task.plan_id = 0;
            _con.tbl_tasks.Add(_task);
            _con.SaveChanges();
            TempData["msg1"] = "The Task is successfully updated now !";
            return RedirectToAction("task_details");
        }
        public IActionResult delete_task(int id)
        {
            var del = _con.tbl_tasks.Find(id);
            _con.tbl_tasks.Remove(del);
            return RedirectToAction("task_details");
        }
        public IActionResult view_task(int id)
        {

            var login = HttpContext.Session.GetString("user_session");
            if (login != null)
            {
                var logined_user = _con.tbl_employee.Where(p => p.id == int.Parse(login)).ToList();
                var task_data = _con.tbl_tasks.Where(p => p.task_id == id).ToList();
                var task_view = _con.tbl_tasks.Find(id);
                var team_details = _con.tbl_teams.Where(p => p.status == 1).ToList();
                var _emp = _con.tbl_employee.FirstOrDefault(p => p.id == task_view.employee_ids);

                var project_details = _con.tbl_projects.FirstOrDefault(p => p.Project_id == task_view.project_);
                var _types = _con.tbl_task_types.FirstOrDefault(p => p.id == task_view.task_types);
                var _status = _con.tbl_task_status.FirstOrDefault(p => p.id == task_view.task_statuss);
                var _prority = _con.tbl_taskpriority.FirstOrDefault(p => p.id == task_view.task_priority);
                var _risk = _con.tbl_risks.ToList();
                var employe = _con.tbl_employee.Where(e => e.status == 1).ToList();
                var _files = _con.tbl_taks_file.Where(u => u.task_ids == id).ToList();
                if (_files.Count == 0)
                {
                    TempData["nofound"] = "there is not any file is upload";
                }
                mainModel data = new mainModel()
                {
                    logined_user = logined_user,
                    employee_data = _emp,

                    task_data = task_view,
                    risk_details = _risk,
                    employee_details = employe,
                    task_details = task_data,
                    task_prioritesData = _prority,
                    task_typedata = _types,
                    task_statusdata = _status,
                    project_data = project_details,
                    team_details = team_details,
                    files_details = _files

                };
                return View(data);
            }
            else
            {
                return RedirectToAction("login_form");
            }




        }

        public IActionResult update_tasks(int id)
        {
            var login = HttpContext.Session.GetString("user_session");
            if (login != null)
            {
                var logined_user = _con.tbl_employee.Where(p => p.id == int.Parse(login)).ToList();

                var task_datas = _con.tbl_tasks.Find(id);
                var project = _con.tbl_projects.ToList();
                var risk = _con.tbl_risks.ToList();
                var _taskstatus = _con.tbl_task_status.Where(p => p.status == 1).ToList();
                var status = _con.tbl_task_status.FirstOrDefault(p => p.id == task_datas.task_statuss);

                var project_info = _con.tbl_projects.FirstOrDefault(p => p.Project_id == task_datas.project_);
                var type = _con.tbl_task_types.Where(a => a.status == 1).ToList();
                var _typedata = _con.tbl_task_types.FirstOrDefault(t => t.id == task_datas.task_types);
                var _priority = _con.tbl_taskpriority.Where(p => p.status == 1).ToList();

                var _priority_selected = _con.tbl_taskpriority.FirstOrDefault(p => p.id == task_datas.task_priority);
                var _emp = _con.tbl_employee.Where(a => a.status == 1).ToList();
                var emp_data = _con.tbl_employee.FirstOrDefault(a => a.id == task_datas.employee_ids);
                mainModel _task_data = new mainModel
                {
                    project_details = project,
                    employee_details = _emp,
                    employee_data = emp_data,
                    task_statusdetails = _taskstatus,
                    task_statusdata = status,
                    project_data = project_info,
                    risk_details = risk,
                    logined_user = logined_user,
                    task_typedata = _typedata,
                    task_typedetails = type,
                    task_prioritesData = _priority_selected,
                    task_prioritesDetails = _priority,

                    task_data = _con.tbl_tasks.Find(id),

                };

                return View(_task_data);
            }
            else
            {
                return RedirectToAction("login_form");
            }
        }
        [HttpPost]
        public IActionResult update_tasks(task _task, IFormFile document)
        {
            if (document != null) // Check if a file was actually uploaded
            {
                string file_extension = Path.GetExtension(document.FileName);
                if (file_extension == ".pdf" || file_extension == ".docx" || file_extension == ".txt")
                {
                    Random ran = new Random();
                    string fileNAme = Path.GetFileName(document.FileName);
                    var R_num = ran.Next(0000, 9999).ToString();
                    var name = fileNAme = R_num; // This line seems redundant; you're re-assigning fileNAme
                    string file_name = name + file_extension;
                    string filepath = Path.Combine(_evn.WebRootPath, "documents", file_name);
                    using (FileStream fs = new FileStream(filepath, FileMode.Create)) // Use 'using' for proper disposal
                    {
                        document.CopyTo(fs);
                    }

                    _task.document = file_name;
                    // Store the path in the project object
                }
                else
                {
                    TempData["msg1"] = "This type of file is not supported !";
                    return RedirectToAction("task_details");
                }
            }
            // If document is null, the code will skip the file processing block.  pro object will be saved without file info.
            _task.created_date = DateTime.Now.ToString();
            _task.updated_date = DateTime.Now.ToString();
            if (_task.plan_id == 0)
            {
                _task.plan_id = 0;
            }
            _con.tbl_tasks.Update(_task);
            _con.SaveChanges();
            TempData["msg1"] = "The Task is successfully updated now !";
            return RedirectToAction("task_details");
        }
        // add_file
        [HttpPost]
        public IActionResult add_file(int id, task_files _file, IFormFile file_name)
        {
            string file_extension = Path.GetExtension(file_name.FileName);
            if (file_extension == ".pdf" || file_extension == ".docx" || file_extension == ".txt")
            {
                Random ran = new Random();
                string fileNAme = Path.GetFileName(file_name.FileName);
                var R_num = ran.Next(0000, 9999).ToString();
                var name = fileNAme = R_num; // This line seems redundant; you're re-assigning fileNAme
                string file_names = name + file_extension;
                string filepath = Path.Combine(_evn.WebRootPath, "documents", file_names);
                using (FileStream fs = new FileStream(filepath, FileMode.Create)) // Use 'using' for proper disposal
                {
                    file_name.CopyTo(fs);
                }

                _file.date = DateTime.Now.Date.ToShortDateString();

                _file.file_name = file_names;
                _con.tbl_taks_file.Add(_file);
                _con.SaveChanges();
                TempData["msg8"] = "The file is successfully updated now !";
                return RedirectToAction("task_details");
            }
            else
            {
                TempData["msg8"] = "This type of file is not supported !";
                return RedirectToAction("task_details");
            }



        }
        public IActionResult delete_file(int ids)
        {
            var del = _con.tbl_taks_file.Find(ids);
            _con.tbl_taks_file.Remove(del);
            _con.SaveChanges();
            return RedirectToAction("view_task");
        }
        [HttpPost]
        public IActionResult add_subtask(sub_task _sub_task)
        {
            _con.tbl_subtask.Add(_sub_task);
            _con.SaveChanges();
            return RedirectToAction("view_task");
        }
        public IActionResult sub_task(int id)
        {
            var sub_task = _con.tbl_subtask.Where(p => p.task_id == id).Include(p => p.emp_details).Include(p => p.risl_details).Include(p => p.task_details).ToList();
            if (sub_task.Count == 0)
            {
                TempData["msg"] = "There is not sub task ";
            }

            mainModel data = new mainModel
            {
                subTask_details = sub_task
            };
            return View(data);
        }
        public IActionResult delete_subtask(int id)
        {
            var del = _con.tbl_subtask.Find(id);
            _con.tbl_subtask.Remove(del);
            _con.SaveChanges();
            return RedirectToAction("view_task");
        }
        public IActionResult update_subtasks(int id)
        {
            var login = HttpContext.Session.GetString("user_session");
            if (login != null)
            {
                var logined_user = _con.tbl_employee.Where(p => p.id == int.Parse(login)).ToList();

                var sub_Taskdata = _con.tbl_subtask.Find(id);
                var emp = _con.tbl_employee.Where(p => p.status == 1).ToList();
                var risk = _con.tbl_risks.ToList();
                var _risk_data = _con.tbl_risks.FirstOrDefault(r => r.id == sub_Taskdata.risk_id);
                var emp_data = _con.tbl_employee.FirstOrDefault(r => r.id == sub_Taskdata.emp_id);

                mainModel data = new mainModel
                {
                    logined_user = logined_user,
                    employee_details = emp,
                    risk_data = _risk_data,
                    risk_details = risk,
                    employee_data = emp_data,

                    subTask_data = sub_Taskdata
                };
                return View(data);
            }
            else
            {
                return RedirectToAction("login_form");
            }


        }
        [HttpPost]
        public IActionResult update_subtasks(sub_task _task)
        {
            _con.tbl_subtask.Update(_task);
            _con.SaveChanges();
            return RedirectToAction("view_task");
        }
        public IActionResult maritialStatus_details()
        {
            var login = HttpContext.Session.GetString("user_session");
            if (login != null)
            {
                var logined_user = _con.tbl_employee.Where(p => p.id == int.Parse(login)).ToList();

                var status = _con.tbl_maritalstatus.ToList();
                mainModel data = new mainModel
                {
                    logined_user = logined_user,
                    materialStatus_details = status
                };

                return View(data);
            }
            else
            {
                return RedirectToAction("login_form");
            }
        }
        [HttpPost]
        public IActionResult addmaritial_status(maritalStatus maritial_status)
        {
            _con.tbl_maritalstatus.Add(maritial_status);
            _con.SaveChanges();
            return RedirectToAction("maritialStatus_details");
        }
        public IActionResult delete_maritalStatus(int id)
        {
            var del = _con.tbl_maritalstatus.Find(id);
            _con.tbl_maritalstatus.Remove(del);

            _con.SaveChanges();
            return RedirectToAction("maritialStatus_details");
        }
        public IActionResult maritalStatusUpdate(int id)
        {
            var find_Id = _con.tbl_maritalstatus.Find(id);
            if (find_Id.status == 1)
            {
                find_Id.status = 0;
            }
            else
            {
                find_Id.status = 1;
            }
            _con.SaveChanges();
            return RedirectToAction("maritialStatus_details");
        }
        public IActionResult update_materialStatus(int id)
        {
            var login = HttpContext.Session.GetString("user_session");
            if (login != null)
            {
                var logined_user = _con.tbl_employee.Where(p => p.id == int.Parse(login)).ToList();
                var find_id = _con.tbl_maritalstatus.Find(id);
                mainModel data = new mainModel
                {
                    logined_user = logined_user,
                    materialStatus_data = find_id
                };
                return View(data);
            }
            else
            {
                return RedirectToAction("login_form");
            }
        }
        [HttpPost]
        public IActionResult update_materialStatus(maritalStatus _status)
        {
            _con.tbl_maritalstatus.Update(_status);
            _con.SaveChanges();
            return RedirectToAction("maritialStatus_details");
        }
        public IActionResult employee_type()
        {
            var login = HttpContext.Session.GetString("user_session");
            if (login != null)
            {
                var types = _con.tbl_emptypes.ToList();
                var logined_user = _con.tbl_employee.Where(p => p.id == int.Parse(login)).ToList();
                mainModel data = new mainModel()
                {
                    emp_types_details = types,
                    logined_user = logined_user,
                };
                return View(data);
            }
            else
            {
                return RedirectToAction("login_form");
            }

        }

        public IActionResult delete_emptypes(int id)
        {
            var find_id = _con.tbl_emptypes.Find(id);
            _con.tbl_emptypes.Remove(find_id);
            _con.SaveChanges();
            return RedirectToAction("employee_type");
        }
        [HttpPost]
        public IActionResult addemployee_type(emp_type _empType)
        {
            _con.tbl_emptypes.Add(_empType);
            _con.SaveChanges();
            return RedirectToAction("employee_type");
        }
        public IActionResult employeeTypesstatusUpdate(int id)
        {
            var find_Id = _con.tbl_emptypes.Find(id);
            if (find_Id.status == 1)
            {
                find_Id.status = 0;
            }
            else
            {
                find_Id.status = 1;
            }
            _con.SaveChanges();
            return RedirectToAction("employee_type");
        }
        public IActionResult update_empType(int id)
        {
            var login = HttpContext.Session.GetString("user_session");
            if (login != null)
            {
                var find_id = _con.tbl_emptypes.Find(id);
                var logined_user = _con.tbl_employee.Where(p => p.id == int.Parse(login)).ToList();
                mainModel data = new mainModel()
                {
                    emp_types_data = find_id,
                    logined_user = logined_user,
                };
                return View(data);
            }
            else
            {
                return RedirectToAction("login_form");
            }
        }
        [HttpPost]
        public IActionResult update_empType(emp_type _type)
        {
            _con.tbl_emptypes.Update(_type);
            _con.SaveChanges();
            return RedirectToAction("employee_type");
        }


        public IActionResult risk_typeDetails()
        {
            var login = HttpContext.Session.GetString("user_session");
            if (login != null)
            {
                var _riskType = _con.tbl_risk_types.ToList();
                var logined_user = _con.tbl_employee.Where(p => p.id == int.Parse(login)).ToList();
                mainModel data = new mainModel
                {
                    logined_user = logined_user,
                    Risktypes_details = _riskType
                };
                if (_riskType.Count == 0)
                {
                    TempData["msg"] = "There is no any Risk type";
                }

                return View(data);
            }
            else
            {
                return RedirectToAction("login_form");
            }
        }
        public IActionResult add_riskType(risk_type _types)
        {
            _con.tbl_risk_types.Add(_types);
            _con.SaveChanges();
            return RedirectToAction("risk_typeDetails");
        }
        public IActionResult delete_riskType(int id)
        {
            var del = _con.tbl_risk_types.Find(id);
            _con.tbl_risk_types.Remove(del);
            _con.SaveChanges();
            return RedirectToAction("risk_typeDetails");
        }
        public IActionResult risk_typestatusUpdate(int id)
        {
            var find_Id = _con.tbl_risk_types.Find(id);
            if (find_Id.status == 1)
            {
                find_Id.status = 0;
            }
            else
            {
                find_Id.status = 1;
            }
            _con.SaveChanges();
            return RedirectToAction("risk_typeDetails");

        }
        public IActionResult update_risk_types(int id)
        {
            var login = HttpContext.Session.GetString("user_session");
            if (login != null)
            {
                var find_id = _con.tbl_risk_types.Find(id);

                var logined_user = _con.tbl_employee.Where(p => p.id == int.Parse(login)).ToList();
                mainModel data = new mainModel()
                {
                    logined_user = logined_user,
                    Risktypes_data = find_id
                };
                return View(data);
            }
            else
            {
                return RedirectToAction("login_form");
            }

        }
        [HttpPost]
        public IActionResult update_risk_types(risk_type _types)
        {
            _con.tbl_risk_types.Update(_types);
            _con.SaveChanges();
            return RedirectToAction("risk_typeDetails");
        }
        public IActionResult audit_typeDetails()
        {
            var login = HttpContext.Session.GetString("user_session");
            if (login != null)
            {
                var _AuditType = _con.tbl_auditTypes.ToList();
                var logined_user = _con.tbl_employee.Where(p => p.id == int.Parse(login)).ToList();
                mainModel data = new mainModel
                {
                    logined_user = logined_user,
                    audittype = _AuditType
                };
                if (_AuditType.Count == 0)
                {
                    TempData["msg"] = "There is no any Audit type";
                }

                return View(data);
            }
            else
            {
                return RedirectToAction("login_form");
            }
        }
        [HttpPost]
        public IActionResult add_AuditType(AuditType _auditType)
        {
            _con.tbl_auditTypes.Add(_auditType);
            _con.SaveChanges();
            return RedirectToAction("audit_typeDetails");

        }

        public IActionResult delete_auditType(int id)
        {
            var del = _con.tbl_auditTypes.Find(id);
            _con.tbl_auditTypes.Remove(del);
            _con.SaveChanges();
            return RedirectToAction("audit_typeDetails");
        }
        public IActionResult audit_typestatusUpdate(int id)
        {
            var find_id = _con.tbl_auditTypes.Find(id);
            if (find_id.status == 1)
            {
                find_id.status = 0;
            }
            else
            {
                find_id.status = 1;

            }
            _con.SaveChanges();
            return RedirectToAction("audit_typeDetails");
        }
        public IActionResult update_audit_types(int id)
        {
            var login = HttpContext.Session.GetString("user_session");
            if (login != null)
            {
                var find_id = _con.tbl_auditTypes.Find(id);

                var logined_user = _con.tbl_employee.Where(p => p.id == int.Parse(login)).ToList();
                mainModel data = new mainModel()
                {
                    logined_user = logined_user,
                    audittypedata = find_id,
                };
                return View(data);
            }
            else
            {
                return RedirectToAction("login_form");
            }
        }
        [HttpPost]
        public IActionResult update_audit_types(AuditType _auditType)
        {
            _con.tbl_auditTypes.Update(_auditType);
            _con.SaveChanges();
            return RedirectToAction("audit_typeDetails");
        }
        public IActionResult audit_priority()
        {
            var login = HttpContext.Session.GetString("user_session");
            if (login != null)
            {
                var priority = _con.tbl_auditpriority.ToList();
                var logined_user = _con.tbl_employee.Where(p => p.id == int.Parse(login)).ToList();
                mainModel data = new mainModel()
                {
                    auditPriorityDetails = priority,
                    logined_user = logined_user,
                };
                return View(data);
            }
            else
            {
                return RedirectToAction("login_form");
            }


        }
        [HttpPost]
        public IActionResult addaudit_priority(auditpriority _pro)
        {
            _con.tbl_auditpriority.Add(_pro);
            _con.SaveChanges();
            return RedirectToAction("audit_priority");
        }
        public IActionResult delete_auditpriority(int id)
        {
            var del = _con.tbl_auditpriority.Find(id);
            _con.tbl_auditpriority.Remove(del);
            _con.SaveChanges();
            return RedirectToAction("audit_priority");
        }
        public IActionResult AuditPriorityStatus(int id)
        {
            var find_id = _con.tbl_auditpriority.Find(id);
            if (find_id.status == 1)
            {
                find_id.status = 0;
            }
            else
            {
                find_id.status = 1;

            }
            _con.SaveChanges();
            return RedirectToAction("audit_priority");
        }
        public IActionResult update_auditpriority(int id)
        {
            var login = HttpContext.Session.GetString("user_session");
            if (login != null)
            {
                var find_id = _con.tbl_auditpriority.Find(id);

                var logined_user = _con.tbl_employee.Where(p => p.id == int.Parse(login)).ToList();
                mainModel data = new mainModel()
                {
                    logined_user = logined_user,
                    auditPrioritydata = find_id
                };
                return View(data);
            }
            else
            {
                return RedirectToAction("login_form");
            }
        }
        [HttpPost]
        public IActionResult update_auditpriority(auditpriority _pro)
        {
            _con.tbl_auditpriority.Update(_pro);
            _con.SaveChanges();
            return RedirectToAction("audit_priority");
        }
        public IActionResult audit_details(string search_text)
        {
            var login = HttpContext.Session.GetString("user_session");
            if (login != null)
            {
                var logined_user = _con.tbl_employee.Where(p => p.id == int.Parse(login)).ToList();


                List<Audit> audit_data = new List<Audit>();
                if (string.IsNullOrEmpty(search_text))
                {
                    audit_data = _con.tbl_audit.ToList();
                }

                else
                {
                    audit_data = _con.tbl_audit.FromSqlInterpolated($"select * from tbl_audit  where Tittle like '%'+ {search_text}+ '%'").ToList();
                }
                if (audit_data.Count == 0)
                {
                    TempData["msg"] = $"sorry the record is not found {search_text} ";
                }
                var team_details = _con.tbl_teams.Where(p => p.status == 1).ToList();
                var emp = _con.tbl_employee.Where(p => p.status == 1).ToList();
                var project_details = _con.tbl_projects.ToList();
                var task = _con.tbl_tasks.ToList();
                var type = _con.tbl_auditTypes.Where(p => p.status == 1).ToList();
                var priority = _con.tbl_auditpriority.Where(p => p.status == 1).ToList();

                mainModel data = new mainModel()
                {
                    logined_user = logined_user,
                    employee_details = emp,
                    auditPriorityDetails = priority,
                    audittype = type,
                    task_details = task,
                    audit_datails = audit_data,
                    project_details = project_details,
                    team_details = team_details
                };
                return View(data);
            }

            else
            {
                return RedirectToAction("login_form");
            }


        }

        [HttpPost]
        public IActionResult add_audit(Audit _audit, IFormFile Attachment)
        {

            if (Attachment != null)
            {
                string file_extension = Path.GetExtension(Attachment.FileName);
                if (file_extension == ".pdf" || file_extension == ".docx" || file_extension == ".txt" || file_extension == ".xlsx" || file_extension == ".png" || file_extension == ".jpg" || file_extension == ".jpeg")
                {
                    Random ran = new Random();
                    string fileNAme = Path.GetFileName(Attachment.FileName);
                    var R_num = ran.Next(0000, 9999).ToString();
                    var name = fileNAme = R_num; // This line seems redundant; you're re-assigning fileNAme
                    string file_name = name + file_extension;
                    string filepath = Path.Combine(_evn.WebRootPath, "documents/audit", file_name);
                    using (FileStream fs = new FileStream(filepath, FileMode.Create)) // Use 'using' for proper disposal
                    {
                        Attachment.CopyTo(fs);
                    }

                    _audit.Attachment = file_name;

                }
                else
                {
                    TempData["msg1"] = "This type of file is not supported !";
                    return RedirectToAction("audit_details");
                }
            }

            _con.tbl_audit.Add(_audit);
            _con.SaveChanges();
            TempData["msg1"] = "The audit is successfully added now !";
            return RedirectToAction("audit_details");
        }


        public IActionResult view_aduit(int id)
        {
            var login = HttpContext.Session.GetString("user_session");
            if (login != null)
            {
                var logined_user = _con.tbl_employee.Where(p => p.id == int.Parse(login)).ToList();
                var find_id = _con.tbl_audit.Find(id);
                var audit_data = _con.tbl_audit.Where(p => p.id == find_id.id).ToList();
                var emp = _con.tbl_employee.FirstOrDefault(p => p.id == find_id.assignedauditor);
                var project = _con.tbl_projects.FirstOrDefault(p => p.Project_id == find_id.project_id);
                var task = _con.tbl_tasks.FirstOrDefault(p => p.task_id == find_id.task_id);

                if (task == null)
                {

                    TempData["msg"] = "No tasks found.";
                }
                var prority = _con.tbl_auditpriority.FirstOrDefault(p => p.id == find_id.Priority_id);
                var type = _con.tbl_auditTypes.FirstOrDefault(p => p.id == find_id.Type_id);
                mainModel data = new mainModel()
                {
                    auditPrioritydata = prority,
                    audittypedata = type,
                    logined_user = logined_user,
                    audit_data = find_id,
                    audit_datails = audit_data,
                    employee_data = emp,
                    task_data = task,
                    project_data = project,


                };
                return View(data);
            }

            else
            {
                return RedirectToAction("login_form");
            }

        }
        public IActionResult delete_audit(int id)
        {
            var del = _con.tbl_audit.Find(id);
            _con.tbl_audit.Remove(del);
            _con.SaveChanges();
            return RedirectToAction("audit_details");
        }
        public IActionResult update_audit(int id)
        {

            var login = HttpContext.Session.GetString("user_session");
            if (login != null)
            {
                var logined_user = _con.tbl_employee.Where(p => p.id == int.Parse(login)).ToList();
                var find_id = _con.tbl_audit.Find(id);
                var emp = _con.tbl_employee.FirstOrDefault(p => p.id == find_id.assignedauditor);
                var empList = _con.tbl_employee.ToList();
                var project = _con.tbl_projects.FirstOrDefault(p => p.Project_id == find_id.project_id);
                var task = _con.tbl_tasks.FirstOrDefault(p => p.task_id == find_id.task_id);
                var prority = _con.tbl_auditpriority.FirstOrDefault(p => p.id == find_id.Priority_id);
                var type = _con.tbl_auditTypes.FirstOrDefault(p => p.id == find_id.Type_id);
                var project_list = _con.tbl_projects.ToList();
                var task_list = _con.tbl_tasks.ToList();
                var priority_list = _con.tbl_auditpriority.ToList();
                var type_list = _con.tbl_auditTypes.ToList();
                mainModel data = new mainModel()
                {
                    employee_details = empList,
                    auditPrioritydata = prority,
                    audittypedata = type,
                    logined_user = logined_user,
                    audit_data = find_id,
                    employee_data = emp,
                    task_data = task,
                    task_details = task_list,
                    project_details = project_list,
                    auditPriorityDetails = priority_list,
                    audittype = type_list,
                    project_data = project,


                };
                return View(data);
            }

            else
            {
                return RedirectToAction("login_form");
            }
        }
        [HttpPost]
        public IActionResult update_audit(Audit _audit, IFormFile Attachment)
        {
            if (Attachment != null)
            {
                string file_extension = Path.GetExtension(Attachment.FileName);
                if (file_extension == ".pdf" || file_extension == ".docx" || file_extension == ".txt" || file_extension == ".xlsx" || file_extension == ".png" || file_extension == ".jpg" || file_extension == ".jpeg")
                {
                    Random ran = new Random();
                    string fileNAme = Path.GetFileName(Attachment.FileName);
                    var R_num = ran.Next(0000, 9999).ToString();
                    var name = fileNAme = R_num; // This line seems redundant; you're re-assigning fileNAme
                    string file_name = name + file_extension;
                    string filepath = Path.Combine(_evn.WebRootPath, "documents/audit", file_name);
                    using (FileStream fs = new FileStream(filepath, FileMode.Create)) // Use 'using' for proper disposal
                    {
                        Attachment.CopyTo(fs);
                    }

                    _audit.Attachment = file_name;

                }
                else
                {
                    TempData["msg1"] = "This type of file is not supported !";
                    return RedirectToAction("audit_details");
                }
            }

            _con.tbl_audit.Update(_audit);
            _con.SaveChanges();
            TempData["msg1"] = "The audit is successfully updated now !";
            return RedirectToAction("audit_details");

        }
        public IActionResult budget_details()
        {
            var login = HttpContext.Session.GetString("user_session");
            if (login != null)
            {
                var logined_user = _con.tbl_employee.Where(p => p.id == int.Parse(login)).ToList();
                var buget = _con.tbl_bugetaherence.ToList();
                mainModel data = new mainModel()
                {
                    buget_details = buget,
                    logined_user = logined_user,



                };
                return View(data);
            }

            else
            {
                return RedirectToAction("login_form");
            }

        }

        [HttpPost]
        public IActionResult add_budget(BudgetAdherence _budget)
        {
            _con.tbl_bugetaherence.Add(_budget);
            _con.SaveChanges();
            return RedirectToAction("budget_details");
        }
        public IActionResult delete_budget(int id)
        {
            var del = _con.tbl_bugetaherence.Find(id);
            _con.tbl_bugetaherence.Remove(del);
            _con.SaveChanges();
            return RedirectToAction("budget_details");
        }
        public IActionResult budgetStatus(int id)
        {
            var find_id = _con.tbl_bugetaherence.Find(id);
            if (find_id.status == 1)
            {
                find_id.status = 0;
            }
            else
            {
                find_id.status = 1;

            }
            _con.SaveChanges();
            return RedirectToAction("budget_details");

        }
        public IActionResult update_buget(int id)
        {
            var login = HttpContext.Session.GetString("user_session");
            if (login != null)
            {
                var logined_user = _con.tbl_employee.Where(p => p.id == int.Parse(login)).ToList();
                var buget = _con.tbl_bugetaherence.ToList();
                var findId = _con.tbl_bugetaherence.Find(id);
                mainModel data = new mainModel()
                {
                    buget_data = findId,
                    logined_user = logined_user,



                };
                return View(data);
            }

            else
            {
                return RedirectToAction("login_form");
            }
        }
        [HttpPost]
        public IActionResult update_buget(BudgetAdherence _budget)
        {
            _con.tbl_bugetaherence.Update(_budget);
            _con.SaveChanges();
            return RedirectToAction("budget_details");
        }
        public IActionResult Schedule_details()
        {
            var login = HttpContext.Session.GetString("user_session");
            if (login != null)
            {
                var logined_user = _con.tbl_employee.Where(p => p.id == int.Parse(login)).ToList();
                var schedule = _con.tbl_ScheduleAdherence.ToList();
                mainModel data = new mainModel()
                {
                    scheduleAdherences = schedule,
                    logined_user = logined_user,



                };
                return View(data);
            }

            else
            {
                return RedirectToAction("login_form");
            }

        }
        [HttpPost]
        public IActionResult add_Schedule(ScheduleAdherence schedule)
        {
            _con.tbl_ScheduleAdherence.Add(schedule);
            _con.SaveChanges();
            return RedirectToAction("Schedule_details");
        }
        public IActionResult delete_Schedule(int id)
        {
            var del = _con.tbl_ScheduleAdherence.Find(id);
            _con.tbl_ScheduleAdherence.Remove(del);
            _con.SaveChanges();
            return RedirectToAction("Schedule_details");
        }
        public IActionResult ScheduleStatus(int id)
        {
            var find_id = _con.tbl_ScheduleAdherence.Find(id);
            if (find_id.status == 1)
            {
                find_id.status = 0;
            }
            else
            {
                find_id.status = 1;

            }
            _con.SaveChanges();
            return RedirectToAction("Schedule_details");


        }
        public IActionResult update_Schedule(int id)
        {
            var login = HttpContext.Session.GetString("user_session");
            if (login != null)
            {
                var logined_user = _con.tbl_employee.Where(p => p.id == int.Parse(login)).ToList();

                var findId = _con.tbl_ScheduleAdherence.Find(id);
                mainModel data = new mainModel()
                {
                    scheduleAdherences_data = findId,
                    logined_user = logined_user,



                };
                return View(data);
            }

            else
            {
                return RedirectToAction("login_form");
            }
        }
        [HttpPost]
        public IActionResult update_Schedule(ScheduleAdherence schedule)
        {
            _con.tbl_ScheduleAdherence.Update(schedule);
            _con.SaveChanges();
            return RedirectToAction("Schedule_details");
        }
        public IActionResult audit_projectDetails(string search_text)
        {
            var login = HttpContext.Session.GetString("user_session");
            if (login != null)
            {
                var logined_user = _con.tbl_employee.Where(p => p.id == int.Parse(login)).ToList();


                List<audit_Project> auditproject_data = new List<audit_Project>();
                if (string.IsNullOrEmpty(search_text))
                {
                    auditproject_data = _con.tbl_auditProject.Include(p => p.project).ToList();
                }

                else
                {

                }
                if (auditproject_data.Count == 0)
                {
                    TempData["msg"] = $"sorry the record is not found {search_text} ";
                }
                var project = _con.tbl_audit.Include(a => a.projectDetails).ToList();
                var buget = _con.tbl_bugetaherence.Where(p => p.status == 1).ToList();
                var schdelu = _con.tbl_ScheduleAdherence.Where(p => p.status == 1).ToList();
                var ststus = _con.tbl_projectStatus.ToList();
                mainModel data = new mainModel()
                {
                    audit_datails = project,
                    auditproject_details = auditproject_data,
                    logined_user = logined_user,
                    scheduleAdherences = schdelu,
                    buget_details = buget,
                    pro_Statusdetail = ststus

                };
                return View(data);
            }

            else
            {
                return RedirectToAction("login_form");
            }

        }
        public IActionResult addproject_audit()
        {
            var login = HttpContext.Session.GetString("user_session");
            if (login != null)
            {
                var logined_user = _con.tbl_employee.Where(p => p.id == int.Parse(login)).ToList();

                var project = _con.tbl_audit.Include(a => a.projectDetails).ToList();
                var buget = _con.tbl_bugetaherence.Where(p => p.status == 1).ToList();
                var schdelu = _con.tbl_ScheduleAdherence.Where(p => p.status == 1).ToList();
                var ststus = _con.tbl_projectStatus.ToList();
                mainModel data = new mainModel()
                {
                    audit_datails = project,

                    logined_user = logined_user,
                    scheduleAdherences = schdelu,
                    buget_details = buget,
                    pro_Statusdetail = ststus

                };
                return View(data);
            }

            else
            {
                return RedirectToAction("login_form");
            }
        }
        [HttpPost]
        public IActionResult addproject_audit(audit_Project _project)
        {
            _con.tbl_auditProject.Add(_project);
            _con.SaveChanges();
            TempData["msg"] = "Audit project added successfully!";
            return RedirectToAction("addproject_audit");
        }
        public IActionResult delete_auditproject(int id)
        {
            var find_id = _con.tbl_auditProject.Find(id);
            _con.tbl_auditProject.Remove(find_id);
            _con.SaveChanges();
            return RedirectToAction("audit_projectDetails");
        }
        public IActionResult view_aduitproject(int id)
        {
            var login = HttpContext.Session.GetString("user_session");
            if (login != null)
            {
                var logined_user = _con.tbl_employee.Where(p => p.id == int.Parse(login)).ToList();
                var find_id = _con.tbl_auditProject.Find(id);

                var project = _con.tbl_projects.FirstOrDefault(p => p.Project_id == find_id.project_id);
                var status = _con.tbl_projectStatus.FirstOrDefault(a => a.status_id == find_id.project_Status);
                var budget = _con.tbl_bugetaherence.FirstOrDefault(b => b.id == find_id.buget_id);
                var sechulde = _con.tbl_ScheduleAdherence.FirstOrDefault(b => b.id == find_id.schedule_id);
                mainModel data = new mainModel()
                {
                    scheduleAdherences_data = sechulde,
                    buget_data = budget,
                    proStatus_data = status,
                    project_data = project,
                    auditproject_data = find_id,
                    logined_user = logined_user,



                };
                return View(data);
            }

            else
            {
                return RedirectToAction("login_form");
            }
        }
        public IActionResult update_auditproject(int id)
        {
            var login = HttpContext.Session.GetString("user_session");
            if (login != null)
            {
                var logined_user = _con.tbl_employee.Where(p => p.id == int.Parse(login)).ToList();
                var find_id = _con.tbl_auditProject.Find(id);

                var project = _con.tbl_projects.FirstOrDefault(p => p.Project_id == find_id.project_id);
                var status = _con.tbl_projectStatus.FirstOrDefault(a => a.status_id == find_id.project_Status);
                var budget = _con.tbl_bugetaherence.FirstOrDefault(b => b.id == find_id.buget_id);
                var sechulde = _con.tbl_ScheduleAdherence.FirstOrDefault(b => b.id == find_id.schedule_id);
                var projectlist = _con.tbl_audit.Include(a => a.projectDetails).ToList();
                var buget = _con.tbl_bugetaherence.Where(p => p.status == 1).ToList();
                var schdelu = _con.tbl_ScheduleAdherence.Where(p => p.status == 1).ToList();
                var ststus = _con.tbl_projectStatus.ToList();
                mainModel data = new mainModel()
                {
                    scheduleAdherences_data = sechulde,
                    buget_data = budget,
                    proStatus_data = status,
                    project_data = project,
                    auditproject_data = find_id,
                    logined_user = logined_user,
                    audit_datails = projectlist,

                    scheduleAdherences = schdelu,
                    buget_details = buget,
                    pro_Statusdetail = ststus


                };
                return View(data);
            }

            else
            {
                return RedirectToAction("login_form");
            }

        }
        [HttpPost]
        public IActionResult update_auditproject(audit_Project _pro)
        {
            _con.tbl_auditProject.Update(_pro);
            _con.SaveChanges();
            return RedirectToAction("audit_projectDetails");
        }
        public IActionResult StakeholderEngagement_details()
        {
            var login = HttpContext.Session.GetString("user_session");
            if (login != null)
            {
                var logined_user = _con.tbl_employee.Where(p => p.id == int.Parse(login)).ToList();

                var stakeholder = _con.tbl_StakeholderEngagement.ToList();

                mainModel data = new mainModel()
                {

                    StakeholderEngagement_details = stakeholder,
                    logined_user = logined_user,



                };
                return View(data);
            }

            else
            {
                return RedirectToAction("login_form");
            }
        }
        [HttpPost]
        public IActionResult add_StakeholderEngagement(StakeholderEngagement _sem)
        {
            _con.tbl_StakeholderEngagement.Add(_sem);
            _con.SaveChanges();
            return RedirectToAction("StakeholderEngagement_details");
        }
        public IActionResult delete_StakeholderEngagement(int id)
        {
            var find_id = _con.tbl_StakeholderEngagement.Find(id);
            _con.tbl_StakeholderEngagement.Remove(find_id);
            _con.SaveChanges();
            return RedirectToAction("StakeholderEngagement_details");

        }
        public IActionResult StakeholderEngagementStatus(int id)
        {
            var find_id = _con.tbl_StakeholderEngagement.Find(id);
            if (find_id.status == 1)
            {
                find_id.status = 0;
            }
            else
            {
                find_id.status = 1;

            }
            _con.SaveChanges();
            return RedirectToAction("StakeholderEngagement_details");

        }
        public IActionResult update_StakeholderEngagement(int id)
        {
            var login = HttpContext.Session.GetString("user_session");
            if (login != null)
            {
                var logined_user = _con.tbl_employee.Where(p => p.id == int.Parse(login)).ToList();

                var stakeholder = _con.tbl_StakeholderEngagement.Find(id);

                mainModel data = new mainModel()
                {

                    StakeholderEngagement_data = stakeholder,
                    logined_user = logined_user,



                };
                return View(data);
            }

            else
            {
                return RedirectToAction("login_form");
            }
        }
        public IActionResult Communication_Effectivenes_details()
        {
            var login = HttpContext.Session.GetString("user_session");
            if (login != null)
            {
                var logined_user = _con.tbl_employee.Where(p => p.id == int.Parse(login)).ToList();

                var effectvences = _con.tbl_CommunicationEffectiveness.ToList();

                mainModel data = new mainModel()
                {

                    Communication_Effectivenes_details = effectvences,
                    logined_user = logined_user,



                };
                return View(data);
            }

            else
            {
                return RedirectToAction("login_form");
            }
        }
        [HttpPost]
        public IActionResult add_CommunicationEffectiveness(Communication_Effectivenes _Effectivenes)
        {
            _con.tbl_CommunicationEffectiveness.Add(_Effectivenes);
            _con.SaveChanges();
            return RedirectToAction("Communication_Effectivenes_details");
        }
        public IActionResult CommunicationEffectivenessStatus(int id)
        {
            var find_id = _con.tbl_CommunicationEffectiveness.Find(id);
            if (find_id.status == 1)
            {
                find_id.status = 0;
            }
            else
            {
                find_id.status = 1;

            }
            _con.SaveChanges();
            return RedirectToAction("Communication_Effectivenes_details");
        }
        public IActionResult update_CommunicationEffectiveness(int id)
        {
            var login = HttpContext.Session.GetString("user_session");
            if (login != null)
            {
                var logined_user = _con.tbl_employee.Where(p => p.id == int.Parse(login)).ToList();

                var _Effectivenes = _con.tbl_CommunicationEffectiveness.Find(id);

                mainModel data = new mainModel()
                {

                    Communication_Effectivenes_data = _Effectivenes,
                    logined_user = logined_user,



                };
                return View(data);
            }

            else
            {
                return RedirectToAction("login_form");
            }
        }
        [HttpPost]
        public IActionResult update_CommunicationEffectiveness(Communication_Effectivenes _Effectivenes)
        {
            _con.tbl_CommunicationEffectiveness.Update(_Effectivenes);
            _con.SaveChanges();
            return RedirectToAction("Communication_Effectivenes_details");
        }
        public IActionResult risk_mangamentDetails(string search_text)
        {
            var login = HttpContext.Session.GetString("user_session");
            if (login != null)
            {
                var logined_user = _con.tbl_employee.Where(p => p.id == int.Parse(login)).ToList();


                List<riskmangament> riskmangament_data = new List<riskmangament>();
                if (string.IsNullOrEmpty(search_text))
                {
                    riskmangament_data = _con.tbl_riskmangament.Include(p => p.Stakeholder_Engagement).Include(p => p.Effectiveness_Engagement).ToList();
                }

                else
                {

                }
                if (riskmangament_data.Count == 0)
                {
                    TempData["msg"] = $"sorry the record is not found {search_text} ";
                }
                var risk = _con.tbl_riskmangament.ToList();
                var impactRisk = _con.tbl_impact_risk.ToList();
                var likdelihoodRisk = _con.tbl_likedhood_risk.ToList();
                var effetiveness = _con.tbl_CommunicationEffectiveness.Where(p => p.status == 1).ToList();
                var stakholder = _con.tbl_StakeholderEngagement.Where(p => p.status == 1).ToList();
                mainModel data = new mainModel()
                {
                    StakeholderEngagement_details = stakholder,
                    Communication_Effectivenes_details = effetiveness,
                    riskmangament_details = riskmangament_data,
                    impact_Risks = impactRisk,

                    Likelihood_risk = likdelihoodRisk,
                    logined_user = logined_user,


                };
                return View(data);
            }

            else
            {
                return RedirectToAction("login_form");
            }
        }
        public IActionResult addRisk_Mangement()
        {
            var login = HttpContext.Session.GetString("user_session");
            if (login != null)
            {
                var logined_user = _con.tbl_employee.Where(p => p.id == int.Parse(login)).ToList();

                var risk = _con.tbl_riskmangament.ToList();
                var impactRisk = _con.tbl_impact_risk.ToList();
                var likdelihoodRisk = _con.tbl_likedhood_risk.ToList();
                var effetiveness = _con.tbl_CommunicationEffectiveness.Where(p => p.status == 1).ToList();
                var stakholder = _con.tbl_StakeholderEngagement.Where(p => p.status == 1).ToList();
                mainModel data = new mainModel()
                {
                    StakeholderEngagement_details = stakholder,
                    Communication_Effectivenes_details = effetiveness,

                    impact_Risks = impactRisk,

                    Likelihood_risk = likdelihoodRisk,
                    logined_user = logined_user,


                };
                return View(data);
            }

            else
            {
                return RedirectToAction("login_form");
            }


        }
        [HttpPost]
        public IActionResult addRisk_Mangement(riskmangament _risk)
        {

            _con.tbl_riskmangament.Add(_risk);
            _con.SaveChanges();
            return RedirectToAction("risk_mangamentDetails");
        }
        public IActionResult delete_riskmang(int id)
        {
            var del = _con.tbl_riskmangament.Find(id);
            _con.tbl_riskmangament.Remove(del);
            _con.SaveChanges();
            return RedirectToAction("risk_mangamentDetails");

        }
        public IActionResult view_riskmang(int id)
        {
            var login = HttpContext.Session.GetString("user_session");
            if (login != null)
            {
                var logined_user = _con.tbl_employee.Where(p => p.id == int.Parse(login)).ToList();
                var find_id = _con.tbl_riskmangament.Find(id);
                var effetivencess = _con.tbl_CommunicationEffectiveness.FirstOrDefault(p => p.id == find_id.Effectiveness_id);
                var stakeholder = _con.tbl_StakeholderEngagement.FirstOrDefault(p => p.id == find_id.Stakeholder_id);
                mainModel data = new mainModel()
                {
                    riskmangament_data = find_id,
                    Communication_Effectivenes_data = effetivencess,
                    StakeholderEngagement_data = stakeholder,
                    logined_user = logined_user,

                };
                return View(data);
            }

            else
            {
                return RedirectToAction("login_form");
            }

        }
        public IActionResult update_riskmang(int id)
        {
            var login = HttpContext.Session.GetString("user_session");
            if (login != null)
            {
                var risk = _con.tbl_riskmangament.ToList();
                var impactRisk = _con.tbl_impact_risk.ToList();
                var likdelihoodRisk = _con.tbl_likedhood_risk.ToList();
                var effetiveness = _con.tbl_CommunicationEffectiveness.Where(p => p.status == 1).ToList();
                var stakholder = _con.tbl_StakeholderEngagement.Where(p => p.status == 1).ToList();
                var logined_user = _con.tbl_employee.Where(p => p.id == int.Parse(login)).ToList();
                var find_id = _con.tbl_riskmangament.Find(id);
                var effetivencess = _con.tbl_CommunicationEffectiveness.FirstOrDefault(p => p.id == find_id.Effectiveness_id);
                var stakeholder = _con.tbl_StakeholderEngagement.FirstOrDefault(p => p.id == find_id.Stakeholder_id);
                mainModel data = new mainModel()
                {
                    StakeholderEngagement_details = stakholder,
                    Communication_Effectivenes_details = effetiveness,

                    impact_Risks = impactRisk,

                    Likelihood_risk = likdelihoodRisk,
                    riskmangament_data = find_id,
                    Communication_Effectivenes_data = effetivencess,
                    StakeholderEngagement_data = stakeholder,
                    logined_user = logined_user,

                };
                return View(data);
            }

            else
            {
                return RedirectToAction("login_form");
            }
        }
        [HttpPost]
        public IActionResult update_riskmang(riskmangament _risk)
        {
            _con.tbl_riskmangament.Update(_risk);
            _con.SaveChanges();
            return RedirectToAction("risk_mangamentDetails");
        }
        public IActionResult complianceStatusDetails()
        {
            var login = HttpContext.Session.GetString("user_session");
            if (login != null)
            {
                var logined_user = _con.tbl_employee.Where(p => p.id == int.Parse(login)).ToList();

                var status = _con.tbl_compliancestatus.ToList();
                mainModel data = new mainModel
                {
                    logined_user = logined_user,
                    complianceStatus_details = status,
                };

                return View(data);
            }
            else
            {
                return RedirectToAction("login_form");
            }

        }
        [HttpPost]
        public IActionResult addcompliance_status(compliance_status _Status)
        {
            _con.tbl_compliancestatus.Add(_Status);
            _con.SaveChanges();
            return RedirectToAction("complianceStatusDetails");
        }
        public IActionResult delete_complianceStatus(int id)
        {
            var del = _con.tbl_compliancestatus.Find(id);
            _con.tbl_compliancestatus.Remove(del);

            _con.SaveChanges();
            return RedirectToAction("complianceStatusDetails");
        }
        public IActionResult complianceStatusUpdate(int id)
        {
            var find_Id = _con.tbl_compliancestatus.Find(id);
            if (find_Id.status == 1)
            {
                find_Id.status = 0;
            }
            else
            {
                find_Id.status = 1;
            }
            _con.SaveChanges();
            return RedirectToAction("complianceStatusDetails");
        }

        public IActionResult update_complienceStatus(int id)
        {
            var login = HttpContext.Session.GetString("user_session");
            if (login != null)
            {
                var logined_user = _con.tbl_employee.Where(p => p.id == int.Parse(login)).ToList();
                var find_id = _con.tbl_compliancestatus.Find(id);
                mainModel data = new mainModel
                {
                    logined_user = logined_user,
                    complianceStatus_data = find_id,
                };
                return View(data);
            }
            else
            {
                return RedirectToAction("login_form");
            }
        }
        [HttpPost]
        public IActionResult update_complienceStatus(compliance_status _status)
        {
            _con.tbl_compliancestatus.Update(_status);
            _con.SaveChanges();
            return RedirectToAction("complianceStatusDetails");
        }
        public IActionResult ComplianceStandardsDetails()
        {
            var login = HttpContext.Session.GetString("user_session");
            if (login != null)
            {
                var logined_user = _con.tbl_employee.Where(p => p.id == int.Parse(login)).ToList();

                var standard = _con.tbl_complianceandStandards.Include(a => a.status).ToList();
                var status = _con.tbl_compliancestatus.Where(p => p.status == 1).ToList();
                mainModel data = new mainModel
                {
                    logined_user = logined_user,
                    complianceStatus_details = status,
                    ComplianceStandards_details = standard
                };

                return View(data);
            }
            else
            {
                return RedirectToAction("login_form");
            }
        }
        public IActionResult addComplianceStandards()
        {
            var login = HttpContext.Session.GetString("user_session");
            if (login != null)
            {
                var logined_user = _con.tbl_employee.Where(p => p.id == int.Parse(login)).ToList();

                var standard = _con.tbl_complianceandStandards.Include(a => a.status).ToList();
                var status = _con.tbl_compliancestatus.Where(p => p.status == 1).ToList();
                mainModel data = new mainModel
                {
                    logined_user = logined_user,
                    complianceStatus_details = status,
                    ComplianceStandards_details = standard
                };

                return View(data);
            }
            else
            {
                return RedirectToAction("login_form");
            }
        }
        [HttpPost]
        public IActionResult addComplianceStandards(ComplianceStandards _standard, IFormFile Evidence)
        {
            if (Evidence != null)
            {
                string file_extension = Path.GetExtension(Evidence.FileName);
                if (file_extension == ".pdf" || file_extension == ".docx" || file_extension == ".txt" || file_extension == ".xlsx" || file_extension == ".png" || file_extension == ".jpg" || file_extension == ".jpeg")
                {
                    Random ran = new Random();
                    string fileNAme = Path.GetFileName(Evidence.FileName);

                    string filepath = Path.Combine(_evn.WebRootPath, "documents/Evidence", fileNAme);
                    using (FileStream fs = new FileStream(filepath, FileMode.Create)) // Use 'using' for proper disposal
                    {
                        Evidence.CopyTo(fs);
                    }

                    _standard.Evidence = fileNAme;

                }
                else
                {
                    TempData["msg1"] = "This type of file is not supported !";
                    return RedirectToAction("addComplianceStandards");
                }
            }



            _con.tbl_complianceandStandards.Add(_standard);
            _con.SaveChanges();
            return RedirectToAction("ComplianceStandardsDetails");
        }
        public IActionResult delete_complianceStandard(int id)
        {
            var del = _con.tbl_complianceandStandards.Find(id);
            _con.tbl_complianceandStandards.Remove(del);
            _con.SaveChanges();
            return RedirectToAction("ComplianceStandardsDetails");

        }
        public IActionResult update_complianceStandard(int id)
        {
            var login = HttpContext.Session.GetString("user_session");
            if (login != null)
            {
                var logined_user = _con.tbl_employee.Where(p => p.id == int.Parse(login)).ToList();
                var find_id = _con.tbl_complianceandStandards.Find(id);
                var status = _con.tbl_compliancestatus.Where(p => p.status == 1).ToList();
                var selected_status = _con.tbl_compliancestatus.FirstOrDefault(p => p.id == find_id.compliacne_id);
                mainModel data = new mainModel
                {
                    logined_user = logined_user,
                    complianceStatus_details = status,
                    ComplianceStandards_data = find_id,
                    complianceStatus_data = selected_status,
                };
                return View(data);
            }
            else
            {
                return RedirectToAction("login_form");
            }
        }
        [HttpPost]
        public IActionResult update_complianceStandard(ComplianceStandards _standard, IFormFile Evidence)
        {
            if (Evidence != null)
            {
                string file_extension = Path.GetExtension(Evidence.FileName);
                if (file_extension == ".pdf" || file_extension == ".docx" || file_extension == ".txt" || file_extension == ".xlsx" || file_extension == ".png" || file_extension == ".jpg" || file_extension == ".jpeg")
                {
                    Random ran = new Random();
                    string fileNAme = Path.GetFileName(Evidence.FileName);

                    string filepath = Path.Combine(_evn.WebRootPath, "documents/Evidence", fileNAme);
                    using (FileStream fs = new FileStream(filepath, FileMode.Create)) // Use 'using' for proper disposal
                    {
                        Evidence.CopyTo(fs);
                    }

                    _standard.Evidence = fileNAme;

                }
                else
                {
                    TempData["msg1"] = "This type of file is not supported !";
                    return RedirectToAction("audit_details");
                }
            }



            _con.tbl_complianceandStandards.Update(_standard);
            _con.SaveChanges();
            return RedirectToAction("ComplianceStandardsDetails");
        }
        public IActionResult IssueSeverityDetails()
        {
            var login = HttpContext.Session.GetString("user_session");
            if (login != null)
            {
                var logined_user = _con.tbl_employee.Where(p => p.id == int.Parse(login)).ToList();

                var IssueSeverity = _con.tbl_issueSeverity.ToList();
                mainModel data = new mainModel
                {
                    logined_user = logined_user,
                    IssueSeverity_details = IssueSeverity,
                };

                return View(data);
            }
            else
            {
                return RedirectToAction("login_form");
            }

        }
        [HttpPost]
        public IActionResult add_IssueSeverity(IssueSeverity _issue)
        {
            _con.tbl_issueSeverity.Add(_issue);
            _con.SaveChanges();
            return RedirectToAction("IssueSeverityDetails");
        }
        public IActionResult delete_issue(int id)
        {
            var del = _con.tbl_issueSeverity.Find(id);
            _con.tbl_issueSeverity.Remove(del);
            _con.SaveChanges();
            return RedirectToAction("IssueSeverityDetails");
        }
        public IActionResult issueStatusUpdate(int id)
        {
            var find_Id = _con.tbl_issueSeverity.Find(id);
            if (find_Id.status == 1)
            {
                find_Id.status = 0;
            }
            else
            {
                find_Id.status = 1;
            }
            _con.SaveChanges();
            return RedirectToAction("IssueSeverityDetails");
        }
        public IActionResult update_issueStatus(int id)
        {
            var login = HttpContext.Session.GetString("user_session");
            if (login != null)
            {
                var logined_user = _con.tbl_employee.Where(p => p.id == int.Parse(login)).ToList();
                var find_id = _con.tbl_issueSeverity.Find(id);
                mainModel data = new mainModel
                {
                    logined_user = logined_user,
                    IssueSeverity_data = find_id,
                };
                return View(data);
            }
            else
            {
                return RedirectToAction("login_form");
            }
        }
        [HttpPost]
        public IActionResult update_issueStatus(IssueSeverity _status)
        {
            _con.tbl_issueSeverity.Update(_status);
            _con.SaveChanges();
            return RedirectToAction("complianceStatusDetails");
        }
        public IActionResult ResolutionStatusDetails()
        {
            var login = HttpContext.Session.GetString("user_session");
            if (login != null)
            {
                var logined_user = _con.tbl_employee.Where(p => p.id == int.Parse(login)).ToList();

                var status = _con.tbl_ResolutionStatus.ToList();
                mainModel data = new mainModel
                {
                    logined_user = logined_user,
                    ResolutionStatus_details = status
                };

                return View(data);
            }
            else
            {
                return RedirectToAction("login_form");
            }

        }
        [HttpPost]
        public IActionResult add_ResolutionStatus(ResolutionStatus _status)
        {
            _con.tbl_ResolutionStatus.Add(_status);
            _con.SaveChanges();
            return RedirectToAction("ResolutionStatusDetails");
        }
        public IActionResult update_resolutionStatus(int id)
        {
            var find_Id = _con.tbl_ResolutionStatus.Find(id);
            if (find_Id.status == 1)
            {
                find_Id.status = 0;
            }
            else
            {
                find_Id.status = 1;
            }
            _con.SaveChanges();
            return RedirectToAction("ResolutionStatusDetails");
        }
        public IActionResult update_resStatus(int id)
        {
            var login = HttpContext.Session.GetString("user_session");
            if (login != null)
            {
                var logined_user = _con.tbl_employee.Where(p => p.id == int.Parse(login)).ToList();
                var find_id = _con.tbl_ResolutionStatus.Find(id);
                mainModel data = new mainModel
                {
                    logined_user = logined_user,
                    ResolutionStatus_data = find_id,
                };
                return View(data);
            }
            else
            {
                return RedirectToAction("login_form");
            }
        }
        [HttpPost]
        public IActionResult update_resStatus(ResolutionStatus _status)
        {
            _con.tbl_ResolutionStatus.Update(_status);
            _con.SaveChanges();
            return RedirectToAction("ResolutionStatusDetails");
        }
        public IActionResult delete_resolution(int id)
        {
            var del = _con.tbl_ResolutionStatus.Find(id);
            _con.tbl_ResolutionStatus.Remove(del);
            _con.SaveChanges();
            return RedirectToAction("ResolutionStatusDetails");
        }
        public IActionResult issueandchallenges_Details()
        {
            var login = HttpContext.Session.GetString("user_session");
            if (login != null)
            {
                var logined_user = _con.tbl_employee.Where(p => p.id == int.Parse(login)).ToList();
                var issue = _con.tbl_issue.ToList();
                var res = _con.tbl_ResolutionStatus.Where(p => p.status == 1).ToList();
                var sov_issue = _con.tbl_issueSeverity.Where(p => p.status == 1).ToList();
                mainModel data = new mainModel
                {
                    IssueSeverity_details = sov_issue,
                    ResolutionStatus_details = res,
                    issues_Detail = issue,
                    logined_user = logined_user,

                };
                return View(data);
            }
            else
            {
                return RedirectToAction("login_form");
            }
        }
        public IActionResult addissue_challenge()
        {
            var login = HttpContext.Session.GetString("user_session");
            if (login != null)
            {
                var logined_user = _con.tbl_employee.Where(p => p.id == int.Parse(login)).ToList();
                var issue = _con.tbl_issue.ToList();
                var res = _con.tbl_ResolutionStatus.Where(p => p.status == 1).ToList();
                var sov_issue = _con.tbl_issueSeverity.Where(p => p.status == 1).ToList();
                mainModel data = new mainModel
                {
                    IssueSeverity_details = sov_issue,
                    ResolutionStatus_details = res,
                    issues_Detail = issue,
                    logined_user = logined_user,

                };
                return View(data);
            }
            else
            {
                return RedirectToAction("login_form");
            }
        }
        [HttpPost]
        public IActionResult addissue_challenge(Issues_Challenges _issue)
        {
            _con.tbl_issue.Add(_issue);
            _con.SaveChanges();
            return RedirectToAction("issueandchallenges_Details");
        }
        public IActionResult delete_issuechallenge(int id)
        {
            var del = _con.tbl_issue.Find(id);
            _con.tbl_issue.Remove(del);
            _con.SaveChanges();
            return RedirectToAction("issueandchallenges_Details");
        }
        public IActionResult update_issuechallenge(int id)
        {
            var login = HttpContext.Session.GetString("user_session");
            if (login != null)
            {
                var logined_user = _con.tbl_employee.Where(p => p.id == int.Parse(login)).ToList();
                var issue = _con.tbl_issue.Find(id);

                var res = _con.tbl_ResolutionStatus.Where(p => p.status == 1).ToList();
                var sov_issue = _con.tbl_issueSeverity.Where(p => p.status == 1).ToList();
                mainModel data = new mainModel
                {
                    IssueSeverity_details = sov_issue,
                    ResolutionStatus_details = res,
                    issues_Data = issue,
                    logined_user = logined_user,

                };
                return View(data);
            }
            else
            {
                return RedirectToAction("login_form");
            }
        }
        [HttpPost]
        public IActionResult update_issuechallenge(Issues_Challenges _issue)
        {
            _con.tbl_issue.Update(_issue);
            _con.SaveChanges();
            return RedirectToAction("issueandchallenges_Details");
        }

        public IActionResult correctiveMeasurePrority()
        {
            var login = HttpContext.Session.GetString("user_session");
            if (login != null)
            {
                var priority = _con.tbl_measurePrority.ToList();
                var logined_user = _con.tbl_employee.Where(p => p.id == int.Parse(login)).ToList();

                mainModel data = new mainModel()
                {
                    correctiveMeasures_PriorityDetails = priority,
                    logined_user = logined_user,
                };
                return View(data);
            }
            else
            {
                return RedirectToAction("login_form");
            }


        }
        [HttpPost]
        public IActionResult addCorrectiveMeasures_priority(CorrectiveMeasures_priority _pro)
        {
            _con.tbl_measurePrority.Add(_pro);
            _con.SaveChanges();
            return RedirectToAction("correctiveMeasurePrority");
        }
        public IActionResult delete_measurepriority(int id)
        {
            var del = _con.tbl_measurePrority.Find(id);
            _con.tbl_measurePrority.Remove(del);
            _con.SaveChanges();
            return RedirectToAction("correctiveMeasurePrority");
        }
        public IActionResult measurePriorityStatus(int id)
        {
            var find_id = _con.tbl_measurePrority.Find(id);
            if (find_id.status == 1)
            {
                find_id.status = 0;
            }
            else
            {
                find_id.status = 1;

            }
            _con.SaveChanges();
            return RedirectToAction("correctiveMeasurePrority");
        }
        public IActionResult update_measurepriority(int id)
        {
            var login = HttpContext.Session.GetString("user_session");
            if (login != null)
            {
                var find_id = _con.tbl_measurePrority.Find(id);

                var logined_user = _con.tbl_employee.Where(p => p.id == int.Parse(login)).ToList();
                mainModel data = new mainModel()
                {
                    logined_user = logined_user,
                    correctiveMeasures_PriorityData = find_id
                };
                return View(data);
            }
            else
            {
                return RedirectToAction("login_form");
            }
        }
        [HttpPost]
        public IActionResult update_measurepriority(CorrectiveMeasures_priority _pro)
        {
            _con.tbl_measurePrority.Update(_pro);
            _con.SaveChanges();
            return RedirectToAction("correctiveMeasurePrority");
        }

        public IActionResult CorrectiveMeasures_Details()
        {
            var login = HttpContext.Session.GetString("user_session");
            if (login != null)
            {
                var priority = _con.tbl_measurePrority.Where(a => a.status == 1).ToList();
                var logined_user = _con.tbl_employee.Where(p => p.id == int.Parse(login)).ToList();
                var corretiveMeasure = _con.tbl_auditCorrectiveMeasures.ToList();
                mainModel data = new mainModel()
                {
                    correctiveMeasures_PriorityDetails = priority,
                    logined_user = logined_user,
                    correctiveMeasures_Details = corretiveMeasure,
                };
                return View(data);
            }
            else
            {
                return RedirectToAction("login_form");
            }
        }
        public IActionResult addCorrectiveMeasures()
        {
            var login = HttpContext.Session.GetString("user_session");
            if (login != null)
            {
                var priority = _con.tbl_measurePrority.Where(a => a.status == 1).ToList();
                var logined_user = _con.tbl_employee.Where(p => p.id == int.Parse(login)).ToList();
                var corretiveMeasure = _con.tbl_auditCorrectiveMeasures.ToList();
                mainModel data = new mainModel()
                {
                    correctiveMeasures_PriorityDetails = priority,
                    logined_user = logined_user,
                    correctiveMeasures_Details = corretiveMeasure,
                };
                return View(data);
            }
            else
            {
                return RedirectToAction("login_form");
            }
        }
        [HttpPost]
        public IActionResult addCorrectiveMeasures(CorrectiveMeasures _measure)
        {
            _con.tbl_auditCorrectiveMeasures.Add(_measure);
            _con.SaveChanges();
            return RedirectToAction("CorrectiveMeasures_Details");
        }
        public IActionResult delete_auditmeasure(int id)
        {
            var del = _con.tbl_auditCorrectiveMeasures.Find(id);
            _con.tbl_auditCorrectiveMeasures.Remove(del);
            _con.SaveChanges();
            return RedirectToAction("CorrectiveMeasures_Details");

        }
        public IActionResult update_auditmeasure(int id)
        {
            var login = HttpContext.Session.GetString("user_session");
            if (login != null)
            {
                var priority = _con.tbl_measurePrority.Where(a => a.status == 1).ToList();
                var logined_user = _con.tbl_employee.Where(p => p.id == int.Parse(login)).ToList();
                var corretiveMeasure = _con.tbl_auditCorrectiveMeasures.Find(id);
                var priority_selected = _con.tbl_measurePrority.FirstOrDefault(p => p.id == corretiveMeasure.priority_id);
                mainModel data = new mainModel()
                {
                    correctiveMeasures_PriorityData = priority_selected,
                    correctiveMeasures_PriorityDetails = priority,
                    logined_user = logined_user,
                    correctiveMeasures_Data = corretiveMeasure,
                };
                return View(data);
            }
            else
            {
                return RedirectToAction("login_form");
            }
        }
        public IActionResult auditRating()
        {
            var login = HttpContext.Session.GetString("user_session");
            if (login != null)
            {
                var priority = _con.tbl_measurePrority.ToList();
                var logined_user = _con.tbl_employee.Where(p => p.id == int.Parse(login)).ToList();
                var rating = _con.tbl_auditRating.ToList();
                mainModel data = new mainModel()
                {
                    Audit_Rating_Details = rating,
                    correctiveMeasures_PriorityDetails = priority,
                    logined_user = logined_user,
                };
                return View(data);
            }
            else
            {
                return RedirectToAction("login_form");
            }


        }
        public IActionResult delete_rating(int id)
        {
            var del = _con.tbl_auditRating.Find(id);
            _con.tbl_auditRating.Remove(del);
            _con.SaveChanges();
            return RedirectToAction("auditRating");
        }

        [HttpPost]
        public IActionResult addaudit_rating(Audit_Rating _rating)
        {
            _con.tbl_auditRating.Add(_rating);
            _con.SaveChanges();
            return RedirectToAction("auditRating");
        }
        public IActionResult AuditratingStatus(int id)
        {
            var find_id = _con.tbl_auditRating.Find(id);
            if (find_id.status == 1)
            {
                find_id.status = 0;
            }
            else
            {
                find_id.status = 1;

            }
            _con.SaveChanges();
            return RedirectToAction("auditRating");
        }
        public IActionResult update_Auditrating(int id)
        {
            var login = HttpContext.Session.GetString("user_session");
            if (login != null)
            {
                var find_id = _con.tbl_auditRating.Find(id);

                var logined_user = _con.tbl_employee.Where(p => p.id == int.Parse(login)).ToList();
                mainModel data = new mainModel()
                {
                    logined_user = logined_user,
                    Audit_Rating_Data = find_id,
                };
                return View(data);
            }
            else
            {
                return RedirectToAction("login_form");
            }
        }
        [HttpPost]
        public IActionResult update_Auditrating(Audit_Rating _rating)
        {
            _con.tbl_auditRating.Update(_rating);
            _con.SaveChanges();
            return RedirectToAction("auditRating");
        }
        public IActionResult auditConclusion()
        {
            var login = HttpContext.Session.GetString("user_session");
            if (login != null)
            {
                var conlusion = _con.tblaudit_Conclusions.Include(o => o._Rating).ToList();
                var audit_rating = _con.tbl_auditRating.Where(p => p.status == 1).ToList();

                var logined_user = _con.tbl_employee.Where(p => p.id == int.Parse(login)).ToList();
                mainModel data = new mainModel()
                {
                    Audit_Rating_Details = audit_rating,
                    logined_user = logined_user,
                    Audit_Conclusion_Details = conlusion,
                };
                return View(data);
            }
            else
            {
                return RedirectToAction("login_form");
            }
        }
        public IActionResult delete_AuditConclusion(int id)
        {
            var del = _con.tblaudit_Conclusions.Find(id);
            _con.tblaudit_Conclusions.Remove(del);
            _con.SaveChanges();
            return RedirectToAction("auditConclusion");
        }
        [HttpPost]
        public IActionResult addauditconclusion(Audit_Conclusion _Conclusion)
        {
            _con.tblaudit_Conclusions.Add(_Conclusion);
            _con.SaveChanges();
            return RedirectToAction("auditConclusion");
        }
        public IActionResult addauditconclusion()
        {
            var login = HttpContext.Session.GetString("user_session");
            if (login != null)
            {
                var conlusion = _con.tblaudit_Conclusions.Include(o => o._Rating).ToList();
                var audit_rating = _con.tbl_auditRating.Where(p => p.status == 1).ToList();

                var logined_user = _con.tbl_employee.Where(p => p.id == int.Parse(login)).ToList();
                mainModel data = new mainModel()
                {
                    Audit_Rating_Details = audit_rating,
                    logined_user = logined_user,
                    Audit_Conclusion_Details = conlusion,
                };
                return View(data);
            }
            else
            {
                return RedirectToAction("login_form");
            }
        }
        public IActionResult update_AuditConclusion(int id)
        {
            var login = HttpContext.Session.GetString("user_session");
            if (login != null)
            {
                var conlusion = _con.tblaudit_Conclusions.Include(o => o._Rating).ToList();
                var audit_rating = _con.tbl_auditRating.Where(p => p.status == 1).ToList();
                var find_id = _con.tblaudit_Conclusions.Find(id);
                var rating = _con.tbl_auditRating.FirstOrDefault(p => p.id == find_id.rating_id);
                var logined_user = _con.tbl_employee.Where(p => p.id == int.Parse(login)).ToList();
                mainModel data = new mainModel()
                {
                    Audit_Conclusion_Data = find_id,
                    Audit_Rating_Details = audit_rating,
                    logined_user = logined_user,
                    Audit_Conclusion_Details = conlusion,
                    Audit_Rating_Data = rating,
                };
                return View(data);
            }
            else
            {
                return RedirectToAction("login_form");
            }
        }
        [HttpPost]
        public IActionResult update_AuditConclusion(Audit_Conclusion _Conclusion)
        {
            _con.tblaudit_Conclusions.Update(_Conclusion);
            _con.SaveChanges();
            return RedirectToAction("auditConclusion");
        }
        [HttpGet]
        public IActionResult docs_details()
        {
            var login = HttpContext.Session.GetString("user_session");
            if (login != null)
            {
                var docs = _con.tbl_docs.Where(d => d.user_id == int.Parse(login)).ToList();
                if (docs.Count == 0)
                {
                    TempData["msg"] = "There is no document";
                }
                var logined_user = _con.tbl_employee.Where(p => p.id == int.Parse(login)).ToList();
                mainModel data = new mainModel()
                {
                    Docs_Details = docs,
                    logined_user = logined_user
                };
                return View(data);
            }
            else
            {
                return RedirectToAction("login_form");
            }
        }
        public IActionResult add_Document()
        {
            var login = HttpContext.Session.GetString("user_session");
            if (login != null)
            {
                var docs = _con.tbl_docs.ToList();
                var logined_user = _con.tbl_employee.Where(p => p.id == int.Parse(login)).ToList();
                mainModel data = new mainModel()
                {
                    Docs_Details = docs,
                    logined_user = logined_user
                };
                return View(data);
            }
            else
            {
                return RedirectToAction("login_form");
            }

        }
        [HttpPost]
        public IActionResult add_Document(Docs _docs)
        {
            var login = HttpContext.Session.GetString("user_session");
            _docs.user_id = int.Parse(login);
            _con.tbl_docs.Add(_docs);
            _con.SaveChanges();
            TempData["msg"] = "The document is added successfully";
            return RedirectToAction("add_Document");
        }
        public IActionResult delete_doc(int id)
        {
            var del = _con.tbl_docs.Find(id);
            _con.tbl_docs.Remove(del);
            _con.SaveChanges();
            return RedirectToAction("docs_details");
        }
        public IActionResult update_doc(int id)
        {
            var login = HttpContext.Session.GetString("user_session");
            if (login != null)
            {
                var docs = _con.tbl_docs.Find(id);
                var logined_user = _con.tbl_employee.Where(p => p.id == int.Parse(login)).ToList();
                mainModel data = new mainModel()
                {
                    Doc_Data = docs,
                    logined_user = logined_user
                };
                return View(data);
            }
            else
            {
                return RedirectToAction("login_form");
            }
        }
        [HttpPost]
        public IActionResult update_doc(Docs _docs)
        {
            var login = HttpContext.Session.GetString("user_session");
            _docs.user_id = int.Parse(login);
            _con.tbl_docs.Update(_docs);
            _con.SaveChanges();
            return RedirectToAction("docs_details");
        }
        public IActionResult doc_pdf(int id)
        {
            HtmlToPdf Docs_pdf = new HtmlToPdf();
            var find = _con.tbl_docs.Find(id);
            string doc_pdf = System.IO.File.ReadAllText(_evn.WebRootPath + "/htmlpage.html");
            Docs _doc = new Docs();
            _doc.Tittle = find.Tittle;
            _doc.Desciprtion = find.Desciprtion;
            doc_pdf = doc_pdf.Replace("{{tittle}}", _doc.Tittle);
            doc_pdf = doc_pdf.Replace("{{Desciprtion}}", _doc.Desciprtion);

            PdfDocument pdf = Docs_pdf.ConvertHtmlString(doc_pdf);
            var btyes = pdf.Save();

            return File(btyes, "application/pdf", find.Tittle + ".pdf");
        }
        [HttpPost]
        public IActionResult Duplicate_docs(int id)
        {
            var duplicatedDoc = _con.tbl_docs.Find(id);
            if (duplicatedDoc != null)
            {

                var newDoc = new Docs
                {
                    Tittle = duplicatedDoc.Tittle,
                    Desciprtion = duplicatedDoc.Desciprtion

                };
                _con.tbl_docs.Add(newDoc);
                _con.SaveChanges();

                return RedirectToAction("docs_details");
            }

            return RedirectToAction("Index");
        }
        [HttpPost]
        public IActionResult Fav_docs(int id, doc_favourite _docs)
        {
            var login = HttpContext.Session.GetString("user_session");
            var find_id = _con.tbl_docs.Find(id);
            var fav_data = _con.tbl_favdocs.FirstOrDefault(p => p.doc_id == find_id.id);
            if (login != null && fav_data == null)
            {
                _docs.doc_id = find_id.id;
                _docs.user_id = int.Parse(login);
                _con.tbl_favdocs.Add(_docs);
                _con.SaveChanges();
                TempData["msg1"] = "the document is added to favourite successfully";
            }
            else
            {
                TempData["msg1"] = "the document is  already added to favourite successfully";
            }
            return RedirectToAction("docs_details");
        }
        public IActionResult plan_typeDetails()
        {
            var login = HttpContext.Session.GetString("user_session");
            if (login != null)
            {
                var type = _con.tbl_plantypes.ToList();
                if (type.Count == 0)
                {
                    TempData["msg"] = "any plan type is doesnot found";
                }
                var logined_user = _con.tbl_employee.Where(p => p.id == int.Parse(login)).ToList();
                mainModel data = new mainModel()
                {
                    planType_Details = type,
                    logined_user = logined_user,
                };
                return View(data);
            }
            else
            {
                return RedirectToAction("login_form");
            }

        }
        [HttpPost]
        public IActionResult add_planType(plan_types _types)
        {
            _con.tbl_plantypes.Add(_types);
            _con.SaveChanges();
            return RedirectToAction("plan_typeDetails");
        }
        public IActionResult delete_plantype(int id)
        {
            var del = _con.tbl_plantypes.Find(id);
            _con.tbl_plantypes.Remove(del);
            _con.SaveChanges();
            return RedirectToAction("plan_typeDetails");
        }
        public IActionResult update_plantype(int id)
        {
            var login = HttpContext.Session.GetString("user_session");
            if (login != null)
            {
                var find_id = _con.tbl_plantypes.Find(id);
                var logined_user = _con.tbl_employee.Where(p => p.id == int.Parse(login)).ToList();
                mainModel data = new mainModel()
                {
                    planType_Data = find_id,
                    logined_user = logined_user,
                };
                return View(data);
            }
            else
            {
                return RedirectToAction("login_form");
            }
        }
        [HttpPost]
        public IActionResult update_plantype(plan_types _types)
        {
            _con.tbl_plantypes.Update(_types);
            _con.SaveChanges();
            return RedirectToAction("plan_typeDetails");
        }
        public IActionResult plan_typestatusUpdate(int id)
        {
            var find_id = _con.tbl_plantypes.Find(id);
            if (find_id.status == 1)
            {
                find_id.status = 0;
            }
            else
            {
                find_id.status = 1;

            }
            _con.SaveChanges();
            return RedirectToAction("plan_typeDetails");
        }
        public IActionResult plan_details()
        {
            var login = HttpContext.Session.GetString("user_session");
            if (login != null)
            {
                var plan_data = _con.tbl_plan.Include(e => e._team).ToList();
                if (plan_data.Count == 0)
                {
                    TempData["msg"] = "No plan is found.";
                }
                var logined_user = _con.tbl_employee.Where(p => p.id == int.Parse(login)).ToList();
                mainModel data = new mainModel()
                {
                    plan_Details = plan_data,

                    logined_user = logined_user,
                };
                return View(data);
            }
            else
            {
                return RedirectToAction("login_form");
            }

        }
        public IActionResult add_plan()
        {
            var login = HttpContext.Session.GetString("user_session");
            if (login != null)
            {
                var team = _con.tbl_teams.Where(e => e.status == 1).ToList();
                var type = _con.tbl_plantypes.Where(e => e.status == 1).ToList();
                var logined_user = _con.tbl_employee.Where(p => p.id == int.Parse(login)).ToList();

                mainModel data = new mainModel()
                {
                    team_details = team,
                    planType_Details = type,
                    logined_user = logined_user,
                };
                return View(data);
            }
            else
            {
                return RedirectToAction("login_form");
            }
        }
        [HttpPost]
        public IActionResult add_plan(plan _plan)
        {

            TempData["msg1"] = "the plan is added successfully";
            _con.tbl_plan.Add(_plan);
            _con.SaveChanges();
            return RedirectToAction("plan_details");
        }
        public IActionResult delete_plan(int id)
        {
            var del = _con.tbl_plan.Find(id);
            _con.tbl_plan.Remove(del);
            _con.SaveChanges();
            return RedirectToAction("plan_details");

        }
        public IActionResult update_plan(int id)
        {
            var login = HttpContext.Session.GetString("user_session");
            if (login != null)
            {
                var find_id = _con.tbl_plan.Find(id);
                var team = _con.tbl_teams.Where(e => e.status == 1).ToList();
                var selectedteam = _con.tbl_teams.FirstOrDefault(p => p.id == find_id.team_id);
                var type = _con.tbl_plantypes.Where(e => e.status == 1).ToList();
                var selectedtype = _con.tbl_plantypes.FirstOrDefault(p => p.id == find_id.type_id);
                var logined_user = _con.tbl_employee.Where(p => p.id == int.Parse(login)).ToList();
                mainModel data = new mainModel()
                {
                    plan_data = find_id,
                    planType_Data = selectedtype,
                    team_data = selectedteam,
                    team_details = team,
                    planType_Details = type,
                    logined_user = logined_user,
                };
                return View(data);
            }
            else
            {
                return RedirectToAction("login_form");
            }

        }
        [HttpPost]
        public IActionResult update_plan(plan _plan)
        {

            _con.tbl_plan.Update(_plan);
            _con.SaveChanges();
            return RedirectToAction("plan_details");
        }
        public IActionResult plan_report(int id)
        {
            var login = HttpContext.Session.GetString("user_session");
            if (login != null)
            {
                var find_id = _con.tbl_plan.Find(id);
                var plan_pillar = _con.tbl_plan_Pillars.Where(p => p.plan_id == find_id.id).ToList();
                var logined_user = _con.tbl_employee.Where(p => p.id == int.Parse(login)).ToList();
                if (plan_pillar.Count == 0)
                {
                    TempData["msg"] = "there is no plan pillar";
                };

                var task_plan = _con.tbl_tasks.Where(a => a.plan_id == find_id.id).ToList();
                if (task_plan.Count == 0)
                {
                    TempData["msg1"] = "there is no task";
                };
                var project_plan = _con.tbl_projects.Where(a => a.plan_id == find_id.id).ToList();
                if (project_plan.Count == 0)
                {
                    TempData["msg1"] = "There is no project";
                };

                var team_details = _con.tbl_teams.Where(p => p.status == 1).ToList();
                var emp = _con.tbl_employee.Where(p => p.status == 1).ToList();

                var _types = _con.tbl_task_types.Where(p => p.status == 1).ToList();
                var _status = _con.tbl_task_status.Where(p => p.status == 1).ToList();
                var _prority = _con.tbl_taskpriority.Where(p => p.status == 1).ToList();
                var _risk = _con.tbl_risks.ToList();

                var emp_data = _con.tbl_employee.Where(p => p.status == 1).ToList();
                var client_details = _con.tbl_clients.ToList();
                var status = _con.tbl_projectStatus.Where(p => p.status == 1).ToList();
                var priority = _con.tbl_projectpriority.Where(p => p.status == 1).ToList();
                var type = _con.tbl_project_types.Where(p => p.status == 1).ToList();

                mainModel data = new mainModel()
                {
                    logined_user = logined_user,
                    projects_types = type,
                    projects_prioritesDetails = priority,
                    pro_Statusdetail = status,

                    employee_details = emp_data,
                    team_details = team_details,
                    client_details = client_details,


                    risk_details = _risk,
                    task_details = task_plan,
                    task_prioritesDetails = _prority,
                    task_typedetails = _types,
                    task_statusdetails = _status,
                    project_details = project_plan,


                    planPillar_Details = plan_pillar,
                    plan_data = find_id,


                };
                return View(data);
            }
            else
            {
                return RedirectToAction("login_form");
            }
        }
        [HttpPost]
        public IActionResult add_planPillar(plan_pillars _plan)
        {
            _con.tbl_plan_Pillars.Add(_plan);
            _con.SaveChanges();
            return RedirectToAction("plan_report", new { id = _plan.plan_id });
        }
        public IActionResult del_planPillar(int id)
        {

            var del = _con.tbl_plan_Pillars.Find(id);

            if (del != null)
            {

                int planId = del.plan_id;

                _con.tbl_plan_Pillars.Remove(del);
                _con.SaveChanges();


                return RedirectToAction("plan_report", new { id = planId });
            }
            else
            {

                TempData["msg1"] = "Plan pillar not found.";
                return RedirectToAction("plan_report", new { id = id }); // Redirect back to the report with the original id
            }
        }
        public IActionResult edit_planPillar(int id)
        {
            var login = HttpContext.Session.GetString("user_session");
            if (login != null)
            {

                var plan_pillar = _con.tbl_plan_Pillars.Find(id);
                var logined_user = _con.tbl_employee.Where(p => p.id == int.Parse(login)).ToList();

                mainModel data = new mainModel()
                {
                    planPillar_data = plan_pillar,

                    logined_user = logined_user,
                };
                return View(data);
            }
            else
            {
                return RedirectToAction("login_form");
            }

        }
        [HttpPost]
        public IActionResult edit_planPillar(plan_pillars _Plan, int id)
        {

            var existingPlanPillar = _con.tbl_plan_Pillars.Find(id);

            if (existingPlanPillar != null)
            {

                existingPlanPillar.name = _Plan.name;
                existingPlanPillar.description = _Plan.description;
                _con.SaveChanges();
                return RedirectToAction("plan_report", new { id = existingPlanPillar.plan_id });
            }
            else
            {
                TempData["msg"] = "Plan pillar not found.";
                return RedirectToAction("plan_report", new { id = id }); // Redirect back to the report with the original id
            }
        }
        public IActionResult onepage_planDetails()
        {
            var login = HttpContext.Session.GetString("user_session");
            if (login != null)
            {
                var team = _con.tbl_teams.Where(e => e.status == 1).ToList();
                var type = _con.tbl_plantypes.Where(e => e.status == 1).ToList();
                var plan = _con.tbl_plan.ToList();
                var logined_user = _con.tbl_employee.Where(p => p.id == int.Parse(login)).ToList();
                var oneplan = _con.tbl_oneplan.Include(p => p.team_details).ToList();
                mainModel data = new mainModel()
                {
                    oneplan_Details = oneplan,
                    team_details = team,
                    plan_Details = plan,
                    planType_Details = type,
                    logined_user = logined_user,
                };
                return View(data);
            }
            else
            {
                return RedirectToAction("login_form");
            }

        }
        [HttpPost]
        public IActionResult add_oneplan(onepage_plan _Plan)
        {
            _con.tbl_oneplan.Add(_Plan);
            _con.SaveChanges();
            return RedirectToAction("onepage_planDetails");
        }
        [HttpPost]
        public IActionResult add_PlanTask(task _task, IFormFile document)
        {
            if (document != null) // Check if a file was actually uploaded
            {
                string file_extension = Path.GetExtension(document.FileName);
                if (file_extension == ".pdf" || file_extension == ".docx" || file_extension == ".txt")
                {
                    Random ran = new Random();
                    string fileNAme = Path.GetFileName(document.FileName);
                    var R_num = ran.Next(0000, 9999).ToString();
                    var name = fileNAme = R_num; // This line seems redundant; you're re-assigning fileNAme
                    string file_name = name + file_extension;
                    string filepath = Path.Combine(_evn.WebRootPath, "documents", file_name);
                    using (FileStream fs = new FileStream(filepath, FileMode.Create)) // Use 'using' for proper disposal
                    {
                        document.CopyTo(fs);
                    }

                    _task.document = file_name;
                    // Store the path in the project object
                }
                else
                {
                    TempData["msg1"] = "This type of file is not supported !";
                    return RedirectToAction("plan_report", new { id = _task.plan_id });
                }
            }
            // If document is null, the code will skip the file processing block.  pro object will be saved without file info.
            _task.created_date = DateTime.Now.ToString();
            _task.updated_date = DateTime.Now.ToString();

            _con.tbl_tasks.Add(_task);
            _con.SaveChanges();
            TempData["msg1"] = "The Task is successfully added now !";
            return RedirectToAction("plan_report", new { id = _task.plan_id });
        }

        public IActionResult update_PlanTask(int id)
        {
            var login = HttpContext.Session.GetString("user_session");
            if (login != null)
            {
                var logined_user = _con.tbl_employee.Where(p => p.id == int.Parse(login)).ToList();

                var task_datas = _con.tbl_tasks.Find(id);
                var project = _con.tbl_projects.ToList();
                var risk = _con.tbl_risks.ToList();
                var _taskstatus = _con.tbl_task_status.Where(p => p.status == 1).ToList();
                var status = _con.tbl_task_status.FirstOrDefault(p => p.id == task_datas.task_statuss);

                var project_info = _con.tbl_projects.FirstOrDefault(p => p.Project_id == task_datas.project_);
                var type = _con.tbl_task_types.Where(a => a.status == 1).ToList();
                var _typedata = _con.tbl_task_types.FirstOrDefault(t => t.id == task_datas.task_types);
                var _priority = _con.tbl_taskpriority.Where(p => p.status == 1).ToList();

                var _priority_selected = _con.tbl_taskpriority.FirstOrDefault(p => p.id == task_datas.task_priority);
                var _emp = _con.tbl_employee.Where(a => a.status == 1).ToList();
                var emp_data = _con.tbl_employee.FirstOrDefault(a => a.id == task_datas.employee_ids);
                mainModel _task_data = new mainModel
                {
                    project_details = project,
                    employee_details = _emp,
                    employee_data = emp_data,
                    task_statusdetails = _taskstatus,
                    task_statusdata = status,
                    project_data = project_info,
                    risk_details = risk,
                    logined_user = logined_user,
                    task_typedata = _typedata,
                    task_typedetails = type,
                    task_prioritesData = _priority_selected,
                    task_prioritesDetails = _priority,

                    task_data = _con.tbl_tasks.Find(id),

                };

                return View(_task_data);
            }
            else
            {
                return RedirectToAction("login_form");
            }


        }
        [HttpPost]
        public IActionResult update_PlanTask(task _task, IFormFile document)
        {
            if (document != null) // Check if a file was actually uploaded
            {
                string file_extension = Path.GetExtension(document.FileName);
                if (file_extension == ".pdf" || file_extension == ".docx" || file_extension == ".txt")
                {
                    Random ran = new Random();
                    string fileNAme = Path.GetFileName(document.FileName);
                    var R_num = ran.Next(0000, 9999).ToString();
                    var name = fileNAme = R_num; // This line seems redundant; you're re-assigning fileNAme
                    string file_name = name + file_extension;
                    string filepath = Path.Combine(_evn.WebRootPath, "documents", file_name);
                    using (FileStream fs = new FileStream(filepath, FileMode.Create)) // Use 'using' for proper disposal
                    {
                        document.CopyTo(fs);
                    }

                    _task.document = file_name;
                    // Store the path in the project object
                }
                else
                {
                    TempData["msg1"] = "This type of file is not supported !";
                    return RedirectToAction("plan_report", new { id = _task.plan_id });
                }
            }
            // If document is null, the code will skip the file processing block.  pro object will be saved without file info.
            _task.created_date = DateTime.Now.ToString();
            _task.updated_date = DateTime.Now.ToString();

            _con.tbl_tasks.Update(_task);
            _con.SaveChanges();
            TempData["msg1"] = "The Task is successfully updated now !";
            return RedirectToAction("plan_report", new { id = _task.plan_id });
        }
        public IActionResult delete_plantask(int id)
        {

            var del = _con.tbl_tasks.Find(id);

            if (del != null)
            {

                int planId = del.plan_id;

                _con.tbl_tasks.Remove(del);
                _con.SaveChanges();


                return RedirectToAction("plan_report", new { id = planId });
            }
            else
            {

                TempData["msg1"] = "Plan pillar not found.";
                return RedirectToAction("plan_report", new { id = id }); // Redirect back to the report with the original id
            }
        }
        [HttpPost]

        public IActionResult add_planproject(project pro, IFormFile document)
        {
            if (document != null) // Check if a file was actually uploaded
            {
                string file_extension = Path.GetExtension(document.FileName);
                if (file_extension == ".pdf" || file_extension == ".docx" || file_extension == ".txt")
                {
                    Random ran = new Random();
                    string fileNAme = Path.GetFileName(document.FileName);
                    var R_num = ran.Next(0000, 9999).ToString();
                    var name = fileNAme = R_num; // This line seems redundant; you're re-assigning fileNAme
                    string file_name = name + file_extension;
                    string filepath = Path.Combine(_evn.WebRootPath, "documents", file_name);
                    using (FileStream fs = new FileStream(filepath, FileMode.Create)) // Use 'using' for proper disposal
                    {
                        document.CopyTo(fs);
                    }

                    pro.document = file_name; // Store the path in the project object
                }
                else
                {
                    TempData["msg1"] = "This type of file is not supported !";
                    return RedirectToAction("plan_report", new { id = pro.plan_id });
                }
            }
            // If document is null, the code will skip the file processing block.  pro object will be saved without file info.

            _con.tbl_projects.Add(pro);
            _con.SaveChanges();
            TempData["msg1"] = "The project is successfully updated now !";

            return RedirectToAction("plan_report", new { id = pro.plan_id });

        }
        public IActionResult update_planproject(int id)
        {
            var login = HttpContext.Session.GetString("user_session");
            if (login != null)
            {
                var logined_user = _con.tbl_employee.Where(p => p.id == int.Parse(login)).ToList();


                var find_id = _con.tbl_projects.Find(id);
                var team_details = _con.tbl_teams.Where(p => p.status == 1).ToList();
                var emp_data = _con.tbl_employee.Where(p => p.status == 1).ToList();
                var client_details = _con.tbl_clients.ToList();
                var status = _con.tbl_projectStatus.Where(p => p.status == 1).ToList();
                var priority = _con.tbl_projectpriority.Where(p => p.status == 1).ToList();
                var type = _con.tbl_project_types.Where(p => p.status == 1).ToList();
                var client_name = _con.tbl_clients.FirstOrDefault(P => P.id == find_id.client_id);
                var type_name = _con.tbl_project_types.FirstOrDefault(P => P.id == find_id.project_types);
                var employe_name = _con.tbl_employee.FirstOrDefault(P => P.id == find_id.team_id);
                var priority_name = _con.tbl_projectpriority.FirstOrDefault(P => P.id == find_id.project_priority);
                var status_name = _con.tbl_projectStatus.FirstOrDefault(P => P.status_id == find_id.project_statuss);
                mainModel data = new mainModel()
                {
                    client_data = client_name,
                    projects_types = type,
                    projects_prioritesDetails = priority,
                    pro_Statusdetail = status,

                    employee_details = emp_data,
                    team_details = team_details,
                    client_details = client_details,
                    projects_typesData = type_name,
                    employee_data = employe_name,
                    projects_prioritesData = priority_name,
                    proStatus_data = status_name,
                    logined_user = logined_user,


                    project_data = find_id
                };
                return View(data);
            }
            else
            {
                return RedirectToAction("login_form");
            }
        }
        [HttpPost]
        public IActionResult update_planproject(project pro, IFormFile document)
        {
            if (document != null) // Check if a file was actually uploaded
            {
                string file_extension = Path.GetExtension(document.FileName);
                if (file_extension == ".pdf" || file_extension == ".docx" || file_extension == ".txt")
                {
                    Random ran = new Random();
                    string fileNAme = Path.GetFileName(document.FileName);
                    var R_num = ran.Next(0000, 9999).ToString();
                    var name = fileNAme = R_num; // This line seems redundant; you're re-assigning fileNAme
                    string file_name = name + file_extension;
                    string filepath = Path.Combine(_evn.WebRootPath, "documents", file_name);
                    using (FileStream fs = new FileStream(filepath, FileMode.Create)) // Use 'using' for proper disposal
                    {
                        document.CopyTo(fs);
                    }

                    pro.document = file_name; // Store the path in the project object
                }
                else
                {
                    TempData["msg1"] = "This type of file is not supported !";
                    return RedirectToAction("plan_report", new { id = pro.plan_id });
                }
            }
            // If document is null, the code will skip the file processing block.  pro object will be saved without file info.
            pro.plan_id = 0;
            _con.tbl_projects.Update(pro);
            _con.SaveChanges();
            TempData["msg1"] = "The project is successfully updated now !";
            return RedirectToAction("plan_report", new { id = pro.plan_id });

        }
        public IActionResult delete_planproject(int id)
        {

            var del = _con.tbl_projects.Find(id);

            if (del != null)
            {

                int planId = del.plan_id;

                _con.tbl_projects.Remove(del);
                _con.SaveChanges();


                return RedirectToAction("plan_report", new { id = planId });
            }
            else
            {

                TempData["msg1"] = "Plan pillar not found.";
                return RedirectToAction("plan_report", new { id = id }); // Redirect back to the report with the original id
            }
        }


    }
}




