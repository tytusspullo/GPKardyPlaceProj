using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GPKadryPlace.Model
{
    public class EmployeeSchedulePosition
    {
        public int IDEmployeeSchedulePosition { get; set; }
        public int EmployeeScheduleID { get; set; } 
        public int EmployeeID { get; set; } 
        public DateTime ForDate { get; set; }
        public int EmployeeAbsenteeismID { get; set; } 
        public DateTime Hours { get; set; }

        // Navigation properties
        public EmployeeSchedule EmployeeSchedule { get; set; }
        public Employee Employee { get; set; }
        public EmployeeAbsenteeism EmployeeAbsenteeism { get; set; }

        public void Save(SqlTransaction transaction)
        {
            try
            {
                SqlCommand command = new SqlCommand();
                command.Connection = transaction.Connection;
                command.Transaction = transaction;

                if (IDEmployeeSchedulePosition == 0)
                {
                    command.CommandText = @"INSERT INTO [dbo].[employee_schedule_position] 
                                        ([employee_schedule_ID], [employee_ID], [for_date], [employee_absenteeism_ID], [hours]) 
                                        VALUES (@EmployeeScheduleID, @EmployeeID, @ForDate, @EmployeeAbsenteeismID, @Hours);
                                        SELECT SCOPE_IDENTITY();";

                    command.Parameters.AddWithValue("@EmployeeScheduleID", EmployeeScheduleID);
                    command.Parameters.AddWithValue("@EmployeeID", EmployeeID);
                    command.Parameters.AddWithValue("@ForDate", ForDate);
                    command.Parameters.AddWithValue("@EmployeeAbsenteeismID", EmployeeAbsenteeismID);
                    command.Parameters.AddWithValue("@Hours", Hours);

                    IDEmployeeSchedulePosition = Convert.ToInt32(command.ExecuteScalar());
                }
                else
                {
                    command.CommandText = @"UPDATE [dbo].[employee_schedule_position] 
                                        SET [employee_schedule_ID] = @EmployeeScheduleID, 
                                            [employee_ID] = @EmployeeID, 
                                            [for_date] = @ForDate, 
                                            [employee_absenteeism_ID] = @EmployeeAbsenteeismID, 
                                            [hours] = @Hours 
                                        WHERE [ID_employee_schedule_position] = @IDEmployeeSchedulePosition;";

                    command.Parameters.AddWithValue("@EmployeeScheduleID", EmployeeScheduleID);
                    command.Parameters.AddWithValue("@EmployeeID", EmployeeID);
                    command.Parameters.AddWithValue("@ForDate", ForDate);
                    command.Parameters.AddWithValue("@EmployeeAbsenteeismID", EmployeeAbsenteeismID);
                    command.Parameters.AddWithValue("@Hours", Hours);
                    command.Parameters.AddWithValue("@IDEmployeeSchedulePosition", IDEmployeeSchedulePosition);

                    command.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in EmployeeSchedulePosition.Save: {ex.Message}");
                throw;
            }
        }
        private bool Validate()
        {
            return true;
        }
    }
}
