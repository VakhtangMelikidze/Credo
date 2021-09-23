using Credo.Data.IRepositories;
using System.Threading.Tasks;

namespace Credo.Data.Configuration
{
    public interface IUnitOfWork
    {
        ICustomerRepository Customers { get; }
        ILoanRequestRepository LoanRequests { get; }
        ILogRequestorr LogRequests { get; }

        Task ComplateAsync();
    }
}
