using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Nyika.Domain.Concrete;
using Nyika.Domain.Entities.Setup;
using Nyika.Domain.Abstract.Setup;
using Microsoft.AspNet.Identity;
using Nyika.WebUI.Models;
using Nyika.WebUI.Areas.BasicSetup.Models;
using System.IO;

namespace Nyika.WebUI.Areas.BasicSetup.Controllers
{
    [Authorize(Roles = "Super Admin")]
    public class CompaniesController : Controller
    {
        private ICompanyRepo db;
        private string instanceId;

        public CompaniesController(ICompanyRepo DB)
        {
            this.db = DB;
            instanceId = new InstanceVM().InstanceID;
        }


        // GET: Setup/Companies/Edit/5
        public ActionResult Edit()
        {
            ViewBag.Msg = "";
            
            List<string> cr = new List<string>(new string[] { "AED", "AFN", "ALL", "AMD", "ANG", "AOA", "ARS", "AUD", "AWG", "AZN", "BAM", "BBD", "BDT", "BGN", "BHD", "BIF", "BMD", "BND", "BOB", "BRL", "BSD", "BTN", "BWP", "BYR", "BZD", "CAD", "CDF", "CHF", "CLP", "CNY", "COP", "CRC", "CUP", "CVE", "CZK", "DJF", "DKK", "DOP", "DZD", "ECS", "EGP", "ERN", "ETB", "EUR", "FJD", "FKP", "GBP", "GEL", "GGP", "GHS", "GIP", "GMD", "GNF", "GWP", "GYD", "HKD", "HNL", "HRK", "HTG", "HUF", "IDR", "ILS", "INR", "IQD", "IRR", "ISK", "JMD", "JOD", "JPY", "KES", "KGS", "KHR", "KMF", "KPW", "KRW", "KWD", "KYD", "KZT", "LAK", "LBP", "LKR", "LRD", "LSL", "LTL", "LVL", "LYD", "MAD", "MDL", "MGF", "MKD", "MMK", "MNT", "MOP", "MRO", "MUR", "MVR", "MWK", "MXN", "MYR", "MZN", "NAD", "NGN", "NIO", "NOK", "NPR", "NZD", "OMR", "PAB", "PEN", "PGK", "PHP", "PKR", "PLN", "PYG", "QAR", "QTQ", "RON", "RSD", "RUB", "RWF", "SAR", "SBD", "SCR", "SDG", "SEK", "SGD", "SHP", "SLL", "SOS", "SRD", "SSP", "STD", "SVC", "SYP", "SZL", "THB", "TJS", "TMT", "TND", "TOP", "TRY", "TTD", "TWD", "TZS", "UAH", "UGX", "USD", "UYU", "UZS", "VEF", "VND", "VUV", "WST", "XAF", "XCD", "XOF", "XPF", "YER", "ZAR", "ZMW", "ZWD" });
            var Company = db.Company(instanceId).FirstOrDefault();
            ViewBag.Currency = new SelectList(cr.ToList(), Company.Currency);
            return View(Company);
        }

        // POST: Setup/Companies/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "CompanyID,CompanyName,Email,ContactNumber,WebAddress,Address,TIN,VAT,Currency,Stamp,PaymentTerms,ClientNotes,WeekOff1,WeekOff2,SDL,NSSFPPF,HigherStudyLoan,NHIF,PPFEmployerNumber,NSSFEmployerNumber,WCFNumber,NHIFNumber")] Company company, HttpPostedFileBase ImageUpload)
        {
            var validImageTypes = new string[]
            {
                //"image/gif",
                //"image/pjpeg",
                //"image/png",
                "image/jpeg"
            };

            //if (ImageUpload == null || ImageUpload.ContentLength == 0)
            //{
            //    ModelState.AddModelError("ImageUpload", "This field is required");
            //}
            //else if (!validImageTypes.Contains(ImageUpload.ContentType))
            //{
            //    ModelState.AddModelError("ImageUpload", "Please choose either a GIF, JPG or PNG image.");
            //}

            if (ModelState.IsValid)
            {
                if (ImageUpload != null && ImageUpload.ContentLength > 0)
                {
                    if (validImageTypes.Contains(ImageUpload.ContentType))
                    {
                        var uploadDir = "~/Images/Company";
                        string extension = Path.GetExtension(ImageUpload.FileName);
                        var imagePath = Path.Combine(Server.MapPath(uploadDir), company.CompanyID.ToString() + extension);
                        var imageUrl = Path.Combine(uploadDir, company.CompanyID.ToString() + extension);
                        ImageUpload.SaveAs(imagePath);
                        company.ImageUrl = imageUrl.Replace("\\", "/");
                    }
                }

                db.SaveCompany(company);
                ViewBag.Msg = "Save Successful";
                return RedirectToAction("Edit");
            }
            return View(company);
        }


    }
}
