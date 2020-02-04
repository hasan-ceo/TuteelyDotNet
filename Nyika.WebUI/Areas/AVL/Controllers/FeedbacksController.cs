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
    public class FeedbacksController : Controller
    {
        private IFeedbackRepo db;

        public FeedbacksController(IFeedbackRepo DB)
        {
            this.db = DB;
        }


        // GET: BasicSetup/Feedbacks
        public ActionResult Index()
        {
            return View(db.Feedback.ToList());
        }

             

        // GET: BasicSetup/Feedbacks/Edit/5
        public ActionResult Edit(long id=0)
        {
            Feedback Feedback = db.Feedback.Where(p => p.FeedbackID == id).FirstOrDefault();
            if (Feedback == null)
            {
                return RedirectToAction("Index");
            }
            return View(Feedback);
        }

        // POST: BasicSetup/Feedbacks/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "FeedbackID,Slug,Description")] Feedback Feedback)
        {
            if (ModelState.IsValid)
            {
                db.SaveFeedback(Feedback);
                return RedirectToAction("Index");
            }
            return View(Feedback);
        }

        // GET: BasicSetup/Feedbacks/Delete/5
        public ActionResult Delete(long id=0)
        {
            Feedback Feedback = db.Feedback.Where(p => p.FeedbackID == id).FirstOrDefault();
            if (Feedback == null)
            {
                return RedirectToAction("Index");
            }
            return View(Feedback);
        }

        // POST: BasicSetup/Feedbacks/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            Feedback Feedback = db.Feedback.Where(p => p.FeedbackID == id).FirstOrDefault();
            if (Feedback != null)
            {
                db.DeleteFeedback(Feedback.FeedbackID);
            }
            return RedirectToAction("Index");
        }

    }
}
