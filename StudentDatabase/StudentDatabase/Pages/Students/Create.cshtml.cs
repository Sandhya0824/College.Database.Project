using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Data.SqlClient;
using System.IO;

namespace StudentDatabase.Pages.Students
{
	public class CreateModel : PageModel
	{
		public StudentInfo studentInfo = new StudentInfo();
		public string errorMessage = "";
		public string successMessage = "";

		public void OnGet()
		{
		}

		public void OnPost()
		{
			studentInfo.RegdNo = Request.Form["regdNo"];
			studentInfo.Name = Request.Form["name"];
			studentInfo.DOB = Request.Form["dob"];
			studentInfo.Gender = Request.Form["gender"];
			studentInfo.Address = Request.Form["address"];
			studentInfo.CourseId = Request.Form["courseId"];

			IFormFile profilePhoto = Request.Form.Files["profilePhoto"];
			byte[] profilePhotoBytes = null;

			if (profilePhoto != null && profilePhoto.Length > 0)
			{
				using (var memoryStream = new MemoryStream())
				{
					profilePhoto.CopyTo(memoryStream);
					profilePhotoBytes = memoryStream.ToArray();
				}
			}

			if (string.IsNullOrEmpty(studentInfo.RegdNo) || string.IsNullOrEmpty(studentInfo.Name))
			{
				errorMessage = "Student RegdNo/Name is Required";
				return;
			}

			try
			{
				string connectionString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=CollegeLoginPortal;Integrated Security=True;Connect Timeout=30;Encrypt=False";

				using (SqlConnection connection = new SqlConnection(connectionString))
				{
					connection.Open();

					if (IsValidCourseId(connection, studentInfo.CourseId))
					{
						string sql = "INSERT INTO Student (regdNo, name, dob, gender, address, courseId, profilePhoto) " +
									 "VALUES (@regdNo, @name, @dob, @gender, @address, @courseId, @profilePhoto);";

						using (SqlCommand command = new SqlCommand(sql, connection))
						{
							command.Parameters.AddWithValue("@regdNo", studentInfo.RegdNo);
							command.Parameters.AddWithValue("@name", studentInfo.Name);
							command.Parameters.AddWithValue("@dob", studentInfo.DOB);
							command.Parameters.AddWithValue("@gender", studentInfo.Gender);
							command.Parameters.AddWithValue("@address", studentInfo.Address);
							command.Parameters.AddWithValue("@courseId", studentInfo.CourseId);

							if (profilePhotoBytes != null)
							{
								command.Parameters.AddWithValue("@profilePhoto", profilePhotoBytes);
							}
							else
							{
								// Handle the case where no photo was uploaded
								command.Parameters.Add(new SqlParameter("@profilePhoto", DBNull.Value));
							}

							command.ExecuteNonQuery();
							successMessage = "New Student Added Successfully";
						}
					}
					else
					{
						errorMessage = "Invalid CourseId. Please choose a valid course.";
					}
				}
			}
			catch (Exception ex)
			{
				errorMessage = ex.Message;
			}

			Response.Redirect("/Students/Index");
		}

		private bool IsValidCourseId(SqlConnection connection, string courseId)
		{
			string sql = "SELECT COUNT(*) FROM Course WHERE CourseId = @courseId";
			using (SqlCommand command = new SqlCommand(sql, connection))
			{
				command.Parameters.AddWithValue("@courseId", courseId);
				int count = (int)command.ExecuteScalar();
				return count > 0;
			}
		}
	}
}

