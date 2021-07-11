using SpadStorePanel.Infrastructure.Repositories;
using SpadStorePanel.Web.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SpadStorePanel.Web.Controllers
{
    public class AboutUsController : Controller
    {
        private readonly StaticContentDetailsRepository _staticContentDetailsRepository;
        private readonly OurTeamRepository _ourTeamRepository;
        private readonly CertificatesRepository _certificatesRepository;
        private readonly ProductCommentsRepository _productCommentsRepository;


        public AboutUsController(CertificatesRepository certificatesRepository, ProductCommentsRepository productCommentsRepository, OurTeamRepository ourTeamRepository,StaticContentDetailsRepository staticContentDetailsRepository)
        {
            _staticContentDetailsRepository = staticContentDetailsRepository;
            _certificatesRepository = certificatesRepository;
            _productCommentsRepository = productCommentsRepository;
            _ourTeamRepository = ourTeamRepository;
        }
        // GET: AboutUs
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult History()
        {
           var Histoty= _staticContentDetailsRepository.GetAll().Where(a => a.Id == 16 && a.IsDeleted==false).FirstOrDefault();
            return View(Histoty);
        }
        public ActionResult Image()
        {
            //var Image = _staticContentDetailsRepository.Get(17).Image;
            ViewBag.Image = _staticContentDetailsRepository.Get(17);
            return View();
        }
        public ActionResult Text()
        {
           var Text= _staticContentDetailsRepository.GetAll().Where(a => a.StaticContentTypeId==11 && a.IsDeleted == false).ToList();
            return View(Text);
        }
        public ActionResult Statistics()
        {
            ViewBag.first = _staticContentDetailsRepository.GetStaticContentDetail(21);
            ViewBag.two = _staticContentDetailsRepository.GetStaticContentDetail(22);
            ViewBag.three = _staticContentDetailsRepository.GetStaticContentDetail(23);

            return View();
 
        }
        public ActionResult OurTeam()
        {
            var OrTeam = _ourTeamRepository.GetAll().Where(a => a.IsDeleted == false).ToList();

            return View(OrTeam);
        }
        public ActionResult CooperationList()
        {
            var CooperationList = _certificatesRepository.GetAll().Where(a => a.IsDeleted == false).ToList();
            return View(CooperationList);
        }
        public ActionResult ShowTopComments()
        {
            var model = _productCommentsRepository.GetAll().Where(x=>x.IsDeleted== false).OrderByDescending(x=>x.Rate).Take(8).ToList();
            return PartialView(model);
        }
        public ActionResult StaticDetailsInAboutPage()
        {
            var model = _staticContentDetailsRepository.GetContentByTypeId(18);
            return PartialView(model);
        }
    }
}