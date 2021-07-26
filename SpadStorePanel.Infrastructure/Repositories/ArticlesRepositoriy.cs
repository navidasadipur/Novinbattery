﻿using SpadStorePanel.Core.Models;
using SpadStorePanel.Infrastructure.Filters;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using Microsoft.AspNet.Identity;

namespace SpadStorePanel.Infrastructure.Repositories
{
    public class ArticlesRepository : BaseRepository<Article, MyDbContext>
    {
        private readonly MyDbContext _context;
        private readonly LogsRepository _logger;
        public ArticlesRepository(MyDbContext context, LogsRepository logger) : base(context, logger)
        {
            _context = context;
            _logger = logger;
        }

        public void AddComment(ArticleComment comment)
        {
            _context.ArticleComments.Add(comment);
            _context.SaveChanges();
        }

        public Article GetArticle(int id)
        {
            return _context.Articles.Where(a => a.IsDeleted == false).Include(a=>a.User).Include(a=>a.ArticleCategory).Include(a => a.ArticleComments).Include(a => a.ArticleTags).Include(a=>a.ArticleHeadLines).FirstOrDefault(a=>a.Id == id);
        }

        public List<Article> GetArticles()
        {
            return _context.Articles.Where(a=>a.IsDeleted == false).Include(a => a.User).Include(a=>a.ArticleCategory).Include(a=>a.ArticleComments).Include(a=>a.ArticleTags).OrderByDescending(a=>a.AddedDate).ToList();
        }
       
        public List<Article> GetSearchArticle(String txtsearch)
        {
            var SearchArticle = _context.Articles.Where(a =>
                    a.Title.ToLower().Trim().Contains(txtsearch.ToLower().Trim()) || a.ShortDescription.ToLower()
                        .Trim().Contains(txtsearch.ToLower().Trim()) || a.Description.ToLower()
                        .Trim().Contains(txtsearch.ToLower().Trim()) || a.ArticleTags.Any(t => t.Title.ToLower().Trim().Contains(txtsearch.ToLower().Trim()))&& a.IsDeleted == false).Include(a => a.User).Include(a => a.ArticleCategory).Include(a => a.ArticleComments).Include(a => a.ArticleTags).ToList();
            return SearchArticle;
        }

        public List<ArticleCategory> GetArticleCategories()
        {
            return _context.ArticleCategories.Where(a => a.IsDeleted == false).ToList();
        }

        public void AddArticle(Article article)
        {
            var user = GetCurrentUser();
            article.InsertDate = DateTime.Now;
            article.InsertUser = user.UserName;
            article.AddedDate = DateTime.Now;
            article.UserId = user.Id;
            _context.Articles.Add(article);
            _context.SaveChanges();
            _logger.LogEvent(article.GetType().Name, article.Id, "Add");
        }

        public string GetArticleTagsStr(int articleId)
        {
            var articleTags = _context.ArticleTags.Where(t => t.ArticleId == articleId && t.IsDeleted == false).Select(t=>t.Title).ToList();
            var tagsStr = string.Join("-", articleTags.ToList());
            return tagsStr;
        }

        public void AddArticleTags(int articleId, string articleTags)
        {
            if (string.IsNullOrEmpty(articleTags))
                return;
            var oldTags = _context.ArticleTags.Where(t => t.ArticleId == articleId).ToList();
            foreach (var tag in oldTags)
            {
                _context.ArticleTags.Remove(tag);
                _context.SaveChanges();
            }

            string[] tagsArr = articleTags.Trim().Split('-');
            foreach (var tag in tagsArr)
            {
                var tagObj = new ArticleTag();
                tagObj.ArticleId = articleId;
                tagObj.Title = tag.Trim();

                _context.ArticleTags.Add(tagObj);
                _context.SaveChanges();
            }
        }

        public void AddArticleHeadLine(ArticleHeadLine headLine)
        {
            var user = GetCurrentUser();
            headLine.InsertDate = DateTime.Now;
            headLine.InsertUser = user.UserName;
            _context.ArticleHeadLines.Add(headLine);
            _context.SaveChanges();
            _logger.LogEvent(headLine.GetType().Name, headLine.Id, "Add");
        }

        //public Article DeleteArticle(int articleId)
        //{
        //    var article = _context.Articles.Find(articleId);
        //    if (article == null)
        //        return null;
        //    var articleTags = _context.ArticleTags.Where(t => t.ArticleId == articleId).ToList();
        //    var articeheadLines = _context.
        //}

        public ArticleCategory GetCategory(int id)
        {
            return _context.ArticleCategories.Find(id);
        }

        public List<Article> GetArticlesByCategory(int categoryId)
        {
            return _context.Articles.Where(a => a.IsDeleted == false && a.ArticleCategoryId == categoryId).Include(a => a.User).Include(a => a.ArticleCategory).OrderByDescending(a => a.AddedDate).ToList();
        }

        public List<Article> GetRelatedArticlesByCategoryName(String GroupArticleName,int id)
        {
          var category=  _context.ArticleCategories.Where(a=> a.IsDeleted==false && a.Title== GroupArticleName).FirstOrDefault();
            return _context.Articles.Where(a => a.IsDeleted == false && a.ArticleCategoryId == category.Id && a.Id!= id).Include(a => a.User).Include(a => a.ArticleCategory).Include(a => a.ArticleComments).Include(a => a.ArticleTags).OrderByDescending(a => a.AddedDate).ToList();
        }

        public string GetAuthorRole(string userId)
        {
            var userRole = _context.UserRoles.FirstOrDefault(ur => ur.UserId == userId);
            var role = _context.Role.FirstOrDefault(r => r.Id == userRole.RoleId);
            return role.RoleNameLocal;
        }

        //public List<Article> GetSearchArticle(String txtsearch)
        //{
        //    var SearchArticle = _context.Articles.Where(a =>
        //            a.Title.ToLower().Trim().Contains(txtsearch.ToLower().Trim()) || a.ShortDescription.ToLower()
        //                .Trim().Contains(txtsearch.ToLower().Trim()) || a.Description.ToLower()
        //                .Trim().Contains(txtsearch.ToLower().Trim()) || a.ArticleTags.Any(t => t.Title.ToLower().Trim().Contains(txtsearch.ToLower().Trim()))).ToList();
        //    return SearchArticle;
        //}

        public List<ArticleTag> GetArticleTags(int articleId)
        {
            return _context.ArticleTags.Where(c => c.IsDeleted == false && c.ArticleId == articleId).ToList();
        }

        public List<Article> GetLatestArticles(int take)
        {
            return _context.Articles.Where(a => a.IsDeleted == false).Include(a => a.User).OrderByDescending(a => a.Id).Take(take).ToList();
        }
        public List<Article> GetTopArticles(int take)
        {
            return _context.Articles.Where(a => a.IsDeleted == false).Include(a => a.User)
                .OrderByDescending(a => a.ViewCount).Take(take).ToList();
        }
        #region Get Articles List
        public List<Article> GetArticlesList(int skip, int take)
        {
            return _context.Articles.Where(a => a.IsDeleted == false).Include(a => a.User).OrderByDescending(a => a.AddedDate).Skip(skip).Take(take).ToList();
        }
        public List<Article> GetArticlesList(int skip, int take, int categoryId)
        {
            return _context.Articles.Where(a => a.IsDeleted == false && a.ArticleCategoryId == categoryId).Include(a => a.User).OrderByDescending(a => a.AddedDate).Skip(skip).Take(take).ToList();
        }



        public List<Article> GetArticlesList(int skip, int take, string searchString)
        {
            var searchedArticles = new List<Article>();

            var trimedSearchString = searchString.Trim().ToLower();

            var articles = _context.Articles
                    .Where(a => a.IsDeleted == false && (
                           a.Title.Trim().ToLower().Contains(trimedSearchString)
                        || a.ShortDescription != null && a.ShortDescription.Trim().ToLower().Contains(trimedSearchString)
                        || a.Description != null && a.Description.Trim().ToLower().Contains(trimedSearchString)
                     ))
                .Include(a => a.User).OrderByDescending(a => a.AddedDate).Skip(skip).Take(take).ToList();

            var tags = _context.ArticleTags
                    .Where(t => t.IsDeleted == false && (
                           t.Title != null && t.Title.ToLower().Trim().Contains(trimedSearchString)
                   ))
                   .OrderByDescending(t => t.InsertDate).Skip(skip).Take(take).ToList();

            foreach (var tag in tags)
            {
                searchedArticles.Add(GetArticle(tag.ArticleId));
            }

            foreach (var article in articles)
            {
                if (!searchedArticles.Contains(article))
                {
                    searchedArticles.Add(article);
                }
            }

            //|| a.ArticleTags != null && a.ArticleTags.Any(t => t.Title != null && t.Title.ToLower().Trim().Contains(trimedSearchString)

            return searchedArticles;
        }
        #endregion
        #region Get Count
        public int GetArticlesCount()
        {
            return _context.Articles.Where(a => a.IsDeleted == false).Count();
        }
        public int GetArticlesCount(int categoryId)
        {
            return _context.Articles.Where(a => a.IsDeleted == false && a.ArticleCategoryId == categoryId).Count();
        }
        public int GetArticlesCount(string searchString)
        {
            return _context.Articles
                .Where(a => a.IsDeleted == false && (a.Title.Trim().ToLower().Contains(searchString.Trim().ToLower()) || a.ShortDescription.Trim().ToLower().Contains(searchString.Trim().ToLower())))
                .Count();
        }
        #endregion
        public List<ArticleComment> GetArticleComments(int articleId)
        {
            return _context.ArticleComments.Where(c => c.IsDeleted == false && c.ArticleId == articleId).ToList();
        }

        public List<ArticleHeadLine> GetArticleHeadlines(int articleId)
        {
            return _context.ArticleHeadLines.Where(c => c.IsDeleted == false && c.ArticleId == articleId).ToList();
        }

        public List<Article> GetArticlesByCategoryId(int categoryId)
        {
            return _context.Articles.Where(a => a.ArticleCategoryId == categoryId && a.IsDeleted == false).Include(a => a.User).Include(a => a.ArticleCategory).OrderBy(a => a.InsertDate).ToList();
        }

        public void UpdateArticleViewCount(int articleId)
        {
            var article = _context.Articles.Find(articleId);
            article.ViewCount++;
            _context.Entry(article).State = EntityState.Modified;
            _context.SaveChanges();
        }



        //public Article DeleteArticle(int articleId)
        //{
        //    var article = _context.Articles.Find(articleId);
        //    if (article == null)
        //        return null;
        //    var articleTags = _context.ArticleTags.Where(t => t.ArticleId == articleId).ToList();
        //    var articeheadLines = _context.
        //}

    }
}
