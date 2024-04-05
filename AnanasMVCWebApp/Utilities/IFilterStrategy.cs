﻿using AnanasMVCWebApp.Models;
using AnanasMVCWebApp.Models.ViewModels;
using System.Drawing;

namespace AnanasMVCWebApp.Utilities {
    public interface IFilterStrategy {
        public IQueryable<ProductVariant> filter(IQueryable<ProductVariant> productQueryable, string options); 
    }
    public class FilterByCategory : IFilterStrategy {
        public IQueryable<ProductVariant> filter(IQueryable<ProductVariant> productQueryable, string options) {
            return productQueryable.Where(p => p.Product.Collection.Category.Slug == options);
        }
    }
    public class FilterByCollection : IFilterStrategy {
        public IQueryable<ProductVariant> filter(IQueryable<ProductVariant> productQueryable, string options) {
            var collectionOptions = options.Split(",");
            return productQueryable.Where(c => collectionOptions.Contains(c.Product.Collection.Slug));
        }
    }
    public class FilterByStyle : IFilterStrategy {
        public IQueryable<ProductVariant> filter(IQueryable<ProductVariant> productQueryable, string options) {
            var styleOptions = options.Split(",");
            return productQueryable.Where(c => styleOptions.Contains(c.Product.Style.Slug));
        }
    }
    public class FilterByColor : IFilterStrategy {
        public IQueryable<ProductVariant> filter(IQueryable<ProductVariant> productQueryable, string options) {
            var colorOptions = options.Split(",");
            return productQueryable.Where(c => colorOptions.Contains(c.Color.Slug));
        }
    }
    public class FilterByPrice : IFilterStrategy {
        public IQueryable<ProductVariant> filter(IQueryable<ProductVariant> productQueryable, string options) {
            throw new NotImplementedException();
        }
    }
}
