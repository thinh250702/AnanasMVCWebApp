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

        // Product Management
        public ProductBaseEM GetProductForEdit(string code);
        public ProductBaseEM GetProductForCreate();
        public bool UpdateProduct(ProductBaseEM model);
        public bool CreateProduct(ProductBaseEM model);
        public List<Collection> GetCollectionsByCategory(int id);
        public string GenerateProductCode(int collectionId, int count);
        public List<ProductSkuEM> GenerateSKUList(int categoryId, string code);

    }
}