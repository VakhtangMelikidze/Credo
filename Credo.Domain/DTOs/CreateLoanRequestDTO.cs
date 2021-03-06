using Credo.Domain.Enums;
using Credo.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Credo.Domain.DTOs
{
    public class CreateLoanRequestDTO
    {
        public CreateLoanRequestDTO(decimal amount, string currency, DateTime from, DateTime to, DateTime actionDate, LoanType type, LoanStatus loanStatus, Guid customerId)
        {
            Id = Guid.NewGuid();
            Amount = amount;
            Currency = currency;
            From = from;
            To = to;
            ActionDate = actionDate;
            Type = type;
            LoanStatus = loanStatus;
            CustomerId = customerId;
        }

        public Guid Id { get; set; }
        public decimal Amount { get; set; }
        public string Currency { get; set; }
        public DateTime From { get; set; }
        public DateTime To { get; set; }
        public DateTime? ApproveDate { get; set; }
        public DateTime ActionDate { get; set; }
        public LoanType Type { get; set; }
        public LoanStatus LoanStatus { get; set; }
        public Guid CustomerId { get; set; }
    }
}
