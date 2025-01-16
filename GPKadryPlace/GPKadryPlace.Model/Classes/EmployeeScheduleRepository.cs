using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GPKadryPlace.Model
{
    public class EmployeeScheduleRepository : List<EmployeeSchedule>
    {
        public EmployeeScheduleRepository() { }

        private void LoadData()
        {
            var connectionString = LibraryConfig.GetConnectionString();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();

                    string query = @"SELECT es.[ID_employee_schedule], es.[year], es.[month], es.[employee_schedule_type_ID], 
                                es.[name_of_employee_group], es.[avilable], 
                                esp.[ID_employee_schedule_position], esp.[employee_schedule_ID], 
                                esp.[employee_ID], esp.[for_date], esp.[employee_absenteeism_ID], esp.[hours] 
                         FROM [dbo].[employee_schedule] es
                         LEFT JOIN [dbo].[employee_schedule_position] esp 
                         ON es.[ID_employee_schedule] = esp.[employee_schedule_ID]
                         WHERE es.[avilable] = 1";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            const int ID_EMPLOYEE_SCHEDULE = 0;
                            const int YEAR = 1;
                            const int MONTH = 2;
                            const int EMPLOYEE_SCHEDULE_TYPE_ID = 3;
                            const int NAME_OF_EMPLOYEE_GROUP = 4;
                            const int AVAILABLE = 5;
                            const int ID_EMPLOYEE_SCHEDULE_POSITION = 6;
                            const int EMPLOYEE_SCHEDULE_ID = 7;
                            const int EMPLOYEE_ID = 8;
                            const int FOR_DATE = 9;
                            const int EMPLOYEE_ABSENTEEISM_ID = 10;
                            const int HOURS = 11;

                            int scheduleId = reader.GetInt32(ID_EMPLOYEE_SCHEDULE);

                            var schedule = this.Find(es => es.IDEmployeeSchedule == scheduleId);
                            if (schedule == null)
                            {
                                schedule = new EmployeeSchedule
                                {
                                    IDEmployeeSchedule = scheduleId,
                                    Year = reader.GetInt32(YEAR),
                                    Month = reader.GetInt32(MONTH),
                                    EmployeeScheduleTypeID = reader.GetInt32(EMPLOYEE_SCHEDULE_TYPE_ID),
                                    NameOfEmployeeGroup = reader.GetString(NAME_OF_EMPLOYEE_GROUP),
                                    Available = reader.GetBoolean(AVAILABLE),
                                    EmployeeSchedulePositions = new List<EmployeeSchedulePosition>()
                                };

                                this.Add(schedule);
                            }

                            if (!reader.IsDBNull(ID_EMPLOYEE_SCHEDULE_POSITION))
                            {
                                var position = new EmployeeSchedulePosition
                                {
                                    IDEmployeeSchedulePosition = reader.GetInt32(ID_EMPLOYEE_SCHEDULE_POSITION),
                                    EmployeeScheduleID = reader.GetInt32(EMPLOYEE_SCHEDULE_ID),
                                    EmployeeID = reader.GetInt32(EMPLOYEE_ID),
                                    ForDate = reader.GetDateTime(FOR_DATE),
                                    EmployeeAbsenteeismID = reader.GetInt32(EMPLOYEE_ABSENTEEISM_ID),
                                    Hours = reader.GetDateTime(HOURS)
                                };

                                schedule.EmployeeSchedulePositions.Add(position);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error in EmployeeScheduleRepository.LoadData: {ex.Message}");
                    throw;
                }
            }
        }
    }
}
