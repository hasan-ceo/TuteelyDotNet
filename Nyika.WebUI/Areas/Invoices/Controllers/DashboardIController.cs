using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Nyika.Domain.Concrete;
using Nyika.Domain.Entities.Invoices;
using Nyika.Domain.Abstract.Invoices;
using Nyika.WebUI.Models;
using Nyika.Domain.Abstract.Accounts;
using Nyika.Domain.Abstract.Setup;
using Nyika.WebUI.Areas.Invoices.Models;

namespace Nyika.WebUI.Areas.Invoices.Controllers
{
    [Authorize(Roles = "Accountant,Super Admin")]
    public class DashboardIController : Controller
    {
        private IInvoiceRepo db;
        private IInvoiceDetailsRepo invDedb;
        private IPartyRepo pdb;
        private ICompanyRepo cdb;
        private IBusinessDayRepo bddb;
        private string instanceId;

        public DashboardIController(IInvoiceRepo DB, IPartyRepo pDB, ICompanyRepo cDB, IBusinessDayRepo bdDB, IInvoiceDetailsRepo invDeDB)
        {
            this.db = DB;
            this.pdb = pDB;
            this.cdb = cDB;
            this.bddb = bdDB;
            this.invDedb = invDeDB;
            instanceId = new InstanceVM().InstanceID;
        }

        // GET: Invoices/Invoices
        public ActionResult Index()
        {

            var dt = bddb.BusinessDay(instanceId).Where(b => b.DayClose == false).FirstOrDefault();
            if (dt != null)
            {
                var invoice = db.Invoice("x");
                return View(invoice.ToList());
            }
            else
            {
                return RedirectToAction("DayOpen", "DashboardA", new { Area = "Accounts" });
            }

           
        }

        // GET: Invoices/Invoices/Details/5
        public ActionResult PrintPreview(InvCart InvCart)
        {
            if (InvCart.InEdit == true)
            {
               
                Invoice inv = new Invoice();
                inv.InvoiceID = InvCart.InvoiceID;
                inv.InvoiceNumber = InvCart.InvoiceNumber;
                inv.CompanyID = InvCart.CompanyID;
                inv.PartyID = InvCart.PartyID;
                inv.InvoiceDate = InvCart.InvoiceDate;
                inv.DueDate = InvCart.DueDate;
                inv.JobDescription = InvCart.JobDescription;
                inv.SubTotal = InvCart.SubTotal;
                inv.Discount = InvCart.Discount;
                inv.TaxPercent = InvCart.TaxPercent;
                inv.Tax = InvCart.Tax;
                inv.InvoiceTotal = InvCart.InvoiceTotal;
                inv.AmountPaid = InvCart.AmountPaid;
                inv.DueAmount = InvCart.DueAmount;
                inv.Currency = InvCart.Currency;
                inv.Stamp = InvCart.Stamp;
                inv.PaymentTerms = InvCart.PaymentTerms;
                inv.ClientNotes = InvCart.ClientNotes;
                if (InvCart.DueAmount != 0)
                {
                    inv.PaymentComplete = false;
                }
                else
                {
                    inv.PaymentComplete = true;
                }
                inv.WorkDate = bddb.WorkDate(instanceId);
                inv.EntryBy = User.Identity.Name;
                inv.InstanceID = instanceId;
                db.SaveInvoice(inv);
                InvCart.InvoiceID = inv.InvoiceID;

                invDedb.DeleteInvoiceDetails(inv.InvoiceID);
                foreach (var line in InvCart.Lines)
                {
                    InvoiceDetails invd = new InvoiceDetails();
                    invd.InvoiceID = inv.InvoiceID;
                    invd.Item = line.Item;
                    invd.Description = line.Description;
                    invd.Cost = line.Cost;
                    invd.Quantity = line.Quantity;
                    invd.Amount = line.Amount;
                    invDedb.SaveInvoiceDetails(invd);
                }

                InvCart.DraftSave = false;
                return View("PrintPreview",InvCart);
            }
            else
            {
                ViewBag.PartyID = new SelectList(pdb.Party(instanceId), "PartyID", "PartyName", InvCart.PartyID);
                return View("Inv",InvCart);
            }
        }

        // GET: Invoices/Invoices/Create
        public ActionResult Inv(InvCart InvCart, long id)
        {
            var c = cdb.Company(instanceId).FirstOrDefault();
            InvCart.CompanyID = c.CompanyID;
            InvCart.CompanyName = c.CompanyName;
            InvCart.CompanyAddress = c.Address;
            InvCart.CompanyContactNumber = c.ContactNumber;
            InvCart.CompanyEmail = c.Email;
            InvCart.CompanyTIN = c.TIN;
            InvCart.CompanyVAT = c.VAT;
            InvCart.CompanyWebAddress = c.WebAddress;
            InvCart.Currency = c.Currency;
            InvCart.Stamp = c.Stamp;
            InvCart.ImageUrl = c.ImageUrl;

            var inv = db.Single(instanceId, id);
            if (inv == null)
            {
                InvCart.Clear();
                InvCart.InEdit = false;

                InvCart.PaymentTerms = c.PaymentTerms;
                InvCart.ClientNotes = c.ClientNotes;

                InvCart.InvoiceID = 0;
                InvCart.JobDescription = "";
                InvCart.InvoiceDate = bddb.WorkDate(instanceId);
                InvCart.DueDate = new DateTime(InvCart.InvoiceDate.Year, InvCart.InvoiceDate.Month, DateTime.DaysInMonth(InvCart.InvoiceDate.Year, InvCart.InvoiceDate.Month));
                InvCart.InvoiceNumber = "";
                InvCart.Discount = 0;
                InvCart.TaxPercent = 0;
                InvCart.AmountPaid = 0;
                ViewBag.PartyID = new SelectList(pdb.Party(instanceId), "PartyID", "PartyName");
            }
            else
            {
                InvCart.InEdit = true;

                var p = pdb.Single(instanceId, inv.PartyID);
                InvCart.PartyID = inv.PartyID;
                InvCart.PartyAddress = p.Address;
                InvCart.PartyContactNumber = p.ContactNumber;
                InvCart.PartyEmail = p.Email;
                InvCart.PartyName = p.PartyName;
                InvCart.PartyZIPCode = p.ZIPCode;

                InvCart.InvoiceID = inv.InvoiceID;
                InvCart.InvoiceNumber = inv.InvoiceNumber;
                InvCart.InvoiceDate = inv.InvoiceDate;
                InvCart.DueDate = inv.DueDate;
                InvCart.JobDescription = inv.JobDescription;
                InvCart.Discount = inv.Discount;
                InvCart.TaxPercent = inv.TaxPercent;
                InvCart.AmountPaid = inv.AmountPaid;
                InvCart.PaymentTerms = inv.PaymentTerms;
                InvCart.ClientNotes = inv.ClientNotes;

                var invdt = invDedb.Single(instanceId, inv.InvoiceID);
                foreach (var line in invdt)
                {
                    InvCart.AddItem(line.Item, line.Description, line.Cost, line.Quantity);
                }

                ViewBag.PartyID = new SelectList(pdb.Party(instanceId), "PartyID", "PartyName", InvCart.PartyID);
            }
            return View(InvCart);
        }

        // POST: Invoices/Invoices/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Inv([Bind(Include = "InvoiceNumber,PartyID,InvoiceDate,DueDate,JobDescription,PaymentTerms,ClientNotes,Discount,TaxPercent,AmountPaid")]InvVM invVM, InvCart InvCart)
        {
            if (ModelState.IsValid)
            {
                bool pass = false;

                if (db.IsExists(invVM.InvoiceNumber) != 0)
                {
                    if (InvCart.InEdit == true)
                    {
                        pass = true;
                    }
                    else
                    {
                        pass = false;
                        ModelState.AddModelError("InvoiceNumber", "Invoice Number Already Use");
                    }
                }
                else
                {
                    pass = true;
                }

                if (invVM.InvoiceDate <= invVM.DueDate)
                {
                    pass = true;
                }
                else
                {
                    pass = false;
                    ModelState.AddModelError("DueDate", "Invalid Due Date");
                }

                if (pass == true)
                {
                    InvCart.InvoiceNumber = invVM.InvoiceNumber;
                    InvCart.PartyID = invVM.PartyID;

                    var p = pdb.Single(instanceId, invVM.PartyID);
                    InvCart.PartyAddress = p.Address;
                    InvCart.PartyContactNumber = p.ContactNumber;
                    InvCart.PartyEmail = p.Email;
                    InvCart.PartyName = p.PartyName;
                    InvCart.PartyZIPCode = p.ZIPCode;

                    InvCart.InvoiceDate = invVM.InvoiceDate;
                    InvCart.DueDate = invVM.DueDate;
                    InvCart.JobDescription = invVM.JobDescription;
                    InvCart.PaymentTerms = invVM.PaymentTerms;
                    InvCart.ClientNotes = invVM.ClientNotes;
                    InvCart.Discount = invVM.Discount;
                    InvCart.TaxPercent = invVM.TaxPercent;
                    InvCart.AmountPaid = invVM.AmountPaid;
                    InvCart.InEdit = true;

                    return RedirectToAction("PrintPreview");
                }

            }
            ViewBag.PartyID = new SelectList(pdb.Party(instanceId), "PartyID", "PartyName", invVM.PartyID);
            return View(InvCart);
        }


        public ActionResult InvDetailSave()
        {
            return RedirectToAction("Index");
        }

        // POST: Invoices/Invoices/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult InvDetailSave([Bind(Include = "Item,Description,Cost,Quantity")] InvDetailsVM invdetailsVM, InvCart InvCart)
        {
            if (ModelState.IsValid)
            {
                InvCart.AddItem(invdetailsVM.Item, invdetailsVM.Description, invdetailsVM.Cost, invdetailsVM.Quantity);
            }
            ViewBag.PartyID = new SelectList(pdb.Party(instanceId), "PartyID", "PartyName", InvCart.PartyID);
            return View("Inv", InvCart);
        }

        public ActionResult RemoveFromInvoice()
        {
            return RedirectToAction("Index");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult RemoveFromInvoice(InvCart InvCart, string removeItem)
        {
            if (!(string.IsNullOrEmpty(removeItem)))
            {
                InvCart.RemoveLine(removeItem);
            }
            ViewBag.PartyID = new SelectList(pdb.Party(instanceId), "PartyID", "PartyName", InvCart.PartyID);         
            return View("Inv", InvCart);
            //return View("InvNew", InvCart);
        }

        // POST: Invoices/Invoices/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long InvoiceID)
        {
            //Invoice invoice = db.Single(instanceId, InvoiceID);
            db.DeleteInvoice(InvoiceID);
            return RedirectToAction("Index");
        }

        // GET: Employees search
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index(string txtSearch = "")
        {
            if (txtSearch == "")
            {
                txtSearch = "?";
            }
            var invoice = db.Search(instanceId, txtSearch);
            return View("Index", invoice.ToList());
        }
    }
}
