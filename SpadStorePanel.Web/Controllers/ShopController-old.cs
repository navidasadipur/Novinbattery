//using SpadStorePanel.Core.Models;
//using SpadStorePanel.Infrastructure;
//using SpadStorePanel.Infrastructure.Repositories;
//using SpadStorePanel.Infrastructure.Services;
//using SpadStorePanel.Web.ViewModels;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Web;
//using System.Web.Mvc;

//namespace SpadStorePanel.Web.Controllers
//{
//    public class ShopController : Controller
//    {
//        private readonly ProductService _productService;
//        private readonly ProductGroupsRepository _productGroupsRepository;
//        private readonly BrandsRepository _brandsRepository;
//        private readonly ProductsRepository _productsRepository;
//        private readonly ProductGalleriesRepository _productGalleriesRepository;
//        private readonly ProductMainFeaturesRepository _productMainFeaturesRepository;
//        private readonly ProductFeatureValuesRepository _productFeatureValuesRepository;
//        private readonly ProductCommentsRepository _productCommentsRepository;
//        private readonly SubFeaturesRepository _subFeaturesRepository;
//        private readonly MyDbContext _context;
//        public ShopController(MyDbContext context, SubFeaturesRepository subFeaturesRepository, ProductCommentsRepository productCommentsRepository, ProductFeatureValuesRepository productFeatureValuesRepository, ProductMainFeaturesRepository productMainFeaturesRepository, ProductGalleriesRepository productGalleriesRepository, ProductsRepository productsRepository, BrandsRepository brandsRepository, ProductGroupsRepository productGroupsRepository, ProductService productService)
//        {
//            _productService = productService;
//            _productGroupsRepository = productGroupsRepository;
//            _brandsRepository = brandsRepository;
//            _productsRepository = productsRepository;
//            _productFeatureValuesRepository = productFeatureValuesRepository;
//            _productMainFeaturesRepository = productMainFeaturesRepository;
//            _productGalleriesRepository = productGalleriesRepository;
//            _productCommentsRepository = productCommentsRepository;
//            _subFeaturesRepository = subFeaturesRepository;
//            _context = context;
//        }
//        // GET: Shop
//        public ActionResult Index(int? ProductGroupId=null, int page = 1)
//        {
//            int paresh = (page - 1) * 10;
//            //تعداد کل ردیف ها

//            int totalCount = _productService.GetProductsWithPrice().Count();


//            if (ProductGroupId != null)
//            {
//                totalCount = _productService.GetProductsWithPrice().Where(a => a.ProductGroupId == ProductGroupId).Count();
//            }

//            ViewBag.PageID = page;
//            double remain = totalCount % 10;

//            if (remain == 0)
//            {
//                ViewBag.PageCount = totalCount / 10;
//            }
//            else
//            {
//                ViewBag.PageCount = (totalCount / 10) + 1;
//            }
//            ViewBag.TotalCount = totalCount;
//            ViewBag.ProductGroupId = ProductGroupId;

//            var model = _productService.GetProductsWithPrice().OrderByDescending(x => x.Id).Skip(paresh).Take(10).ToList();

//            if (ProductGroupId != null)
//            {
//                 model = _productService.GetProductsWithPrice().Where(a => a.ProductGroupId == ProductGroupId).OrderByDescending(x => x.Id).Skip(paresh).Take(10).ToList();
//            }
//            return View(model);
//        }
//        public ActionResult ShowProducts(int page = 1)
//        {
//            int paresh = (page - 1) * 10;
//            //تعداد کل ردیف ها
//            int totalCount = _productService.GetProductsWithPrice().Count();
//            ViewBag.PageID = page;
//            double remain = totalCount % 10;

//            if (remain == 0)
//            {
//                ViewBag.PageCount = totalCount / 10;
//            }
//            else
//            {
//                ViewBag.PageCount = (totalCount / 10) + 1;
//            }
//            ViewBag.TotalCount = totalCount;

//            var model = _productService.GetProductsWithPrice().OrderByDescending(x => x.Id).Skip(paresh).Take(10).ToList();
//            return View("Index", model);
//        }
//        //public ActionResult ShowProducts(int page = 1)
//        //{
//        //    int paresh = (page - 1) * 3;
//        //    //تعداد کل ردیف ها
//        //    int totalCount = _productService.GetProductsWithPrice().Count();
//        //    ViewBag.PageID = page;
//        //    double remain = totalCount % 3;

//        //    if (remain == 0)
//        //    {
//        //        ViewBag.PageCount = totalCount / 3;
//        //    }
//        //    else
//        //    {
//        //        ViewBag.PageCount = (totalCount / 3) + 1;
//        //    }
//        //    ViewBag.TotalCount = totalCount;
//        //    var model = _productService.GetProductsWithPrice().OrderByDescending(x => x.Id).Skip(paresh).Take(3).ToList();
//        //    return View("Index",model);
//        //}



//        public ActionResult SendCommentProduct(int ProductId)
//        {
//            ViewBag.ProductId = ProductId;
//            return View();
//        }
//        public ActionResult ShowCommentProduct(int ProductId)
//        {
//            var productComments = _productCommentsRepository.GetAll().Where(a => a.ProductId == ProductId).ToList();
//            return View(productComments);
//        }

//        [HttpPost]
//        public string RegisterCommentProduct(ProductComment comment)
//        {


//            if (ModelState.IsValid)
//            {
//                comment.AddedDate = DateTime.Now;
//                _productCommentsRepository.Add(comment);
//                return "success";
//            }
//            return "fail";
//        }

//        public ActionResult GetTags()
//        {
//            return View(_context.ArticleTags.Where(a => a.IsDeleted == false).ToList());
//        }

//        public ActionResult NewProducts()
//        {
//            var model = _productService.GetProductsWithPrice().OrderByDescending(x => x.DateTime).Take(8).ToList();
//            return View(model);
//        }

//        public ActionResult ProductGroupSection()
//        {
//            var ProductGroup = _productGroupsRepository.GetAll().Where(a => a.IsDeleted == false).OrderByDescending(a => a.InsertDate).ToList();
//            return View(ProductGroup);
//        }

//        public ActionResult SizeSection(int? ProductGroupId)
//        {
//            var ProductSize = _subFeaturesRepository.GetSubFeatures(1003);
//            ViewBag.ProductGroupId = ProductGroupId;
//            return View(ProductSize);
//        }
//        public ActionResult GetBySize(int SizeId, int ProductGroupId, int page = 1)
//        {
//            var p = _productMainFeaturesRepository.productMainFeatures(SizeId, ProductGroupId);
//            int paresh = (page - 1) * 3;
//            //تعداد کل ردیف ها
//            int totalCount = _productService.GetProductsWithPriceSize(p).Count();
//            ViewBag.PageID = page;
//            double remain = totalCount % 3;

//            if (remain == 0)
//            {
//                ViewBag.PageCount = totalCount / 3;
//            }
//            else
//            {
//                ViewBag.PageCount = (totalCount / 3) + 1;
//            }
//            ViewBag.TotalCount = totalCount;
//            ViewBag.ProductGroupId = ProductGroupId;
//            ViewBag.SizeId = SizeId;
//            var model = _productService.GetProductsWithPriceSize(p).OrderByDescending(x => x.Id).Skip(paresh).Take(3).ToList();
//            return View("Index", model);

//        }
//        public ActionResult PriceSection(int? ProductGroupId)
//        {
//            ViewBag.ProductGroupId = ProductGroupId;
//            return View();
//        }
//        public ActionResult PriceFilter(int? ProductGroupId, int max, int min, int page = 1)
//        {

//            int paresh = (page - 1) * 3;
//            //تعداد کل ردیف ها
//            int totalCount = _productService.GetProductsWithPrice().Where(x => x.Price < max && x.Price > min).Count();
//            ViewBag.PageID = page;
//            double remain = totalCount % 3;

//            if (remain == 0)
//            {
//                ViewBag.PageCount = totalCount / 3;
//            }
//            else
//            {
//                ViewBag.PageCount = (totalCount / 3) + 1;
//            }
//            ViewBag.TotalCount = totalCount;
//            ViewBag.ProductGroupId = ProductGroupId;
//            ViewBag.min = min;
//            ViewBag.max = max;
//            var model = _productService.GetProductsWithPrice().Where(x => x.Price < max && x.Price > min).OrderByDescending(x => x.Id).Skip(paresh).Take(3).ToList();
//            return View("Index", model);
//        }
//        public ActionResult BrandSection(int? ProductGroupId)
//        {
//            var brands = _brandsRepository.brands(ProductGroupId).ToList();
//            ViewBag.ProductGroupId = ProductGroupId;
//            return View(brands);
//        }
//        public ActionResult GetByBrand(int BrandId, int? ProductGroupId, int page = 1)
//        {

//            int paresh = (page - 1) * 3;
//            //تعداد کل ردیف ها
//            int totalCount = _productService.GetProductsWithPrice().Where(x => x.BrandId == BrandId).Count();
//            ViewBag.PageID = page;
//            double remain = totalCount % 3;

//            if (remain == 0)
//            {
//                ViewBag.PageCount = totalCount / 3;
//            }
//            else
//            {
//                ViewBag.PageCount = (totalCount / 3) + 1;
//            }
//            ViewBag.TotalCount = totalCount;
//            ViewBag.ProductGroupId = ProductGroupId;
//            ViewBag.BrandId = BrandId;
//            var model = _productService.GetProductsWithPrice().Where(x => x.BrandId == BrandId).OrderByDescending(x => x.Id).Skip(paresh).Take(3).ToList();
//            return View("Index", model);
//        }
//        public ActionResult Name(int? ProductGroupId, int page = 1)
//        {
//            int paresh = (page - 1) * 3;
//            //تعداد کل ردیف ها
//            int totalCount = _productService.GetProductsWithPrice().Where(a => a.ProductGroupId == ProductGroupId).Count();
//            ViewBag.PageID = page;
//            double remain = totalCount % 3;

//            if (remain == 0)
//            {
//                ViewBag.PageCount = totalCount / 3;
//            }
//            else
//            {
//                ViewBag.PageCount = (totalCount / 3) + 1;
//            }
//            ViewBag.TotalCount = totalCount;
//            ViewBag.ProductGroupId = ProductGroupId;
//            var model = _productService.GetProductsWithPrice().Where(a => a.ProductGroupId == ProductGroupId).OrderByDescending(x => x.ShortTitle).Skip(paresh).Take(3).ToList();
//            return View("Index", model);
//        }
//        public ActionResult Price(int? ProductGroupId, int page = 1)

//        {
//            int paresh = (page - 1) * 3;
//            //تعداد کل ردیف ها
//            int totalCount = _productService.GetProductsWithPrice().Where(a => a.ProductGroupId == ProductGroupId).Count();
//            ViewBag.PageID = page;
//            double remain = totalCount % 3;

//            if (remain == 0)
//            {
//                ViewBag.PageCount = totalCount / 3;
//            }
//            else
//            {
//                ViewBag.PageCount = (totalCount / 3) + 1;
//            }
//            ViewBag.TotalCount = totalCount;
//            ViewBag.ProductGroupId = ProductGroupId;
//            var model = _productService.GetProductsWithPrice().Where(a => a.ProductGroupId == ProductGroupId).OrderByDescending(x => x.Price).Skip(paresh).Take(3).ToList();
//            return View("Index", model);
//        }

//        public ActionResult Detail(int id)
//        {
//            var product = _productsRepository.GetProduct(id);
//            var productGallery = _productGalleriesRepository.GetProductGalleries(id);
//            var productMainFeatures = _productMainFeaturesRepository.GetProductMainFeature(id);
//            var productFeatureValues = _productFeatureValuesRepository.GetProductFeatures(id);
//            var price = _productService.GetProductPrice(product);
//            //var priceAfterDiscount = _productService.GetProductPriceAfterDiscount(product);
//            var Productcomments = _productCommentsRepository.GetProductComments(id);


//            var vm = new ProductDetailViewModel()
//            {
//                Product = product,
//                ProductGalleries = productGallery,
//                ProductMainFeatures = productMainFeatures,
//                ProductFeatureValues = productFeatureValues,
//                Price = price,
//                Comments = Productcomments,
//                //PriceAfterDiscount = priceAfterDiscount,
//                //DiscountPercentage = (int)(priceAfterDiscount * 100 / price)
//            };
//            return View(vm);
//        }

//        public ActionResult RelatedProduct(int? ProductGroupId, int productid)
//        {
//            var model = _productService.GetProductsWithPrice().Where(a => a.ProductGroupId == ProductGroupId & a.Id != productid).OrderByDescending(x => x.DateTime).Skip(0).Take(10).ToList();
//            return View(model);
//        }

//        public ActionResult Favorit(int idGroupId, int idproduct)
//        {
//            var model = _productService.GetProductsWithPrice().Where(a => a.ProductGroupId == idGroupId && a.Id != idproduct).ToList();
//            return View(model);
//        }

//        [HttpGet]
//        public ActionResult NextProduct(int id)
//        {

//            List<Product> AllProduct = _productsRepository.GetAll().Where(a => a.IsDeleted == false).ToList();

//            int found = 0;
//            for (int i = 0; i < AllProduct.Count(); i++)
//            {
//                if (AllProduct[i].Id == id)
//                {
//                    found = i;
//                    break;
//                }
//            }
//            if (found != (AllProduct.Count() - 1))
//            {

//                return RedirectToAction("Detail", AllProduct[found + 1]);
//            }

//            return RedirectToAction("Detail", "Shop", AllProduct[found]);

//        }

//        [HttpGet]
//        public ActionResult PreviousProduct(int id)
//        {
//            List<Product> AllProduct = _productsRepository.GetAll().Where(a => a.IsDeleted == false).ToList();

//            int found = 0;
//            for (int i = 0; i < AllProduct.Count(); i++)
//            {
//                if (AllProduct[i].Id == id)
//                {
//                    found = i;
//                    break;
//                }
//            }
//            if (found != 0)
//            {

//                return RedirectToAction("Detail", AllProduct[found - 1]);
//            }

//            return RedirectToAction("Detail", "Shop", AllProduct[found]);

//        }

//        public ActionResult Search(String txtsearch, int page = 1)
//        {
//            if (!string.IsNullOrEmpty(txtsearch))
//            {

//                int paresh = (page - 1) * 3;
//                //تعداد کل ردیف ها
//                int totalCount = _productService.GetSearchProductsWithPrice(txtsearch).Count();
//                ViewBag.PageID = page;
//                double remain = totalCount % 3;

//                if (remain == 0)
//                {
//                    ViewBag.PageCount = totalCount / 3;
//                }
//                else
//                {
//                    ViewBag.PageCount = (totalCount / 3) + 1;
//                }
//                ViewBag.searchVal = txtsearch;
//                ViewBag.TotalCount = totalCount;

//                var model = _productService.GetSearchProductsWithPrice(txtsearch).OrderByDescending(x => x.Id).Skip(paresh).Take(3).ToList();
//                if (model.Count() == 0)
//                {
//                    return RedirectToAction("NotFoundSearch");
//                }
//                return View("Index", model);



//            }
//            else return RedirectToAction("Index");
//        }
//        public ActionResult NotFoundSearch()
//        {
//            return View();
//        }
//        public ActionResult ShowAllProduct()
//        {
//            ViewBag.allCategory = _productGroupsRepository.GetAll().Where(x=>x.IsDeleted==false).OrderByDescending(a => a.InsertDate).ToList();

//            var model = _productService.GetProductsWithPrice().OrderByDescending(x => x.Id).Take(10).ToList();
//            return View( model);
//        }
//    }
//}