using System;
using System.Collections.Generic;
using System.Text;

namespace Credo.Domain.DTOs
{
    public class CreateCustomerDTO
    {
        public CreateCustomerDTO() { }

        public CreateCustomerDTO(string username, byte[] passwordHash, byte[] passwordSalt, string name, string lastname, string personalNumber, DateTime dateOfBirth, string phone, string email, string address)
        {
            Id = Guid.NewGuid();
            Username = username;
            PasswordHash = passwordHash;
            PasswordSalt = passwordSalt;
            Name = name;
            Lastname = lastname;
            PersonalNumber = personalNumber;
            DateOfBirth = dateOfBirth;
            Phone = phone;
            Email = email;
            Address = address;
            IsActive = true;
        }

        public Guid Id { get; set; }
        public string Username { get; set; }
        public string PasswordToHash { get; set; }
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
    }
}
