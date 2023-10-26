using Microsoft.AspNetCore.DataProtection.KeyManagement;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;
using System.Net;
using System.Reflection;
using System.Xml.Linq;

namespace StudentDatabase.Pages.Students
{
    public class IndexModel : PageModel
    {
        public List<StudentInfo> studentList=new List<StudentInfo>();
        public IActionResult OnGet()
        {
            try
            {
                String connectionString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=CollegeLoginPortal;Integrated Security=True;Connect Timeout=30;Encrypt=False";

				using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    String sql = "select * from student";
                    using(SqlCommand command= new SqlCommand(sql, connection))
                    {
                        using(SqlDataReader reader = command.ExecuteReader())
                        {
                            while(reader.Read())
                            {
                                StudentInfo studentInfo = new StudentInfo
                                {
                                    RegdNo = "" + reader.GetInt32(0),
                                    Name = reader.GetString(1),
                                    DOB = reader.GetString(2),
                                    Gender = reader.GetString(3),
                                    Address = reader.GetString(4),
                                    CourseId = "" + reader.GetInt32(5)
                                };

								studentList.Add(studentInfo);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error Occurred: " + ex.Message);
            }
            return Page();
        }
    }

    public class StudentInfo
    {
        public String RegdNo;
        public String Name;
        public String DOB;
        public String Gender;
        public String Address;
        public String CourseId;
    }
}
