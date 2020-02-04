using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using Nyika.Domain.Concrete;
using Nyika.WebUI.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Nyika.WebUI.Areas.Accounts.Models;

namespace Nyika.WebUI.Areas.Accounts.Controllers
{
    [Authorize(Roles = "Accountant,Manager,Super Admin")]
    public class AllreportsController : Controller
    {
        private string instanceId;
        private EFDbContext db = new EFDbContext();
        ConnectionInfo connectionInfo = new ConnectionInfo();

        public AllreportsController()
        {
            var connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["EFDbContext"].ConnectionString;

            SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder(connectionString);
            string dbName = builder.InitialCatalog;
            string dbDataSource = builder.DataSource;
            string UserID = builder.UserID;
            string pass = builder.Password;

            connectionInfo.UserID = UserID;
            connectionInfo.Password = pass;
            connectionInfo.ServerName = dbDataSource;
            connectionInfo.DatabaseName = dbName;

            instanceId = new InstanceVM().InstanceID;
        }

        // GET: Reports
        public ActionResult Index()
        {
            
            ReportViewModel rv = new ReportViewModel();
            ViewBag.AccountSubHeadID = new SelectList(db.AccountSubHead.Where(a => a.InstanceID == instanceId), "AccountSubHeadID", "AccountSubHeadName");
            ViewBag.BankID = new SelectList(db.Bank.Where(a => a.InstanceID == instanceId), "BankID", "BankName");
            ViewBag.PartyID = new SelectList(db.Party.Where(a => a.InstanceID == instanceId), "PartyID", "PartyName");
            rv.FromDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
            rv.TillDate = DateTime.Now.Date;
            return View(rv);
        }

        [HttpPost, ActionName("Export")]
        public ActionResult Export([Bind(Include = "fromDate,TillDate,AccountSubHeadID,BankID,PartyID")] ReportViewModel reportViewModel, string BtnAll)
        {
            
            if (ModelState.IsValid)
            {

                if (BtnAll == "Ledger")
                {
                    return RedirectToAction("Ledger", "Allreports", new { FromDate = reportViewModel.FromDate, TillDate = reportViewModel.TillDate, AccountSubHeadID = reportViewModel.AccountSubHeadID, InstanceID = instanceId });
                }

                if (BtnAll == "Bank Ledger")
                {
                    return RedirectToAction("BankLedger", "Allreports", new { FromDate = reportViewModel.FromDate, TillDate = reportViewModel.TillDate, BankID = reportViewModel.BankID, InstanceID = instanceId });
                }

                if (BtnAll == "Trial Balance (Details)")
                {
                    return RedirectToAction("TrialBalanceDetails", "Allreports", new { FromDate = reportViewModel.FromDate, TillDate = reportViewModel.TillDate, InstanceID = instanceId });
                }

                if (BtnAll == "Trial Balance (Consolidated)")
                {
                    return RedirectToAction("TrialBalanceConsolidated", "Allreports", new { FromDate = reportViewModel.FromDate, TillDate = reportViewModel.TillDate, InstanceID = instanceId });
                }

                if (BtnAll == "Party Ledger")
                {
                    return RedirectToAction("PartyLedger", "Allreports", new { FromDate = reportViewModel.FromDate, TillDate = reportViewModel.TillDate, PartyID = reportViewModel.PartyID, InstanceID = instanceId });
                }

                if (BtnAll == "Creditor List")
                {
                    return RedirectToAction("CreditorList", "Allreports", new { InstanceID = instanceId });
                }

                if (BtnAll == "Debitor List")
                {
                    return RedirectToAction("DebitorList", "Allreports", new { InstanceID = instanceId });
                }

                if (BtnAll == "Cash Requisition List")
                {
                    return RedirectToAction("CashRequisitionList", "Allreports", new { FromDate = reportViewModel.FromDate, TillDate = reportViewModel.TillDate, InstanceID = instanceId });
                }
            }

            return RedirectToAction("Index");
        }


        public ActionResult Ledger(DateTime FromDate, DateTime TillDate, int AccountSubHeadID,string InstanceID)
        {
            ReportDocument rptH = new ReportDocument();
            rptH.Load(Path.Combine(Server.MapPath("~/Reports"), "rptAccLedger.rpt"));

            foreach (Table table in rptH.Database.Tables)
            {
                table.LogOnInfo.ConnectionInfo = connectionInfo;
                table.ApplyLogOnInfo(table.LogOnInfo);
            }

            rptH.SetParameterValue("FromDate", FromDate);
            rptH.SetParameterValue("ToDate", TillDate);
            rptH.SetParameterValue("AccountSubHeadID", AccountSubHeadID);
            rptH.SetParameterValue("InstanceID", InstanceID);

            Stream stream = rptH.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
            rptH.Close();
            rptH.Dispose();
            return File(stream, "application/pdf");

        }

        public ActionResult BankLedger(DateTime FromDate, DateTime TillDate, int BankID, string InstanceID)
        {
            ReportDocument rptH = new ReportDocument();
            rptH.Load(Path.Combine(Server.MapPath("~/Reports"), "rptAccBankLedger.rpt"));

            foreach (Table table in rptH.Database.Tables)
            {
                table.LogOnInfo.ConnectionInfo = connectionInfo;
                table.ApplyLogOnInfo(table.LogOnInfo);
            }

            rptH.SetParameterValue("FromDate", FromDate);
            rptH.SetParameterValue("ToDate", TillDate);
            rptH.SetParameterValue("AccountSubHeadID", 2);
            rptH.SetParameterValue("BankID", BankID);
            rptH.SetParameterValue("InstanceID", InstanceID);

            Stream stream = rptH.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
            rptH.Close();
            rptH.Dispose();
            return File(stream, "application/pdf");

        }

        public ActionResult PartyLedger(DateTime FromDate, DateTime TillDate, int PartyID, string InstanceID)
        {
            ReportDocument rptH = new ReportDocument();
            rptH.Load(Path.Combine(Server.MapPath("~/Reports"), "rptAccPartyLedger.rpt"));

            foreach (Table table in rptH.Database.Tables)
            {
                table.LogOnInfo.ConnectionInfo = connectionInfo;
                table.ApplyLogOnInfo(table.LogOnInfo);
            }

            rptH.SetParameterValue("FromDate", FromDate);
            rptH.SetParameterValue("ToDate", TillDate);
            rptH.SetParameterValue("PartyID", PartyID);
            rptH.SetParameterValue("InstanceID", InstanceID);

            Stream stream = rptH.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
            rptH.Close();
            rptH.Dispose();
            return File(stream, "application/pdf");

        }

        public ActionResult TrialBalanceDetails(DateTime FromDate, DateTime TillDate, string InstanceID)
        {
            ReportDocument rptH = new ReportDocument();
            rptH.Load(Path.Combine(Server.MapPath("~/Reports"), "rptAccTrialBalanceDetails.rpt"));


            foreach (Table table in rptH.Database.Tables)
            {
                table.LogOnInfo.ConnectionInfo = connectionInfo;
                table.ApplyLogOnInfo(table.LogOnInfo);
            }

            rptH.SetParameterValue("FromDate", FromDate);
            rptH.SetParameterValue("ToDate", TillDate);
            rptH.SetParameterValue("InstanceID", InstanceID);

            Stream stream = rptH.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
            rptH.Close();
            rptH.Dispose();
            return File(stream, "application/pdf");

        }

        public ActionResult TrialBalanceConsolidated(DateTime FromDate, DateTime TillDate, string InstanceID)
        {
            ReportDocument rptH = new ReportDocument();
            rptH.Load(Path.Combine(Server.MapPath("~/Reports"), "rptAccTrialBalanceConsolidated.rpt"));


            foreach (Table table in rptH.Database.Tables)
            {
                table.LogOnInfo.ConnectionInfo = connectionInfo;
                table.ApplyLogOnInfo(table.LogOnInfo);
            }

            rptH.SetParameterValue("FromDate", FromDate);
            rptH.SetParameterValue("ToDate", TillDate);
            rptH.SetParameterValue("InstanceID", InstanceID);

            Stream stream = rptH.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
            rptH.Close();
            rptH.Dispose();
            return File(stream, "application/pdf");

        }

        public ActionResult CreditorList(string InstanceID)
        {
            ReportDocument rptH = new ReportDocument();
            rptH.Load(Path.Combine(Server.MapPath("~/Reports"), "rptAccCrList.rpt"));


            foreach (Table table in rptH.Database.Tables)
            {
                table.LogOnInfo.ConnectionInfo = connectionInfo;
                table.ApplyLogOnInfo(table.LogOnInfo);
            }

            rptH.SetParameterValue("InstanceID", InstanceID);

            Stream stream = rptH.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
            rptH.Close();
            rptH.Dispose();
            return File(stream, "application/pdf");

        }

        public ActionResult DebitorList(string InstanceID)
        {
            ReportDocument rptH = new ReportDocument();
            rptH.Load(Path.Combine(Server.MapPath("~/Reports"), "rptAccDrList.rpt"));


            foreach (Table table in rptH.Database.Tables)
            {
                table.LogOnInfo.ConnectionInfo = connectionInfo;
                table.ApplyLogOnInfo(table.LogOnInfo);
            }

            rptH.SetParameterValue("InstanceID", InstanceID);

            Stream stream = rptH.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
            rptH.Close();
            rptH.Dispose();
            return File(stream, "application/pdf");

        }


        public ActionResult CashRequisitionList(DateTime FromDate, DateTime TillDate, string InstanceID)
        {
            ReportDocument rptH = new ReportDocument();
            rptH.Load(Path.Combine(Server.MapPath("~/Reports"), "rptAccCashRequisitionList.rpt"));


            foreach (Table table in rptH.Database.Tables)
            {
                table.LogOnInfo.ConnectionInfo = connectionInfo;
                table.ApplyLogOnInfo(table.LogOnInfo);
            }

            rptH.SetParameterValue("FromDate", FromDate);
            rptH.SetParameterValue("TillDate", TillDate);
            rptH.SetParameterValue("InstanceID", InstanceID);

            Stream stream = rptH.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
            rptH.Close();
            rptH.Dispose();
            return File(stream, "application/pdf");

        }




        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);

        }

    }
}