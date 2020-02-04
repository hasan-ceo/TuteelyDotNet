using Microsoft.AspNet.Identity;
using Nyika.Domain.Concrete;
using Nyika.Domain.Concrete.Accounts;
using Nyika.Domain.Entities.Accounts;
using Nyika.WebUI.Areas.Accounts.Models;
using Nyika.WebUI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Nyika.WebUI.Areas.Accounts.Controllers
{
    [Authorize(Roles = "Accountant,Super Admin")]
    public class VouchersController : Controller
    {
        private EFDbContext db = new EFDbContext();
        private string instanceId;

        public VouchersController()
        {
            instanceId = new InstanceVM().InstanceID;
        }

        private List<CashBankVM> cashBank()
        {

            var bank = db.Bank.Where(b => b.InstanceID == instanceId);
            CashBankVM cb = new CashBankVM();
            List<CashBankVM> parts = new List<CashBankVM>();
            cb.ItemID = 0;
            cb.ItemName = "CASH";
            parts.Add(cb);
            foreach (var b in bank)
            {
                CashBankVM cb1 = new CashBankVM();
                cb1.ItemID = b.BankID;
                cb1.ItemName = b.BankName;
                parts.Add(cb1);
            }
            return parts;
        }

        // GET: Vouchers
        public ActionResult Payment()
        {

            ViewBag.BankID = new SelectList(cashBank(), "ItemID", "ItemName");
            ViewBag.AccountSubHeadID = new SelectList(db.AccountSubHead.Where(a => a.AutoAc == false && a.InstanceID == instanceId), "AccountSubHeadID", "AccountSubHeadName");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Payment([Bind(Include = "AccountSubHeadID, Amount, Pat,BankID")] CBPayRecVM peyrec)
        {

            if (ModelState.IsValid)
            {

                db.Database.ExecuteSqlCommand("exec pAccVchPayRec  @TransType= {0} , @BankID= {1} , @AccountSubHeadID={2} ,  @Amount={3},  @Particulars={4} ,  @VType={5}, @instanceId={6},@PartyID= {7},@EntryBy={8}", (peyrec.BankID == 0 ? 0 : 1), (peyrec.BankID == 0 ? 0 : peyrec.BankID), peyrec.AccountSubHeadID, peyrec.Amount, peyrec.Pat, "Payment", instanceId, 0, User.Identity.Name);
                TempData["Msg"] = "Successfully Save";
                peyrec.Amount = 0;
                peyrec.Pat = "";
            }
            ViewBag.BankID = new SelectList(cashBank(), "ItemID", "ItemName", peyrec.BankID);
            ViewBag.AccountSubHeadID = new SelectList(db.AccountSubHead.Where(a => a.AutoAc == false && a.InstanceID == instanceId), "AccountSubHeadID", "AccountSubHeadName", peyrec.AccountSubHeadID);
            return View(peyrec);
        }

        // GET: Vouchers
        public ActionResult Receive()
        {

            ViewBag.BankID = new SelectList(cashBank(), "ItemID", "ItemName");
            ViewBag.AccountSubHeadID = new SelectList(db.AccountSubHead.Where(a => a.AutoAc == false && a.InstanceID == instanceId), "AccountSubHeadID", "AccountSubHeadName");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Receive([Bind(Include = "BankID, AccountSubHeadID, Amount, Pat")] CBPayRecVM peyrec)
        {

            if (ModelState.IsValid)
            {
                db.Database.ExecuteSqlCommand("exec pAccVchPayRec  @TransType= {0} , @BankId= {1} , @AccountSubHeadID={2} ,  @Amount={3},  @Particulars={4} ,  @VType={5},  @instanceId={6},@PartyID= {7},@EntryBy={8}", (peyrec.BankID == 0 ? 0 : 1), (peyrec.BankID == 0 ? 0 : peyrec.BankID), peyrec.AccountSubHeadID, peyrec.Amount, peyrec.Pat, "Receive", instanceId, 0, User.Identity.Name);
                TempData["Msg"] = "Successfully Save";
                peyrec.Amount = 0;
                peyrec.Pat = "";
            }
            ViewBag.BankID = new SelectList(cashBank(), "ItemID", "ItemName", peyrec.BankID);
            ViewBag.AccountSubHeadID = new SelectList(db.AccountSubHead.Where(a => a.AutoAc == false && a.InstanceID == instanceId), "AccountSubHeadID", "AccountSubHeadName", peyrec.AccountSubHeadID);
            return View(peyrec);
        }



        // GET: Vouchers
        public ActionResult PartyPayment()
        {

            ViewBag.BankID = new SelectList(cashBank(), "ItemID", "ItemName");
            ViewBag.PartyID = new SelectList(db.Party.Where(p => p.InstanceID == instanceId), "PartyID", "PartyName");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult PartyPayment([Bind(Include = "Amount, Pat, PartyID, BankID")] CBPayRecVM peyrec)
        {

            if (ModelState.IsValid)
            {

                db.Database.ExecuteSqlCommand("exec pAccVchPartyPayRec  @TransType= {0} , @BankID= {1} ,  @Amount={2},  @Particulars={3} ,  @VType={4}, @instanceId={5},@PartyID= {6},@EntryBy={7}", (peyrec.BankID == 0 ? 0 : 1), (peyrec.BankID == 0 ? 0 : peyrec.BankID), peyrec.Amount, peyrec.Pat, "Payment", instanceId, peyrec.PartyID, User.Identity.Name);
                TempData["Msg"] = "Successfully Save";
                peyrec.Amount = 0;
                peyrec.Pat = "";
            }
            ViewBag.BankID = new SelectList(cashBank(), "ItemID", "ItemName", peyrec.BankID);
            ViewBag.PartyID = new SelectList(db.Party.Where(p => p.InstanceID == instanceId), "PartyID", "PartyName", peyrec.PartyID);
            return View(peyrec);
        }

        // GET: Vouchers
        public ActionResult PartyReceive()
        {

            ViewBag.BankID = new SelectList(cashBank(), "ItemID", "ItemName");
            ViewBag.PartyID = new SelectList(db.Party.Where(p => p.InstanceID == instanceId), "PartyID", "PartyName");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult PartyReceive([Bind(Include = "Amount, Pat, PartyID, BankID")] CBPayRecVM peyrec)
        {

            if (ModelState.IsValid)
            {

                db.Database.ExecuteSqlCommand("exec pAccVchPartyPayRec  @TransType= {0} , @BankID= {1} , @Amount={2},  @Particulars={3} ,  @VType={4}, @instanceId={5},@PartyID= {6},@EntryBy={7}", (peyrec.BankID == 0 ? 0 : 1), (peyrec.BankID == 0 ? 0 : peyrec.BankID), peyrec.Amount, peyrec.Pat, "Receive", instanceId, peyrec.PartyID, User.Identity.Name);
                TempData["Msg"] = "Successfully Save";
                peyrec.Amount = 0;
                peyrec.Pat = "";
            }
            ViewBag.BankID = new SelectList(cashBank(), "ItemID", "ItemName", peyrec.BankID);
            ViewBag.PartyID = new SelectList(db.Party.Where(p => p.InstanceID == instanceId), "PartyID", "PartyName", peyrec.PartyID);
            return View(peyrec);
        }

        // GET: Vouchers
        public ActionResult Journal()
        {

            ViewBag.VType = "Journal";
            ViewBag.AccountSubHeadIDDr = new SelectList(db.AccountSubHead.Where(a => a.AutoAc == false && a.InstanceID == instanceId), "AccountSubHeadID", "AccountSubHeadName");
            ViewBag.AccountSubHeadIDCr = new SelectList(db.AccountSubHead.Where(a => a.AutoAc == false && a.InstanceID == instanceId), "AccountSubHeadID", "AccountSubHeadName");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Journal([Bind(Include = "AccountSubHeadIDDr, AccountSubHeadIDCr, Amount, Pat")] VoucherJournalViewModel peyrec)
        {

            if (peyrec.AccountSubHeadIDDr != peyrec.AccountSubHeadIDCr)
            {
                if (ModelState.IsValid)
                {

                    db.Database.ExecuteSqlCommand("exec pAccVchJournal   @AccountSubHeadIDDr= {0} , @AccountSubHeadIDCr= {1} ,  @Amount={2},  @Particulars={3} ,  @VType={4}, @instanceId={5},@EntryBy={6}", peyrec.AccountSubHeadIDDr, peyrec.AccountSubHeadIDCr, peyrec.Amount, peyrec.Pat, "Journal", instanceId, User.Identity.Name);
                    peyrec.Amount = 0;
                    peyrec.Pat = "";
                    TempData["Msg"] = "Successfully Save";
                }
            }
            else
            {
                TempData["Msg"] = "Please Select Different Account Head";
            }
            ViewBag.AccountSubHeadIDDr = new SelectList(db.AccountSubHead.Where(a => a.AutoAc == false && a.InstanceID == instanceId), "AccountSubHeadID", "AccountSubHeadName", peyrec.AccountSubHeadIDDr);
            ViewBag.AccountSubHeadIDCr = new SelectList(db.AccountSubHead.Where(a => a.AutoAc == false && a.InstanceID == instanceId), "AccountSubHeadID", "AccountSubHeadName", peyrec.AccountSubHeadIDCr);
            return View(peyrec);
        }

        // GET: Vouchers
        public ActionResult Transfer()
        {

            ViewBag.BankID = new SelectList(db.Bank.Where(b => b.BankID != 0 && b.InstanceID == instanceId), "BankID", "BankName");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Transfer([Bind(Include = "TransType, BankID, Amount, Pat, VType")] VoucherTransferViewModel peyrec)
        {

            if (ModelState.IsValid)
            {
                db.Database.ExecuteSqlCommand("exec pAccVchTransfer  @TransType= {0}, @BankID= {1}, @Amount={2}, @Particulars={3}, @VType={4}, @instanceId={5},@EntryBy={6}", peyrec.TransType, peyrec.BankID, peyrec.Amount, peyrec.Pat, "Transfer", instanceId, User.Identity.Name);
                peyrec.Amount = 0;
                peyrec.Pat = "";
                TempData["Msg"] = "Successfully Save";
                //return RedirectToAction("Transfer");
            }
            ViewBag.BankID = new SelectList(db.Bank.Where(b => b.BankID != 0 && b.InstanceID == instanceId), "BankID", "BankName", peyrec.BankID);
            return View(peyrec);
        }


        // GET: Vouchers
        public ActionResult Reverse()
        {
            ViewBag.VType = "Reverse";
            return View(db.AccountGL.Where(a => a.AccountGLID == 0));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Reverse([Bind(Include = "FromDate, TillDate")] VoucherReverseViewModel peyrec)
        {
            if (peyrec.FromDate <= peyrec.TillDate)
            {
                if (ModelState.IsValid)
                {

                    var voucherList = db.AccountGL.Where(a => (a.WorkDate >= peyrec.FromDate && a.WorkDate <= peyrec.TillDate) && a.InstanceID == instanceId);
                    return View(voucherList.ToList());
                }
            }
            else
            {
                TempData["Msg"] = "Please check from date and till date";
            }
            return View(db.AccountGL.Where(a => a.AccountGLID == 0));
        }

        public ActionResult ReverseOk(long id)
        {
            db.Database.ExecuteSqlCommand("exec pAccVchReverse  @AccountGLID={0}", id);
            return RedirectToAction("Reverse");
        }



    }
}