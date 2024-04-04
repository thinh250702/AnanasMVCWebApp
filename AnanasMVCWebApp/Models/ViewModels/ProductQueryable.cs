using AnanasMVCWebApp.Utilities;

namespace AnanasMVCWebApp.Models.ViewModels {
    public class ProductQueryable {
        private IFilterStrategy strategy;
        private IQueryable<ProductVariant> items;
        public ProductQueryable(IQueryable<ProductVariant> productList) {
            this.items = productList;
        }
        public IQueryable<ProductVariant> GetItems() {
            return this.items;
        }

        public void setFilterStrategy(IFilterStrategy strategy) {
            this.strategy = strategy;
        }
        public void performFilter(string options) {
            this.items = strategy.filter(items, options);
        }
    }
}
