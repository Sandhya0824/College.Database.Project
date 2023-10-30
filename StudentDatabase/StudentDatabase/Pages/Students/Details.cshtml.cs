using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace StudentDatabase.Pages.Students
{
	public class DetailsModel : PageModel
	{
		public StudentInfo studentInfo = new StudentInfo();
		public string errorMessage = "";

		public void OnGet()
		{
			String regdNo = Request.Query["regdNo"];
			try
			{
				string connectionString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=CollegeLoginPortal;Integrated Security=True";

				using (SqlConnection sqlConnection = new SqlConnection(connectionString))
				{
					sqlConnection.Open();
					string sql = "SELECT * FROM STUDENT WHERE RegdNo=@RegdNo";
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

								// Check if the ProfilePhoto field is null
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
				errorMessage = "An error occurred while retrieving the student data.";
				Console.WriteLine("Error: " + ex.ToString());
			}
		}
	}
}
