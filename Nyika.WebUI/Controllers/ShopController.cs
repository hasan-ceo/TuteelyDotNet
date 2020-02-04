using CaptchaMvc.HtmlHelpers;
using Nyika.Domain.Abstract.AVL;
using Nyika.Domain.Abstract.Setup;
using Nyika.Domain.Abstract.Stock;
using Nyika.Domain.Entities.AVL;
using Nyika.Domain.Entities.Setup;
using Nyika.WebUI.Models;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace Nyika.WebUI.Controllers
{
    public class ShopController : Controller
    {
        private IShopPageRepo db;
        private IFeedbackRepo fdb;
        private ICategoryRepo cdb;
        private ICategorySubRepo csdb;
        public IItemRepo idb;

        public ShopController(IShopPageRepo DB, IFeedbackRepo FDB, ICategoryRepo CDB, ICategorySubRepo CSDB, IItemRepo IDB)
        {
            this.db = DB;
            this.fdb = FDB;
            this.cdb = CDB;
            this.csdb = CSDB;
            this.idb = IDB;
        }

        [Route("Item/{UrlLink}")]
        public ActionResult CatList(string UrlLink = "")
        {
            string selectedlanguage = Request.Cookies["Language"].Value;
            if (string.IsNullOrEmpty(UrlLink) == true)
            {
                return RedirectToAction("Index");
            }
            var cat = cdb.Category.Where(c => c.UrlLink == UrlLink).FirstOrDefault();
            if (cat != null)
            {
                CatListVM catListVM = new CatListVM();
                catListVM.CategoryName = (selectedlanguage == "bn" ? cat.CategoryNameLocal : cat.CategoryName);
                catListVM.HeaderUrl = cat.HeaderUrl;
                catListVM.categorySubVM = (from g in csdb.CategorySub
                                           where g.CategoryID == cat.CategoryID
                                           select new CategorySubVM
                                           {
                                               UrlLink = "~/Item/" + g.UrlLink,
                                               CategoryName = (selectedlanguage == "bn" ? g.CategorySubNameLocal : g.CategorySubName),
                                               LogoUrl = g.LogoUrl
                                           }).ToList();
                return View("CatList", catListVM);
            }
            else
            {
                var catsub = csdb.CategorySub.Where(c => c.UrlLink == UrlLink).FirstOrDefault();
                if (catsub != null)
                {
                    ItemListVM itemListVM = new ItemListVM();
                    itemListVM.HeaderUrl = catsub.HeaderUrl;
                    itemListVM.CategoryName = (selectedlanguage == "bn" ? catsub.Category.CategoryNameLocal : catsub.Category.CategoryName);
                    itemListVM.CategoryUrl = catsub.Category.UrlLink;
                    itemListVM.CategorySubName = (selectedlanguage == "bn" ? catsub.CategorySubNameLocal : catsub.CategorySubName);
                    itemListVM.itemVM = (from g in idb.Item
                                         where g.CategorySubID == catsub.CategorySubID
                                         select new ItemVM
                                         {

                                             ItemID = g.ItemID,
                                             ItemNo = g.ItemNo,
                                             UrlLink = "~/Details/" + g.UrlLink,
                                             ItemName = (selectedlanguage == "bn" ? g.ItemNameLocal : g.ItemName),
                                             Thumbnail = g.Thumbnail,
                                             OldPrice = g.OldPrice,
                                             NewPrice = g.NewPrice,
                                             Size = (selectedlanguage == "bn" ? g.SizeLocal : g.Size)
                                         }).ToList();
                    return View("ItemList", itemListVM);
                }
                else
                {

                    return RedirectToAction("Index");
                }
            }
        }

        [Route("Details/{UrlLink}")]
        public ActionResult ItemDetails(string UrlLink = "")
        {
            string selectedlanguage = Request.Cookies["Language"].Value;
            if (string.IsNullOrEmpty(UrlLink) == true)
            {
                return RedirectToAction("Index");
            }

            ItemVM itemVM = new ItemVM();

            var VM = idb.Item.Where(g => g.UrlLink == UrlLink).FirstOrDefault();
            itemVM.ItemID = VM.ItemID;
            itemVM.ItemNo = VM.ItemNo;
            itemVM.ItemName = (selectedlanguage == "bn" ? VM.ItemNameLocal : VM.ItemName);
            itemVM.OldPrice = VM.OldPrice;
            itemVM.NewPrice = VM.NewPrice;
            itemVM.UrlLink = VM.UrlLink;
            itemVM.Description = (selectedlanguage == "bn" ? VM.DescriptionLocal : VM.Description);
            itemVM.Thumbnail = VM.Thumbnail;
            itemVM.FrontUrl = VM.FrontUrl;
            itemVM.BackUrl = VM.BackUrl;
            itemVM.SideUrl = VM.SideUrl;
            itemVM.Size = VM.Size;
            itemVM.CategoryName = (selectedlanguage == "bn" ? VM.CategorySub.Category.CategoryNameLocal : VM.CategorySub.Category.CategoryName);
            itemVM.CategoryUrl = VM.CategorySub.Category.UrlLink;
            itemVM.CategorySubName = (selectedlanguage == "bn" ? VM.CategorySub.CategorySubNameLocal : VM.CategorySub.CategorySubName);
            itemVM.CategorySubUrl = VM.CategorySub.UrlLink;



            if (itemVM != null)
            {
                return View("ItemDetails", itemVM);
            }
            else
            {
                return RedirectToAction("Index");
            }

        }

        public ActionResult Search()
        {
            return RedirectToAction("Index");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Search(string txtSearch = "")
        {
            string selectedlanguage = Request.Cookies["Language"].Value;
            if (txtSearch == "")
            {
                txtSearch = "?";
            }
            ItemListVM itemListVM = new ItemListVM();
            itemListVM.HeaderUrl = "";
            itemListVM.CategoryName = "";
            itemListVM.CategoryUrl = "";
            itemListVM.CategorySubName = txtSearch;
            itemListVM.categorySubVM = (from c in cdb.Category
                                        select new CategorySubVM
                                        {
                                            UrlLink = "~/Item/" + c.UrlLink,
                                            CategoryName = (selectedlanguage == "bn" ? c.CategoryNameLocal : c.CategoryName),
                                            LogoUrl = c.LogoUrl
                                        }).ToList();
            itemListVM.itemVM = (from g in idb.Item
                                 where g.ItemNo.Contains(txtSearch) || g.ItemName.Contains(txtSearch) || g.ItemNameLocal.Contains(txtSearch) || g.Keywords.Contains(txtSearch) || g.Description.Contains(txtSearch) || g.DescriptionLocal.Contains(txtSearch)   || g.CategorySub.CategorySubName.Contains(txtSearch) // || g.CategorySub.CategorySubNameLocal.Contains(txtSearch) || g.CategorySub.Category.CategoryName.Contains(txtSearch) || g.CategorySub.Category.CategoryNameLocal.Contains(txtSearch)
                                 select new ItemVM
                                 {
                                     ItemID = g.ItemID,
                                     ItemNo = g.ItemNo,
                                     UrlLink = "~/Details/" + g.UrlLink,
                                     ItemName = (selectedlanguage == "bn" ? g.ItemNameLocal : g.ItemName),
                                     Thumbnail = g.Thumbnail,
                                     Size = (selectedlanguage == "bn" ? g.SizeLocal : g.Size)
                                 }).ToList();
            return View("SearchList", itemListVM);
        }

        public ActionResult Index()
        {
            string selectedlanguage = Request.Cookies["Language"].Value;
            var _menu = from g in cdb.Category
                        select new CategorySubVM
                        {
                            UrlLink = "~/Item/" + g.UrlLink,
                            CategoryName = (selectedlanguage == "bn" ? g.CategoryNameLocal : g.CategoryName),
                            LogoUrl = g.LogoUrl
                        };
            return View(_menu.ToList());
        }


        [Route("About")]
        public ActionResult About()
        {
            return View();
        }

        private List<string> Topic()
        {
            List<string> Topics = new List<string>();
            Topics.Add("General enquiry");
            Topics.Add("HR & Payroll");
            Topics.Add("Accounts");
            Topics.Add("Invoice");
            Topics.Add("Microfinance");
            Topics.Add("School Management");
            Topics.Add("E-Commerce");
            Topics.Add("Asset Register");
            Topics.Add("New Module Development");
            Topics.Add("Report Customization");
            return Topics;
        }

        // GET: BasicSetup/Feedbacks/Create
        [Route("ContactUs")]
        public ActionResult ContactUs()
        {
            ViewBag.TopicName = new SelectList(Topic());
            return View();
        }

        // POST: BasicSetup/Feedbacks/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [Route("ContactUs")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ContactUsSave([Bind(Include = "FeedbackID,TopicName,Detailsoferror,WebsiteLike,WebsiteNotLike,Suggestions,Comments,purposes,VisitFrequency,Email,UserType")] Feedback Feedback)
        {
            if (ModelState.IsValid)
            {
                if (this.IsCaptchaValid("Captcha is not valid"))
                {

                    Feedback.Detailsoferror = string.IsNullOrEmpty(Feedback.Detailsoferror) ? "" : Feedback.Detailsoferror;
                    Feedback.WebsiteLike = string.IsNullOrEmpty(Feedback.WebsiteLike) ? "" : Feedback.WebsiteLike;
                    Feedback.WebsiteNotLike = string.IsNullOrEmpty(Feedback.WebsiteNotLike) ? "" : Feedback.WebsiteNotLike;
                    Feedback.Suggestions = string.IsNullOrEmpty(Feedback.Suggestions) ? "" : Feedback.Suggestions;
                    Feedback.Comments = string.IsNullOrEmpty(Feedback.Comments) ? "" : Feedback.Comments;
                    Feedback.EntryDate = DateTime.Now;
                    fdb.SaveFeedback(Feedback);
                    return RedirectToAction("ContactConfirmation");
                }
                else
                {
                    ViewBag.ErrMessage = "Error: captcha is not valid.";

                }
            }
            ViewBag.TopicName = new SelectList(Topic(), Feedback.TopicName);
            return View("Contact", Feedback);
        }

        [Route("ContactConfirmation")]
        public ActionResult ContactConfirmation()
        {
            return View();
        }

        // GET: AboutUs/Details/5
        [Route("Privacy")]
        public ActionResult Privacy()
        {
            string selectedlanguage = Request.Cookies["Language"].Value;
            ShopPage page = db.ShopPage.Where(p => p.Slug == "Privacy").FirstOrDefault();
            if (page == null)
            {
                return RedirectToAction("Index");
            }
            ShopPageVM shopPageVM = new ShopPageVM();
            shopPageVM.Description = (selectedlanguage == "bn" ? page.DescriptionLocal : page.Description);
            return View("Details", shopPageVM);
        }

        [Route("Terms")]
        public ActionResult Terms()
        {
            string selectedlanguage = Request.Cookies["Language"].Value;
            ShopPage page = db.ShopPage.Where(p => p.Slug == "Terms").FirstOrDefault();
            if (page == null)
            {
                return RedirectToAction("Index");
            }
            ShopPageVM shopPageVM = new ShopPageVM();
            shopPageVM.Description = (selectedlanguage == "bn" ? page.DescriptionLocal : page.Description);
            return View("Details", shopPageVM);
        }

    }


}