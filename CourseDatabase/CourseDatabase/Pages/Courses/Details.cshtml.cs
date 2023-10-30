using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace CourseDatabase.Pages.Courses
{
    public class DetailsModel : PageModel
    {
        public CourseInfo courseInfo { get; set; } = new CourseInfo();
        public List<string> Students { get; set; } = new List<string>();
        public List<string> FacultyMembers { get; set; } = new List<string>();
        public string ErrorMessage { get; set; }

        public void OnGet(string id)
        {
            try
            {
                string connectionString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=CollegeLoginPortal;Integrated Security=True";

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    // Get course details
                    string courseSql = "SELECT * FROM Course WHERE CourseId = @id";
                    using (SqlCommand courseCommand = new SqlCommand(courseSql, connection))
                    {
                        courseCommand.Parameters.AddWithValue("@id", id);
                        using (SqlDataReader courseReader = courseCommand.ExecuteReader())
                        {
                            if (courseReader.Read())
                            {
                                courseInfo.CourseId = "" + courseReader.GetInt32(0);
                                courseInfo.CourseName = courseReader.GetString(1);
                                courseInfo.Duration = "" + courseReader.GetInt32(2);
                                courseInfo.Domain = courseReader.GetString(3);
                            }
                        }
                    }

                    // Get students enrolled in the course
                    string studentSql = "SELECT Name FROM Student s JOIN Course c ON s.CourseId = c.CourseId WHERE c.CourseId = @id;";
                    using (SqlCommand studentCommand = new SqlCommand(studentSql, connection))
                    {
                        studentCommand.Parameters.AddWithValue("@id", id);
                        using (SqlDataReader studentReader = studentCommand.ExecuteReader())
                        {
                            while (studentReader.Read())
                            {
                                Students.Add(studentReader["Name"].ToString());
                            }
                        }
                    }

                    // Get faculty members teaching the course
                    string facultySql = "SELECT Name FROM Faculty f JOIN Course c ON f.CourseId = c.CourseId WHERE c.CourseId = @id;";
                    using (SqlCommand facultyCommand = new SqlCommand(facultySql, connection))
                    {
                        facultyCommand.Parameters.AddWithValue("@id", id);
                        using (SqlDataReader facultyReader = facultyCommand.ExecuteReader())
                        {
                            while (facultyReader.Read())
                            {
                                FacultyMembers.Add(facultyReader["Name"].ToString());
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message;
            }
        }
    }
}
