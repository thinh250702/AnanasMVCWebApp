namespace AnanasMVCWebApp.Models.ViewModels {
    public class ProductListViewModel {
        public List<ProductViewModel> ProductList { get; set; }
        public Category? Category { get; set; } = null;
        public List<Collection> CollectionFilter { get; set; }
        public List<Style>? StyleFilter { get; set; } = null;
        public List<Color> ColorFilter { get; set; }
        public ProductListViewModel() {
            ProductList = new List<ProductViewModel>();
        }
    }
}
