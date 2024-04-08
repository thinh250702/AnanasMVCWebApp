using AnanasMVCWebApp.Models;

namespace AnanasMVCWebApp.Utilities
{
    public class ProductQueryable
    {
        private IProductFilterStrategy filterStrategy = new FilterByCategory(); // default filter strategy
        private IQueryable<ProductVariant> items;
        public ProductQueryable(IQueryable<ProductVariant> productList)
        {
            items = productList;
        }
        public IQueryable<ProductVariant> GetItems()
        {
            return items;
        }
        public void setFilterStrategy(IProductFilterStrategy strategy)
        {
            this.filterStrategy = strategy;
        }
        public void performFilter(string options)
        {
            items = filterStrategy.filter(items, options);
        }
    }
}
