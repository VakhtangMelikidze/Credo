using Credo.Data.IRepositories;
using Credo.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Credo.Data.Repositories
{
    public class LogRepository : GenericRepository<Log>, ILogRequestorr
    {
        public LogRepository(SQLContext context, ILogger logger)
            : base(context, logger)
        {

        }

        public override async Task<IEnumerable<Log>> All()
        {
            try
            {
                return await dbSet.ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{Repo} All method error", typeof(LogRepository));
                return new List<Log>();
            }
        }
        public override async Task<bool> Upsert(Log entity)
        {
            try
            {
                var existingLog = await dbSet.Where(p => p.Id == entity.Id).FirstOrDefaultAsync();

                if (existingLog != null)
                {
                    return await Add(entity);
                }

                existingLog.LogDate = entity.LogDate;
                existingLog.Body = entity.Body;
                return true;

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{Repo} All method error", typeof(LogRepository));
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
                _logger.LogError(ex, "{Repo} All method error", typeof(LogRepository));
                return false;
            }
        }
    }
}
