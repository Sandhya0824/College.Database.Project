using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace StudentDatabase.Pages.Students
{
    public class EditModel : PageModel
    {
        public StudentInfo studentInfo = new StudentInfo();
        public string errorMessage = "";
        public string successMessage = "";


		public void OnGet()
		{
			String regdNo = Request.Query["regdNo"];

			try
			{
				String connectionString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=CollegeLoginPortal;Integrated Security=True;Connect Timeout=30;Encrypt=False";

				using (SqlConnection connection = new SqlConnection(connectionString))
				{
					connection.Open();
					String sql = "SELECT * FROM Student WHERE RegdNo = @regdNo";

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

		public IActionResult OnPost()
		{
			try
			{
				// Retrieve the form data for updating the student details
				string regdNo = Request.Form["regdNo"];
				string name = Request.Form["name"];
				string dob = Request.Form["dob"];
				string gender = Request.Form["gender"];
				string address = Request.Form["address"];
				string courseId = Request.Form["courseId"];

				// Validate the form data
				if (string.IsNullOrEmpty(regdNo) || string.IsNullOrEmpty(name)
					|| string.IsNullOrEmpty(dob) || string.IsNullOrEmpty(gender)
					|| string.IsNullOrEmpty(address) || string.IsNullOrEmpty(courseId))
				{
					errorMessage = "All fields are required";
					return Page();
				}

				// Update the student details in the database
				string connectionString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=CollegeLoginPortal;Integrated Security=True;Connect Timeout=30;Encrypt=False";

				using (SqlConnection connection = new SqlConnection(connectionString))
				{
					connection.Open();

					string sql = "UPDATE Student " +
						"SET name = @name, dob = @dob, gender = @gender, address = @address, courseId = @courseId " +
						"WHERE regdNo = @regdNo";

					using (SqlCommand command = new SqlCommand(sql, connection))
					{
						command.Parameters.AddWithValue("@regdNo", regdNo);
						command.Parameters.AddWithValue("@name", name);
						command.Parameters.AddWithValue("@dob", dob);
						command.Parameters.AddWithValue("@gender", gender);
						command.Parameters.AddWithValue("@address", address);
						command.Parameters.AddWithValue("@courseId", courseId);

						command.ExecuteNonQuery();
					}
				}

				// Redirect to the students index page after a successful update
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




