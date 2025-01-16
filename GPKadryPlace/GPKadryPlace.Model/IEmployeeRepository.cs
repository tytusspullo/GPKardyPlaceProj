using GPKadryPlace.Model;
using System.Collections.Generic;

namespace GPKadryPlace.Model
{
    public interface IEmployeeRepository
    {
        IEnumerable<Employee> GetAllEmployees();
        Employee GetEmployeeById(int id);
        Employee AddEmployee(Employee employee);
        Employee UpdateEmployee(Employee employee);
        Employee DeleteEmployee(Employee employee);
    }
}