using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Nyika.Domain.Entities.Stock
{
    public class Item
    {
        [Key]
        [HiddenInput(DisplayValue = false)]
        public long ItemID { get; set; }

        [Required]
        [MaxLength(50)]
        [Display(Name = "Item No")]
        public string ItemNo { get; set; }

        [MaxLength(255)]
        [Required]
        [Display(Name = "Item Name")]
        public string ItemName { get; set; }

        [MaxLength(255)]
        [Required]
        [Display(Name = "Item Name Local")]
        public string ItemNameLocal { get; set; }

        [MaxLength(512)]
        //[AllowHtmlAttribute]
        [Required]
        [DefaultValue("")]
        [DataType(DataType.MultilineText)]
        [Display(Name = "Description")]
        public string Description { get; set; }

        [MaxLength(512)]
        //[AllowHtmlAttribute]
        [Required]
        [DefaultValue("")]
        [DataType(DataType.MultilineText)]
        [Display(Name = "Description Local")]
        public string DescriptionLocal { get; set; }

        public long CategorySubID { get; set; }
        public virtual CategorySub CategorySub { get; set; }

        public long BrandID { get; set; }
        public virtual Brand Brand { get; set; }

        [MaxLength(255)]
        [Display(Name = "Type")]
        [DefaultValue("")]
        public string ItemType { get; set; }

        [MaxLength(255)]
        [DefaultValue("")]
        [Display(Name = "Type Local")]
        public string ItemTypeLocal { get; set; }

        [MaxLength(255)]
        [Display(Name = "Color")]
        [DefaultValue("")]
        public string Color { get; set; }

        [MaxLength(255)]
        [DefaultValue("")]
        [Display(Name = "Color Local")]
        public string ColorLocal { get; set; }

        [MaxLength(255)]
        [DefaultValue("")]
        [Display(Name = "Size")]
        public string Size { get; set; }

        [MaxLength(255)]
        [DefaultValue("")]
        [Display(Name = "Size Local")]
        public string SizeLocal { get; set; }

        [MaxLength(125)]
        [Display(Name = "Thumbnail")]
        public string Thumbnail { get; set; }

        [MaxLength(125)]
        [Display(Name = "Item Front Side")]
        public string FrontUrl { get; set; }

        [MaxLength(125)]
        [Display(Name = "Item Back Side")]
        public string BackUrl { get; set; }

        [MaxLength(125)]
        [Display(Name = "Item Left/Right Side")]
        public string SideUrl { get; set; }

        [DefaultValue(0)]
        [Display(Name = "Old Price")]
        [Range(0.0, double.MaxValue, ErrorMessage = "Please enter a positive price")]
        public decimal OldPrice { get; set; }

        [DefaultValue(0)]
        [Display(Name = "New Price")]
        [Range(0.0, double.MaxValue, ErrorMessage = "Please enter a positive price")]
        public decimal NewPrice { get; set; }

        [MaxLength(255)]
        [Display(Name = "Slug")]
        public string UrlLink { get; set; }

        [Display(Name = "Inactive")]
        [DefaultValue(false)]
        public bool Inactive { get; set; }

        [MaxLength(255)]
        [Display(Name = "Keywords")]
        [DefaultValue("")]
        public string Keywords { get; set; }

    }
}
