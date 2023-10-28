using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace FacultyDatabase.Pages.Faculty
{
    public class EditModel : PageModel
    {
        public FacultyInfo facultyInfo = new FacultyInfo();
        public String errorMessage = "";
        public String successMessage = "";
        public void OnGet()
        {
            String facultyId = Request.Query["FacultyId"];
            try
            {
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
            catch (Exception e)
            {
                errorMessage = e.Message;
            }
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

            try
            {
                String connectionString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=CollegeLoginPortal;Integrated Security=True";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    String sql = "Update Faculty" +
                                 " set Name=@Name, DOB=@DOB, Gender=@Gender, Address=@Address, CourseId=@CourseId " + 
                                 " WHERE FacultyId=@FacultyId";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@FacultyId", facultyInfo.FacultyId);
                        command.Parameters.AddWithValue("@Name", facultyInfo.Name);
                        command.Parameters.AddWithValue("@DOB", facultyInfo.DOB);
                        command.Parameters.AddWithValue("@Gender", facultyInfo.Gender);
                        command.Parameters.AddWithValue("@Address", facultyInfo.Address);
                        command.Parameters.AddWithValue("@CourseId", facultyInfo.CourseId);

                        command.ExecuteNonQuery();
                    }
                }
                successMessage = "Faculty updated successfully";
            }
            catch (Exception e)
            {
                errorMessage = "An error occurred while adding the faculty: " + e.Message;
                return;
            }

            Response.Redirect("/Faculty/FacultyDetail");
        }
    }
}
