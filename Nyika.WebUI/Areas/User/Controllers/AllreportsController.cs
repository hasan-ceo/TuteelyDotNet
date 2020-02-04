using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using Nyika.Domain.Concrete;
using Nyika.WebUI.Areas.User.Models;
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

namespace Nyika.WebUI.Areas.User.Controllers
{
    [Authorize(Roles = "User,Super Admin,Manager")]
    public class AllreportsController : Controller
    {

        private EFDbContext db = new EFDbContext();
        ConnectionInfo connectionInfo = new ConnectionInfo();
        private string instanceId;

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
            
            ReportViewModelU rv = new ReportViewModelU();
            rv.FromDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
            rv.TillDate = DateTime.Now.Date;
            return View(rv);
        }

        [HttpPost, ActionName("Export")]
        public ActionResult Export([Bind(Include = "fromDate,TillDate")] ReportViewModelU reportViewModel, string BtnAll)
        {
            
            var EmployeeEmail = User.Identity.Name;
            var Employee = db.Employee.Where(e => e.Email== EmployeeEmail).FirstOrDefault();
            if (Employee != null)
            {
                if (ModelState.IsValid)
                {

                    if (BtnAll == "Payslip")
                    {
                        return RedirectToAction("Payslip", "Allreports", new { FromDate = reportViewModel.FromDate, TillDate = reportViewModel.TillDate, InstanceID = instanceId, employeeID = Employee.EmployeeID });
                    }

                    if (BtnAll == "Time Card")
                    {
                        return RedirectToAction("TimeCard", "Allreports", new { FromDate = reportViewModel.FromDate, TillDate = reportViewModel.TillDate, InstanceID = instanceId, employeeID = Employee.EmployeeID });
                    }
                }
            }

            return RedirectToAction("Index");
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
            rptH.SetParameterValue("EmployeeID", EmployeeID);
            rptH.SetParameterValue("InstanceID", InstanceID);

            Stream stream = rptH.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
            rptH.Close();
            rptH.Dispose();
            return File(stream, "application/pdf");

        }

        public ActionResult TimeCard(DateTime FromDate, DateTime TillDate, string InstanceID,long EmployeeID)
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
            rptH.SetParameterValue("instanceId", InstanceID);

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