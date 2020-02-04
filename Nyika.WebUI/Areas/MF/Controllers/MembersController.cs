using Nyika.Domain.Abstract.Accounts;
using Nyika.Domain.Abstract.MF;
using Nyika.Domain.Entities.MF;
using Nyika.WebUI.Areas.MF.Models;
using Nyika.WebUI.Models;
using System;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Nyika.WebUI.Areas.MF.Controllers
{
    [Authorize(Roles = "Accountant,Super Admin")]
    public class MembersController : Controller
    {
        private IMemberRepo db;
        private IGroupsRepo gdb;
        private IProjectRepo pdb;
        private IBusinessDayRepo bddb;
        private string instanceId;

        public MembersController(IMemberRepo DB, IGroupsRepo gDB, IProjectRepo PDB, IBusinessDayRepo bdDB)
        {
            this.db = DB;
            this.gdb = gDB;
            this.pdb = PDB;
            this.bddb = bdDB;
            instanceId = new InstanceVM().InstanceID;
        }


        // GET: BasicSetup/Member
        public ActionResult Index()
        {
            return View(db.Member("x").ToList());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index(string txtSearch = "")
        {
            if (txtSearch == "")
            {
                txtSearch = "?";
            }

            var Member = db.Search(instanceId, txtSearch,false);
            return View("Index", Member.ToList());
        }

        

        // GET: BasicSetup/Member/Create
        public ActionResult Create()
        {
            MemberVM memvm = new MemberVM();
            memvm.Project = new SelectList(pdb.Project(instanceId), "ProjectID", "ProjectName");
            //ViewBag.ProjectID = new SelectList(pdb.Project(instanceId), "ProjectID", "ProjectName");
            //ViewBag.GroupsID = new SelectList(gdb.Groups(instanceId), "GroupsID", "GroupsName");
            return View(memvm);
        }



        public ActionResult GetMember(long GroupsID)
        {
            var Member = new SelectList(db.Member(instanceId).Where(m => m.GroupsID == GroupsID && m.Inactive == false).Select(m => new { memberID = m.MemberID, memberName = m.MemberNo + "-" + m.MemberName }), "memberID", "memberName");
            return Json(Member.ToList(), JsonRequestBehavior.AllowGet);

        }

        public ActionResult GetMemberAll(long GroupsID)
        {
            var Member = new SelectList(db.Member(instanceId).Where(m => m.GroupsID == GroupsID).Select(m => new { memberID = m.MemberID,memberName = m.MemberNo + "-" + m.MemberName }), "memberID", "memberName");
            return Json(Member.ToList(), JsonRequestBehavior.AllowGet);

        }

        public ActionResult GetMemberScurity(long GroupsID)
        {
            var mem = db.Member(instanceId).Where(m => m.GroupsID == GroupsID && m.Inactive == false && m.SecurityAmountTotal != 0).Select(s => new
            {
                MemberID = s.MemberID,
                MemberName = s.MemberNo +" - "+s.MemberName + " - " + s.SecurityAmountTotal.ToString()
            });
            var Member = new SelectList(mem, "MemberID", "MemberName");
            return Json(Member.ToList(), JsonRequestBehavior.AllowGet);

        }

        // POST: BasicSetup/Member/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "SelectedProject,SelectedGroup,member")] MemberVM Membervm, HttpPostedFileBase ImageUpload)
        {
            var validImageTypes = new string[]
            {
                 //"image/gif",
                //"image/pjpeg",
                //"image/png",
                "image/jpeg"
            };

            if (ModelState.IsValid)
            {

                if ((DateTime.Now.Date.Year - Membervm.member.DoB.Year) >= 18)
                {
                    Membervm.member.ImageUrl = "~/images/employee.jpg";
                    if (ImageUpload != null && ImageUpload.ContentLength > 0)
                    {
                        if (validImageTypes.Contains(ImageUpload.ContentType))
                        {
                            var ImageName = Guid.NewGuid().ToString() + ".jpg";
                            var uploadDir = "~/Images/MF";
                            var imagePath = Path.Combine(Server.MapPath(uploadDir), ImageName);
                            ImageUpload.SaveAs(imagePath);
                            var imageUrl = Path.Combine(uploadDir, ImageName);
                            Membervm.member.ImageUrl = imageUrl.Replace("\\", "/");
                        }
                    }

                    Membervm.member.MemberNo = db.NewMemberNo(Membervm.SelectedGroup);
                    Membervm.member.MembershipDate = bddb.WorkDate(instanceId);
                    Membervm.member.MembershipExpireDate = Membervm.member.MembershipDate.AddYears(1);
                    Membervm.member.WorkDate = bddb.WorkDate(instanceId);
                    Membervm.member.EntryBy = User.Identity.Name;
                    Membervm.member.InstanceID = instanceId;
                    Membervm.member.GroupsID = Membervm.SelectedGroup;

                    db.SaveMember(Membervm.member);
                    return View("Index", db.Search(instanceId, Membervm.member.MemberID.ToString(),false).ToList());
                    //return RedirectToAction("Index");
                }
                else
                {
                    ModelState.AddModelError("DoB", "Check Date of Birth");
                }


            }
            Membervm.Project = new SelectList(pdb.Project(instanceId), "ProjectID", "ProjectName");
            //ViewBag.GroupsID = new SelectList(gdb.Groups(instanceId), "GroupsID", "GroupsName", Membervm.member.GroupsID);
            return View(Membervm);
        }

        // GET: BasicSetup/Member/Edit/5
        public ActionResult Edit(long id=0)
        {

            Member Member = db.Single(instanceId, id);
            if (Member == null)
            {
                return RedirectToAction("Index");
            }
            ViewBag.GroupsID = new SelectList(gdb.Groups(instanceId), "GroupsID", "GroupsName", Member.GroupsID);
            return View(Member);
        }

        // POST: BasicSetup/Member/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MemberID,MemberName,NationalID,DoB,FatherName,MotherName,Gender,MaritalStatus,PassportNo,DrivingLicenceNo,Occupation,NomineeName,NomineeRelationship,GurdianName,GurdianNationalID,GurdianRelationship,ContactNumber,PresentAddress,PermanentAddress,ImageUrl")] Member Member, HttpPostedFileBase ImageUpload)
        {
            if (ModelState.IsValid)
            {
                var validImageTypes = new string[]
                {
                 //"image/gif",
                //"image/pjpeg",
                //"image/png",
                "image/jpeg"
                };

                if ((DateTime.Now.Date.Year - Member.DoB.Year) >= 18)
                {
                    if (ImageUpload != null && ImageUpload.ContentLength > 0)
                    {
                        if (validImageTypes.Contains(ImageUpload.ContentType))
                        {
                            var uploadDir = "~/Images/MF";
                            string filename = Path.GetFileName(Member.ImageUrl);
                            if (filename == "employee.jpg")
                            {
                                filename = Guid.NewGuid().ToString() + ".jpg";
                            }
                            var imagePath = Path.Combine(Server.MapPath(uploadDir), filename);
                            ImageUpload.SaveAs(imagePath);
                            var imageUrl = Path.Combine(uploadDir, filename);
                            Member.ImageUrl = imageUrl.Replace("\\", "/");
                        }
                    }

                    Member.EntryBy = User.Identity.Name;
                    db.SaveMember(Member);
                    return View("Index", db.Search(instanceId, Member.MemberID.ToString(), false).ToList());
                }
                else
                {
                    ModelState.AddModelError("DoB", "Check Date of Birth");
                }
            }
            ViewBag.GroupsID = new SelectList(gdb.Groups(instanceId), "GroupsID", "GroupsName", Member.GroupsID);
            return View(Member);
        }

        // GET: BasicSetup/Member/Delete/5
        public ActionResult Delete(long id=0)
        {
            Member Member = db.Single(instanceId, id);
            if (Member == null)
            {
                return RedirectToAction("Index");
            }

            if (db.IsExists(Member.MemberID) != 0)
            {
                TempData["Delete"] = "In use, can not delete at this moment";
            }
            else
            {
                TempData["Delete"] = "";
            }

            return View(Member);
        }

        // POST: BasicSetup/Member/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            Member Member = db.Single(instanceId, id);
            if (Member != null)
            {
                db.DeleteMember(Member.MemberID);
            }
            return RedirectToAction("Index");
        }

    }
}
