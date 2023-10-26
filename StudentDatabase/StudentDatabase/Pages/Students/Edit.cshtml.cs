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

		public void OnPost()
		{
			studentInfo.RegdNo = Request.Form["regdNo"];
			studentInfo.Name = Request.Form["name"];
			studentInfo.DOB = Request.Form["dob"];
			studentInfo.Gender = Request.Form["gender"];
			studentInfo.Address = Request.Form["address"];
			studentInfo.CourseId = Request.Form["courseId"];

			if (string.IsNullOrEmpty(studentInfo.RegdNo) || string.IsNullOrEmpty(studentInfo.Name)
				|| string.IsNullOrEmpty(studentInfo.DOB) || string.IsNullOrEmpty(studentInfo.Gender)
				|| string.IsNullOrEmpty(studentInfo.Address) || string.IsNullOrEmpty(studentInfo.CourseId))
			{
				errorMessage = "All fields are required";
				return;
			}

			try
			{
				string connectionString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=CollegeLoginPortal;Integrated Security=True;Connect Timeout=30;Encrypt=False";

				using (SqlConnection connection = new SqlConnection(connectionString))
				{
					connection.Open();

					string sql = "UPDATE Student " +
								"SET name = @name, dob = @dob, gender = @gender, address = @address, courseId = @courseId " +
								"WHERE regdNo = @regdNo";

					using (SqlCommand command = new SqlCommand(sql, connection))
					{
						command.Parameters.AddWithValue("@regdNo", studentInfo.RegdNo); // Add this line
						command.Parameters.AddWithValue("@name", studentInfo.Name);
						command.Parameters.AddWithValue("@dob", studentInfo.DOB);
						command.Parameters.AddWithValue("@gender", studentInfo.Gender);
						command.Parameters.AddWithValue("@address", studentInfo.Address);
						command.Parameters.AddWithValue("@courseId", studentInfo.CourseId);

						command.ExecuteNonQuery();
						successMessage = "Student Updated Successfully";
					}
				}
			}
			catch (Exception ex)
			{
				errorMessage = ex.Message;
			}
			Response.Redirect("/Students/Index");
		}

	}

}




