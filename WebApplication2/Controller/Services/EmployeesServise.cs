using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Project.Models;
using Project.Model;
using System.Net;
using Project.Data;

namespace Project.Controller.Services
{
    public interface IEmployeesService
    {
        Task<ActionResult<List<Employee>>> GetAllEmployee();
        Task<EmployeeRequestModel> GetEmployee(Guid id);
        Task<Employee> AddEmployee(EmployeeRequestModel employee);
        Task<Guid> UpdateEmployee(Guid id, Employee newChanges);
        Task<Guid> DeleteEmployee(Guid id);
    }
    public class EmployeesServise : IEmployeesService
    {
        private readonly BankContext _context;

        public EmployeesServise(BankContext context) 
        { 
            _context = context;    
        }
        public async Task<Employee> AddEmployee(EmployeeRequestModel employee)
        {
            if (employee == null)
            {
                throw new ArgumentNullException(nameof(employee));
            }
            var res = MapRequestToEmployee(employee);

            _context.Employees.Add(res);
            await _context.SaveChangesAsync();
            return res;
        }
        public async Task<Guid> DeleteEmployee(Guid id)
        {
            var account = await _context.Employees.FindAsync(id);
            if(account == null)
            {
                return Guid.Empty;
            }
            _context.Employees.Remove(account);
            await _context.SaveChangesAsync();

            return id;
        }
        public async Task<ActionResult<List<Employee>>> GetAllEmployee()
        {
            var employee = await _context.Employees.ToListAsync();
            return employee;
        }
        public async Task<EmployeeRequestModel> GetEmployee(Guid id)
        {
            var employee = await _context.Employees.FindAsync(id);
            var res = MapRequestToDB(employee);
            if(employee == null)
            {
                return null;
            }
            return res;
        }
        public async Task<Guid> UpdateEmployee(Guid id, Employee newChanges)
        {
            var account = await _context.Employees.FindAsync(id);
            if(account == null)
            {
                return Guid.Empty;
            }
            account.FirstName = newChanges.FirstName;
            account.LastName = newChanges.LastName;
            account.Country = newChanges.Country; ;
            account.phone = newChanges.phone;
            _context.Employees.Update(account);
            await _context.SaveChangesAsync();
            return id;
        }
        private Employee MapRequestToEmployee(EmployeeRequestModel requestModel)
        {
            var employee = new Employee();
            employee.Id = Guid.NewGuid();
            employee.FirstName = requestModel.Name;
            employee.LastName = requestModel.LastName;
            employee.Country = requestModel.Country;
            employee.phone = requestModel.Phone;
            return employee;
        }
        private EmployeeRequestModel MapRequestToDB(Employee employee)
        {
            var requestModel = new EmployeeRequestModel();
            requestModel.Name = employee.FirstName;
            requestModel.LastName = employee.LastName;
            requestModel.Country = employee.Country;
            requestModel.Phone = employee.phone;
            return requestModel;
        }

    }
}
