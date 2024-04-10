using AnanasMVCWebApp.Models;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace AnanasMVCWebApp.Areas.Admin.Models {
    public class ProductBaseEM {
        public int ProductId { get; set; }
        [Required(ErrorMessage = "Vui lòng nhập tên sản phẩm"), 
        DisplayName("Tên sản phẩm")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Vui lòng chọn danh mục"), 
        DisplayName("Danh mục")]
        public int CategoryId { get; set; }
        public List<Category> Categories { get; set; } = new List<Category>();
        [DisplayName("Kiểu dáng")]
        public int StyleId { get; set; } = -1;
        public List<Style> Styles { get; set; } = new List<Style>();
        [Required(ErrorMessage = "Vui lòng chọn dòng sản phẩm"), 
        DisplayName("Dòng sản phẩm")]
        public int CollectionId { get; set; }
        public List<Collection> Collections { get; set; } = new List<Collection>();
        [Required(ErrorMessage = "Vui lòng nhập giá bán"),
        Range(0, int.MaxValue, ErrorMessage = "Please enter valid integer Number"), 
        DisplayName("Giá bán")]
        public int Price { get; set; }
        [Required(ErrorMessage = "Vui lòng nhập mô tả sản phẩm"),
        DisplayName("Mô tả")]
        public string Description { get; set; }
        public List<ProductVariantEM> Variants { get; set; } = new List<ProductVariantEM>();
        public ProductBaseEM() { }
    }
}
