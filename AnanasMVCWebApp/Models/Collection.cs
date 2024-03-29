﻿using System.ComponentModel.DataAnnotations;

namespace AnanasMVCWebApp.Models {
    public class Collection {
        [Key]
        public string Id { get; set; }
        public string Name { get; set; }
        public string Slug { get; set; }
    }
}
