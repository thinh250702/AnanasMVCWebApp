using AnanasMVCWebApp.Models;
using AnanasMVCWebApp.Models.ViewModels;
using AnanasMVCWebApp.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis;
using System.Diagnostics;
using System.Linq;

namespace AnanasMVCWebApp.Controllers {
    public class ProductController : Controller {
        private readonly DataContext _context;
        private IWebHostEnvironment _environment;

        public ProductController(DataContext context, IWebHostEnvironment environment) {
            _context = context;
            _environment = environment;
        }
        public IActionResult Index(string category, string collection, string style, string color, string price, string page) {
            var products = new List<ProductViewModel>();
            _context.ProductVariants.ToList().ForEach(item => {
                Product product = item.Product;
                products.Add(new ProductViewModel() {
                    ProductId = item.Id,
                    ProductName = product.Name,
                    Price = product.Price,
                    ColorName = item.ColorName,
                    ImageList = GetAllImageById(item.Id),
                    Style = product.Style
                });
            });
            Category queryCategory = _context.Categories.Where(c => c.Slug == category).FirstOrDefault();
            ViewBag.Category = queryCategory;
            ViewBag.Collection = _context.Collections.Where(c => c.CategoryId == queryCategory.Id).ToList();
            ViewBag.Style = (category == "shoes") ? _context.Styles.ToList() : null;
            ViewBag.ColorList = _context.Colors.ToList();
            return View(products);
        }
        public async Task<IActionResult> Detail(string id = "") {
            if (id == "") RedirectToAction("Index");
            var variantById = _context.ProductVariants.Where(p => p.Id == id).FirstOrDefault();
            var product = variantById.Product;
            var productViewModel = new ProductViewModel() {
                ProductId = variantById.Id,
                ProductName = product.Name,
                Description = product.Description,
                Price = product.Price,
                ColorName = variantById.ColorName,
                HexCode = variantById.HexCode,
                Category = product.Collection.Category,
                Collection = product.Collection,
                Style = product.Style,
                ImageList = GetAllImageById(variantById.Id),
                SiblingProducts = GetAllSiblingProducts(variantById),
                SKUList = GetAllUnitOfVariant(variantById.Id)
            };
            return View(productViewModel);
        }
        [NonAction]
        public List<string> GetAllImageById(string id) {
            var imageList = new List<string>();
            string[] filePaths = Directory.GetFiles(Path.Combine(this._environment.WebRootPath, "uploads/"));
            foreach (string filePath in filePaths) {
                string name = Path.GetFileName(filePath);
                if (name.Contains(id)) {
                    imageList.Add(name);
                }
            }
            return imageList;
        }
        [NonAction]
        public List<Dictionary<string, string>> GetAllSiblingProducts(ProductVariant variant) {
            var siblings = new List<Dictionary<string, string>>();
            var siblingVariants = _context.ProductVariants.Where(x => x.ProductId == variant.ProductId && x.Id != variant.Id).ToList();
            siblingVariants.ForEach(x => {
                siblings.Add(new Dictionary<string, string>() {
                    { "productId", x.Id },
                    { "hexCode", x.HexCode },
                });
            });
            return siblings;
        }
        [NonAction]
        public List<ProductSKU> GetAllUnitOfVariant(string variantId) {
            var list = new List<ProductSKU>();
            _context.ProductSKUs.Where(i => i.ProductVariantId == variantId).ToList().ForEach(x => {
                list.Add(x);
            });
            return list;
        }
    }
}
