using Credo.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Credo.Domain.Models
{
    public class LoanRequst
    {
        public Guid Id { get; set; }
        public decimal Amount { get; set; }
        public string Currency { get; set; }
        public DateTime From { get; set; }
        public DateTime To { get; set; }
        public DateTime? ApproveDate { get; set; }
        public DateTime ActionDate { get; set; }

        public Customer Customer { get; set; }

        public LoanType Type { get; set; }
        public LoanStatus LoanStatus { get; set; }

        public int Month => MonthCount(From, To) < 1 ? 1 : MonthCount(From, To);

        public int MonthCount(DateTime from, DateTime to)
        {
            var span = to.Subtract(from);
            return (int)span.TotalDays;
        }
    }
}
