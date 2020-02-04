using Nyika.Domain.Abstract.MF;
using Nyika.Domain.Entities.MF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Data;

namespace Nyika.Domain.Concrete.MF
{
    public class EFMemberRepo : IMemberRepo
    {
        private EFDbContext context = new EFDbContext();

        public IEnumerable<Member> Member(string InstanceID)
        {
             return context.Member.Include(e => e.Groups).Where(b => b.InstanceID==InstanceID); 
        }

        public Member Single(string InstanceID, long ID)
        {
            return context.Member.Include(e => e.Groups).Where(a => a.InstanceID == InstanceID && a.MemberID == ID).FirstOrDefault();
        }

        //public IEnumerable<Member> SingleToList(string InstanceID, long ID)
        //{
        //    return context.Member.Where(e => (e.InstanceID == InstanceID & e.Inactive == false && e.MemberID==ID));
        //}

        public IEnumerable<Member> Search(string InstanceID, string txtSearch, bool loan)
        {
            if (loan == false)
            {
                return context.Member.Include(e => e.Groups).Where(e => (e.MemberID.ToString().Contains(txtSearch) || e.MemberNo.ToString().Contains(txtSearch) || e.MemberName.Contains(txtSearch) || e.Groups.GroupsName.Contains(txtSearch)) && e.InstanceID == InstanceID).OrderBy(e => e.Groups.GroupsName).OrderBy(e => e.MemberNo);
            }
            else
            {
                return context.Member.Include(e => e.Groups).Where(e => ((e.MemberID.ToString().Contains(txtSearch) ||  e.MemberNo.ToString().Contains(txtSearch) || e.MemberName.Contains(txtSearch) || e.Groups.GroupsName.Contains(txtSearch)) && e.InstanceID == InstanceID & e.Inactive==false)).OrderBy(e => e.Groups.GroupsName).OrderBy(e => e.MemberNo);
            }
        }

        public void SaveMember(Member member)
        {

            if (member.MemberID == 0)
            {
                member.InactiveDate = new DateTime(1900, 1, 1);
                context.Member.Add(member);
                context.SaveChanges();

                context.Database.ExecuteSqlCommand("exec pMFMembershipFee  @MemberID={0}, @InstanceID={1}, @EntryBy={2}, @renew={3}", member.MemberID, member.InstanceID, member.EntryBy, 0);
            }
            else
            {
                Member dbEntry = context.Member.Find(member.MemberID);
                if (dbEntry != null)
                {

                    //dbEntry.GroupsID = Member.GroupsID;
                    //dbEntry.MemberNo = member.MemberNo;
                    dbEntry.MemberName = member.MemberName;
                    //dbEntry.ApplicationDate = Member.ApplicationDate;
                    //dbEntry.MemberStatus = Member.MemberStatus;
                    //dbEntry.Salutation = Member.Salutation;
                    dbEntry.NationalID = member.NationalID;
                    dbEntry.PassportNo = member.PassportNo;
                    dbEntry.DrivingLicenceNo = member.DrivingLicenceNo;
                    dbEntry.Gender = member.Gender;
                    dbEntry.MaritalStatus = member.MaritalStatus;
                    dbEntry.Occupation = member.Occupation;
                    dbEntry.DoB = member.DoB;
                    dbEntry.FatherName = member.FatherName;
                    dbEntry.MotherName = member.MotherName;
                    dbEntry.PresentAddress = member.PresentAddress;
                    dbEntry.PermanentAddress = member.PermanentAddress;
                    dbEntry.ContactNumber = member.ContactNumber;
                    dbEntry.NomineeName = member.NomineeName;
                    dbEntry.NomineeRelationship = member.NomineeRelationship;
                    dbEntry.GurdianName = member.GurdianName;

                    dbEntry.GurdianNationalID = member.GurdianNationalID;
                    dbEntry.GurdianRelationship = member.GurdianRelationship;
                    dbEntry.ImageUrl = member.ImageUrl;
                    //dbEntry.MembershipExpireDate = Member.MembershipExpireDate;
                    dbEntry.SecurityAmountTotal = member.SecurityAmountTotal;

               
                }
                context.SaveChanges();
            }
            
        }

        public Member DeleteMember(long MemberID)
        {
            Member dbEntry = context.Member.Find(MemberID);
            var count = context.Loan.Where(e => e.MemberID == MemberID).Count();
            if (dbEntry != null && count == 0)
            {
                context.Member.Remove(dbEntry);
                context.SaveChanges();

                context.Database.ExecuteSqlCommand("exec pMFMembershipFeeDelete  @MemberID={0}", MemberID);

            }
            return dbEntry;
        }

        public int IsExists(long MemberID)
        {
            return context.Loan.Where(e => e.MemberID == MemberID).Count();
        }

        public long NewMemberNo(long GroupsID)
        {
            var c = context.Member.Where(e => e.GroupsID == GroupsID).Count();
            if (c == 0)
            {
                return 101;
            }
            else
            {
                return (context.Member.Where(e => e.GroupsID == GroupsID).Max(e => e.MemberNo)) + 1;

            }
        }
    }
}
