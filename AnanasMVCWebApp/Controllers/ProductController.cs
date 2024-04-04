using AnanasMVCWebApp.Models;
using AnanasMVCWebApp.Models.ViewModels;
using AnanasMVCWebApp.Utilities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis;

namespace AnanasMVCWebApp.Controllers {
    public class ProductController : Controller {
        private readonly DataContext _context;
        private readonly IWebHostEnvironment _environment;

        public ProductController(DataContext context, IWebHostEnvironment environment) {
            _context = context;
            _environment = environment;
        }
        public IActionResult Index(string category, string collection, string style, string color, string price, string page) {
            ProductQueryable productQueryable = new(_context.ProductVariants);
            var collectionFilter = new List<Collection>();
            var styleFilter = new List<Style>();
            Category? queryCategory = (category != null) ? _context.Categories.Where(c => c.Slug == category).FirstOrDefault() : null;

            if (queryCategory != null) {
                productQueryable.setFilterStrategy(new FilterByCategory());
                productQueryable.performFilter(category);

                collectionFilter = _context.Collections.Where(c => c.CategoryId == queryCategory.Id).ToList();
                styleFilter = (category == "shoes") ? _context.Styles.ToList() : null;
            } else {
                collectionFilter = _context.Collections.ToList();
                styleFilter = _context.Styles.ToList();
            }

            if (collection != null || style != null || color != null || price != null || page != null) {
                if (collection != null) {
                    productQueryable.setFilterStrategy(new FilterByCollection());
                    productQueryable.performFilter(collection);
                }
                if (style != null) {
                    productQueryable.setFilterStrategy(new FilterByStyle());
                    productQueryable.performFilter(style);
                }
                if (color != null) {
                    productQueryable.setFilterStrategy(new FilterByColor());
                    productQueryable.performFilter(color);
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
            var variantById = _context.ProductVariants.Where(p => p.Code == id).FirstOrDefault();
            if (variantById == null) {
                return RedirectToAction("Index");
            } else {
                var product = variantById.Product;
                var productViewModel = new ProductViewModel() {
                    ProductId = variantById.Code,
                    ProductName = product.Name,
                    Description = product.Description,
                    Price = product.Price,
                    StockQuantity = 12,
                    ColorName = variantById.ColorName,
                    HexCode = variantById.HexCode,
                    Category = product.Collection.Category,
                    Collection = product.Collection,
                    Style = product.Style,
                    ImageList = GetAllImageById(variantById.Code),
                    SiblingProducts = GetAllSiblingProducts(variantById),
                    SKUList = GetAllUnitOfVariant(variantById.Code)
                };
                return View(productViewModel);
            }
        }
        [HttpGet]
        public IActionResult GetStock(string id) {
            var product = _context.ProductSKUs.Where(c => c.Code == id).FirstOrDefault();
            int productStock = (product != null) ? product.StockQuantity : 0;
            int maxOrderQty = 0;
            List<CartItemViewModel> cartItems = HttpContext.Session.GetJson<List<CartItemViewModel>>("Cart");
            if (cartItems != null) {
                CartItemViewModel? item = cartItems.Where(c => c.ProductId == id).FirstOrDefault();
                if (item != null) {
                    maxOrderQty = (productStock - item.Quantity >= 12) ? 12 : productStock - item.Quantity;
                    return Json(new { stock = maxOrderQty });
                }
            }
            maxOrderQty = (productStock >= 12) ? 12 : productStock;
            return Json(new { stock = maxOrderQty });
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
                    { "productId", x.Code },
                    { "hexCode", x.HexCode },
                });
            });
            return siblings;
        }
        [NonAction]
        public List<ProductSKU> GetAllUnitOfVariant(string variantId) {
            var list = new List<ProductSKU>();
            _context.ProductSKUs.Where(i => i.ProductVariant.Code == variantId).ToList().ForEach(x => {
                list.Add(x);
            });
            return list;
        }
        [NonAction]
        public List<ProductViewModel> ToProductViewModelList(ProductQueryable queryable) {
            var list = new List<ProductViewModel>();
            queryable.GetItems().ToList().ForEach(item => {
                Product product = item.Product;
                list.Add(new ProductViewModel() {
                    ProductId = item.Code,
                    ProductName = product.Name,
                    Price = product.Price,
                    ColorName = item.ColorName,
                    ImageList = GetAllImageById(item.Code),
                    Collection = product.Collection,
                    Style = product.Style
                });
            });
            return list;
        }
    }
}
