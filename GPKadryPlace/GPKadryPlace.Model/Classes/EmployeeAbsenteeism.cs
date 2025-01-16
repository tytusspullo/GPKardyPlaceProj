using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GPKadryPlace.Model
{
    public class EmployeeAbsenteeism
    {

        public EmployeeAbsenteeism() { }
        public EmployeeAbsenteeism(string shortName, string fullName) 
        {
            ShortName = shortName;
            FullName = fullName;
        }
        public int IDEmployeeAbsenteeism { get; set; }
        public string ShortName { get; set; }
        public string FullName { get; set; }
        public bool Available { get; set; } = true;

        public void Save()
        {
            string connectionString = LibraryConfig.GetConnectionString();

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    string query;
                    if (IDEmployeeAbsenteeism == 0)
                    {
                        // INSERT statement
                        query = "INSERT INTO employee_absenteeism (short_name, name, avilable) " +
                                "VALUES (@ShortName, @FullName, @Available);";
                    }
                    else
                    {
                        // UPDATE statement
                        query = "UPDATE employee_absenteeism SET short_name = @ShortName, name = @FullName, avilable = @Available " +
                                "WHERE ID_employee_absenteeism = @IDEmployeeAbsenteeism;";
                    }

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        // Add parameters
                        command.Parameters.AddWithValue("@ShortName", ShortName);
                        command.Parameters.AddWithValue("@FullName", FullName);
                        command.Parameters.AddWithValue("@Available", Available);

                        if (IDEmployeeAbsenteeism != 0)
                        {
                            command.Parameters.AddWithValue("@IDEmployeeAbsenteeism", IDEmployeeAbsenteeism);
                        }

                        // Execute the command
                        command.ExecuteNonQuery();

                        if (IDEmployeeAbsenteeism == 0)
                        {
                            // Retrieve the newly generated ID for an INSERT operation
                            command.CommandText = "SELECT SCOPE_IDENTITY();";
                            IDEmployeeAbsenteeism = Convert.ToInt32(command.ExecuteScalar());
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Log or handle the exception as needed
                throw;
            }
        }
    }
}
