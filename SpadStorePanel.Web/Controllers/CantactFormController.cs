using SpadStorePanel.Core.Models;
using SpadStorePanel.Infrastructure;
using SpadStorePanel.Infrastructure.Repositories;
using SpadStorePanel.Web.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SpadStorePanel.Web.Controllers
{
    public class CantactFormController : Controller
    {

        private readonly StaticContentDetailsRepository _staticContentDetailsRepository;
        private readonly ContactFormsRepository _contactFormsRepository;
        private readonly MyDbContext _context;
        public CantactFormController(MyDbContext context,ContactFormsRepository contactFormsRepository,StaticContentDetailsRepository staticContentDetailsRepository)
        {
            _staticContentDetailsRepository = staticContentDetailsRepository;
            _contactFormsRepository = contactFormsRepository;
            _context = context;
        }
        // GET: Cantact
        public ActionResult Index()
        {
            SocialViewModel socialViewModel = new SocialViewModel();
            FooterViewModel footerViewModel = new FooterViewModel();

            footerViewModel.Facebook = _staticContentDetailsRepository.Get(5);

            footerViewModel.Linkdin = _staticContentDetailsRepository.Get(6);

            footerViewModel.GooglePlus = _staticContentDetailsRepository.Get(41);

            footerViewModel.Pintrest = _staticContentDetailsRepository.Get(42);

            footerViewModel.Twitter = _staticContentDetailsRepository.Get(12);

            footerViewModel.Phone = _staticContentDetailsRepository.Get(9);

            footerViewModel.Email = _staticContentDetailsRepository.Get(8);

            footerViewModel.Address = _staticContentDetailsRepository.Get(7);

            return View(footerViewModel);
        }


        public ActionResult FormCantact()
        {
            return View();
        }
   
        [HttpPost]
        public string ContactUS(ContactForm form)
        {
            try
            {
                _context.ContactForms.Add(form);
                _context.SaveChanges();
      return "success";
            }
            catch
            {
                return "fail";
            }

        }
    }
}