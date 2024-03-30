﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AnanasMVCWebApp.Models {
    public class Customer {
        [Key]
        public string Id { get; set; }
        public string Name { get; set; }
        public DateTime Dob {  get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public bool Gender { get; set; } // true = male, false = female
        public string? Address { get; set; }
        public string? City { get; set; }
        public string? District { get; set; }
        public string? Ward { get; set; }
        public Customer() {
            Id = Guid.NewGuid().ToString("N").Substring(0, 8);
        }

    }
}
