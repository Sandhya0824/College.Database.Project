using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace CourseDatabase.Pages.Courses
{
    public class CreateModel : PageModel
    {
        public CourseInfo courseInfo = new CourseInfo();
        public string errorMessage = "";
        public string successMessage = "";
        public void OnGet()
        {
        }

        public void OnPost() 
        {
            courseInfo.CourseId = Request.Form["courseId"];
            courseInfo.CourseName = Request.Form["courseName"];
            courseInfo.Duration = Request.Form["duration"];
            courseInfo.Domain = Request.Form["domain"];

            if (courseInfo.CourseId.Length == 0 || courseInfo.CourseName.Length == 0 ||
                courseInfo.Duration.Length == 0 || courseInfo.Domain.Length == 0)
            {
                errorMessage = "All the fields are required";
                return;
            }

            //saving the client to the database

            try
            {
                string connectionString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=CollegeLoginPortal;Integrated Security=True";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string sql = "INSERT INTO Course " +
                                 "(courseId, courseName, duration, domain) VALUES " +
                                 "(@courseId, @courseName, @duration, @domain);";

                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@courseId", courseInfo.CourseId);
                        command.Parameters.AddWithValue("@courseName", courseInfo.CourseName);
                        command.Parameters.AddWithValue("@duration", courseInfo.Duration);
                        command.Parameters.AddWithValue("@domain", courseInfo.Domain);

                        command.ExecuteNonQuery();
                        successMessage = "New Course Added Correctly";
                    }
                }
            }

            catch (Exception ex)
            {
                errorMessage = ex.Message;
                return;
            }

            courseInfo.CourseId = "";
            courseInfo.CourseName = "";
            courseInfo.Duration = "";
            courseInfo.Domain = "";
            

            Response.Redirect("/Courses/Index");
        }
    }
}











