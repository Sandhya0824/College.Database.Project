using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data;
using System.Data.SqlClient;

namespace StudentDatabase.Pages.Students
{
    public class EditModel : PageModel
    {
        public StudentInfo studentInfo = new StudentInfo();
        public string errorMessage = "";
        public string successMessage = "";


		public void OnGet(string regdNo)
		{
			try
			{
				string connectionString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=CollegeLoginPortal;Integrated Security=True";

				using (SqlConnection sqlConnection = new SqlConnection(connectionString))
				{
					sqlConnection.Open();
					string sql = "SELECT RegdNo, Name, DOB, Gender, Address, CourseId, ProfilePhoto FROM STUDENT WHERE RegdNo=@RegdNo";

					using (SqlCommand sqlCommand = new SqlCommand(sql, sqlConnection))
					{
						sqlCommand.Parameters.AddWithValue("@RegdNo", regdNo);

						using (SqlDataReader reader = sqlCommand.ExecuteReader())
						{
							if (reader.Read())
							{
								studentInfo.RegdNo = "" + reader.GetInt32(0);
								studentInfo.Name = reader.GetString(1);
								studentInfo.DOB = reader.GetString(2);
								studentInfo.Gender = reader.GetString(3);
								studentInfo.Address = reader.GetString(4);
								studentInfo.CourseId = "" + reader.GetInt32(5);

								if (!reader.IsDBNull(6))
								{
									studentInfo.ProfilePhoto = (byte[])reader.GetValue(6);
								}
								else
								{
									// Handle the case where ProfilePhoto is null (e.g., assign a default photo or set it to an empty byte[])
									studentInfo.ProfilePhoto = new byte[0]; // You can choose how to handle this case.
								}
							}
						}
					}
				}
			}
			catch (Exception ex)
			{
				Console.WriteLine("Error: " + ex.ToString());
			}
		}

		public IActionResult OnPost()
		{
			try
			{
				// Retrieve the form data for updating the student details, including the profile photo
				string regdNo = Request.Form["regdNo"];
				string name = Request.Form["name"];
				string dob = Request.Form["dob"];
				string gender = Request.Form["gender"];
				string address = Request.Form["address"];
				string courseId = Request.Form["courseId"];

				// Get the profile photo from the form
				var profilePhoto = Request.Form.Files["profilePhoto"];
				byte[] profilePhotoBytes = null;

				if (profilePhoto != null && profilePhoto.Length > 0)
				{
					using (MemoryStream ms = new MemoryStream())
					{
						profilePhoto.CopyTo(ms);
						profilePhotoBytes = ms.ToArray();
					}
				}

				// Validate the form data
				if (string.IsNullOrEmpty(regdNo) || string.IsNullOrEmpty(name)
					|| string.IsNullOrEmpty(dob) || string.IsNullOrEmpty(gender)
					|| string.IsNullOrEmpty(address) || string.IsNullOrEmpty(courseId))
				{
					errorMessage = "All fields are required";
					return Page();
				}

				// Update the student details and profile photo in the database
				string connectionString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=CollegeLoginPortal;Integrated Security=True";

				using (SqlConnection connection = new SqlConnection(connectionString))
				{
					connection.Open();

					// Define your SQL UPDATE statement to include the profile photo
					string sql = "UPDATE Student " +
			 "SET Name = @name, DOB = @dob, Gender = @gender, Address = @address, CourseId = @courseId, " +
			 "ProfilePhoto = CONVERT(varbinary(max), @profilePhoto) " +
			 "WHERE RegdNo = @regdNo";


					using (SqlCommand command = new SqlCommand(sql, connection))
					{
						command.Parameters.AddWithValue("@regdNo", regdNo);
						command.Parameters.AddWithValue("@name", name);
						command.Parameters.AddWithValue("@dob", dob);
						command.Parameters.AddWithValue("@gender", gender);
						command.Parameters.AddWithValue("@address", address);
						command.Parameters.AddWithValue("@courseId", courseId);

						if (profilePhotoBytes != null)
						{
							// Only add the profilePhoto parameter if a photo was uploaded
							command.Parameters.Add(new SqlParameter("@profilePhoto", SqlDbType.VarBinary, -1)
							{
								Value = profilePhotoBytes,
								Size = -1
							});
						}
						else
						{
							// Handle the case where no photo was uploaded
							command.Parameters.Add(new SqlParameter("@profilePhoto", DBNull.Value));
						}

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




