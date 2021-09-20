using Credo.Domain.DTOs;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Credo.Domain.Interfaces
{
    public interface ICustomerRepo
    {
        Task<string> CreateCustomer(CreateCustomerDTO model);
    }
}
