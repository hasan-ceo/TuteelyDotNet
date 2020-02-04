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
    public class CategorySubsController : Controller
    {
        private ICategorySubRepo db;
        private ICategoryRepo dbc;

        public CategorySubsController(ICategorySubRepo DB, ICategoryRepo DBc)
        {
            this.db = DB;
            this.dbc = DBc;
        }


        // GET: BasicSetup/CategorySubs
        public ActionResult Index()
        {
            return View(db.CategorySub.ToList());
        }

       

        // GET: BasicSetup/CategorySubs/Create
        public ActionResult Create()
        {
            ViewBag.CategoryID = new SelectList(dbc.Category, "CategoryID", "CategoryName");
            return View();
        }

        // POST: BasicSetup/CategorySubs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "CategorySubID,CategorySubName,CategorySubNameLocal,UrlLink,CategoryID")] CategorySub CategorySub, HttpPostedFileBase HeaderUrl, HttpPostedFileBase LogoUrl)
        {
            var validImageTypes = new string[]
            {
                 //"image/gif",
                //"image/pjpeg",
                "image/png",
                "image/jpeg"
            };

            if (ModelState.IsValid)
            {
                if (string.IsNullOrEmpty(CategorySub.UrlLink) == true)
                {
                    CategorySub.UrlLink = Regex.Replace(CategorySub.CategorySubName, @"[^0-9a-zA-Z]+", "-").ToLower();
                }
                else
                {
                    CategorySub.UrlLink = Regex.Replace(CategorySub.UrlLink, @"[^0-9a-zA-Z]+", "-").ToLower();
                }

                CategorySub.HeaderUrl = "~/images/notfound.jpg";
                if (HeaderUrl != null && HeaderUrl.ContentLength > 0)
                {
                    if (validImageTypes.Contains(HeaderUrl.ContentType))
                    {
                        var ImageName = Guid.NewGuid().ToString() + ".jpg";
                        var uploadDir = "~/Images/Stock";
                        var imagePath = Path.Combine(Server.MapPath(uploadDir), ImageName);
                        HeaderUrl.SaveAs(imagePath);
                        var imageUrl = Path.Combine(uploadDir, ImageName);
                        CategorySub.HeaderUrl = imageUrl.Replace("\\", "/");
                    }
                }

                CategorySub.LogoUrl = "~/images/demologo.png";
                if (LogoUrl != null && LogoUrl.ContentLength > 0)
                {
                    if (validImageTypes.Contains(LogoUrl.ContentType))
                    {
                        var ImageName = Guid.NewGuid().ToString() + ".png";
                        var uploadDir = "~/Images/Stock";
                        var imagePath = Path.Combine(Server.MapPath(uploadDir), ImageName);
                        LogoUrl.SaveAs(imagePath);
                        var imageUrl = Path.Combine(uploadDir, ImageName);
                        CategorySub.LogoUrl = imageUrl.Replace("\\", "/");
                    }
                }

                db.SaveCategorySub(CategorySub);
                return RedirectToAction("Index");
            }
            ViewBag.CategoryID = new SelectList(dbc.Category, "CategoryID", "CategoryName",CategorySub.CategoryID);
            return View(CategorySub);
        }

        // GET: BasicSetup/CategorySubs/Edit/5
        public ActionResult Edit(long id=0)
        {
            CategorySub CategorySub = db.Single(id);
            if (CategorySub == null)
            {
                return RedirectToAction("Index");
            }
            ViewBag.CategoryID = new SelectList(dbc.Category, "CategoryID", "CategoryName", CategorySub.CategoryID);
            return View(CategorySub);
        }

        // POST: BasicSetup/CategorySubs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "CategorySubID,CategorySubName,CategorySubNameLocal,UrlLink,ImageUrl,CategoryID,LogoUrl,HeaderUrl")] CategorySub CategorySub, HttpPostedFileBase HeaderUrl, HttpPostedFileBase LogoUrl)
        {
            var validImageTypes = new string[]
            {
                 //"image/gif",
                //"image/pjpeg",
                "image/png",
                "image/jpeg"
            };

            if (ModelState.IsValid)
            {
                if (string.IsNullOrEmpty(CategorySub.UrlLink) == true)
                {
                    CategorySub.UrlLink = Regex.Replace(CategorySub.CategorySubName, @"[^0-9a-zA-Z]+", "-").ToLower();
                }
                else
                {
                    CategorySub.UrlLink = Regex.Replace(CategorySub.UrlLink, @"[^0-9a-zA-Z]+", "-").ToLower();
                }

                if (HeaderUrl != null && HeaderUrl.ContentLength > 0)
                {
                    if (validImageTypes.Contains(HeaderUrl.ContentType))
                    {
                        var uploadDir = "~/Images/Stock";
                        string filename = Path.GetFileName(CategorySub.HeaderUrl);
                        if (filename == "notfound.jpg")
                        {
                            filename = Guid.NewGuid().ToString() + ".jpg";
                        }
                        var imagePath = Path.Combine(Server.MapPath(uploadDir), filename);
                        HeaderUrl.SaveAs(imagePath);
                        var imageUrl = Path.Combine(uploadDir, filename);
                        CategorySub.HeaderUrl = imageUrl.Replace("\\", "/");
                    }
                }

                if (LogoUrl != null && LogoUrl.ContentLength > 0)
                {
                    if (validImageTypes.Contains(LogoUrl.ContentType))
                    {
                        var uploadDir = "~/Images/Stock";
                        string filename = Path.GetFileName(CategorySub.LogoUrl);
                        if (filename == "demologo.png")
                        {
                            filename = Guid.NewGuid().ToString() + ".png";
                        }
                        var imagePath = Path.Combine(Server.MapPath(uploadDir), filename);
                        LogoUrl.SaveAs(imagePath);
                        var imageUrl = Path.Combine(uploadDir, filename);
                        CategorySub.LogoUrl = imageUrl.Replace("\\", "/");
                    }
                }

                db.SaveCategorySub(CategorySub);
                return RedirectToAction("Index");
            }
            ViewBag.CategoryID = new SelectList(dbc.Category, "CategoryID", "CategoryName", CategorySub.CategoryID);
            return View(CategorySub);
        }

        // GET: BasicSetup/CategorySubs/Delete/5
        public ActionResult Delete(long id=0)
        {
            
            CategorySub CategorySub = db.Single(id);

            if (CategorySub == null)
            {
                return RedirectToAction("Index");
            }

            if (db.IsExists(CategorySub.CategorySubID) != 0)
            {
                TempData["Delete"] = "In use, can not delete at this moment";
            }
            else
            {
                TempData["Delete"] = "";
            }

            return View(CategorySub);
        }

        // POST: BasicSetup/CategorySubs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            CategorySub CategorySub = db.Single(id);
            if (CategorySub != null)
            {
                db.DeleteCategorySub(CategorySub.CategorySubID);
            }
            return RedirectToAction("Index");
        }

    }
}
