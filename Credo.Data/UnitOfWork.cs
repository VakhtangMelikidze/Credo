using Credo.Data.Configuration;
using Credo.Data.IRepositories;
using Credo.Data.Repositories;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace Credo.Data
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        private readonly SQLContext _context;
        private readonly ILogger _logger;

        public ICustomerRepository Customers { get; private set; }
        public ILoanRequestRepository LoanRequests { get; private set; }
        public ILogRequestorr LogRequests { get; set; }

        public UnitOfWork(SQLContext context, ILogger<UnitOfWork> logger)
        {
            _context = context;
            _logger = logger;

            Customers = new CustomerRepository(_context, _logger);
            LoanRequests = new LoanRequestRepository(_context, _logger);
            LogRequests = new LogRepository(_context, _logger);
        }

        public async Task ComplateAsync() 
        {
            await _context.SaveChangesAsync();
        }

        public async void Dispose()
        {
            await _context.DisposeAsync();
        }
    }
}
