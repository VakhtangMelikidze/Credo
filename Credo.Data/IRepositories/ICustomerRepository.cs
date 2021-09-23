using Credo.Domain.Models;
using System.Threading.Tasks;

namespace Credo.Data.IRepositories
{
    public interface ICustomerRepository : IGenericRepoisitory<Customer>
    {
        Task<Customer> GetCustomerByUsername(string username);
    }
}
