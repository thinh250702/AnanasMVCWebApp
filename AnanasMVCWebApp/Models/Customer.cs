using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AnanasMVCWebApp.Models {
    public class Customer {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime Dob {  get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public bool Gender { get; set; } // true = male, false = female
        public string? Address { get; set; }
        public string? City { get; set; }
        public string? District { get; set; }
        public string? Ward { get; set; }

        public Customer(string name, DateTime dob, string phone, string email, bool gender, string address = "", string city = "", string district = "", string ward = "") {
            Name = name;
            Dob = dob;
            Phone = phone;
            Email = email;
            Gender = gender;
            Address = address;
            City = city;
            District = district;
            Ward = ward;
        }
    }
}
