using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AnanasMVCWebApp.Models {
    public class Customer : IdentityUser {
        public string FullName { get; set; }
        public DateTime Dob {  get; set; }
        public bool Gender { get; set; } // true = male, false = female
        public string? Address { get; set; }
        public string? City { get; set; }
        public string? District { get; set; }
        public string? Ward { get; set; }
    }
}
