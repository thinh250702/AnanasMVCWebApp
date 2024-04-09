using AnanasMVCWebApp.Areas.Admin.Models;
using AnanasMVCWebApp.Models;
using AnanasMVCWebApp.Models.ViewModels;

namespace AnanasMVCWebApp.Services {
    public interface IProductService {
        public ProductViewModel ToProductViewModel(ProductVariant productVariant);
        public ProductViewModel? GetProductByCode(string code);
        public ProductListViewModel GetAllProductByFilters(string category, string collection, string style, string color, string price);
        public int GetProductMaxOrderQuantity(string code, int cartQuantity);
        public List<ProductViewModel> GetProductList();
        public ProductBaseEM GetProductForEdit(string code);
        public void UpdateProduct(ProductBaseEM model);
        
    }
}