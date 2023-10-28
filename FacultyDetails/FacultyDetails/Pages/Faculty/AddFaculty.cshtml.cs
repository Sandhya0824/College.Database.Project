using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace FacultyDatabase.Pages.Faculty
{
    public class AddFacultyModel : PageModel
    {
        public FacultyInfo facultyInfo = new FacultyInfo();
        public String errorMessage = "";
        public String successMessage = "";

        public void OnGet()
        {
        }
        public void OnPost()
        {
            facultyInfo.FacultyId = Request.Form["FacultyId"];
            facultyInfo.Name = Request.Form["Name"];
            facultyInfo.DOB = Request.Form["DOB"];
            facultyInfo.Gender = Request.Form["Gender"];
            facultyInfo.Address = Request.Form["Address"];
            facultyInfo.CourseId = Request.Form["CourseId"];

            if (facultyInfo.FacultyId.Length == 0 || facultyInfo.Name.Length == 0 || 
                facultyInfo.DOB.Length == 0 || facultyInfo.Gender.Length == 0 || 
                facultyInfo.Address.Length == 0 || facultyInfo.CourseId.Length == 0)
            {
                errorMessage = "All the fields are required";
                return;
            }

            //Saving the new faculty data to the database

            try
            {
                String connectionString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=CollegeLoginPortal;Integrated Security=True;";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    if (IsValidCourseId(connection, facultyInfo.CourseId))
                    {
                        String sql = "INSERT INTO Faculty (FacultyId, Name, DOB, Gender, Address, CourseId) VALUES (@FacultyId, @Name, @DOB, @Gender, @Address, @CourseId)";
                        using (SqlCommand command = new SqlCommand(sql, connection))
                        {
                            command.Parameters.AddWithValue("@FacultyId", facultyInfo.FacultyId);
                            command.Parameters.AddWithValue("@Name", facultyInfo.Name);
                            command.Parameters.AddWithValue("@DOB", facultyInfo.DOB);
                            command.Parameters.AddWithValue("@Gender", facultyInfo.Gender);
                            command.Parameters.AddWithValue("@Address", facultyInfo.Address);
                            command.Parameters.AddWithValue("@CourseId", facultyInfo.CourseId);

                            command.ExecuteNonQuery();
                            successMessage = "New Faculty Added Successfully";

                        }
                    }
                    else
                    {
                        errorMessage = "Invalid CourseId. Please choose a valid course.";
                    }
                }


            }
            catch (Exception e)
            {
                errorMessage = "An error occurred while adding the faculty: " + e.Message;
                return;
            }
            /*facultyInfo.FacultyId = "";
            facultyInfo.Name = "";
            facultyInfo.DOB = "";
            facultyInfo.Gender = "";
            facultyInfo.Address = "";
            facultyInfo.CourseId = "";*/
            
            
            Response.Redirect("/Faculty/FacultyDetail");
        }
        private bool IsValidCourseId(SqlConnection connection, string CourseId)
        {
            // Check if the CourseId exists in the Course table
            string sql = "SELECT COUNT(*) FROM Course WHERE CourseId = @CourseId";
            using (SqlCommand command = new SqlCommand(sql, connection))
            {
                command.Parameters.AddWithValue("@CourseId", CourseId);
                int count = (int)command.ExecuteScalar();
                return count > 0;
            }
        }
    }
}
