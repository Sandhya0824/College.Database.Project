using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace FacultyDatabase.Pages.Faculty
{
    public class CompleteInfoOfFacultyModel : PageModel
    {
        public FacultyInfo FacultyInfo = new FacultyInfo(); public string ErrorMessage { get; set; }
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
                            FacultyInfo.FacultyId = reader.GetInt32(0).ToString();
                            FacultyInfo.Name = reader.GetString(1);
                            FacultyInfo.DOB = reader.GetString(2);
                            FacultyInfo.Gender = reader.GetString(3);
                            FacultyInfo.Address = reader.GetString(4);
                            FacultyInfo.CourseId = reader.GetInt32(5).ToString();
                            // Check if the ProfilePhoto field is null
                            if (!reader.IsDBNull(6))
                            {
                                FacultyInfo.ProfilePhoto = (byte[])reader.GetValue(6);
                            }
                            else
                            {
                                // Handle the case where ProfilePhoto is null (e.g., assign a default photo or set it to an empty byte[])
                                FacultyInfo.ProfilePhoto = new byte[0]; // You can choose how to handle this case.
                            }
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
