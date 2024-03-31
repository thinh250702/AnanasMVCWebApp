﻿using System.ComponentModel.DataAnnotations;

namespace AnanasMVCWebApp.Models {
    public class OrderStatus {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public OrderStatus(string name) {
            Name = name;
        }
    }
}
