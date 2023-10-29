using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace CourseDatabase.Pages.Courses
{
    public class DeleteModel : PageModel
    {
        public CourseInfo courseInfo = new CourseInfo();
        public string errorMessage = "";
        public int count1, count2;
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

                    string sql1 = "SELECT COUNT(*) AS StudentCount FROM Student s JOIN Course c ON s.CourseId = c.CourseId WHERE c.CourseId = @id;";
                    using (SqlCommand command = new SqlCommand(sql1, connection))
                    {
                        command.Parameters.AddWithValue("@id", courseId);
                        count1 = (int)command.ExecuteScalar();


                        string sql2 = "SELECT COUNT(*) AS FacultyCount FROM Faculty f JOIN Course c ON f.CourseId = c.CourseId WHERE c.CourseId = @id;";
                        using (SqlCommand command2 = new SqlCommand(sql2, connection))
                        {
                            command2.Parameters.AddWithValue("@id", courseId);
                            count2 = (int)command2.ExecuteScalar();


                            if (count1 > 0 || count2 > 0)
                            {
                                errorMessage = "Could not delete the Course as it has a reference to other tables. In order to delete the course, please delete it from the other table it references to.";
                            }

                            else
                            {
                                string sql = "DELETE FROM Course WHERE CourseId = @id";
                                using (SqlCommand command3 = new SqlCommand(sql, connection))
                                {
                                    command3.Parameters.AddWithValue("@id", courseId);
                                    command3.ExecuteNonQuery();
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception e)
            {
                errorMessage = e.Message;
            }
              
         Response.Redirect("/Courses/Index");
        }

    
    }
}



