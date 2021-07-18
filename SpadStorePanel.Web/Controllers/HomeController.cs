using SpadStorePanel.Core.Models;
using SpadStorePanel.Core.Utility;
using SpadStorePanel.Infrastructure;
using SpadStorePanel.Infrastructure.Repositories;
using SpadStorePanel.Infrastructure.Services;
using SpadStorePanel.Web.ViewModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace SpadStorePanel.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly StaticContentDetailsRepository _contentRepo;
        private readonly StaticContentDetailsRepository _staticContentDetailsRepository;
        private readonly FaqRepository _faqRepository;
        private readonly UsersRepository _repo;
        private readonly MyDbContext _context;
        private readonly ContactFormsRepository _contactFormRepo;
        private readonly ProductGroupsRepository _productGroupsRepository;
        private readonly ContactFormsRepository _contactFormsRepository;
        private readonly ArticlesRepository _articlesRepo;
        private readonly ProductService _productService;
        private readonly ProductsRepository _productsRepository;

        public HomeController
            (
            StaticContentDetailsRepository contentRepo
            , ProductsRepository productsRepository
            , ProductService productService
            , ContactFormsRepository contactFormRepo
            , ArticlesRepository articlesRepo
            , ContactFormsRepository contactFormsRepository
            , ProductGroupsRepository productGroupsRepository
            , MyDbContext context, UsersRepository repo
            , FaqRepository faqRepository
            , StaticContentDetailsRepository staticContentDetailsRepository
            )
        {
            _contentRepo = contentRepo;
            _staticContentDetailsRepository = staticContentDetailsRepository;
            _faqRepository = faqRepository;
            _repo = repo;
            _context = context;
            _productGroupsRepository = productGroupsRepository;
            _contactFormsRepository = contactFormsRepository;
            _articlesRepo = articlesRepo;
            _productService = productService;
            _contactFormRepo = contactFormRepo;
            _productsRepository = productsRepository;
        }
        public ActionResult Index()
        {
            ////return Redirect("/Admin/Dashboard");
            var productGroups = _productGroupsRepository.GetAll().Where(a => a.ParentId == 37 & a.IsDeleted == false).Skip(0).Take(6).ToList();

            return View(productGroups);
        }

        public ActionResult HomeTopBannerSection()
        {
            var staticContentsList = new List<StaticContentDetail>();

            staticContentsList.Add(_staticContentDetailsRepository.GetStaticContentDetail((int)StaticContents.HomeTopBaner));

            return PartialView(staticContentsList);
        }

        public ActionResult BuyingStepsSection()
        {
            var model = _staticContentDetailsRepository.GetContentByTypeId((int)StaticContentTypes.BuyingStepsSection);

            return PartialView(model);
        }

        public ActionResult HomeMiddleBannerSection()
        {
            var staticContentsList = new List<StaticContentDetail>();

            staticContentsList.Add(_staticContentDetailsRepository.GetStaticContentDetail((int)StaticContents.HomeMidleBaner));

            return PartialView(staticContentsList);
        }

        public ActionResult SubMenu(int I, int ProductId)
        {
            ////return Redirect("/Admin/Dashboard");
            var ListProduct = _productsRepository.GetAll().Where(a => a.ProductGroupId == ProductId).Skip(0).Take(4).ToList();
            ViewBag.i = I;
            return View(ListProduct);
        }


        public ActionResult test()
        {
            ////return Redirect("/Admin/Dashboard");
            return View();
        }
        public ActionResult HeaderMobile()
        {
            ////return Redirect("/Admin/Dashboard");
            return View();
        }


        public ActionResult ContactUsSummary()
        {
            return View();
        }

        public ActionResult FooterSection()
        {
            SocialViewModel socialViewModel = new SocialViewModel();

            FooterViewModel model = new FooterViewModel();

            model.Facebook = _staticContentDetailsRepository.Get(5);

            model.Linkdin = _staticContentDetailsRepository.Get(6);

            model.GooglePlus = _staticContentDetailsRepository.Get(41);

            model.Pintrest = _staticContentDetailsRepository.Get(42);

            model.Twitter = _staticContentDetailsRepository.Get(12);

            model.Phone = _staticContentDetailsRepository.Get(9);

            model.Email = _staticContentDetailsRepository.Get(8);

            model.Address = _staticContentDetailsRepository.Get(7);

            return View(model);
        }

        public ActionResult SocialNetwork()
        {
            SocialViewModel socialViewModel = new SocialViewModel();

            FooterViewModel model = new FooterViewModel();

            model.Facebook = _staticContentDetailsRepository.Get(5);

            model.Linkdin = _staticContentDetailsRepository.Get(6);

            model.GooglePlus = _staticContentDetailsRepository.Get(41);

            model.Pintrest = _staticContentDetailsRepository.Get(42);

            model.Twitter = _staticContentDetailsRepository.Get(12);

            model.Phone = _staticContentDetailsRepository.Get(9);

            model.Email = _staticContentDetailsRepository.Get(8);

            model.Address = _staticContentDetailsRepository.Get(7);

            return View(model);
        }
        public ActionResult Faq()
        {
            var FaqList = _faqRepository.GetAll().Where(a => a.IsDeleted == false).ToList();
            return View(FaqList);

        }
        public ActionResult store()
        {
            return View();

        }

        public ActionResult HeaderSection()
        {
            //{
            //    var wishListModel = new WishListModel();

            //    try
            //    {
            //        HttpCookie cartCookie = Request.Cookies["wishList"] ?? new HttpCookie("wishList");

            //        if (!string.IsNullOrEmpty(cartCookie.Values["wishList"]))
            //        {
            //            string cartJsonStr = cartCookie.Values["wishList"];
            //            wishListModel = new WishListModel(cartJsonStr);

            //        }

            //    }
            //    catch (Exception e)
            //    {
            //        HttpCookie cartCookie = Request.Cookies["cart"] ?? new HttpCookie("cart");

            //        cartCookie.Values.Set("wishList", "");

            //        cartCookie.Expires = DateTime.Now.AddHours(12);
            //        cartCookie.SameSite = SameSiteMode.Lax;
            //        Response.Cookies.Add(cartCookie);
            //    }
            //    return View(wishListModel);
            return View();
        }

        public ActionResult Gallery()
        {
            var Gallry = _staticContentDetailsRepository.GetAll().Where(a => a.StaticContentTypeId == 9 && a.IsDeleted == false).ToList();
            return View(Gallry);
        }

        public ActionResult SliderShowSection()
        {
            var SliderShow = _staticContentDetailsRepository.GetAll().Where(a => a.StaticContentTypeId == 15 && a.IsDeleted == false).ToList();
            return View(SliderShow);
        }
        public ActionResult SliderConstSection()
        {
            var SliderConst = _staticContentDetailsRepository.GetAll().Where(a => a.StaticContentTypeId == 16 && a.IsDeleted == false).FirstOrDefault();
            //var sliders = new List<StaticContentDetail>();
            //foreach(var item in SliderConst)
            //{
            //    sliders.Add(item);
            //}


            return View(SliderConst);
        }

        public ActionResult ProductGroup()
        {
            var ProductGroup = _productGroupsRepository.GetAll().Where(a => a.IsDeleted == false).OrderByDescending(a => a.InsertDate).ToList();
            return View(ProductGroup);
        }
        public ActionResult ProductGroupMobile()
        {
            var ProductGroup = _productGroupsRepository.GetAll().Where(a => a.IsDeleted == false).OrderByDescending(a => a.InsertDate).ToList();
            return View(ProductGroup);
        }
        public ActionResult CategoryGroupMobile()
        {
            var ProductGroup = _productGroupsRepository.GetAll().Where(a => a.IsDeleted == false).OrderByDescending(a => a.InsertDate).ToList();
            return PartialView(ProductGroup);
        }

        public ActionResult SubscribeInNews()
        {
            return View();
        }
        //[HttpPost]
        //public string RegisterEmail(ContactForm contactForm)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        #region Check for duplicate username or email
        //        if (_contactFormsRepository.GetAll().Where(a=>a.Email== contactForm.Email).FirstOrDefault()!=null)
        //        {
        //            return "faill";
        //            //ViewBag.Message = "کاربر دیگری با همین ایمیل در سیستم ثبت شده";
        //            //return View(form);
        //        }
        //        #endregion
        //        _contactFormsRepository.Add(contactForm);
        //        //return Json(success, JsonRequestBehavior.AllowGet);
        //        return "success";
        //    }
        //    else
        //    {
        //        return "fail";
        //    }


        //}


        public string RegisterEmail(ContactForm contactForm)
        {
            if (ModelState.IsValid)
            {
                #region Check for duplicate username or email
                if (_contactFormsRepository.GetAll().Where(a => a.Email == contactForm.Email).FirstOrDefault() != null)
                {
                    return "faill";
                    //ViewBag.Message = "کاربر دیگری با همین ایمیل در سیستم ثبت شده";
                    //return View(form);
                }
                #endregion
                _context.ContactForms.Add(contactForm);
                _context.SaveChanges();
                // _contactFormsRepository.Add(contactForm);
                //return Json(success, JsonRequestBehavior.AllowGet);
                return "success";
            }
            else
            {
                return "fail";
            }


        }
        public ActionResult Service()
        {
            return View();
        }

        public ActionResult TwoImage()
        {
            if (_staticContentDetailsRepository.GetAll().Where(a => a.Id == 32 && a.IsDeleted == false).FirstOrDefault() != null)
                ViewBag.Image1 = _staticContentDetailsRepository.GetAll().Where(a => a.Id == 32 && a.IsDeleted == false).FirstOrDefault();
            if (_staticContentDetailsRepository.GetAll().Where(a => a.Id == 33 && a.IsDeleted == false).FirstOrDefault() != null)
                ViewBag.Image2 = _staticContentDetailsRepository.GetAll().Where(a => a.Id == 33 && a.IsDeleted == false).FirstOrDefault();
            return View();
        }
        public ActionResult MuchSellProducts()
        {
            var model = _productService.GetTopSoldProductsWithPrice(10).Take(15);
            return View(model);
        }
        public ActionResult PopularProducts(int take)
        {
            var model = _productService.GetHighRatedProductsWithPrice(take);
            return View(model);
        }
        public ActionResult NewProducts(int take)
        {
            var model = _productService.GetLatestProductsWithPrice(take);
            return View(model);
        }
        public ActionResult HomeNewProducts(int take)
        {
            var model = _productService.GetLatestProductsWithPrice(take);
            return View(model);
        }
        public ActionResult SpecialProducts(int take)
        {
            var model = _productService.GetTopSoldProductsWithPrice(take);
            return View(model);
        }
        public ActionResult BestsellersProducts(int take)
        {
            var model = _productService.GetTopSoldProductsWithPrice(take);
            return View(model);
        }

        public ActionResult bannerad()
        {
            var Banner = _staticContentDetailsRepository.GetAll().Where(a => a.Id == 27 && a.IsDeleted == false).FirstOrDefault();
            return View(Banner);
        }

        public ActionResult NewArticle()
        {
            var articles = new List<Article>();

            articles = _articlesRepo.GetArticles();
            var articlelistVm = new List<ArticleListViewModel>();
            foreach (var item in articles)
            {
                var vm = new ArticleListViewModel(item);
                vm.Role = _articlesRepo.GetAuthorRole(item.UserId);
                vm.GroupArticleName = _articlesRepo.GetCategory(item.ArticleCategoryId.Value).Title;
                vm.CountVoute = item.ArticleComments.Count();
                articlelistVm.Add(vm);
            }

            //ViewBag.ArticleTags = _articlesRepo.GetArticleTags(articles.Id);
            return View(articlelistVm.Skip(0).Take(3));
        }
        public ActionResult Appreciation()
        {
            if (_staticContentDetailsRepository.GetAll().Where(a => a.StaticContentTypeId == 6 && a.IsDeleted == false).ToList().Count() > 0)
            {
                var appreciation = _staticContentDetailsRepository.GetAll().Where(a => a.StaticContentTypeId == 6 && a.IsDeleted == false).ToList();
                return View(appreciation);
            }
            return View();

        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Contact(ContactForm contactForm)
        {
            if (ModelState.IsValid)
            {
                _contactFormRepo.Add(contactForm);
                return RedirectToAction("Contact");
            }
            return View(contactForm);
        }
        public ActionResult ContactUsStaticDetail()
        {
            var take = 3;
            var data = _contentRepo.GetContentByTypeIdTake(8, take);
            return PartialView(data);
        }
        public ActionResult UploadImage(HttpPostedFileBase upload, string CKEditorFuncNum, string CKEditor, string langCode)
        {
            string vImagePath = String.Empty;
            string vMessage = String.Empty;
            string vFilePath = String.Empty;
            string vOutput = String.Empty;
            try
            {
                if (upload != null && upload.ContentLength > 0)
                {
                    var vFileName = DateTime.Now.ToString("yyyyMMdd-HHMMssff") +
                                    Path.GetExtension(upload.FileName).ToLower();
                    var vFolderPath = Server.MapPath("/Upload/");
                    if (!Directory.Exists(vFolderPath))
                    {
                        Directory.CreateDirectory(vFolderPath);
                    }
                    vFilePath = Path.Combine(vFolderPath, vFileName);
                    upload.SaveAs(vFilePath);
                    vImagePath = Url.Content("/Upload/" + vFileName);
                    vMessage = "Image was saved correctly";
                }
            }
            catch
            {
                vMessage = "There was an issue uploading";
            }
            vOutput = @"<html><body><script>window.parent.CKEDITOR.tools.callFunction(" + CKEditorFuncNum + ", \"" + vImagePath + "\", \"" + vMessage + "\");</script></body></html>";
            return Content(vOutput);
        }


        public ActionResult Navbar()
        {
            return PartialView();
        }
        public ActionResult Footer()
        {
            SocialViewModel socialViewModel = new SocialViewModel();

            FooterViewModel model = new FooterViewModel();

            model.Facebook = _staticContentDetailsRepository.Get(5);

            model.Linkdin = _staticContentDetailsRepository.Get(6);

            model.GooglePlus = _staticContentDetailsRepository.Get(41);

            model.Pintrest = _staticContentDetailsRepository.Get(42);

            model.Twitter = _staticContentDetailsRepository.Get(12);

            model.Phone = _staticContentDetailsRepository.Get(9);

            model.Email = _staticContentDetailsRepository.Get(8);

            model.Address = _staticContentDetailsRepository.Get(7);

            ViewBag.AboutUs = _staticContentDetailsRepository.GetStaticContentDetail(16).ShortDescription;

            return PartialView(model);
        }
        public ActionResult Navbar2()
        {
            return PartialView();
        }
        public ActionResult Footer2()
        {
            SocialViewModel socialViewModel = new SocialViewModel();

            FooterViewModel model = new FooterViewModel();

            model.Facebook = _staticContentDetailsRepository.Get(5);

            model.Linkdin = _staticContentDetailsRepository.Get(6);

            model.GooglePlus = _staticContentDetailsRepository.Get(41);

            model.Pintrest = _staticContentDetailsRepository.Get(42);

            model.Twitter = _staticContentDetailsRepository.Get(12);

            model.Phone = _staticContentDetailsRepository.Get(9);

            model.Email = _staticContentDetailsRepository.Get(8);

            model.Address = _staticContentDetailsRepository.Get(7);

            ViewBag.AboutUs = _staticContentDetailsRepository.GetStaticContentDetail(16).ShortDescription;

            return PartialView(model);
        }
        public ActionResult Map()
        {

            return PartialView(_contentRepo.GetStaticContentDetail(11));
        }
        public ActionResult MobileNav()
        {
            return PartialView();
        }
        public ActionResult OurHonorInFooter()
        {
            var model = _staticContentDetailsRepository.GetContentByTypeId(6);
            return PartialView(model);
        }
        public ActionResult BannerSectionBig()
        {
            var model = _staticContentDetailsRepository.GetStaticContentDetail(1054);
            return PartialView(model);
        }
        public ActionResult BannerSectionMd()
        {
            var model = _staticContentDetailsRepository.GetStaticContentDetail(1055);
            return PartialView(model);
        }
        public ActionResult BannerSectionSm1()
        {
            var model = _staticContentDetailsRepository.GetStaticContentDetail(1056);
            return PartialView(model);
        }
        public ActionResult BannerSectionSm2()
        {
            var model = _staticContentDetailsRepository.GetStaticContentDetail(1057);
            return PartialView(model);
        }
    }
}