﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Project.Controller.Services;
using Project.Model;
using Project.Models;
using Project.Data;


namespace ProjectBank.Controller.Controllers
{
    [Route("api/customers")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly BankContext _context;
        private readonly CustomerService customerService;

        public CustomerController(BankContext context, CustomerService customerService)
        {
            _context = context;
            this.customerService = customerService;
        }

        [HttpGet]
        public async Task<ActionResult<List<Customer>>> GetAllCustomers() //work
        {
            var customers = await customerService.GetAllCustomers();

            return Ok(customers);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CustomerRequestModel>> GetCustomer(CustomerRequestModel requestModel) //work
        {
            if (requestModel == null)
            {
                return NotFound();
            }
            return Ok(requestModel);
        }

        [HttpPost]
        public async Task<ActionResult<Customer>> AddCustomer(CustomerRequestModel customer)
        {
            try
            {
                var createdCustomer = await customerService.AddCustomer(customer);
                return Ok(createdCustomer);
            }
            catch (ArgumentNullException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCustomer(Guid id, Customer customer) //Work
        {
            if (id == Guid.Empty)
            {
                return BadRequest();
            }
            await customerService.UpdateCustomer(id, customer);
            return Ok(id);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCustomer(Guid id) //work
        {
            if (id == Guid.Empty)
            {
                return BadRequest();
            }
            await customerService.DeleteCustomer(id);
            return NoContent();
        }

    }
}
