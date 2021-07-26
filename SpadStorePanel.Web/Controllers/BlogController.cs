﻿using SpadStorePanel.Core.Models;
using SpadStorePanel.Core.Utility;
using SpadStorePanel.Infrastructure.Repositories;
using SpadStorePanel.Web.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SpadStorePanel.Web.Controllers
{
    public class BlogController : Controller
    {
        private readonly ArticlesRepository _articlesRepo;
        private readonly ArticleCategoriesRepository _articleCategoriesRepository;
        private readonly StaticContentDetailsRepository _contentRepo;
        private readonly ArticleCommentsRepository _articleCommentsRepository;

        public BlogController(ArticleCommentsRepository articleCommentsRepository, ArticleCategoriesRepository articleCategoriesRepository, ArticlesRepository articlesRepo, StaticContentDetailsRepository contentRepo)
        {
            _articlesRepo = articlesRepo;
            _contentRepo = contentRepo;
            _articleCategoriesRepository = articleCategoriesRepository;
            _articleCommentsRepository = articleCommentsRepository;
        }

        // GET: Blog
        public ActionResult Index(int? id = null, string searchString = null, int page = 1)
        {
            //ViewBag.BlogImage = _contentRepo.GetStaticContentDetail((int)StaticContents.BlogImage).Image;
            var articles = new List<Article>();
            if (id == null)
            {
                articles = _articlesRepo.GetArticles();
                if (!string.IsNullOrEmpty(searchString))
                {
                    ViewBag.BreadCrumb = $"جستجو {searchString}";
                    articles = articles.Where(a =>
                        a.Title.ToLower().Trim().Contains(searchString.ToLower().Trim()) || a.ShortDescription.ToLower()
                            .Trim().Contains(searchString.ToLower().Trim()) || a.Description.ToLower()
                            .Trim().Contains(searchString.ToLower().Trim()) || a.ArticleTags.Any(t => t.Title.ToLower().Trim().Contains(searchString.ToLower().Trim()))).ToList();
                }
            }
            else
            {
                var category = _articlesRepo.GetCategory(id.Value);
                if (category != null)
                {
                    ViewBag.BreadCrumb = category.Title;
                    articles = _articlesRepo.GetArticlesByCategory(id.Value);
                }
            }

            var articlelistVm = new List<ArticleListViewModel>();
            foreach (var item in articles)
            {
                var vm = new ArticleListViewModel(item);
                vm.Role = _articlesRepo.GetAuthorRole(item.UserId);
                vm.GroupArticleName = _articlesRepo.GetCategory(item.ArticleCategoryId.Value).Id.ToString();
                vm.CountVoute = item.ArticleComments.Count();
                articlelistVm.Add(vm);
            }

            int paresh = (page - 1) * 9;
            //تعداد کل ردیف ها
            int totalCount = articlelistVm.Count();
            ViewBag.PageID = page;
            double remain = totalCount % 9;

            if (remain == 0)
            {
                ViewBag.PageCount = totalCount / 9;
            }
            else
            {
                ViewBag.PageCount = (totalCount / 9) + 1;
            }
            ViewBag.TotalCount = totalCount;
            ViewBag.allCategory = _articlesRepo.GetArticleCategories();
            var model = articlelistVm.OrderByDescending(x => x.Id).Skip(paresh).Take(9).ToList();
            return View(model);


            //ViewBag.ArticleTags = _articlesRepo.GetArticleTags(articles.Id);
            //return View(articlelistVm);
        }

        public ActionResult CategoryBlogs(int BlogCategoryId, int page = 1)
        {
            var Articles = _articlesRepo.GetArticles().Where(a => a.ArticleCategoryId == BlogCategoryId).ToList();
            var articlelistVm = new List<ArticleListViewModel>();
            foreach (var item in Articles)
            {
                var vm = new ArticleListViewModel(item);
                vm.Role = _articlesRepo.GetAuthorRole(item.UserId);
                vm.GroupArticleName = _articlesRepo.GetCategory(item.ArticleCategoryId.Value).Title;
                vm.CountVoute = item.ArticleComments.Count();
                articlelistVm.Add(vm);
            }


            int paresh = (page - 1) * 3;
            //تعداد کل ردیف ها
            int totalCount = articlelistVm.Count();
            ViewBag.PageID = page;
            double remain = totalCount % 3;

            if (remain == 0)
            {
                ViewBag.PageCount = totalCount / 3;
            }
            else
            {
                ViewBag.PageCount = (totalCount / 3) + 1;
            }
            ViewBag.TotalCount = totalCount;
            ViewBag.BlogCategoryId = BlogCategoryId;
            var model = articlelistVm.OrderByDescending(x => x.Id).Skip(paresh).Take(3).ToList();
            return View("Index", model);

            //return View("Index", articlelistVm);
        }

        public ActionResult StaticBlog()
        {
            ViewBag.ImageAdvertiseBlog = _contentRepo.Get(25).Image;
            ViewBag.ShortDescriptionBlog = _contentRepo.Get(26).ShortDescription;
            return View();
        }
        public ActionResult CategoryBlog()
        {
            var CategoryBlog = _articleCategoriesRepository.GetAll().Where(a => a.IsDeleted == false);
            return View(CategoryBlog);
        }

        public ActionResult BlogDetail(int id)
        {
            ViewBag.Tags = _articlesRepo.GetArticleTags(id);
            var Article = _articlesRepo.GetArticle(id);
            var vm = new ArticleListViewModel(Article);
            vm.Role = _articlesRepo.GetAuthorRole(Article.UserId);
            vm.GroupArticleName = _articlesRepo.GetCategory(Article.ArticleCategoryId.Value).Title;
            vm.CountVoute = Article.ArticleComments.Count();
            ViewBag.ArticleTags = _articlesRepo.GetArticleTags(id);
            return View(vm);
        }
        public ActionResult RelatedPosts(String GroupArticleName, int id)
        {
            var RelatedPosts = _articlesRepo.GetRelatedArticlesByCategoryName(GroupArticleName, id);
            var articlelistVm = new List<ArticleListViewModel>();
            foreach (var item in RelatedPosts)
            {
                var vm = new ArticleListViewModel(item);
                vm.Role = _articlesRepo.GetAuthorRole(item.UserId);
                vm.GroupArticleName = _articlesRepo.GetCategory(item.ArticleCategoryId.Value).Title;
                vm.CountVoute = item.ArticleComments.Count();
                articlelistVm.Add(vm);
            }
            return View(articlelistVm);
        }
        //public ActionResult FormComment()
        //{
        //    return View();
        //}


        [HttpPost]
        public ActionResult PostComment(CommentFormViewModel form)
        {
            if (ModelState.IsValid)
            {
                var comment = new ArticleComment()
                {
                    ArticleId = form.ArticleId,
                    ParentId = form.ParentId,
                    Name = form.Name,
                    Email = form.Email,
                    Message = form.Message,
                    AddedDate = DateTime.Now,
                };
                _articlesRepo.AddComment(comment);
                return RedirectToAction("BlogDetail", new { id = form.ArticleId });
            }
            return RedirectToAction("BlogDetail", new { id = form.ArticleId });
        }

        public ActionResult ShowComment(int id)
        {
            var Comment = _articleCommentsRepository.GetArticleComments(id);
            return View(Comment);
        }
        public ActionResult SendComment(int id)
        {
            ViewBag.ArticleId = id;
            return View();
        }



        [HttpGet]
        public ActionResult NextBlog(int id)
        {


            var Articles = _articlesRepo.GetArticles();
            var articlelistVm = new List<ArticleListViewModel>();
            foreach (var item in Articles)
            {
                var vm = new ArticleListViewModel(item);
                vm.Role = _articlesRepo.GetAuthorRole(item.UserId);
                vm.GroupArticleName = _articlesRepo.GetCategory(item.ArticleCategoryId.Value).Title;
                vm.CountVoute = item.ArticleComments.Count();
                articlelistVm.Add(vm);
            }

            int found = 0;
            for (int i = 0; i < articlelistVm.Count(); i++)
            {
                if (articlelistVm[i].Id == id)
                {
                    found = i;
                    break;
                }
            }
            if (found != (articlelistVm.Count() - 1))
            {
                //return View("BlogDetail", articlelistVm[found + 1]);
                return RedirectToAction("BlogDetail", new { @id = articlelistVm[found + 1].Id });
            }

            return RedirectToAction("BlogDetail", new { @id = articlelistVm[found].Id });

        }

        [HttpGet]
        public ActionResult PreviousBlog(int id)
        {
            var Articles = _articlesRepo.GetArticles();
            var articlelistVm = new List<ArticleListViewModel>();
            foreach (var item in Articles)
            {
                var vm = new ArticleListViewModel(item);
                vm.Role = _articlesRepo.GetAuthorRole(item.UserId);
                vm.GroupArticleName = _articlesRepo.GetCategory(item.ArticleCategoryId.Value).Title;
                vm.CountVoute = item.ArticleComments.Count();
                articlelistVm.Add(vm);
            }

            int found = 0;
            for (int i = 0; i < articlelistVm.Count(); i++)
            {
                if (articlelistVm[i].Id == id)
                {
                    found = i;
                    break;
                }
            }
            if (found != 0)
            {
                //return View("BlogDetail", AllArticle[found - 1]);
                return RedirectToAction("BlogDetail", new { @id = articlelistVm[found - 1].Id });
            }

            //return View("BlogDetail", AllArticle[found]);
            return RedirectToAction("BlogDetail", new { @id = articlelistVm[found].Id });

        }

        public ActionResult SearchResult(String txtsearch, int page = 1)
        {
            if (!string.IsNullOrEmpty(txtsearch))
            {
                int paresh = (page - 1) * 3;
                //تعداد کل ردیف ها
                int totalCount = _articlesRepo.GetSearchArticle(txtsearch).Count();

                ViewBag.PageID = page;
                double remain = totalCount % 3;

                if (remain == 0)
                {
                    ViewBag.PageCount = totalCount / 3;
                }
                else
                {
                    ViewBag.PageCount = (totalCount / 3) + 1;
                }
                ViewBag.searchVal = txtsearch;
                ViewBag.TotalCount = totalCount;

                var Articles = _articlesRepo.GetSearchArticle(txtsearch);
                var articlelistVm = new List<ArticleListViewModel>();
                foreach (var item in Articles)
                {
                    var vm = new ArticleListViewModel(item);
                    vm.Role = _articlesRepo.GetAuthorRole(item.UserId);
                    vm.GroupArticleName = _articlesRepo.GetCategory(item.ArticleCategoryId.Value).Title;
                    vm.CountVoute = item.ArticleComments.Count();
                    articlelistVm.Add(vm);
                }
                var model = articlelistVm.OrderByDescending(x => x.Id).Skip(paresh).Take(3).ToList();
                return View("Index", model);
            }
            else return RedirectToAction("Index");
        }

        public ActionResult NewBlog()
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

        public ActionResult PopularBlog()
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
            var q = articlelistVm.OrderByDescending(a => a.CountVoute);
            //ViewBag.ArticleTags = _articlesRepo.GetArticleTags(articles.Id);
            return View(q);
        }


    }
}