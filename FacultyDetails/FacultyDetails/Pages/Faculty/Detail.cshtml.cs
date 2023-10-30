using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace FacultyDatabase.Pages.Faculty
{
    public class DetailModel : PageModel
    {
        public FacultyInfo FacultyInfo { get; set; }
        public string ErrorMessage { get; set; }
        public void OnGet(string facultyId)
        {
            if (string.IsNullOrEmpty(facultyId))
            {
                ErrorMessage = "Faculty ID is required.";
                return;
            }

            string connectionString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=CollegeLoginPortal;Integrated Security=True";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                string sql = "SELECT * FROM Faculty WHERE FacultyId = @FacultyId";

                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@FacultyId", facultyId);

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            FacultyInfo = new FacultyInfo
                            {
                                FacultyId = reader.GetInt32(0).ToString(),
                                Name = reader.GetString(1),
                                DOB = reader.GetString(2),
                                Gender = reader.GetString(3),
                                Address = reader.GetString(4),
                                CourseId = reader.GetInt32(5).ToString(),


                            };
                        }
                        else
                        {
                            ErrorMessage = "Faculty not found";
                        }
                    }
                }
            }
        }
    }
}
