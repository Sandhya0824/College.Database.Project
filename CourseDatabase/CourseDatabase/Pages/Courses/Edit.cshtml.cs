using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace CourseDatabase.Pages.Courses
{
    public class EditModel : PageModel
    {
        public CourseInfo courseInfo = new CourseInfo();
        public string errorMessage = "";
        public string successMessage = "";
        public void OnGet()
        {
            string courseId = Request.Query["id"];

            try
            {
                string connectionString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=CollegeLoginPortal;Integrated Security=True";

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string sql = "SELECT * FROM Course WHERE CourseId = @id";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@id", courseId);
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                courseInfo.CourseId = "" + reader.GetInt32(0);
                                courseInfo.CourseName = reader.GetString(1);
                                courseInfo.Duration = "" + reader.GetInt32(2);
                                courseInfo.Domain = reader.GetString(3);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
            }
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

            try
            {
                string connectionString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=CollegeLoginPortal;Integrated Security=True";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string sql = "UPDATE Course " +
                                 "SET CourseId=@courseId, CourseName=@courseName, Duration=@duration, Domain=@domain " +
                                 "WHERE CourseId=@courseId;";

                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@courseId", courseInfo.CourseId);
                        command.Parameters.AddWithValue("@courseName", courseInfo.CourseName);
                        command.Parameters.AddWithValue("@duration", courseInfo.Duration);
                        command.Parameters.AddWithValue("@domain", courseInfo.Domain);

                        command.ExecuteNonQuery();
                    }
                }
            }

            catch (Exception ex)
            {
                errorMessage = ex.Message;
                return;
            }

            Response.Redirect("/Courses/Index");
        }
    }
}
