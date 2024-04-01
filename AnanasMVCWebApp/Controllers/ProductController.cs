using AnanasMVCWebApp.Models;
using AnanasMVCWebApp.Models.ViewModels;
using AnanasMVCWebApp.Repository;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis;
using System.Diagnostics;
using System.Diagnostics.Tracing;
using System.Linq;

namespace AnanasMVCWebApp.Controllers {
    public class ProductController : Controller {
        private readonly DataContext _context;
        private readonly IWebHostEnvironment _environment;

        public ProductController(DataContext context, IWebHostEnvironment environment) {
            _context = context;
            _environment = environment;
        }
        public IActionResult Index(string category, string collection, string style, string color, string price, string page) {
            IQueryable<ProductVariant> productQueryable = _context.ProductVariants;
            var collectionFilter = new List<Collection>();
            var styleFilter = new List<Style>();
            Category? queryCategory = (category != null) ? _context.Categories.Where(c => c.Slug == category).FirstOrDefault() : null;

            if (category != null) {
                productQueryable = productQueryable.Where(p => p.Product.Collection.CategoryId == queryCategory.Id);
                collectionFilter = _context.Collections.Where(c => c.CategoryId == queryCategory.Id).ToList();
                styleFilter = (category == "shoes") ? _context.Styles.ToList() : null;
            } else {
                collectionFilter = _context.Collections.ToList();
                styleFilter = _context.Styles.ToList();
            }

            if (collection != null || style != null || color != null || price != null || page != null) {
                if (collection != null) {
                    var collectionOptions = collection.Split(",");
                    productQueryable = productQueryable.Where(c => collectionOptions.Contains(c.Product.Collection.Slug));
                }
                if (style != null) {
                    var styleOptions = style.Split(",");
                    productQueryable = productQueryable.Where(c => styleOptions.Contains(c.Product.Style.Slug));
                }
                if (color != null) {
                    var colorOptions = color.Split(",");
                    productQueryable = productQueryable.Where(c => colorOptions.Contains(c.Color.Slug));
                }
            }

            ViewBag.Category = queryCategory;
            ViewBag.Collection = collectionFilter;
            ViewBag.Style = styleFilter;
            ViewBag.ColorList = _context.Colors.ToList();
            return View(ToProductViewModelList(productQueryable));
        }
        public async Task<IActionResult> Detail(string id = "") {
            if (id == "") return RedirectToAction("Index");
            var variantById = _context.ProductVariants.Where(p => p.Id == id).FirstOrDefault();
            if (variantById == null) {
                return RedirectToAction("Index");
            } else {
                var product = variantById.Product;
                var productViewModel = new ProductViewModel() {
                    ProductId = variantById.Id,
                    ProductName = product.Name,
                    Description = product.Description,
                    Price = product.Price,
                    StockQuantity = 12,
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
        }
        [HttpGet]
        public IActionResult GetStock(string id) {
            var quantity = _context.ProductSKUs.Where(c => c.Id == id).FirstOrDefault();
            return Json(new { stock = (quantity != null) ? quantity.StockQuantity : -1 });
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
        [NonAction]
        public List<ProductViewModel> ToProductViewModelList(IQueryable<ProductVariant> queryable) {
            var list = new List<ProductViewModel>();
            queryable.ToList().ForEach(item => {
                Product product = item.Product;
                list.Add(new ProductViewModel() {
                    ProductId = item.Id,
                    ProductName = product.Name,
                    Price = product.Price,
                    ColorName = item.ColorName,
                    ImageList = GetAllImageById(item.Id),
                    Collection = product.Collection,
                    Style = product.Style
                });
            });
            return list;
        }
    }
}
