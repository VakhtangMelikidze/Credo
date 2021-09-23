using AutoMapper;
using Credo.Data.Configuration;
using Credo.Data.Shared;
using Credo.Domain.DTOs;
using Credo.Domain.Models;
using Credo.Interfaces;
using Credo.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace Credo.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly ILogger _logegr;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IJwtAuthenticationManager _jwtAuthenticationManager;

        public CustomerController(ILogger<CustomerController> logegr, IUnitOfWork unitOfWork, IMapper mapper, IJwtAuthenticationManager jwtAuthenticationManager)
        {
            _logegr = logegr;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _jwtAuthenticationManager = jwtAuthenticationManager;
        }

        [AllowAnonymous]
        [HttpPost("login")]
        public IActionResult Authentication([FromBody] UserModel userModel)
        {
            var customer = _unitOfWork.Customers.GetCustomerByUsername(userModel.Username);

            var validated = PasswordCreator.VerifyPasswordHash(userModel.Password, customer.Result.PasswordHash, customer.Result.PasswordSalt);

            if (!validated)
            {
                return Unauthorized();
            }

            var token = _jwtAuthenticationManager.Authenticate(userModel.Username);

            if (string.IsNullOrEmpty(token))
            {
                return Unauthorized();
            }

            return Ok(token);
        }

        #region Customer

        [AllowAnonymous]
        [HttpPost("create-Customer")]
        public async Task<IActionResult> CreateCustomer(CreateCustomerDTO customer)
        {
            //if (ModelState.IsValid)
            //{

            //}

            byte[] passwordHash, passwordSalt;
            PasswordCreator.CreatePasswordHash(customer.PasswordToHash, out passwordHash, out passwordSalt);

            var newCustomer = new Customer
            {
                Id = customer.Id,
                Username = customer.Username,
                PasswordHash = passwordHash,
                PasswordSalt = passwordSalt,
                Name = customer.Name,
                Lastname = customer.Lastname,
                PersonalNumber = customer.PersonalNumber,
                DateOfBirth = customer.DateOfBirth,
                Phone = customer.Phone,
                Email = customer.Email,
                Address = customer.Address,
                IsActive = customer.IsActive,
            };

            await _unitOfWork.Customers.Add(newCustomer);
            await _unitOfWork.ComplateAsync();

            return CreatedAtAction("GetItem", new { customer.Id });
        }

        [HttpGet("get-customer-by-id")]
        public async Task<IActionResult> GetItem(Guid id)
        {
            var customer = await _unitOfWork.Customers.GetById(id);

            if (customer != null)
            {
                return Ok(customer);
            }

            return NotFound();
        }

        [HttpGet("all-customers")]
        public async Task<IActionResult> GetAllCustumers()
        {
            var customer = await _unitOfWork.Customers.All();

            if (customer != null)
            {
                return Ok(customer);
            }

            return NotFound();
        }

        [HttpGet("delete-customer-by-id")]
        public async Task<IActionResult> DeleteCustomerById(Guid id)
        {
            var status = await _unitOfWork.Customers.Delete(id);
            await _unitOfWork.ComplateAsync();

            if (status)
            {
                return Ok(status);
            }

            return NotFound();
        }


        #endregion

        #region Loan Request

        [HttpPost("create-loan-request")]
        public async Task<IActionResult> CreateLoanRequest(CreateLoanRequestDTO loanRequest)
        {
            var customer = await _unitOfWork.Customers.GetById(loanRequest.CustomerId);

            var Request = new LoanRequst
            {
                Amount = loanRequest.Amount,
                Currency = loanRequest.Currency,
                From = loanRequest.From,
                To = loanRequest.To,
                ActionDate = loanRequest.ActionDate,
                Customer = customer,
                Type = loanRequest.Type,
                LoanStatus = loanRequest.LoanStatus
            };

            await _unitOfWork.LoanRequests.Add(Request);
            await _unitOfWork.ComplateAsync();

            return CreatedAtAction("EditLoanRequest", new { Request.Id });
        }

        [HttpPost("edit-loan-request")]
        public async Task<IActionResult> EditLoanRequest(EditLoadnRequestDTO editLoadnRequest)
        {
            var loanRequest = await _unitOfWork.LoanRequests.GetById(editLoadnRequest.Id);

            if (loanRequest != null)
            {
                var status = _unitOfWork.LoanRequests.Upsert(loanRequest);
                if (status.Result)
                {
                    await _unitOfWork.ComplateAsync();
                    return Ok(loanRequest);
                }
            }

            return NotFound();
        }

        [HttpGet("get-all-loan-requests")]
        public async Task<IActionResult> GetAllLoanRequests()
        {
            var loanRequests = await _unitOfWork.LoanRequests.All();

            if (loanRequests != null)
            {
                return Ok(loanRequests);
            }

            return NotFound();
        }

        [HttpGet("delete-loan-request-by-id")]
        public async Task<IActionResult> DeleteLoanRequetById(Guid id)
        {
            var requst = await _unitOfWork.LoanRequests.GetById(id);

            var status = await _unitOfWork.LoanRequests.Delete(id);
            await _unitOfWork.ComplateAsync();

            if (status)
            {
                return Ok(status);
            }

            return NotFound();
        }

        #endregion
    }
}
