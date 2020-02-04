using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using Nyika.Domain.Concrete;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Nyika.WebUI.Models;
using Nyika.WebUI.Areas.MF.Models;
using Nyika.Domain.Abstract.MF;
using Nyika.Domain.Abstract.Accounts;

namespace Nyika.WebUI.Areas.MF.Controllers
{
    [Authorize(Roles = "Accountant,Manager,Super Admin")]
    public class AllreportsController : Controller
    {

        private EFDbContext db = new EFDbContext();
        private IProjectRepo pdb;
        private IBusinessDayRepo bdb;

        ConnectionInfo connectionInfo = new ConnectionInfo();
        private string instanceId;

        public AllreportsController(IProjectRepo PDB, IBusinessDayRepo BDB)
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

            this.pdb = PDB;
            this.bdb = BDB;
        }

        // GET: Reports
        public ActionResult Index()
        {

            ReportViewModelM rv = new ReportViewModelM();
            rv.Project = new SelectList(pdb.Project(instanceId), "ProjectID", "ProjectName");
            rv.FromDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
            rv.TillDate = DateTime.Now.Date;
            return View(rv);
        }

        [HttpPost, ActionName("Export")]
        public ActionResult Export([Bind(Include = "fromDate,TillDate,SelectedProject,SelectedGroup,SelectedMember,SelectedLoan,SelectedDay")] ReportViewModelM reportViewModel, string BtnAll)
        {

            if (ModelState.IsValid)
            {

                if (BtnAll == "Member List - Active")
                {
                    return RedirectToAction("MemberList", "Allreports", new { InstanceID = instanceId, Inactive = 0, ProjectID = reportViewModel.SelectedProject });
                }
                if (BtnAll == "Member List - Inactive")
                {
                    return RedirectToAction("MemberList", "Allreports", new { InstanceID = instanceId, Inactive = 1, ProjectID = reportViewModel.SelectedProject });
                }
                if (BtnAll == "Member Wise Disbursement List")
                {
                    return RedirectToAction("MemberWiseDisbursementList", "Allreports", new { InstanceID = instanceId, Memberid = reportViewModel.SelectedMember });
                }

                if (BtnAll == "Loan Schedule")
                {
                    return RedirectToAction("LoanSchedule", "Allreports", new { InstanceID = instanceId, LoanID = reportViewModel.SelectedLoan, memberid = reportViewModel.SelectedMember });
                }

                if (BtnAll == "Loan Collection")
                {
                    return RedirectToAction("LoanCollection", "Allreports", new { InstanceID = instanceId, LoanID = reportViewModel.SelectedLoan, memberid = reportViewModel.SelectedMember });
                }
                if (BtnAll == "Member Wise Security Ledger")
                {
                    return RedirectToAction("MemberWiseSecurityLedger", "Allreports", new { InstanceID = instanceId, Memberid = reportViewModel.SelectedMember });
                }

                if (BtnAll == "Total Security Balance")
                {
                    return RedirectToAction("TotalSecurityBalance", "Allreports", new { InstanceID = instanceId });
                }

                if (BtnAll == "Group Security Balance")
                {
                    return RedirectToAction("GroupSecurityBalance", "Allreports", new { InstanceID = instanceId });
                }

                if (BtnAll == "DCR List")
                {
                    return RedirectToAction("DCRList", "Allreports", new { FromDate = reportViewModel.FromDate, TillDate = reportViewModel.TillDate, InstanceID = instanceId });
                }

                if (BtnAll == "DCR Mismatch")
                {
                    return RedirectToAction("DCRMismatch", "Allreports", new { InstanceID = instanceId });
                }

                if (BtnAll == "Disbursement List")
                {
                    return RedirectToAction("DisbursementList", "Allreports", new { FromDate = reportViewModel.FromDate, TillDate = reportViewModel.TillDate, ProjectID = reportViewModel.SelectedProject, InstanceID = instanceId });
                }

                if (BtnAll == "Outstanding Loan List")
                {
                    return RedirectToAction("OutstandingLoanList", "Allreports", new { ProjectID = reportViewModel.SelectedProject, InstanceID = instanceId });
                }

                if (BtnAll == "Monthly Collection Sheet")
                {
                    return RedirectToAction("MonthlyCollectionSheet", "Allreports", new { Colday = reportViewModel.SelectedDay, InstanceID = instanceId });
                }

                if (BtnAll == "Monthly Data Collection")
                {
                    return RedirectToAction("MonthlyDataCollection", "Allreports", new { FromDate = reportViewModel.FromDate, TillDate = reportViewModel.TillDate, InstanceID = instanceId });
                }

                if (BtnAll == "Monthly Basic Info")
                {
                    return RedirectToAction("MonthlyBasicInfo", "Allreports", new { FromDate = reportViewModel.FromDate, TillDate = reportViewModel.TillDate, InstanceID = instanceId });
                }
            }

            return RedirectToAction("Index");
        }


        public ActionResult MemberList(string InstanceID, int inactive, long ProjectID)
        {
            ReportDocument rptH = new ReportDocument();
            rptH.Load(Path.Combine(Server.MapPath("~/Reports/MF"), "rptMFMemberList.rpt"));

            foreach (Table table in rptH.Database.Tables)
            {
                table.LogOnInfo.ConnectionInfo = connectionInfo;
                table.ApplyLogOnInfo(table.LogOnInfo);
            }

            rptH.SetParameterValue("InstanceID", InstanceID);
            rptH.SetParameterValue("inactive", inactive);
            rptH.SetParameterValue("ProjectID", ProjectID);

            Stream stream = rptH.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
            rptH.Close();
            rptH.Dispose();
            return File(stream, "application/pdf");

        }

        public ActionResult LoanSchedule(long MemberID, long LoanID, string InstanceID)
        {
            ReportDocument rptH = new ReportDocument();
            rptH.Load(Path.Combine(Server.MapPath("~/Reports/MF"), "rptMFLoanSchedule.rpt"));

            foreach (Table table in rptH.Database.Tables)
            {
                table.LogOnInfo.ConnectionInfo = connectionInfo;
                table.ApplyLogOnInfo(table.LogOnInfo);
            }


            rptH.SetParameterValue("MemberID", MemberID);
            rptH.SetParameterValue("LoanID", LoanID);
            rptH.SetParameterValue("InstanceID", InstanceID);

            Stream stream = rptH.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
            rptH.Close();
            rptH.Dispose();
            return File(stream, "application/pdf");

        }

        public ActionResult LoanCollection(long MemberID, long LoanID, string InstanceID)
        {
            ReportDocument rptH = new ReportDocument();
            rptH.Load(Path.Combine(Server.MapPath("~/Reports/MF"), "rptMFLoanCollection.rpt"));

            foreach (Table table in rptH.Database.Tables)
            {
                table.LogOnInfo.ConnectionInfo = connectionInfo;
                table.ApplyLogOnInfo(table.LogOnInfo);
            }

            rptH.SetParameterValue("MemberID", MemberID);
            rptH.SetParameterValue("LoanID", LoanID);
            rptH.SetParameterValue("InstanceID", InstanceID);

            Stream stream = rptH.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
            rptH.Close();
            rptH.Dispose();
            return File(stream, "application/pdf");

        }

        public ActionResult MemberWiseSecurityLedger(string InstanceID, long MemberID)
        {
            ReportDocument rptH = new ReportDocument();
            rptH.Load(Path.Combine(Server.MapPath("~/Reports/MF"), "rptMFMemberWiseSecurityLedger.rpt"));


            foreach (Table table in rptH.Database.Tables)
            {
                table.LogOnInfo.ConnectionInfo = connectionInfo;
                table.ApplyLogOnInfo(table.LogOnInfo);
            }

            rptH.SetParameterValue("MemberID", MemberID);
            rptH.SetParameterValue("InstanceID", InstanceID);

            Stream stream = rptH.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
            rptH.Close();
            rptH.Dispose();
            return File(stream, "application/pdf");

        }

        public ActionResult TotalSecurityBalance(string InstanceID)
        {
            ReportDocument rptH = new ReportDocument();
            rptH.Load(Path.Combine(Server.MapPath("~/Reports/MF"), "rptMFTotalSecurityBalance.rpt"));


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

        public ActionResult GroupSecurityBalance(string InstanceID)
        {
            ReportDocument rptH = new ReportDocument();
            rptH.Load(Path.Combine(Server.MapPath("~/Reports/MF"), "rptMFGroupSecurityBalance.rpt"));


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

        public ActionResult DCRList(DateTime FromDate, DateTime TillDate, string InstanceID)
        {
            ReportDocument rptH = new ReportDocument();
            rptH.Load(Path.Combine(Server.MapPath("~/Reports/MF"), "rptMFDCRList.rpt"));


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

        public ActionResult DCRMismatch(string InstanceID)
        {
            ReportDocument rptH = new ReportDocument();
            rptH.Load(Path.Combine(Server.MapPath("~/Reports/MF"), "rptMFDCRListMis.rpt"));


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

        public ActionResult MemberWiseDisbursementList(string InstanceID, long MemberID)
        {
            ReportDocument rptH = new ReportDocument();
            rptH.Load(Path.Combine(Server.MapPath("~/Reports/MF"), "rptMFMemberWiseDisbursement.rpt"));


            foreach (Table table in rptH.Database.Tables)
            {
                table.LogOnInfo.ConnectionInfo = connectionInfo;
                table.ApplyLogOnInfo(table.LogOnInfo);
            }

            //rptH.SetParameterValue("InstanceID", InstanceID);
            rptH.SetParameterValue("InstanceID", InstanceID);
            rptH.SetParameterValue("MemberID", MemberID);

            Stream stream = rptH.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
            rptH.Close();
            rptH.Dispose();
            return File(stream, "application/pdf");

        }

        public ActionResult DisbursementList(DateTime FromDate, DateTime TillDate, long ProjectID, string InstanceID)
        {
            ReportDocument rptH = new ReportDocument();
            rptH.Load(Path.Combine(Server.MapPath("~/Reports/MF"), "rptMFDisbursementList.rpt"));


            foreach (Table table in rptH.Database.Tables)
            {
                table.LogOnInfo.ConnectionInfo = connectionInfo;
                table.ApplyLogOnInfo(table.LogOnInfo);
            }

            //rptH.SetParameterValue("InstanceID", InstanceID);
            rptH.SetParameterValue("ProjectID", ProjectID);
            rptH.SetParameterValue("FromDate", FromDate);
            rptH.SetParameterValue("TillDate", TillDate);

            Stream stream = rptH.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
            rptH.Close();
            rptH.Dispose();
            return File(stream, "application/pdf");

        }

        public ActionResult OutstandingLoanList(long ProjectID, string InstanceID)
        {
            ReportDocument rptH = new ReportDocument();
            rptH.Load(Path.Combine(Server.MapPath("~/Reports/MF"), "rptMFOutStandingLoanList.rpt"));


            foreach (Table table in rptH.Database.Tables)
            {
                table.LogOnInfo.ConnectionInfo = connectionInfo;
                table.ApplyLogOnInfo(table.LogOnInfo);
            }

            //rptH.SetParameterValue("InstanceID", InstanceID);
            rptH.SetParameterValue("ProjectID", ProjectID);

            Stream stream = rptH.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
            rptH.Close();
            rptH.Dispose();
            return File(stream, "application/pdf");

        }

        public static DateTime GetNextWeekday(DateTime start, DayOfWeek day)
        {
            // The (... + 7) % 7 ensures we end up with a value in the range [0, 6]
            int daysToAdd = ((int)day - (int)start.DayOfWeek + 7) % 7;
            return start.AddDays(daysToAdd);
        }

        public ActionResult MonthlyCollectionSheet(int Colday, string InstanceID)
        {
            ReportDocument rptH = new ReportDocument();
            rptH.Load(Path.Combine(Server.MapPath("~/Reports/MF"), "rptMFMonthlyCollectionSheet.rpt"));


            foreach (Table table in rptH.Database.Tables)
            {
                table.LogOnInfo.ConnectionInfo = connectionInfo;
                table.ApplyLogOnInfo(table.LogOnInfo);
            }


            rptH.SetParameterValue("Colday", Colday);
            rptH.SetParameterValue("InstanceID", InstanceID);

            DateTime a = bdb.WorkDate(InstanceID);
            DateTime firstDate = a.AddDays(a.Day * -1).Date.AddDays(1);
            DateTime endDate = firstDate.AddMonths(1).AddDays(-1);
            DateTime tmpDate = tmpDate = GetNextWeekday(firstDate, ((DayOfWeek)Colday)-1);
            int c = 1;


            //rptH.SetParameterValue("IntDay02", GetNextWeekday(firstDate, (DayOfWeek)Colday));
            //rptH.SetParameterValue("IntDay03", GetNextWeekday(firstDate, (DayOfWeek)Colday));
            //rptH.SetParameterValue("IntDay04", GetNextWeekday(firstDate, (DayOfWeek)Colday));
            //rptH.SetParameterValue("IntDay05", GetNextWeekday(firstDate, (DayOfWeek)Colday));

            while (tmpDate <= endDate)
            {
                if (c == 1)
                {
                    rptH.SetParameterValue("IntDay01", tmpDate.Date.ToString("dd/MMM/yy"));
                }
                if (c == 2)
                {
                    rptH.SetParameterValue("IntDay02", tmpDate.Date.ToString("dd/MMM/yy"));
                }
                if (c == 3)
                {
                    rptH.SetParameterValue("IntDay03", tmpDate.Date.ToString("dd/MMM/yy"));
                }
                if (c == 4)
                {
                    rptH.SetParameterValue("IntDay04", tmpDate.Date.ToString("dd/MMM/yy"));
                }
                if (c == 5)
                {
                    rptH.SetParameterValue("IntDay05", tmpDate.Date.ToString("dd/MMM/yy"));
                }
                else
                {
                    rptH.SetParameterValue("IntDay05", "");
                }
                c++;

                tmpDate = tmpDate.AddDays(7); //GetNextWeekday(tmpDate, ((DayOfWeek)Colday)-1);

            };


            Stream stream = rptH.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
            rptH.Close();
            rptH.Dispose();
            return File(stream, "application/pdf");

        }

        public ActionResult MonthlyDataCollection(DateTime FromDate, DateTime TillDate, string InstanceID)
        {
            ReportDocument rptH = new ReportDocument();
            rptH.Load(Path.Combine(Server.MapPath("~/Reports/MF"), "rptMFDataCollection.rpt"));


            foreach (Table table in rptH.Database.Tables)
            {
                table.LogOnInfo.ConnectionInfo = connectionInfo;
                table.ApplyLogOnInfo(table.LogOnInfo);
            }

            rptH.SetParameterValue("FromDate", FromDate);
            rptH.SetParameterValue("TillDate", TillDate);
            //rptH.SetParameterValue("InstanceID", InstanceID);

            Stream stream = rptH.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
            rptH.Close();
            rptH.Dispose();
            return File(stream, "application/pdf");

        }

        public ActionResult MonthlyBasicInfo(DateTime FromDate, DateTime TillDate, string InstanceID)
        {
            ReportDocument rptH = new ReportDocument();
            rptH.Load(Path.Combine(Server.MapPath("~/Reports/mf"), "rptMFMonthlyBasicInfo.rpt"));


            foreach (Table table in rptH.Database.Tables)
            {
                table.LogOnInfo.ConnectionInfo = connectionInfo;
                table.ApplyLogOnInfo(table.LogOnInfo);
            }

            rptH.SetParameterValue("FromDate", FromDate);
            rptH.SetParameterValue("TillDate", TillDate);
            //rptH.SetParameterValue("InstanceID", InstanceID);

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