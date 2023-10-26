using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace CourseDatabase.Pages.Course
{
    public class IndexModel : PageModel
    {
        public List<CourseInfo> courseList = new List<CourseInfo>();
        public void OnGet()
        {
            try 
            {
                string connectionString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=CollegeLoginPortal;Integrated Security=True";

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string sql = "SELECT * FROM Course";
                    using (SqlCommand command = new SqlCommand(sql, connection)) 
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                CourseInfo courseInfo = new CourseInfo();
                                courseInfo.CourseId = reader.GetString(0);
                                courseInfo.CourseName = reader.GetString(1);
                                courseInfo.Duration = reader.GetString(2);
                                courseInfo.Domain = reader.GetString(3);

                                courseList.Add(courseInfo);
                            }
                        }
                    }
                }
            } 
            catch (Exception ex) 
            { 
                Console.WriteLine("An exception occurred : " + ex.ToString());  
            }
        }
    }

    public class CourseInfo
    {
        public string CourseName { get; set; }
        public string CourseId { get; set; }  
        public string Duration { get; set; }    
        public string Domain { get; set; }
    }
}
