using Nyika.Domain.Abstract.Accounts;
using Nyika.Domain.Abstract.MF;
using Nyika.Domain.Entities.Accounts;
using Nyika.Domain.Entities.MF;
using Nyika.WebUI.Areas.MF.Models;
using Nyika.WebUI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Nyika.WebUI.Areas.MF.Controllers
{
    [Authorize(Roles = "Accountant,Super Admin")]
    public class LoanProductChangeController : Controller
    {
        private IAccountGLRepo db;
        private string instanceId;
        private IProjectRepo pdb;
        private IProductRepo productdb;

        public LoanProductChangeController(IAccountGLRepo DB, IProjectRepo PDB, IProductRepo PRODUCTDB)
        {
            this.db = DB;
            this.pdb = PDB;
            this.productdb=PRODUCTDB;
            instanceId = new InstanceVM().InstanceID;
        }

        // GET: BasicSetup/Groups/Create
        public ActionResult ProductChange()
        {
            ProductChangeVM productChangeVM = new ProductChangeVM();
            productChangeVM.Project = new SelectList(pdb.Project(instanceId), "ProjectID", "ProjectName");
            productChangeVM.Product = new SelectList(productdb.Product(instanceId), "ProductID", "ProductName");
            return View(productChangeVM);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ProductChange([Bind(Include = "SelectedLoan,SelectedProduct")] ProductChangeVM productchangeVM)
        {
            if (ModelState.IsValid)
            {
                productdb.ProductChange(productchangeVM.SelectedLoan, productchangeVM.SelectedProduct);
                productchangeVM.Particulars = "Successfull";
            }
            productchangeVM.Project = new SelectList(pdb.Project(instanceId), "ProjectID", "ProjectName", productchangeVM.SelectedProject);
            productchangeVM.Product = new SelectList(productdb.Product(instanceId), "ProductID", "ProductName", productchangeVM.SelectedProduct);
            return View(productchangeVM);
        }
    }
}