using Credo.Data.IRepositories;
using Credo.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Credo.Data.Repositories
{
    public class CustomerRepository : GenericRepository<Customer>, ICustomerRepository
    {
        public CustomerRepository(SQLContext context, ILogger logger)
            : base(context, logger)
        {
        }

        public override async Task<IEnumerable<Customer>> All()
        {
            try
            {
                return await dbSet.ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{Repo} All method error", typeof(CustomerRepository));
                return new List<Customer>();
            }
        }

        public override async Task<bool> Upsert(Customer entity)
        {
            try
            {
                var existingCustomer = await dbSet.Where(p => p.Id == entity.Id).FirstOrDefaultAsync();

                if (existingCustomer != null)
                {
                    return await Add(entity);
                }

                existingCustomer.Username = entity.Username;
                existingCustomer.Name = entity.Name;
                existingCustomer.Lastname = entity.Lastname;
                existingCustomer.DateOfBirth = entity.DateOfBirth;
                existingCustomer.Email = entity.Email;
                existingCustomer.Phone = entity.Phone;
                existingCustomer.PasswordHash = entity.PasswordHash;
                existingCustomer.PasswordSalt = entity.PasswordSalt;
                existingCustomer.Address = entity.Address;
                existingCustomer.IsActive = entity.IsActive;

                return true;

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{Repo} All method error", typeof(CustomerRepository));
                return false;
            }
        }

        public override async Task<bool> Delete(Guid id)
        {
            try
            {
                var existingCustomer = await dbSet.Where(p => p.Id == id).FirstOrDefaultAsync();

                if (existingCustomer != null)
                {
                     dbSet.Remove(existingCustomer);
                    return true;
                }

                return false;

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{Repo} All method error", typeof(CustomerRepository));
                return false;
            }
        }

        public async Task<Customer> GetCustomerByUsername(string username)
        {
            try
            {
                var existingCustomer = await dbSet.Where(p => p.Username == username).FirstOrDefaultAsync();

                if (existingCustomer != null)
                {
                    return existingCustomer;
                }

                return new Customer();

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{Repo} All method error", typeof(CustomerRepository));
                return new Customer();
            }
        }
    }
}
