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
    public class LoanRequestRepository : GenericRepository<LoanRequst>, ILoanRequestRepository
    {
        public LoanRequestRepository(SQLContext context, ILogger logger)
            : base(context, logger)
        {

        }

        public override async Task<IEnumerable<LoanRequst>> All()
        {
            try
            {
                return await dbSet.ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{Repo} All method error", typeof(CustomerRepository));
                return new List<LoanRequst>();
            }
        }
        public override async Task<bool> Upsert(LoanRequst entity)
        {
            try
            {
                var existingCustomer = await dbSet.Where(p => p.Id == entity.Id).FirstOrDefaultAsync();

                if (existingCustomer != null)
                {
                    return await Add(entity);
                }

                existingCustomer.Amount = entity.Amount;
                existingCustomer.Currency = entity.Currency;
                existingCustomer.From = entity.From;
                existingCustomer.To = entity.To;
                existingCustomer.ApproveDate = entity.ApproveDate;
                existingCustomer.ActionDate = entity.ActionDate;
                existingCustomer.Customer = entity.Customer;
                existingCustomer.Type = entity.Type;
                existingCustomer.LoanStatus = entity.LoanStatus;

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
    }
}
