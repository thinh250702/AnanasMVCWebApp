namespace AnanasMVCWebApp.Areas.Admin.Models {
    public class ProductVM {
        public string Name { get; set; }
        public string Code { get; set; }
        public string Image {  get; set; }
        public string CategoryName { get; set; }
        public string? StyleName { get; set; }
        public string CollectionName { get; set; }
        public string ColorName { get; set; }
        public int Price { get; set; }
        public int InStock { get; set; }
        public int Sold { get; set; }
    }
}
