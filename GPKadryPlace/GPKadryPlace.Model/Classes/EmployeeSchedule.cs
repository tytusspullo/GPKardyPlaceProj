using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using YourNamespace.Repositories;

namespace GPKadryPlace.Model
{
    public class EmployeeSchedule
    {
        private EmployeeScheduleType _scheduleType;
        public EmployeeSchedule() { }
        public EmployeeSchedule(int year, int month, EmployeeScheduleType scheduleType,
            string nameOfEmployeeGroup) 
        {
            IDEmployeeSchedule = 0;
            this.Year = year;
            this.Month = month;
            this.ScheduleType = scheduleType;
            this.NameOfEmployeeGroup = nameOfEmployeeGroup;
        }
        public int IDEmployeeSchedule { get; set; }
        public int Year { get; set; }
        public int Month { get; set; }
        EmployeeScheduleType ScheduleType {
            get { return _scheduleType; }
            set { 
                _scheduleType = value;
                switch (_scheduleType)
                {
                    case EmployeeScheduleType.Planed:
                        EmployeeScheduleTypeID = 1;
                        break;
                    case EmployeeScheduleType.RealWorkHours:
                        EmployeeScheduleTypeID = 2;
                        break;
                    default:
                        _scheduleType = EmployeeScheduleType.Planed;
                        EmployeeScheduleTypeID = 1;
                        break;
                }
            }
        }
        public int EmployeeScheduleTypeID { get; set; } 
        public string NameOfEmployeeGroup { get; set; }
        public bool Available { get; set; } = true;
        public List<EmployeeSchedulePosition> EmployeeSchedulePositions { get; set; } = new List<EmployeeSchedulePosition>();
        public void Save()
        {
            string connectionString = LibraryConfig.GetConnectionString();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlTransaction transaction = connection.BeginTransaction();

                try
                {
                    SqlCommand command = new SqlCommand();
                    command.Connection = connection;
                    command.Transaction = transaction;

                    if (IDEmployeeSchedule == 0)
                    {
                        command.CommandText = @"INSERT INTO [dbo].[employee_schedule] 
                                            ([year], [month], [employee_schedule_type_ID], [name_of_employee_group], [avilable]) 
                                            VALUES (@Year, @Month, @EmployeeScheduleTypeID, @NameOfEmployeeGroup, @Available);
                                            SELECT SCOPE_IDENTITY();";

                        command.Parameters.AddWithValue("@Year", Year);
                        command.Parameters.AddWithValue("@Month", Month);
                        command.Parameters.AddWithValue("@EmployeeScheduleTypeID", EmployeeScheduleTypeID);
                        command.Parameters.AddWithValue("@NameOfEmployeeGroup", NameOfEmployeeGroup);
                        command.Parameters.AddWithValue("@Available", Available);

                        IDEmployeeSchedule = Convert.ToInt32(command.ExecuteScalar());
                    }
                    else
                    {
                        command.CommandText = @"UPDATE [dbo].[employee_schedule] 
                                            SET [year] = @Year, 
                                                [month] = @Month, 
                                                [employee_schedule_type_ID] = @EmployeeScheduleTypeID, 
                                                [name_of_employee_group] = @NameOfEmployeeGroup, 
                                                [avilable] = @Available 
                                            WHERE [ID_employee_schedule] = @IDEmployeeSchedule;";

                        command.Parameters.AddWithValue("@Year", Year);
                        command.Parameters.AddWithValue("@Month", Month);
                        command.Parameters.AddWithValue("@EmployeeScheduleTypeID", EmployeeScheduleTypeID);
                        command.Parameters.AddWithValue("@NameOfEmployeeGroup", NameOfEmployeeGroup);
                        command.Parameters.AddWithValue("@Available", Available);
                        command.Parameters.AddWithValue("@IDEmployeeSchedule", IDEmployeeSchedule);

                        command.ExecuteNonQuery();
                    }

                    foreach (var position in EmployeeSchedulePositions)
                    {
                        position.EmployeeScheduleID = IDEmployeeSchedule;
                        position.Save(transaction);
                    }

                    transaction.Commit();
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    throw;
                }
            }
        }

        /// <summary>
        /// PL:
        /// Generuje Pozyce dla Grafiku Pracowniczego dla każdego dnia
        /// w miesiącu z domyslną Zmianą Pracy/Absencją od Pn - Pt.
        /// Bierze pierwszego dostępnego pracownika
        /// EN:
        /// Generate Positions for Schedule for each day in month
        /// with default Absenteeism for each day from Monday to Friday.
        /// Takes first avilable employee.
        /// </summary>
        public bool GeneratePositionsForYearAndMonth()
        {
            if (!IsValidSQLServerDate(this.Year, this.Month))
            {
                return false;
            }

            var employee = GetDefaultEmployee();
            int defaultIDEmployeeAbsenteeism = GetDefaultAbsenteeismID();
            int daysInMonth = DaysInMonth(this.Year, this.Month);

            EmployeeSchedulePositions.Clear();

            for (int day = 1; day <= daysInMonth; day++)
            {
                DateTime date = new DateTime(this.Year, this.Month, day);
                DayOfWeek dayOfWeek = date.DayOfWeek;

                if (dayOfWeek == DayOfWeek.Monday ||
                    dayOfWeek == DayOfWeek.Tuesday ||
                    dayOfWeek == DayOfWeek.Wednesday ||
                    dayOfWeek == DayOfWeek.Thursday ||
                    dayOfWeek == DayOfWeek.Friday)
                {
                    var position = new EmployeeSchedulePosition
                    {
                        EmployeeScheduleID = this.IDEmployeeSchedule,
                        EmployeeID = employee.ID_employee,
                        ForDate = date,
                        EmployeeAbsenteeismID = defaultIDEmployeeAbsenteeism,
                        Hours = new DateTime(1900,1,1,8,0,0)
                    };

                    this.EmployeeSchedulePositions.Add(position);
                }
            }

            return true;
        }
        private bool IsValidSQLServerDate(int year, int month)
        {
            // SQL Server DateTime supports dates from 1900-01-01 to 2059-12-31
            return year >= 1900 && year <= 2059 && month >= 1 && month <= 12;
        }
        private int GetDefaultAbsenteeismID()
        {
            return 1;
        }
        private Employee GetDefaultEmployee()
        {
            var employee = new Employee();
            //EmployeeRepository repository = new EmployeeRepository()
            return employee;
        }
        private int DaysInMonth(int year, int month)
        {
            return DateTime.DaysInMonth(year, month);
        }
        private bool Validate()
        {
            //UQ month and year
            return true;
        }
    }
}
