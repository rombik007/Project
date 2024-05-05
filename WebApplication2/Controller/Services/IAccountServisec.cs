//using Microsoft.AspNetCore.Mvc;
//using Project.Models;
//using Project.Data;

//namespace Project.Controller.Services
//{
//    public interface IAccountServices
//    {
//        Task<ActionResult<List<Account>>> GetAllAccount();
//        Task<Account> GetAccount(Guid id);
//        Task<Guid> AddAccount(Account customer);
//        Task<Guid> UpdateAccount(Guid id, Account account);
//        Task<Guid> DeleteAccount(Guid id);

//    }
//    public class AccountService : IAccountServices
//    {
//        private readonly BankContext _context;

//        public AccountService(BankContext context)
//        {
//            _context = context;
//        }

//        public async Task<Guid> AddAccount(Account account)
//        {
//            account.Id = Guid.NewGuid();
//            _context.Accounts.Add(account);
//            await _context.SaveChangesAsync();

//            return account.Id;
//        }


//        public async Task<Guid> DeleteAccount(Guid id)
//        {
//            var account = await _context.Accounts.FindAsync(id);
//            if (account == null)
//            {
//                return Guid.Empty;
//            }

//            _context.Accounts.Remove(account);
//            await _context.SaveChangesAsync();

//            return id;
//        }

//        public async Task<ActionResult<List<Account>>> GetAllAccount()
//        {
//            var customers = await _context.Accounts.ToListAsync();

//            return customers;
//        }

//        public async Task<Account> GetAccount(Guid id)
//        {
//            var account = await _context.Accounts.FindAsync(id);

//            if (account == null)
//            {
//                return null;
//            }

//            return account;
//        }

//        public async Task<Guid> UpdateAccount(Guid id, Account newChanges)
//        {
//            var account = await _context.Accounts.FindAsync(id);
//            if (account == null)
//            {
//                return Guid.Empty;
//            }
//            account.Name = newChanges.Name;
//            account.Balance = newChanges.Balance;
//            account.EmployeeID = newChanges.EmployeeID;
//            account.CustomerId = newChanges.CustomerId;

//            _context.Entry(account).State = EntityState.Modified;
//            await _context.SaveChangesAsync();

//            return id;
//        }
//    }
//}


