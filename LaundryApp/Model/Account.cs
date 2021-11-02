using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace LaundryApp.Model
{
    public class Account
    {
        [Key]
        public int? AccountId { get; set; }
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }
        public string AccountName { get; set; }
        public string Address { get; set; }
        [Required]
        public string Username { get; set; }
        public bool isDeleted { get; set; }
        public virtual ICollection<Order> Orders { get; set; }

    }
}
