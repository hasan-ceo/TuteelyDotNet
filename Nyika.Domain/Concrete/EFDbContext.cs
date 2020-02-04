using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Data.Entity;
using Nyika.Domain.Entities.Setup;
using Nyika.Domain.Entities.Accounts;
using Nyika.Domain.Entities.HR;
using Nyika.Domain.Entities.Invoices;
using Nyika.Domain.Entities.MF;
using Nyika.Domain.Entities.AVL;
using Nyika.Domain.Entities.Stock;

namespace Nyika.Domain.Concrete
{
    public class EFDbContext: DbContext
    {
        public DbSet<Page> Page { get; set; }
        public DbSet<Feedback> Feedback { get; set; }
        public DbSet<PaypalIPN> PaypalIPN { get; set; }
        //public DbSet<Subscription> Subscription { get; set; }

        public DbSet<AllDed> AllDed { get; set; }
        public DbSet<AttenStatus> AttenStatus { get; set; }
        public DbSet<Company> Company { get; set; }
        public DbSet<Department> Department { get; set; }
        public DbSet<Designation> Designation { get; set; }
        public DbSet<Holiday> Holiday { get; set; }
        public DbSet<Education> Education { get; set; }
        public DbSet<Leave> Leave { get; set; }
        public DbSet<ResignReason> ResignReason { get; set; }
        public DbSet<Section> Section { get; set; }
        public DbSet<Shift> Shift { get; set; }
        public DbSet<EmploymentType> EmploymentType { get; set; }
        public DbSet<ErrorLog> ErrorLog { get; set; }
        public DbSet<ShopPage> ShopPage { get; set; }

        public DbSet<Employee> Employee { get; set; }
        public DbSet<EmployeeLeave> EmployeeLeave { get; set; }
        public DbSet<EmployeeResign> EmployeeResign { get; set; }
        public DbSet<EmployeeAttendance> EmployeeAttendance { get; set; }
        public DbSet<EmployeeShowcause> EmployeeShowcause { get; set; }
        public DbSet<EmployeeShift> EmployeeShift { get; set; }
        public DbSet<EmployeeTour> EmployeeTour { get; set; }
        public DbSet<EmployeePayroll> EmployeePayroll { get; set; }
        public DbSet<EmployeeAllDed> EmployeeAllDed { get; set; }
        public DbSet<EmployeeIncrement> EmployeeIncrement { get; set; }
        public DbSet<EmployeeTransfer> EmployeeTransfer { get; set; }

        public DbSet<AccountType> AccountType { get; set; }
        public DbSet<AccountMainHead> AccountMainHead { get; set; }
        public DbSet<AccountSubHead> AccountSubHead { get; set; }
        public DbSet<AccountGL> AccountGL { get; set; }
        public DbSet<Bank> Bank { get; set; }
        public DbSet<BusinessDay> BusinessDay { get; set; }
        public DbSet<BusinessMonthStatus> BusinessMonthStatus { get; set; }
        public DbSet<CashSummary> CashSummary { get; set; }
        public DbSet<Party> Party { get; set; }
        public DbSet<CashRequisition> CashRequisition { get; set; }

        public DbSet<Invoice> Invoice { get; set; }
        public DbSet<InvoiceDetails> InvoiceDetails { get; set; }

        public DbSet<Groups> Groups { get; set; }
        public DbSet<Loan> Loan { get; set; }
        public DbSet<LoanCollection> LoanCollection { get; set; }
        public DbSet<LoanCycle> LoanCycle { get; set; }
        public DbSet<LoanSchedule> LoanSchedule { get; set; }
        public DbSet<Member> Member { get; set; }
        public DbSet<Product> Product { get; set; }
        public DbSet<Project> Project { get; set; }
        public DbSet<Scheme> Scheme { get; set; }

        public DbSet<Brand> Brand { get; set; }
        public DbSet<Category> Category { get; set; }
        public DbSet<CategorySub> CategorySub { get; set; }
        public DbSet<Item> Item { get; set; }

        public EFDbContext()
        {
            //var adapter = (IObjectContextAdapter)this;
            //var objectContext = adapter.ObjectContext;
            //objectContext.CommandTimeout = 1 * 60; // value in seconds
        }

        protected override void OnModelCreating(System.Data.Entity.DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            //modelBuilder.Conventions.Remove<ManyToManyCascadeDeleteConvention>();
            base.OnModelCreating(modelBuilder);
        }

    }
}
