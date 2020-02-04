using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Nyika.Domain.Concrete.Stock;
using Nyika.Domain.Entities.Stock;
using Nyika.Domain.Abstract.Stock;
using Microsoft.AspNet.Identity;
using Nyika.WebUI.Models;
using System.Text.RegularExpressions;
using System.IO;

namespace Nyika.WebUI.Areas.Stock.Controllers
{
    [Authorize(Roles = "Super Admin")]
    public class ItemsController : Controller
    {
        private IItemRepo db;
        private ICategorySubRepo dbc;
        private IBrandRepo dbb;

        public ItemsController(IItemRepo DB, ICategorySubRepo DBC, IBrandRepo DBB)
        {
            this.db = DB;
            this.dbc = DBC;
            this.dbb = DBB;
        }


        // GET: BasicSetup/Items
        public ActionResult Index()
        {
            return View(db.Item.Where(i => i.ItemID == 0).ToList());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index(string txtSearch = "")
        {
            if (txtSearch == "")
            {
                txtSearch = "?";
            }

            var Item = db.Search(txtSearch);
            return View("Index", Item.ToList());
        }



        // GET: BasicSetup/Items/Create
        public ActionResult Create()
        {
            ViewBag.BrandID = new SelectList(dbb.Brand, "BrandID", "BrandName");
            ViewBag.CategorySubID = new SelectList(dbc.CategorySub, "CategorySubID", "CategorySubName");
            return View();
        }

        // POST: BasicSetup/Items/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ItemNo,ItemName,ItemNameLocal,Description,DescriptionLocal,CategorySubID,BrandID,ItemType,ItemTypeLocal,Color,ColorLocal,Size,SizeLocal,OldPrice,NewPrice,UrlLink,Keywords")] Item Item, HttpPostedFileBase Thumbnail, HttpPostedFileBase FrontUrl, HttpPostedFileBase BackUrl, HttpPostedFileBase SideUrl)
        {
            var validImageTypes = new string[]
            {
                 //"image/gif",
                //"image/pjpeg",
                //"image/png",
                "image/jpeg"
            };

            if (ModelState.IsValid)
            {
                if (string.IsNullOrEmpty(Item.UrlLink) == true)
                {
                    Item.UrlLink = Regex.Replace(Item.ItemName, @"[^0-9a-zA-Z]+", "-").ToLower();
                }
                else
                {
                    Item.UrlLink = Regex.Replace(Item.UrlLink, @"[^0-9a-zA-Z]+", "-").ToLower();
                }

                Item.Thumbnail = "~/images/notfound.jpg";
                if (Thumbnail != null && Thumbnail.ContentLength > 0)
                {
                    if (validImageTypes.Contains(Thumbnail.ContentType))
                    {
                        var ImageName = Guid.NewGuid().ToString() + ".jpg";
                        var uploadDir = "~/Images/Stock";
                        var imagePath = Path.Combine(Server.MapPath(uploadDir), ImageName);
                        Thumbnail.SaveAs(imagePath);
                        var imageUrl = Path.Combine(uploadDir, ImageName);
                        Item.Thumbnail = imageUrl.Replace("\\", "/");
                    }
                }

                Item.FrontUrl = "~/images/notfound.jpg";
                if (FrontUrl != null && FrontUrl.ContentLength > 0)
                {
                    if (validImageTypes.Contains(FrontUrl.ContentType))
                    {
                        var ImageName = Guid.NewGuid().ToString() + ".jpg";
                        var uploadDir = "~/Images/Stock";
                        var imagePath = Path.Combine(Server.MapPath(uploadDir), ImageName);
                        FrontUrl.SaveAs(imagePath);
                        var imageUrl = Path.Combine(uploadDir, ImageName);
                        Item.FrontUrl = imageUrl.Replace("\\", "/");
                    }
                }

                Item.BackUrl = "~/images/notfound.jpg";
                if (BackUrl != null && BackUrl.ContentLength > 0)
                {
                    if (validImageTypes.Contains(BackUrl.ContentType))
                    {
                        var ImageName = Guid.NewGuid().ToString() + ".jpg";
                        var uploadDir = "~/Images/Stock";
                        var imagePath = Path.Combine(Server.MapPath(uploadDir), ImageName);
                        BackUrl.SaveAs(imagePath);
                        var imageUrl = Path.Combine(uploadDir, ImageName);
                        Item.BackUrl = imageUrl.Replace("\\", "/");
                    }
                }

                Item.SideUrl = "~/images/notfound.jpg";
                if (SideUrl != null && SideUrl.ContentLength > 0)
                {
                    if (validImageTypes.Contains(SideUrl.ContentType))
                    {
                        var ImageName = Guid.NewGuid().ToString() + ".jpg";
                        var uploadDir = "~/Images/Stock";
                        var imagePath = Path.Combine(Server.MapPath(uploadDir), ImageName);
                        SideUrl.SaveAs(imagePath);
                        var imageUrl = Path.Combine(uploadDir, ImageName);
                        Item.SideUrl = imageUrl.Replace("\\", "/");
                    }
                }

                if (string.IsNullOrEmpty(Item.ItemType))
                {
                    Item.ItemType = "";
                }

                if (string.IsNullOrEmpty(Item.ItemTypeLocal))
                {
                    Item.ItemTypeLocal = "";
                }

                if (string.IsNullOrEmpty(Item.Size))
                {
                    Item.Size = "";
                }

                if (string.IsNullOrEmpty(Item.SizeLocal))
                {
                    Item.SizeLocal = "";
                }

                if (string.IsNullOrEmpty(Item.Color))
                {
                    Item.Color = "";
                }

                if (string.IsNullOrEmpty(Item.ColorLocal))
                {
                    Item.ColorLocal = "";
                }

                if (string.IsNullOrEmpty(Item.Keywords))
                {
                    Item.Keywords = "";
                }



                db.SaveItem(Item);
                return View("Index", db.Item.Where(i => i.ItemID == Item.ItemID).ToList());

            }

            ViewBag.BrandID = new SelectList(dbb.Brand, "BrandID", "BrandName", Item.BrandID);
            ViewBag.CategorySubID = new SelectList(dbc.CategorySub, "CategorySubID", "CategorySubName", Item.CategorySubID);
            return View(Item);
        }

        // GET: BasicSetup/Items/Edit/5
        public ActionResult Edit(long id)
        {
            Item Item = db.Single(id);
            if (Item == null)
            {
                return RedirectToAction("Index");
            }
            ViewBag.BrandID = new SelectList(dbb.Brand, "BrandID", "BrandName", Item.BrandID);
            ViewBag.CategorySubID = new SelectList(dbc.CategorySub, "CategorySubID", "CategorySubName", Item.CategorySubID);
            return View(Item);
        }

        // POST: BasicSetup/Items/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ItemID,ItemNo,ItemName,ItemNameLocal,Description,DescriptionLocal,CategorySubID,BrandID,ItemType,ItemTypeLocal,Color,ColorLocal,Size,SizeLocal,OldPrice,NewPrice,UrlLink,Keywords,Thumbnail,FrontUrl,BackUrl,SideUrl")] Item Item, HttpPostedFileBase fThumbnail, HttpPostedFileBase fFrontUrl, HttpPostedFileBase fBackUrl, HttpPostedFileBase fSideUrl)
        {
            var validImageTypes = new string[]
            {
                 //"image/gif",
                //"image/pjpeg",
                //"image/png",
                "image/jpeg"
            };

            if (ModelState.IsValid)
            {
                if (string.IsNullOrEmpty(Item.UrlLink) == true)
                {
                    Item.UrlLink = Regex.Replace(Item.ItemName, @"[^0-9a-zA-Z]+", "-").ToLower();
                }
                else
                {
                    Item.UrlLink = Regex.Replace(Item.UrlLink, @"[^0-9a-zA-Z]+", "-").ToLower();
                }

                //Item.Thumbnail = "~/images/notfound.jpg";
                if (fThumbnail != null && fThumbnail.ContentLength > 0)
                {
                    if (validImageTypes.Contains(fThumbnail.ContentType))
                    {
                        var ImageName = Guid.NewGuid().ToString() + ".jpg";
                        var uploadDir = "~/Images/Stock";
                        var imagePath = Path.Combine(Server.MapPath(uploadDir), ImageName);
                        fThumbnail.SaveAs(imagePath);
                        var imageUrl = Path.Combine(uploadDir, ImageName);
                        Item.Thumbnail = imageUrl.Replace("\\", "/");
                    }
                }

                //Item.FrontUrl = "~/images/notfound.jpg";
                if (fFrontUrl != null && fFrontUrl.ContentLength > 0)
                {
                    if (validImageTypes.Contains(fFrontUrl.ContentType))
                    {
                        var ImageName = Guid.NewGuid().ToString() + ".jpg";
                        var uploadDir = "~/Images/Stock";
                        var imagePath = Path.Combine(Server.MapPath(uploadDir), ImageName);
                        fFrontUrl.SaveAs(imagePath);
                        var imageUrl = Path.Combine(uploadDir, ImageName);
                        Item.FrontUrl = imageUrl.Replace("\\", "/");
                    }
                }

                //Item.BackUrl = "~/images/notfound.jpg";
                if (fBackUrl != null && fBackUrl.ContentLength > 0)
                {
                    if (validImageTypes.Contains(fBackUrl.ContentType))
                    {
                        var ImageName = Guid.NewGuid().ToString() + ".jpg";
                        var uploadDir = "~/Images/Stock";
                        var imagePath = Path.Combine(Server.MapPath(uploadDir), ImageName);
                        fBackUrl.SaveAs(imagePath);
                        var imageUrl = Path.Combine(uploadDir, ImageName);
                        Item.BackUrl = imageUrl.Replace("\\", "/");
                    }
                }

                //Item.SideUrl = "~/images/notfound.jpg";
                if (fSideUrl != null && fSideUrl.ContentLength > 0)
                {
                    if (validImageTypes.Contains(fSideUrl.ContentType))
                    {
                        var ImageName = Guid.NewGuid().ToString() + ".jpg";
                        var uploadDir = "~/Images/Stock";
                        var imagePath = Path.Combine(Server.MapPath(uploadDir), ImageName);
                        fSideUrl.SaveAs(imagePath);
                        var imageUrl = Path.Combine(uploadDir, ImageName);
                        Item.SideUrl = imageUrl.Replace("\\", "/");
                    }
                }


                if (string.IsNullOrEmpty(Item.ItemType))
                {
                    Item.ItemType = "";
                }

                if (string.IsNullOrEmpty(Item.ItemTypeLocal))
                {
                    Item.ItemTypeLocal = "";
                }

                if (string.IsNullOrEmpty(Item.Size))
                {
                    Item.Size = "";
                }

                if (string.IsNullOrEmpty(Item.SizeLocal))
                {
                    Item.SizeLocal = "";
                }

                if (string.IsNullOrEmpty(Item.Color))
                {
                    Item.Color = "";
                }

                if (string.IsNullOrEmpty(Item.ColorLocal))
                {
                    Item.ColorLocal = "";
                }

                if (string.IsNullOrEmpty(Item.Keywords))
                {
                    Item.Keywords = "";
                }


                db.SaveItem(Item);
                return View("Index", db.Item.Where(i => i.ItemID == Item.ItemID).ToList());
            }
            ViewBag.BrandID = new SelectList(dbb.Brand, "BrandID", "BrandName", Item.BrandID);
            ViewBag.CategorySubID = new SelectList(dbc.CategorySub, "CategorySubID", "CategorySubName", Item.CategorySubID);
            return View(Item);
        }

        // GET: BasicSetup/Items/Delete/5
        public ActionResult Delete(long id)
        {
            Item Item = db.Single(id);

            if (Item == null)
            {
                return RedirectToAction("Index");
            }

            if (db.IsExists(Item.ItemID) != 0)
            {
                TempData["Delete"] = "In use, can not delete at this moment";
            }
            else
            {
                TempData["Delete"] = "";
            }

            return View(Item);
        }

        // POST: BasicSetup/Items/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Item Item = db.Single(id);
            if (Item != null)
            {
                db.DeleteItem(Item.ItemID);
            }
            return RedirectToAction("Index");
        }

    }
}
