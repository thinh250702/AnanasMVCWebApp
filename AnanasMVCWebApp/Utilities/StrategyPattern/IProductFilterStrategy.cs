using AnanasMVCWebApp.Models;
using AnanasMVCWebApp.Models.ViewModels;
using System.Drawing;

namespace AnanasMVCWebApp.Utilities.StrategyPattern
{
    public interface IProductFilterStrategy
    {
        public IQueryable<ProductVariant> filter(IQueryable<ProductVariant> products, string options);
    }
    public class FilterByCategory : IProductFilterStrategy
    {
        public IQueryable<ProductVariant> filter(IQueryable<ProductVariant> products, string options)
        {
            return products.Where(p => p.Product.Collection.Category.Slug == options);
        }
    }
    public class FilterByCollection : IProductFilterStrategy
    {
        public IQueryable<ProductVariant> filter(IQueryable<ProductVariant> products, string options)
        {
            var collectionOptions = options.Split(",");
            return products.Where(c => collectionOptions.Contains(c.Product.Collection.Slug));
        }
    }
    public class FilterByStyle : IProductFilterStrategy
    {
        public IQueryable<ProductVariant> filter(IQueryable<ProductVariant> products, string options)
        {
            var styleOptions = options.Split(",");
            return products.Where(c => styleOptions.Contains(c.Product.Style.Slug));
        }
    }
    public class FilterByColor : IProductFilterStrategy
    {
        public IQueryable<ProductVariant> filter(IQueryable<ProductVariant> products, string options)
        {
            var colorOptions = options.Split(",");
            return products.Where(c => colorOptions.Contains(c.Color.Slug));
        }
    }
    public class FilterByPrice : IProductFilterStrategy
    {
        public IQueryable<ProductVariant> filter(IQueryable<ProductVariant> products, string options)
        {
            throw new NotImplementedException();
        }
    }
}
