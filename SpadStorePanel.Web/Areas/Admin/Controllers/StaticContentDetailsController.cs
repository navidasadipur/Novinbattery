using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using SpadStorePanel.Core.Models;
using SpadStorePanel.Core.Utility;
using SpadStorePanel.Infrastructure;
using SpadStorePanel.Infrastructure.Helpers;
using SpadStorePanel.Infrastructure.Repositories;
using SpadStorePanel.Web.ViewModels;

namespace SpadStorePanel.Web.Areas.Admin.Controllers
{
    [Authorize]
    public class StaticContentDetailsController : Controller
    {
        private readonly StaticContentDetailsRepository _repo;
        private readonly StaticContentTypesRepository _staticContentTypesRepository;
        public StaticContentDetailsController(StaticContentDetailsRepository repo, StaticContentTypesRepository staticContentTypesRepository)
        {
            _repo = repo;
            _staticContentTypesRepository = staticContentTypesRepository;
        }
        // GET: Admin/StaticContentDetails
        public ActionResult Index()
        {
            return View(_repo.GetStaticContentDetails());
        }
        // GET: Admin/StaticContentDetails/Create
        public ActionResult Create()
        {
            // ViewBag.StaticContentTypeId = new SelectList(_repo.GetStaticContentTypes(), "Id", "Name");
            ViewBag.StaticContentTypeId = new SelectList(_staticContentTypesRepository.GetAll().Where(a => a.Id == 15), "Id", "Name");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(StaticContentDetail staticContentDetail, HttpPostedFileBase StaticContentDetailImage)
        {
            if (ModelState.IsValid)
            {
                #region Upload Image
                if (StaticContentDetailImage != null)
                {
                    // Saving Temp Image
                    var newFileName = Guid.NewGuid() + Path.GetExtension(StaticContentDetailImage.FileName);
                    StaticContentDetailImage.SaveAs(Server.MapPath("/Files/StaticContentImages/Temp/" + newFileName));

                    // Resizing Image
                    ImageResizer image = new ImageResizer();
                    //if (staticContentDetail.StaticContentTypeId == (int)StaticContentTypes.HomeTopSlider)
                    //    image = new ImageResizer(1413, 600, true);
                    //if (staticContentDetail.StaticContentTypeId == (int)StaticContentTypes.PageBanner)
                    //    image = new ImageResizer(1450, 250, true);
                    //if (staticContentDetail.StaticContentTypeId == (int)StaticContentTypes.About)
                    //    image = new ImageResizer(1450, 600, true);
                    //if (staticContentDetail.StaticContentTypeId == (int)StaticContentTypes.HeaderFooter)
                    //    image = new ImageResizer(1400, 1400, true);
                    if (staticContentDetail.Id == (int)StaticContents.HomeMidleBaner || staticContentDetail.Id == (int)StaticContents.HomeTopBaner)
                        image = new ImageResizer(2000, 1000, true);
                    if (staticContentDetail.Id == (int)StaticContents.HeaderBackGroundImage)
                        image = new ImageResizer(1700, 310, true);

                    image.Resize(Server.MapPath("/Files/StaticContentImages/Temp/" + newFileName),
                    Server.MapPath("/Files/StaticContentImages/Image/" + newFileName));

                    //var newFileName = Guid.NewGuid() + Path.GetExtension(StaticContentDetailImage.FileName);
                    //StaticContentDetailImage.SaveAs(Server.MapPath("/Files/StaticContentImages/Image/" + newFileName));

                    ImageResizer thumb = new ImageResizer();
                    thumb.Resize(Server.MapPath("/Files/StaticContentImages/Temp/" + newFileName),
                        Server.MapPath("/Files/StaticContentImages/Thumb/" + newFileName));

                    // Deleting Temp Image
                    System.IO.File.Delete(Server.MapPath("/Files/StaticContentImages/Temp/" + newFileName));

                    staticContentDetail.Image = newFileName;
                }
                #endregion

                _repo.Add(staticContentDetail);

                return RedirectToAction("Index");
            }
            ViewBag.StaticContentTypeId = new SelectList(_repo.GetStaticContentTypes(), "Id", "Name", staticContentDetail.StaticContentTypeId);
            return View(staticContentDetail);
        }

        // GET: Admin/StaticContentDetails/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            StaticContentDetail staticContentDetail = _repo.GetStaticContentDetail(id.Value);
            if (staticContentDetail == null)
            {
                return HttpNotFound();
            }
            //ViewBag.StaticContentTypeId = new SelectList(_repo.GetStaticContentTypes(), "Id", "Name", staticContentDetail.StaticContentTypeId);
            return View(staticContentDetail);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(StaticContentDetail staticContentDetail, HttpPostedFileBase StaticContentDetailImage)
        {
            if (ModelState.IsValid)
            {
                #region Upload Image
                if (StaticContentDetailImage != null)
                {
                    if (System.IO.File.Exists(Server.MapPath("/Files/StaticContentImages/Image/" + staticContentDetail.Image)))
                        System.IO.File.Delete(Server.MapPath("/Files/StaticContentImages/Image/" + staticContentDetail.Image));

                    if (System.IO.File.Exists(Server.MapPath("/Files/StaticContentImages/Thumb/" + staticContentDetail.Image)))
                        System.IO.File.Delete(Server.MapPath("/Files/StaticContentImages/Thumb/" + staticContentDetail.Image));

                    // Saving Temp Image
                    var newFileName = Guid.NewGuid() + Path.GetExtension(StaticContentDetailImage.FileName);
                    StaticContentDetailImage.SaveAs(Server.MapPath("/Files/StaticContentImages/Temp/" + newFileName));

                    // Resizing Image
                    ImageResizer image = new ImageResizer();
                    //if (staticContentDetail.StaticContentTypeId == (int)StaticContentTypes.HomeTopSlider)
                    //    image = new ImageResizer(1413, 600, true);
                    //if (staticContentDetail.StaticContentTypeId == (int)StaticContentTypes.PageBanner)
                    //    image = new ImageResizer(1450, 250, true);
                    //if (staticContentDetail.StaticContentTypeId == (int)StaticContentTypes.About)
                    //    image = new ImageResizer(1450, 600, true);
                    //if (staticContentDetail.StaticContentTypeId == (int)StaticContentTypes.HeaderFooter)
                    //    image = new ImageResizer(1000, 1000, true);
                    if (staticContentDetail.Id == (int)StaticContents.HomeMidleBaner || staticContentDetail.Id == (int)StaticContents.HomeTopBaner)
                        image = new ImageResizer(2000, 1000, true);
                    if (staticContentDetail.Id == (int)StaticContents.HeaderBackGroundImage)
                        image = new ImageResizer(1700, 310, true);

                    image.Resize(Server.MapPath("/Files/StaticContentImages/Temp/" + newFileName),
                    Server.MapPath("/Files/StaticContentImages/Image/" + newFileName));

                    //var newFileName = Guid.NewGuid() + Path.GetExtension(StaticContentDetailImage.FileName);
                    //StaticContentDetailImage.SaveAs(Server.MapPath("/Files/StaticContentImages/Image/" + newFileName));

                    ImageResizer thumb = new ImageResizer();
                    thumb.Resize(Server.MapPath("/Files/StaticContentImages/Temp/" + newFileName), Server.MapPath("/Files/StaticContentImages/Thumb/" + newFileName));
                    staticContentDetail.Image = newFileName;

                    // Deleting Temp Image
                    System.IO.File.Delete(Server.MapPath("/Files/StaticContentImages/Temp/" + newFileName));
                    staticContentDetail.Image = newFileName;
                }
                #endregion

                _repo.Update(staticContentDetail);
                return RedirectToAction("Index");
            }
            ViewBag.StaticContentTypeId = new SelectList(_repo.GetStaticContentTypes(), "Id", "Name", staticContentDetail.StaticContentTypeId);
            return View(staticContentDetail);
        }
        // GET: Admin/StaticContentDetails/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            StaticContentDetail staticContentDetail = _repo.GetStaticContentDetail(id.Value);
            if (staticContentDetail == null)
            {
                return HttpNotFound();
            }
            return PartialView(staticContentDetail);
        }

        // POST: Admin/StaticContentDetails/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var staticContentDetail = _repo.Get(id);

            //#region Delete StaticContentDetail Image
            //if (staticContentDetail.Image != null)
            //{
            //    if (System.IO.File.Exists(Server.MapPath("/Files/StaticContentImages/Image/" + staticContentDetail.Image)))
            //        System.IO.File.Delete(Server.MapPath("/Files/StaticContentImages/Image/" + staticContentDetail.Image));

            //    if (System.IO.File.Exists(Server.MapPath("/Files/StaticContentImages/Thumb/" + staticContentDetail.Image)))
            //        System.IO.File.Delete(Server.MapPath("/Files/StaticContentImages/Thumb/" + staticContentDetail.Image));
            //}
            //#endregion

            _repo.Delete(id);
            return RedirectToAction("Index");
        }
    }
}
