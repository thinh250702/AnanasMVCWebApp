using AnanasMVCWebApp.Models;

namespace AnanasMVCWebApp.Utilities {
    public interface IProductSortStrategy {
        public IQueryable<ProductVariant> Sort(IQueryable<ProductVariant> productQueryable, string option); //option: asc, desc
    }
    public class SortByStock : IProductSortStrategy {
        public IQueryable<ProductVariant> Sort(IQueryable<ProductVariant> productQueryable, string option) {
            throw new NotImplementedException();
        }
    }
    public class SortBySold : IProductSortStrategy {
        public IQueryable<ProductVariant> Sort(IQueryable<ProductVariant> productQueryable, string option) {
            throw new NotImplementedException();
        }
    }
}
