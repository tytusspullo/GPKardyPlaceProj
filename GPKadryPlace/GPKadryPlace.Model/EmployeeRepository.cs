using GPKadryPlace.Model;
using System.Collections.Generic;
using System.Linq;

namespace YourNamespace.Repositories
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly ApplicationDbContext _context;

        public EmployeeRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Employee> GetAllEmployees()
        {
            return _context.Employees.ToList().Where(x => x.Available = true);
        }

        public Employee GetEmployeeById(int id)
        {
            return _context.Employees.Find(id);
        }

        public Employee AddEmployee(Employee employee)
        {
            _context.Employees.Add(employee);
            _context.SaveChanges();
            return employee;
        }

        public Employee UpdateEmployee(Employee employee)
        {
            _context.Employees.Update(employee);
            _context.SaveChanges();
            return employee;
        }

        public Employee DeleteEmployee(Employee employee)
        {
            employee.Available = false;
            _context.Employees.Update(employee);
            _context.SaveChanges();
            return employee;
        }
    }
}
