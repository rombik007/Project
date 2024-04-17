﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Security.Cryptography.X509Certificates;

namespace Project.Models
{
    
        public class Account
        {
            [Key]
            public Guid Id { get; set; }
            public Guid CustomerID { get; set; }
            public string Name { get; set; }
            public string LastName { get; set; }
            public string City { get; set; }
            public string Email { get; set; }
            public string Address { get; set; }
            public string Phone { get; set; }
            public double Balance { get; set; }
        public List<Transactions> Transactions { get; set; }
        public List<Card> Cards { get; set; }
        public virtual Customer Customers { get; set; }

        [ForeignKey("Customer")]
        public Guid CustomerId { get; set; } // Змінено тип на Guid
    }

    
}