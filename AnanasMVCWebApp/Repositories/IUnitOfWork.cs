using AnanasMVCWebApp.Models;
using AnanasMVCWebApp.Utilities;

namespace AnanasMVCWebApp.Repositories {
    public interface IUnitOfWork : IDisposable {
        public IProductRepository ProductRepository { get; }
        public IProductVariantRepository ProductVariantRepository { get; }
        public IProductSKURepository ProductSKURepository { get; }
        public ICategoryRepository CategoryRepository { get; }
        public ICollectionRepository CollectionRepository { get; }
        public IStyleRepository StyleRepository { get; }
        public IColorRepository ColorRepository { get; }
        public ISizeRepository SizeRepository { get; }
        public void Complete();
    }
    public class UnitOfWork : IUnitOfWork {
        private readonly DataContext _context;
        private readonly IProductRepository _productRepo;
        private readonly IProductVariantRepository _productVariantRepo;
        private readonly IProductSKURepository _productSKURepository;
        private readonly ICategoryRepository _categoryRepo;
        private readonly ICollectionRepository _collectionRepo;
        private readonly IStyleRepository _styleRepo;
        private readonly IColorRepository _colorRepo;
        private readonly ISizeRepository _sizeRepo;

        public UnitOfWork(DataContext context, IProductRepository productRepo, IProductVariantRepository productVariantRepo, IProductSKURepository productSKURepository, ICategoryRepository categoryRepo, ICollectionRepository collectionRepo, IStyleRepository styleRepo, IColorRepository colorRepo, ISizeRepository sizeRepo) {
            _context = context;
            _productRepo = productRepo;
            _productVariantRepo = productVariantRepo;
            _productSKURepository = productSKURepository;
            _categoryRepo = categoryRepo;
            _collectionRepo = collectionRepo;
            _styleRepo = styleRepo;
            _colorRepo = colorRepo;
            _sizeRepo = sizeRepo;
        }

        public IProductRepository ProductRepository { get { return _productRepo; } }
        public IProductVariantRepository ProductVariantRepository { get { return _productVariantRepo; } }
        public IProductSKURepository ProductSKURepository { get { return _productSKURepository; } }
        public ICategoryRepository CategoryRepository { get { return _categoryRepo; } }
        public ICollectionRepository CollectionRepository { get { return _collectionRepo; } }
        public IStyleRepository StyleRepository { get { return _styleRepo; } }
        public IColorRepository ColorRepository { get { return _colorRepo; } }
        public ISizeRepository SizeRepository { get { return _sizeRepo; } }

        public void Complete() {
            _context.SaveChanges();
        }

        public void Dispose() {
            _context.Dispose();
        }
    }
}
