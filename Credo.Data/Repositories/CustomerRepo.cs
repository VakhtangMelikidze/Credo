using AutoMapper;
using Credo.Data.Shared;
using Credo.Domain.DTOs;
using Credo.Domain.Interfaces;
using Credo.Domain.Models;
using System;
using System.Threading.Tasks;

namespace Credo.Data.Repositories
{
    public class CustomerRepo : ICustomerRepo
    {
        private readonly IMapper _mapper;

        public CustomerRepo(IMapper mapper)
        {
            _mapper = mapper;
        }

        public Task<string> CreateCustomer(CreateCustomerDTO model)
        {
            byte[] passwordHash, passwordSalt;
            PasswordCreator.CreatePasswordHash(model.PasswordToHash, out passwordHash, out passwordSalt);

            var customer = _mapper.Map<Customer>(model);

            throw new NotImplementedException();
            //return Statuses.Success;
        }
    }
}
