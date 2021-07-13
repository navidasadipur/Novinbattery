using SpadStorePanel.Core.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SpadStorePanel.Web.ViewModels
{
    public class ProductDetailViewModel
    {
        public Product Product { get; set; }
        public List<ProductGallery> ProductGalleries { get; set; }
        public ProductMainFeature ProductMainFeatures { get; set; }
        public List<ProductFeatureValue> ProductFeatureValues { get; set; }
        public List<ProductComment> Comments { get; set; }
        public long Price { get; set; }
        public long PriceAfterDiscount { get; set; }
        public int DiscountPercentage { get; set; }

    }

    public class ProductCommentFormViewModel
    {
        public int? ParentId { get; set; }
        public int? ArticleId { get; set; }
        public int? ProductId { get; set; }
        [Display(Name = "نام")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(300, ErrorMessage = "{0} باید کمتر از 300 کارکتر باشد")]
        public string Name { get; set; }
        [Display(Name = "ایمیل")]
        [EmailAddress(ErrorMessage = "ایمیل نا معتبر")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(400, ErrorMessage = "{0} باید کمتر از 400 کارکتر باشد")]
        public string Email { get; set; }
        [Display(Name = "پیام")]
        [DataType(DataType.MultilineText)]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(800, ErrorMessage = "{0} باید کمتر از 800 کارکتر باشد")]
        public string Message { get; set; }
    }
}