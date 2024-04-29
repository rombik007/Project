using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Project.Models;
using Project.Data;
using Project.Controller.Services;
using Project.Model;
namespace Project.Controller.Controllers
{

    [Route("api/Employee")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly BankContext _context;
        private readonly IEmployeesService employeesService;


        public EmployeeController(BankContext context)
        {
            _context = context;
            this.employeesService = employeesService;
        }

        // GET: api/Employee
        [HttpGet]
        public async Task<ActionResult<List<Employee>>> GetAllEmployees()
        {
            return await _context.Employees.ToListAsync();
        }

        // GET: api/Employee/5
        [HttpGet("{id}")]
        public async Task<ActionResult<EmployeeRequestModel>> GetCustomer(EmployeeRequestModel requestModel)
        {
            
            if (requestModel == null)
            {
                return NotFound();
            }

            return Ok(requestModel);
        }

        // POST: api/Employee
        [HttpPost]
        public async Task<ActionResult<Employee>> AddEmployee(EmployeeRequestModel employee)
        {
            try
            {
                var ceatedEmployee = await employeesService.AddEmployee(employee);
                return Ok(ceatedEmployee);
            }
            catch(ArgumentNullException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }         
        }

        // PUT: api/Employee/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateEmployee(Guid id, Employee employee)
        {
            if (id == Guid.Empty)
            {
                return BadRequest();
            }

            await employeesService.UpdateEmployee(id, employee);
            return Ok(id);
        }

        // DELETE: api/Employee/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEmployee(Guid id)
        {
            if (id == Guid.Empty)
            {
                return BadRequest();
            }
            await employeesService.DeleteEmployee(id);
            return NoContent();
        }
    }
}
