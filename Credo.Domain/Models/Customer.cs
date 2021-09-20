using System;
using System.Collections.Generic;
using System.Text;

namespace Credo.Domain.Models
{
    public class Customer
    {
        public Guid Id { get; set; }
        public string Username { get; set; }
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }
        public string Name { get; set; }
        public string Lastname { get; set; }
        public string PersonalNumber { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public bool IsActive { get; set; }

        public List<LoanRequst> LoanRequst { get; set; }
    }
}
