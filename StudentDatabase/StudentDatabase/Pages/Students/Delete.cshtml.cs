using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Data.SqlClient;

namespace StudentDatabase.Pages.Students
{
    public class DeleteModel : PageModel
    {
        public StudentInfo studentInfo = new StudentInfo();
        public string errorMessage = "";

        public void OnGet(string regdNo)
        {
            try
            {
                // Retrieve student information based on regdNo and populate studentInfo
                // You can use similar code as your "Edit" page to fetch the student information.
                string connectionString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=CollegeLoginPortal;Integrated Security=True;Connect Timeout=30;Encrypt=False";

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string sql = "SELECT * FROM Student WHERE RegdNo = @regdNo";

                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@regdNo", regdNo);

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                studentInfo.RegdNo = "" + reader.GetInt32(0);
                                studentInfo.Name = reader.GetString(1);
                                studentInfo.DOB = reader.GetString(2);
                                studentInfo.Gender = reader.GetString(3);
                                studentInfo.Address = reader.GetString(4);
                                studentInfo.CourseId = "" + reader.GetInt32(5);
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

        public IActionResult OnPost(string regdNo)
        {
            try
            {
                regdNo = Request.Form["regdNo"];

                string connectionString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=CollegeLoginPortal;Integrated Security=True;Connect Timeout=30;Encrypt=False";

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    string sql = "DELETE FROM Student WHERE RegdNo = @regdNo";

                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@regdNo", regdNo);
                        command.ExecuteNonQuery();
                    }
                }

                // Redirect to the students index page after successful deletion
                return RedirectToPage("/Students/Index");
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
                return Page();
            }
        }
    }
}
