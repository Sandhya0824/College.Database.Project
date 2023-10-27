using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace FacultyDatabase.Pages.Faculty
{
    public class FacultyDetailModel : PageModel
    {
        public List<FacultyInfo> facultyList = new List<FacultyInfo>();

        public void OnGet()
        {
            try
            {
                string connectionString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=CollegeLoginPortal;Integrated Security=True;";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string sql = "SELECT * FROM Faculty";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                FacultyInfo facultyInfo = new FacultyInfo();
                                facultyInfo.FacultyId = "" + reader.GetInt32(0);
                                facultyInfo.Name = reader.GetString(1);
                                facultyInfo.DOB = reader.GetString(2);
                                facultyInfo.Gender = reader.GetString(3);
                                facultyInfo.Address = reader.GetString(4);
                                facultyInfo.CourseId = "" + reader.GetInt32(5);

                                facultyList.Add(facultyInfo);
                            }
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception: " + e.ToString());
            }
        }
    }

    public class FacultyInfo
    {
        public String FacultyId;
        public String Name;
        public String DOB;
        public String Gender;
        public String Address;
        public String CourseId;
    }
}
