using AutoMapper;
using Credo.Domain.DTOs;
using Credo.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Credo.Domain
{
    public class Mapper : Profile
    {
        public Mapper()
        {
            CreateMap<Customer, CreateCustomerDTO>();
            CreateMap<CreateCustomerDTO, Customer>();
        }
    }
}
