using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Data.SqlClient;
using Timetable_and_Classroom_Management_System.Models;

namespace Timetable_and_Classroom_Management_System.DataAccessLayer
{
    public class BranchRepository
    {
        public List<Branch> GetAll()
        {
            List<Branch> branches = new List<Branch>();

            using SqlConnection connection = DatabaseConnection.GetConnection();

            string query = "SELECT BranchID, BranchName FROM Branches";

            using SqlCommand command = new SqlCommand(query, connection);

            connection.Open();

            using SqlDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                Branch branch = new Branch
                {
                    BranchID = Convert.ToInt32(reader["BranchID"]),
                    BranchName = reader["BranchName"].ToString()!
                };

                branches.Add(branch);
            }

            return branches;
        }
    }
}
