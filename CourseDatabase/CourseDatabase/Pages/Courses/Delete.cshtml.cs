using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace CourseDatabase.Pages.Courses
{
    public class DeleteModel : PageModel
    {
        public CourseInfo courseInfo = new CourseInfo();
        public string errorMessage = "";

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
            try
            {
                string connectionString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=CollegeLoginPortal;Integrated Security=True";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    if (!IsReferencingAnotherTable(connection, courseInfo.CourseId))
                    {
                        string sql = "DELETE FROM Course WHERE CourseId = @id";
                        using (SqlCommand command = new SqlCommand(sql, connection))
                        {
                            command.Parameters.AddWithValue("@id", courseId);
                            command.ExecuteNonQuery();
                        }
                    }
                    else
                    {
                        errorMessage = "Could not delete the Course as it has a reference to other tables. In order to delete the course, please delete it from the other table it references to.";
                    }

                }
            }
            catch (Exception e)
            {
                errorMessage = e.Message;
            }
                
            
           
         Response.Redirect("/Courses/Index");
        }

        private bool IsReferencingAnotherTable(SqlConnection connection, string courseId)
        {
            string sql1 = "SELECT COUNT(*) AS StudentCount FROM Student s JOIN Course c ON s.CourseId = c.CourseId WHERE c.CourseId = @id;";
            string sql2 = "SELECT COUNT(*) AS FacultyCount FROM Faculty f JOIN Course c ON f.CourseId = c.CourseId WHERE c.CourseId = @id;";
            using (SqlCommand command = new SqlCommand(sql1 + sql2, connection))
            {
                command.Parameters.AddWithValue("@id", courseId);
                int count = (int)command.ExecuteScalar();
                return count > 0;
            }
        }
    }
}
