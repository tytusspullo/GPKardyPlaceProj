using GPKadryPlace.Model;
using GPKadryPlace.Utils;

namespace GPKadryPlace.Tests
{
    public class IntegrationTests
    {

        [SetUp]
        public void Setup()
        {
            var configurationReader = new ConfigurationReader();
            LibraryConfig.SetConnectionString(configurationReader.ReturnConnection());
        }

        [Test]
        public void CreateScheduleWithPositions()
        {
            EmployeeSchedule employeeSchedule = new EmployeeSchedule();
            employeeSchedule.IDEmployeeSchedule = 0;
            employeeSchedule.Month = DateTime.Now.Month;
            employeeSchedule.Year = DateTime.Now.Year;
            
            //employeeSchedule
        }

        public void SaveAndRetriveSchedule()
        { 
            
        }
    }
}