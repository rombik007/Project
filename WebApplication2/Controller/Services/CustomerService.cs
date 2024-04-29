using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Project.Models;
using Project.Model;
using System.Net;
using Project.Data;

namespace Project.Controller.Services
{
    public interface ICustomerService
    {
        Task<ActionResult<List<Customer>>> GetAllCustomers();
        Task<CustomerRequestModel> GetCustomer(Guid id);
        Task<Customer> AddCustomer(CustomerRequestModel customer);
        Task<Guid> UpdateCustomer(Guid id, Customer newChanges);
        Task<Guid> DeleteCustomer(Guid id);
    }
    public class CustomerService : ICustomerService
    {
        private readonly BankContext _context;

        public CustomerService(BankContext context)
        {
            _context = context;
        }
        public async Task<Customer> AddCustomer(CustomerRequestModel customer)
        {
            if (customer == null)
            {
                throw new ArgumentNullException(nameof(customer));
            }
            var res = MapRequestToCustomer(customer);

            _context.Customers.AddAsync(res);
            await _context.SaveChangesAsync();

            return res;
        }

        public async Task<Guid> DeleteCustomer(Guid id)
        {
            var account = await _context.Customers.FindAsync(id);
            if (account == null)
            {
                return Guid.Empty;
            }

            _context.Customers.Remove(account);
            await _context.SaveChangesAsync();

            return id;
        }

        public async Task<ActionResult<List<Customer>>> GetAllCustomers()
        {
            var customers = await _context.Customers.ToListAsync();

            return customers;
        }

        public async Task<CustomerRequestModel> GetCustomer(Guid id)
        {
            var customer = await _context.Customers.FindAsync(id);
            var res = MapRequestToDB(customer);

            if (customer == null)
            {
                return null;
            }

            return res;
        }

        public async Task<Guid> UpdateCustomer(Guid id, Customer newChanges)//need changes
        {
            var account = await _context.Customers.FindAsync(id);
            if (account == null)
            {
                return Guid.Empty;
            }
            account.CName = newChanges.CName;
            account.CLName = newChanges.CLName;
            account.Country = newChanges.Country;
            account.CPhone = newChanges.CPhone;
            account.CEmail = newChanges.CEmail;
            _context.Customers.Update(account);
            await _context.SaveChangesAsync();

            return id;
        }
        private Customer MapRequestToCustomer(CustomerRequestModel requestModel)
        {
            var customer = new Customer();
            customer.Id = Guid.NewGuid();
            customer.CName = requestModel.Name;
            customer.CLName = requestModel.LastName;
            customer.Country = requestModel.Country;
            customer.CPhone = requestModel.Phone;
            customer.CEmail = requestModel.Email;
            return customer;
        }

        private CustomerRequestModel MapRequestToDB(Customer customer)
        {
            var requestModel = new CustomerRequestModel();
            requestModel.Name = customer.CName;
            requestModel.LastName = customer.CLName;
            requestModel.Country = customer.Country;
            requestModel.Phone = customer.CPhone;
            requestModel.Email = customer.CEmail;

            return requestModel;
        }
    }
}
