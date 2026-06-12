using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Data.SqlClient;

namespace Timetable_and_Classroom_Management_System.DataAccessLayer
{
    public static class DatabaseConnection
    {
        private static readonly string connectionString =
            @"Server=localhost\SQLEXPRESS;Database=UniversityTimetableDB;Trusted_Connection=True;TrustServerCertificate=True;";

        public static SqlConnection GetConnection()
        {
            return new SqlConnection(connectionString);
        }

        public static bool TestConnection()
        {
            try
            {
                using SqlConnection connection = GetConnection();

                connection.Open();

                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}