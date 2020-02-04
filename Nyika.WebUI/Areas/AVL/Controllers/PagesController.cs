using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Nyika.Domain.Concrete.Setup;
using Nyika.Domain.Entities.AVL;
using Nyika.Domain.Abstract.AVL;
using Microsoft.AspNet.Identity;

namespace Nyika.WebUI.Areas.AVL.Controllers
{
    [Authorize(Roles = "Surzo")]
    public class PagesController : Controller
    {
        private IPageRepo db;

        public PagesController(IPageRepo DB)
        {
            this.db = DB;
        }


        // GET: BasicSetup/Pages
        public ActionResult Index()
        {
            return View(db.Page.ToList());
        }



        // GET: BasicSetup/Pages/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: BasicSetup/Pages/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "PageID,Slug,Description")] Page Page)
        {
            if (ModelState.IsValid)
            {
                db.SavePage(Page);
                return RedirectToAction("Index");
            }

            return View(Page);
        }

        // GET: BasicSetup/Pages/Edit/5
        public ActionResult Edit(long id=0)
        {
            Page page = db.Page.Where(p => p.PageID == id).FirstOrDefault();
            if (page == null)
            {
                return RedirectToAction("Index");
            }
            return View(page);
        }

        // POST: BasicSetup/Pages/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "PageID,Slug,Description")] Page Page)
        {
            if (ModelState.IsValid)
            {
                db.SavePage(Page);
                return RedirectToAction("Index");
            }
            return View(Page);
        }

        // GET: BasicSetup/Pages/Delete/5
        public ActionResult Delete(long id=0)
        {
            Page page = db.Page.Where(p => p.PageID == id).FirstOrDefault();
            if (page == null)
            {
                return RedirectToAction("Index");
            }
            return View(page);
        }

        // POST: BasicSetup/Pages/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            Page page = db.Page.Where(p => p.PageID == id).FirstOrDefault();
            if (page != null)
            {
                db.DeletePage(page.PageID);
            }
            return RedirectToAction("Index");
        }

    }
}
