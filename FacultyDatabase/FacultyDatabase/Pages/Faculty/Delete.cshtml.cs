using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace FacultyDatabase.Pages.Faculty
{
    public class DeleteModel : PageModel
    {
        public FacultyInfo facultyInfo = new FacultyInfo();
        public string errorMessage = "";
        public void OnGet()
        {
            String facultyId = Request.Query["FacultyId"];
            try
            {
                
                // Retrieve student information based on regdNo and populate studentInfo
                // You can use similar code as your "Edit" page to fetch the student information.
                String connectionString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=CollegeLoginPortal;Integrated Security=True";

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    String sql = "SELECT * FROM Faculty WHERE FacultyId = @FacultyId";

                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@FacultyId", facultyId);

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                facultyInfo.FacultyId = "" + reader.GetInt32(0);
                                facultyInfo.Name = reader.GetString(1);
                                facultyInfo.DOB = reader.GetString(2);
                                facultyInfo.Gender = reader.GetString(3);
                                facultyInfo.Address = reader.GetString(4);
                                facultyInfo.CourseId = "" + reader.GetInt32(5);
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
            try
            {
                String facultyId = Request.Form["FacultyId"];

                String connectionString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=CollegeLoginPortal;Integrated Security=True";

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    String sql = "DELETE FROM Faculty WHERE FacultyId = @FacultyId";

                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@FacultyId", facultyId);
                        command.ExecuteNonQuery();
                    }
                }

                // Redirect to the students index page after successful deletion
                Response.Redirect("/Faculty/FacultyDetail");
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
                return;
            }
        }
    }
}
