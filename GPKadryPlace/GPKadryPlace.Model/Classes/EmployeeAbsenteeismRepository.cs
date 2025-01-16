using System;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;

namespace GPKadryPlace.Model
{
    public class EmployeeAbsenteeismRepository : List<EmployeeAbsenteeism>
    {
        public EmployeeAbsenteeismRepository()
        {
            LoadData();
        }

        private void LoadData()
        {
            var connectionString = LibraryConfig.GetConnectionString();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "SELECT [ID_employee_absenteeism], [short_name], [name], [avilable] FROM [dbo].[employee_absenteeism]";
                SqlCommand command = new SqlCommand(query, connection);

                try
                {
                    connection.Open();
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            const int ID_EMPLOYEE_ABSENTEEISM = 0;
                            const int SHORT_NAME = 1;
                            const int NAME = 2;
                            const int AVAILABLE = 3;

                            EmployeeAbsenteeism absenteeism = new EmployeeAbsenteeism
                            {
                                IDEmployeeAbsenteeism = reader.GetInt32(ID_EMPLOYEE_ABSENTEEISM),
                                ShortName = reader.GetString(SHORT_NAME),
                                FullName = reader.GetString(NAME),
                                Available = reader.GetBoolean(AVAILABLE)
                            };
                            this.Add(absenteeism);
                        }
                    }
                }
                catch (Exception ex)
                {
                    // Handle exceptions (log error, rethrow, etc.)
                    Console.WriteLine("An error occurred: " + ex.Message);
                }
            }
        }
    }
}
