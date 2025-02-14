﻿using AnanasMVCWebApp.Models;

namespace AnanasMVCWebApp.Utilities.StrategyPattern
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
        public void SetFilterStrategy(IProductFilterStrategy strategy)
        {
            filterStrategy = strategy;
        }
        public void PerformFilter(string options)
        {
            items = filterStrategy.filter(items, options);
        }
    }
}
