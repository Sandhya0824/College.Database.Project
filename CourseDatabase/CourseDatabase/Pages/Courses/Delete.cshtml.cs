using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace CourseDatabase.Pages.Courses
{
    public class DeleteModel : PageModel
    {
        public CourseInfo courseInfo = new CourseInfo();
        

        public void OnGet()
        {
            string courseId = Request.Query["id"];

            try
            {
                string connectionString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=CollegeLoginPortal;Integrated Security=True";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    string sql = "SELECT CourseName, Duration, Domain FROM Course WHERE CourseId = @id";
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
            }
        }

        public void OnPost()
        {
            string courseId = Request.Query["id"];
            string connectionString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=CollegeLoginPortal;Integrated Security=True";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string sql = "DELETE FROM Course WHERE CourseId = @id";
                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@id", courseId);
                    command.ExecuteNonQuery();
                }
            }

         Response.Redirect("/Courses/Index");
        }
    }
}
