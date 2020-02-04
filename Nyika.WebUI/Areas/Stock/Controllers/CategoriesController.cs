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
    public class CategoriesController : Controller
    {
        private ICategoryRepo db;

        public CategoriesController(ICategoryRepo DB)
        {
            this.db = DB;
        }


        // GET: BasicSetup/Categorys
        public ActionResult Index()
        {
            return View(db.Category.ToList());
        }

        

        // GET: BasicSetup/Categorys/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: BasicSetup/Categorys/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "CategoryID,CategoryName,CategoryNameLocal,UrlLink")] Category Category, HttpPostedFileBase HeaderUrl, HttpPostedFileBase LogoUrl)
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
                if (string.IsNullOrEmpty(Category.UrlLink) == true)
                {
                    Category.UrlLink = Regex.Replace(Category.CategoryName, @"[^0-9a-zA-Z]+", "-").ToLower();
                }
                else
                {
                    Category.UrlLink = Regex.Replace(Category.UrlLink, @"[^0-9a-zA-Z]+", "-").ToLower();
                }

                Category.HeaderUrl = "~/images/notfound.jpg";
                if (HeaderUrl != null && HeaderUrl.ContentLength > 0)
                {
                    if (validImageTypes.Contains(HeaderUrl.ContentType))
                    {
                        var ImageName = Guid.NewGuid().ToString() + ".jpg";
                        var uploadDir = "~/Images/Stock";
                        var imagePath = Path.Combine(Server.MapPath(uploadDir), ImageName);
                        HeaderUrl.SaveAs(imagePath);
                        var imageUrl = Path.Combine(uploadDir, ImageName);
                        Category.HeaderUrl = imageUrl.Replace("\\", "/");
                    }
                }

                Category.LogoUrl = "~/images/demologo.png";
                if (LogoUrl != null && LogoUrl.ContentLength > 0)
                {
                    if (validImageTypes.Contains(LogoUrl.ContentType))
                    {
                        var ImageName = Guid.NewGuid().ToString() + ".png";
                        var uploadDir = "~/Images/Stock";
                        var imagePath = Path.Combine(Server.MapPath(uploadDir), ImageName);
                        LogoUrl.SaveAs(imagePath);
                        var imageUrl = Path.Combine(uploadDir, ImageName);
                        Category.LogoUrl = imageUrl.Replace("\\", "/");
                    }
                }

                db.SaveCategory(Category);
                return RedirectToAction("Index");
            }

            return View(Category);
        }

        // GET: BasicSetup/Categorys/Edit/5
        public ActionResult Edit(long id=0)
        {
            Category Category = db.Single(id);
            if (Category == null)
            {
                return RedirectToAction("Index");
            }
            return View(Category);
        }

        // POST: BasicSetup/Categorys/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "CategoryID,CategoryName,CategoryNameLocal,UrlLink,ImageUrl,LogoUrl,HeaderUrl")] Category Category, HttpPostedFileBase HeaderUrl, HttpPostedFileBase LogoUrl)
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
                if (string.IsNullOrEmpty(Category.UrlLink) == true)
                {
                    Category.UrlLink = Regex.Replace(Category.CategoryName, @"[^0-9a-zA-Z]+", "-").ToLower();
                }
                else
                {
                    Category.UrlLink = Regex.Replace(Category.UrlLink, @"[^0-9a-zA-Z]+", "-").ToLower();
                }

                if (HeaderUrl != null && HeaderUrl.ContentLength > 0)
                {
                    if (validImageTypes.Contains(HeaderUrl.ContentType))
                    {
                        var uploadDir = "~/Images/Stock";
                        string filename = Path.GetFileName(Category.HeaderUrl);
                        if (filename == "notfound.jpg")
                        {
                            filename = Guid.NewGuid().ToString() + ".jpg";
                        }
                        var imagePath = Path.Combine(Server.MapPath(uploadDir), filename);
                        HeaderUrl.SaveAs(imagePath);
                        var imageUrl = Path.Combine(uploadDir, filename);
                        Category.HeaderUrl = imageUrl.Replace("\\", "/");
                    }
                }

                if (LogoUrl != null && LogoUrl.ContentLength > 0)
                {
                    if (validImageTypes.Contains(LogoUrl.ContentType))
                    {
                        var uploadDir = "~/Images/Stock";
                        string filename = Path.GetFileName(Category.LogoUrl);
                        if (filename == "demologo.png")
                        {
                            filename = Guid.NewGuid().ToString() + ".png";
                        }
                        var imagePath = Path.Combine(Server.MapPath(uploadDir), filename);
                        LogoUrl.SaveAs(imagePath);
                        var imageUrl = Path.Combine(uploadDir, filename);
                        Category.LogoUrl = imageUrl.Replace("\\", "/");
                    }
                }

                db.SaveCategory(Category);
                return RedirectToAction("Index");
            }
            return View(Category);
        }

        // GET: BasicSetup/Categorys/Delete/5
        public ActionResult Delete(long id=0)
        {
            Category Category = db.Single(id);

            if (Category == null)
            {
                return RedirectToAction("Index");
            }

            if (db.IsExists(Category.CategoryID) != 0)
            {
                TempData["Delete"] = "In use, can not delete at this moment";
            }
            else
            {
                TempData["Delete"] = "";
            }

            return View(Category);
        }

        // POST: BasicSetup/Categorys/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Category Category = db.Single(id);
            if (Category != null)
            {
                db.DeleteCategory(Category.CategoryID);
            }
            return RedirectToAction("Index");
        }

    }
}
