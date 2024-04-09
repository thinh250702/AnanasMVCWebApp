using AnanasMVCWebApp.Models;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace AnanasMVCWebApp.Areas.Admin.Models {
    public class ProductBaseEM {
        public int ProductId { get; set; }
        [Required(ErrorMessage = "Vui lòng nhập tên sản phẩm"), DisplayName("Tên sản phẩm")]
        public string Name { get; set; }
        [DisplayName("Danh mục")]
        public int CategoryId { get; set; }
        public List<Category> Categories { get; set; }
        [DisplayName("Kiểu dáng")]
        public int StyleId { get; set; }
        public List<Style> Styles { get; set; }
        [DisplayName("Dòng sản phẩm")]
        public int CollectionId { get; set; }
        public List<Collection> Collections { get; set; }
        [DisplayName("Giá bán")]
        public int Price { get; set; }
        [DisplayName("Mô tả")]
        public string Description { get; set; }
        public List<ProductVariantEM> Variants { get; set; }
    }
}
