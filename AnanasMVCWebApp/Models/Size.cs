﻿using System.ComponentModel.DataAnnotations;

namespace AnanasMVCWebApp.Models {
    public class Size {
        [Key]
        public string Id { get; set; }
        public string Name { get; set; }
    }
}
