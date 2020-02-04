using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using Nyika.Domain.Concrete;
using Nyika.WebUI.Areas.HRnPayroll.Models;
using Nyika.WebUI.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Nyika.WebUI.Helper;
using Nyika.Domain.Abstract.HR;

namespace Nyika.WebUI.Areas.HRnPayroll.Controllers
{
    [Authorize(Roles = "HR Executive,Manager,Super Admin")]
    public class AllreportsController : Controller
    {
        private IEmployeeRepo emp;
        private IEmployeeAttendanceRepo db;
        ConnectionInfo connectionInfo = new ConnectionInfo();
        private string instanceId;

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ProcessAttendance()
        {
            
            db.ProcessAttendance(instanceId);
            TempData["ProcessComplete"] = "Process Complete";
            return RedirectToAction("Index", "Allreports");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ProcessSalary()
        {
            
            db.ProcessSalary(instanceId);
            TempData["ProcessComplete"] = "Process Complete";
            return RedirectToAction("Index", "Allreports");
        }

        public AllreportsController(IEmployeeRepo EmpDB, IEmployeeAttendanceRepo DB)
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

            this.db = DB;
            this.emp = EmpDB;
            instanceId = new InstanceVM().InstanceID;
        }

        // GET: Reports
        public ActionResult Index()
        {
            
            ViewBag.EmployeeID = new SelectList(emp.Employee(instanceId), "EmployeeID", "PIN");
            ReportViewModelH rv = new ReportViewModelH();
            rv.FromDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);  
            rv.TillDate = DateTime.Now.Date;
            //ViewBag.AccountSubHeadID = new SelectList(db.AccountSubHead.Where(a => a.InstanceID == rv.InstanceID), "AccountSubHeadID", "AccountSubHeadName");
            //ViewBag.BankID = new SelectList(db.Bank.Where(a => a.InstanceID == rv.InstanceID), "BankID", "BankName");
            //ViewBag.PartyID = new SelectList(db.Party.Where(a => a.InstanceID == rv.InstanceID), "PartyID", "PartyName");
            return View(rv);
        }

        [HttpPost, ActionName("Export")]
        public ActionResult Export([Bind(Include = "fromDate,TillDate,EmployeeID")] ReportViewModelH reportViewModel, string BtnAll)
        {
            


            if (ModelState.IsValid)
            {

                if (BtnAll == "Time Card")
                {
                    return RedirectToAction("TimeCard", "Allreports", new { FromDate = reportViewModel.FromDate, TillDate = reportViewModel.TillDate, employeeid = reportViewModel.EmployeeID });
                }

                if (BtnAll == "Time Card All")
                {
                    return RedirectToAction("TimeCardAll", "Allreports", new { FromDate = reportViewModel.FromDate, TillDate = reportViewModel.TillDate });
                }

                if (BtnAll == "Employee List")
                {
                    return RedirectToAction("EmployeeList", "Allreports", new { InstanceID = instanceId });
                }

                if (BtnAll == "New Join")
                {
                    return RedirectToAction("NewJoin", "Allreports", new { FromDate = reportViewModel.FromDate, TillDate = reportViewModel.TillDate, InstanceID = instanceId });
                }

                if (BtnAll == "Resign List")
                {
                    return RedirectToAction("ResignList", "Allreports", new { FromDate = reportViewModel.FromDate, TillDate = reportViewModel.TillDate, InstanceID = instanceId });
                }

                if (BtnAll == "Daily Attendance")
                {
                    return RedirectToAction("DailyAttAllstatus", "Allreports", new { FromDate = reportViewModel.FromDate, TillDate = reportViewModel.TillDate, InstanceID = instanceId });
                }

                if (BtnAll == "Attendance Summary")
                {
                    return RedirectToAction("AttendanceSummary", "Allreports", new { FromDate = reportViewModel.FromDate, TillDate = reportViewModel.TillDate, InstanceID = instanceId });
                }

                if (BtnAll == "Attendance Register")
                {
                    return RedirectToAction("AttendanceRegister", "Allreports", new { FromDate = reportViewModel.FromDate, TillDate = reportViewModel.TillDate, InstanceID = instanceId });
                }

                if (BtnAll == "Salary Sheet")
                {
                    return RedirectToAction("SalarySheet", "Allreports", new { FromDate = reportViewModel.FromDate, TillDate = reportViewModel.TillDate, InstanceID = instanceId });
                }

                if (BtnAll == "Payslip")
                {
                    return RedirectToAction("Payslip", "Allreports", new { FromDate = reportViewModel.FromDate, TillDate = reportViewModel.TillDate, InstanceID = instanceId, employeeid = reportViewModel.EmployeeID });
                }

                if (BtnAll == "Payslip All")
                {
                    return RedirectToAction("PayslipAll", "Allreports", new { FromDate = reportViewModel.FromDate, TillDate = reportViewModel.TillDate, InstanceID = instanceId });
                }


                if (BtnAll == "SDL + TAX Paye Sheet")
                {
                    return RedirectToAction("SDLTAXPaye", "Allreports", new { FromDate = reportViewModel.FromDate, TillDate = reportViewModel.TillDate, InstanceID = instanceId });
                }

                if (BtnAll == "PPF Sheet")
                {
                    return RedirectToAction("PPFSheet", "Allreports", new { FromDate = reportViewModel.FromDate, TillDate = reportViewModel.TillDate, InstanceID = instanceId });
                }

                if (BtnAll == "NSSF Sheet")
                {
                    return RedirectToAction("NSSFSheet", "Allreports", new { FromDate = reportViewModel.FromDate, TillDate = reportViewModel.TillDate, InstanceID = instanceId });
                }

                if (BtnAll == "Bank Sheet")
                {
                    return RedirectToAction("BankSheet", "Allreports", new { FromDate = reportViewModel.FromDate, TillDate = reportViewModel.TillDate, InstanceID = instanceId });
                }

                if (BtnAll == "WCF Sheet")
                {
                    return RedirectToAction("WCFSheet", "Allreports", new { FromDate = reportViewModel.FromDate, TillDate = reportViewModel.TillDate, InstanceID = instanceId });
                }

                if (BtnAll == "NHIF Sheet")
                {
                    return RedirectToAction("NHIFSheet", "Allreports", new { FromDate = reportViewModel.FromDate, TillDate = reportViewModel.TillDate, InstanceID = instanceId });
                }

                if (BtnAll == "HESLB Sheet")
                {
                    return RedirectToAction("HESLBSheet", "Allreports", new { FromDate = reportViewModel.FromDate, TillDate = reportViewModel.TillDate, InstanceID = instanceId });
                }
            }

            return RedirectToAction("Index");
        }


        public ActionResult EmployeeList(string InstanceID)
        {
            ReportDocument rptH = new ReportDocument();
            rptH.Load(Path.Combine(Server.MapPath("~/Reports"), "rptHREmployeeList.rpt"));

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

        public ActionResult NewJoin(DateTime FromDate, DateTime TillDate, string InstanceID)
        {
            ReportDocument rptH = new ReportDocument();
            rptH.Load(Path.Combine(Server.MapPath("~/Reports"), "rptHREmployeeNewJoin.rpt"));

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

        public ActionResult ResignList(DateTime FromDate, DateTime TillDate, string InstanceID)
        {
            ReportDocument rptH = new ReportDocument();
            rptH.Load(Path.Combine(Server.MapPath("~/Reports"), "rptHREmployeeResignList.rpt"));

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

        public ActionResult DailyAttAllstatus(DateTime FromDate, DateTime TillDate, string InstanceID)
        {
            ReportDocument rptH = new ReportDocument();
            rptH.Load(Path.Combine(Server.MapPath("~/Reports"), "rptHRDailyAttAllstatus.rpt"));

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

        public ActionResult AttendanceSummary(DateTime FromDate, DateTime TillDate, string InstanceID)
        {
            ReportDocument rptH = new ReportDocument();
            rptH.Load(Path.Combine(Server.MapPath("~/Reports"), "rptHRAttSummary.rpt"));

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

        public ActionResult AttendanceRegister(DateTime FromDate, DateTime TillDate, string InstanceID)
        {
            ReportDocument rptH = new ReportDocument();
            rptH.Load(Path.Combine(Server.MapPath("~/Reports"), "rptHRAttRegister.rpt"));


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

        public ActionResult SalarySheet(DateTime FromDate, DateTime TillDate, string InstanceID)
        {
            ReportDocument rptH = new ReportDocument();
            rptH.Load(Path.Combine(Server.MapPath("~/Reports"), "rptHRSalarySheet.rpt"));


            foreach (Table table in rptH.Database.Tables)
            {
                table.LogOnInfo.ConnectionInfo = connectionInfo;
                table.ApplyLogOnInfo(table.LogOnInfo);
            }

            rptH.SetParameterValue("SalaryYear", TillDate.Year);
            rptH.SetParameterValue("SalaryMonth", TillDate.Month);
            rptH.SetParameterValue("InstanceID", InstanceID);

            Stream stream = rptH.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
            rptH.Close();
            rptH.Dispose();
            return File(stream, "application/pdf");

        }

        public ActionResult PayslipALL(DateTime FromDate, DateTime TillDate, string InstanceID)
        {
            ReportDocument rptH = new ReportDocument();
            rptH.Load(Path.Combine(Server.MapPath("~/Reports"), "rptHRSalarySlipAll.rpt"));


            foreach (Table table in rptH.Database.Tables)
            {
                table.LogOnInfo.ConnectionInfo = connectionInfo;
                table.ApplyLogOnInfo(table.LogOnInfo);
            }

            rptH.SetParameterValue("SalaryYear", TillDate.Year);
            rptH.SetParameterValue("SalaryMonth", TillDate.Month);
            rptH.SetParameterValue("InstanceID", InstanceID);

            Stream stream = rptH.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
            rptH.Close();
            rptH.Dispose();
            return File(stream, "application/pdf");

        }

        public ActionResult Payslip(DateTime FromDate, DateTime TillDate, string InstanceID, long EmployeeID)
        {
            ReportDocument rptH = new ReportDocument();
            rptH.Load(Path.Combine(Server.MapPath("~/Reports"), "rptHRSalarySlip.rpt"));


            foreach (Table table in rptH.Database.Tables)
            {
                table.LogOnInfo.ConnectionInfo = connectionInfo;
                table.ApplyLogOnInfo(table.LogOnInfo);
            }

            rptH.SetParameterValue("SalaryYear", TillDate.Year);
            rptH.SetParameterValue("SalaryMonth", TillDate.Month);
            rptH.SetParameterValue("InstanceID", InstanceID);
            rptH.SetParameterValue("EmployeeID", EmployeeID);

            Stream stream = rptH.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
            rptH.Close();
            rptH.Dispose();
            return File(stream, "application/pdf");

        }

        public ActionResult SDLTAXPaye(DateTime FromDate, DateTime TillDate, string InstanceID)
        {
            ReportDocument rptH = new ReportDocument();
            rptH.Load(Path.Combine(Server.MapPath("~/Reports"), "rptHRSalarySDLTAXPaye.rpt"));


            foreach (Table table in rptH.Database.Tables)
            {
                table.LogOnInfo.ConnectionInfo = connectionInfo;
                table.ApplyLogOnInfo(table.LogOnInfo);
            }

            rptH.SetParameterValue("SalaryYear", TillDate.Year);
            rptH.SetParameterValue("SalaryMonth", TillDate.Month);
            rptH.SetParameterValue("InstanceID", InstanceID);

            Stream stream = rptH.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
            rptH.Close();
            rptH.Dispose();
            return File(stream, "application/pdf");

        }

        public ActionResult PPFSheet(DateTime FromDate, DateTime TillDate, string InstanceID)
        {
            ReportDocument rptH = new ReportDocument();
            rptH.Load(Path.Combine(Server.MapPath("~/Reports"), "rptHRSalaryPPF.rpt"));


            foreach (Table table in rptH.Database.Tables)
            {
                table.LogOnInfo.ConnectionInfo = connectionInfo;
                table.ApplyLogOnInfo(table.LogOnInfo);
            }

            rptH.SetParameterValue("SalaryYear", TillDate.Year);
            rptH.SetParameterValue("SalaryMonth", TillDate.Month);
            rptH.SetParameterValue("InstanceID", InstanceID);

            Stream stream = rptH.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
            rptH.Close();
            rptH.Dispose();
            return File(stream, "application/pdf");

        }

        public ActionResult NSSFSheet(DateTime FromDate, DateTime TillDate, string InstanceID)
        {
            ReportDocument rptH = new ReportDocument();
            rptH.Load(Path.Combine(Server.MapPath("~/Reports"), "rptHRSalaryNSSF.rpt"));


            foreach (Table table in rptH.Database.Tables)
            {
                table.LogOnInfo.ConnectionInfo = connectionInfo;
                table.ApplyLogOnInfo(table.LogOnInfo);
            }

            rptH.SetParameterValue("SalaryYear", TillDate.Year);
            rptH.SetParameterValue("SalaryMonth", TillDate.Month);
            rptH.SetParameterValue("InstanceID", InstanceID);

            Stream stream = rptH.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
            rptH.Close();
            rptH.Dispose();
            return File(stream, "application/pdf");

        }

        public ActionResult BankSheet(DateTime FromDate, DateTime TillDate, string InstanceID)
        {
            ReportDocument rptH = new ReportDocument();
            rptH.Load(Path.Combine(Server.MapPath("~/Reports"), "rptHRSalaryBank.rpt"));


            foreach (Table table in rptH.Database.Tables)
            {
                table.LogOnInfo.ConnectionInfo = connectionInfo;
                table.ApplyLogOnInfo(table.LogOnInfo);
            }

            rptH.SetParameterValue("SalaryYear", TillDate.Year);
            rptH.SetParameterValue("SalaryMonth", TillDate.Month);
            rptH.SetParameterValue("InstanceID", InstanceID);

            Stream stream = rptH.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
            rptH.Close();
            rptH.Dispose();
            return File(stream, "application/pdf");

        }

        public ActionResult WCFSheet(DateTime FromDate, DateTime TillDate, string InstanceID)
        {
            ReportDocument rptH = new ReportDocument();
            rptH.Load(Path.Combine(Server.MapPath("~/Reports"), "rptHRSalaryWCF.rpt"));


            foreach (Table table in rptH.Database.Tables)
            {
                table.LogOnInfo.ConnectionInfo = connectionInfo;
                table.ApplyLogOnInfo(table.LogOnInfo);
            }

            rptH.SetParameterValue("SalaryYear", TillDate.Year);
            rptH.SetParameterValue("SalaryMonth", TillDate.Month);
            rptH.SetParameterValue("InstanceID", InstanceID);

            Stream stream = rptH.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
            rptH.Close();
            rptH.Dispose();
            return File(stream, "application/pdf");

        }

        public ActionResult NHIFSheet(DateTime FromDate, DateTime TillDate, string InstanceID)
        {
            ReportDocument rptH = new ReportDocument();
            rptH.Load(Path.Combine(Server.MapPath("~/Reports"), "rptHRSalaryNHIF.rpt"));


            foreach (Table table in rptH.Database.Tables)
            {
                table.LogOnInfo.ConnectionInfo = connectionInfo;
                table.ApplyLogOnInfo(table.LogOnInfo);
            }

            rptH.SetParameterValue("SalaryYear", TillDate.Year);
            rptH.SetParameterValue("SalaryMonth", TillDate.Month);
            rptH.SetParameterValue("InstanceID", InstanceID);

            Stream stream = rptH.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
            rptH.Close();
            rptH.Dispose();
            return File(stream, "application/pdf");

        }

        public ActionResult HESLBSheet(DateTime FromDate, DateTime TillDate, string InstanceID)
        {
            ReportDocument rptH = new ReportDocument();
            rptH.Load(Path.Combine(Server.MapPath("~/Reports"), "rptHRSalaryHESLB.rpt"));


            foreach (Table table in rptH.Database.Tables)
            {
                table.LogOnInfo.ConnectionInfo = connectionInfo;
                table.ApplyLogOnInfo(table.LogOnInfo);
            }

            rptH.SetParameterValue("SalaryYear", TillDate.Year);
            rptH.SetParameterValue("SalaryMonth", TillDate.Month);
            rptH.SetParameterValue("InstanceID", InstanceID);

            Stream stream = rptH.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
            rptH.Close();
            rptH.Dispose();
            return File(stream, "application/pdf");

        }

        public ActionResult TimeCard(DateTime FromDate, DateTime TillDate, long EmployeeID)
        {
            ReportDocument rptH = new ReportDocument();
            rptH.Load(Path.Combine(Server.MapPath("~/Reports"), "rptHRTimeCard.rpt"));

            foreach (Table table in rptH.Database.Tables)
            {
                table.LogOnInfo.ConnectionInfo = connectionInfo;
                table.ApplyLogOnInfo(table.LogOnInfo);
            }

            
            rptH.SetParameterValue("FromDate", FromDate);
            rptH.SetParameterValue("tillDate", TillDate);
            rptH.SetParameterValue("EmployeeID", EmployeeID);
            rptH.SetParameterValue("instanceId", instanceId);

            Stream stream = rptH.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
            rptH.Close();
            rptH.Dispose();
            return File(stream, "application/pdf");

        }

        public ActionResult TimeCardAll(DateTime FromDate, DateTime TillDate)
        {
            ReportDocument rptH = new ReportDocument();
            rptH.Load(Path.Combine(Server.MapPath("~/Reports"), "rptHRTimeCardAll.rpt"));

            foreach (Table table in rptH.Database.Tables)
            {
                table.LogOnInfo.ConnectionInfo = connectionInfo;
                table.ApplyLogOnInfo(table.LogOnInfo);
            }

            
            rptH.SetParameterValue("FromDate", FromDate);
            rptH.SetParameterValue("tillDate", TillDate);
            rptH.SetParameterValue("instanceId", instanceId);

            Stream stream = rptH.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
            rptH.Close();
            rptH.Dispose();
            return File(stream, "application/pdf");

        }



        

    }
}