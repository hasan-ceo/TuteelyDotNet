using System;
using System.Collections.Generic;
using System.Configuration;
using System.Web.Mvc;
using Ninject;
using Nyika.Domain.Abstract.Setup;
using Nyika.Domain.Concrete.Setup;
using Nyika.Domain.Entities.Setup;
using Nyika.Domain.Abstract.Accounts;
using Nyika.Domain.Concrete.Accounts;
using Nyika.Domain.Abstract.HR;
using Nyika.Domain.Concrete.HR;
using Nyika.Domain.Entities.HR;
using Nyika.Domain.Abstract.Invoices;
using Nyika.Domain.Concrete.Invoices;
using Nyika.Domain.Abstract.MF;
using Nyika.Domain.Concrete.MF;
using Nyika.Domain.Abstract.AVL;
using Nyika.Domain.Concrete.AVL;
using Nyika.Domain.Concrete.Stock;
using Nyika.Domain.Abstract.Stock;

namespace Nyika.WebUI.Infrastructure
{

    public class NinjectDependencyResolver : IDependencyResolver
    {
        private IKernel kernel;

        public NinjectDependencyResolver(IKernel kernelParam)
        {
            kernel = kernelParam;
            AddBindings();
        }

        public object GetService(Type serviceType)
        {
            return kernel.TryGet(serviceType);
        }

        public IEnumerable<object> GetServices(Type serviceType)
        {
            return kernel.GetAll(serviceType);
        }

        private void AddBindings()
        {
            kernel.Bind<IPageRepo>().To<EFPageRepo>();
            kernel.Bind<IFeedbackRepo>().To<EFFeedbackRepo>();
            kernel.Bind<IPaypalIPNRepo>().To<EFPaypalIPNRepo>();
            //kernel.Bind<ISubscriptionRepo>().To<EFSubscriptionRepo>();

            kernel.Bind<IErrorLogRepo>().To<EFErrorLogRepo>();
            kernel.Bind<IAllDedRepo>().To<EFAllDedRepo>();
            kernel.Bind<ICompanyRepo>().To<EFCompanyRepo>();
            kernel.Bind<IDepartmentRepo>().To<EFDepartmentRepo>();
            kernel.Bind<IDesignationRepo>().To<EFDesignationRepo>();
            kernel.Bind<IEducationRepo>().To<EFEducationRepo>();
            kernel.Bind<IHolidayRepo>().To<EFHolidayRepo>();
            kernel.Bind<ILeaveRepo>().To<EFLeaveRepo>();
            kernel.Bind<IResignReasonRepo>().To<EFResignReasonRepo>();
            kernel.Bind<ISectionRepo>().To<EFSectionRepo>();
            kernel.Bind<IShiftRepo>().To<EFShiftRepo>();
            kernel.Bind<IEmploymentTypeRepo>().To<EFEmploymentTypeRepo>();
            kernel.Bind<IShopPageRepo>().To<EFShopPageRepo>();

            kernel.Bind<IEmployeeRepo>().To<EFEmployeeRepo>();
            kernel.Bind<IEmployeeLeaveRepo>().To<EFEmployeeLeaveRepo>();
            kernel.Bind<IEmployeeResignRepo>().To<EFEmployeeResignRepo>();
            kernel.Bind<IEmployeeShowcauseRepo>().To<EFEmployeeShowcauseRepo>();
            kernel.Bind<IEmployeeAttendanceRepo>().To<EFEmployeeAttendanceRepo>();
            kernel.Bind<IEmployeeShiftRepo>().To<EFEmployeeShiftRepo>();
            kernel.Bind<IAttenStatusRepo>().To<EFAttenStatusRepo>();
            kernel.Bind<IEmployeeTourRepo>().To<EFEmployeeTourRepo>();
            kernel.Bind<IEmployeeAllDedRepo>().To<EFEmployeeAllDedRepo>();
            kernel.Bind<IEmployeeIncrementRepo>().To<EFEmployeeIncrementRepo>();
            kernel.Bind<IEmployeeTransferRepo>().To<EFEmployeeTransferRepo>();

            kernel.Bind<IBankRepo>().To<EFBankRepo>();
            kernel.Bind<IPartyRepo>().To<EFPartyRepo>();
            kernel.Bind<IAccountTypeRepo>().To<EFAccountTypeRepo>();
            kernel.Bind<IAccountMainHeadRepo>().To<EFAccountMainHeadRepo>();
            kernel.Bind<IAccountSubHeadRepo>().To<EFAccountSubHeadRepo>();
            kernel.Bind<ICashSummaryRepo>().To<EFCashSummaryRepo>();
            kernel.Bind<IBusinessDayRepo>().To<EFBusinessDayRepo>();
            kernel.Bind<ICashRequisitionRepo>().To<EFCashRequisitionRepo>();
            kernel.Bind<IAccountGLRepo>().To<EFAccountGLRepo>();

            kernel.Bind<IInvoiceRepo>().To<EFInvoiceRepo>();
            kernel.Bind<IInvoiceDetailsRepo>().To<EFInvoiceDetailsRepo>();

            kernel.Bind<IGroupsRepo>().To<EFGroupsRepo>();
            kernel.Bind<ILoanRepo>().To<EFLoanRepo>();
            kernel.Bind<ILoanCollectionRepo>().To<EFLoanCollectionRepo>();
            kernel.Bind<ILoanCycleRepo>().To<EFLoanCycleRepo>();
            kernel.Bind<ILoanScheduleRepo>().To<EFLoanScheduleRepo>();
            kernel.Bind<IMemberRepo>().To<EFMemberRepo>();
            kernel.Bind<IProductRepo>().To<EFProductRepo>();
            kernel.Bind<IProjectRepo>().To<EFProjectRepo>();
            kernel.Bind<ISchemeRepo>().To<EFSchemeRepo>();

            kernel.Bind<IBrandRepo>().To<EFBrandRepo>();
            kernel.Bind<ICategoryRepo>().To<EFCategoryRepo>();
            kernel.Bind<ICategorySubRepo>().To<EFCategorySubRepo>();
            kernel.Bind<IItemRepo>().To<EFItemRepo>();

            //EmailSettings emailSettings = new EmailSettings
            //{
            //    WriteAsFile = bool.Parse(ConfigurationManager
            //        .AppSettings["Email.WriteAsFile"] ?? "false")
            //};

            //kernel.Bind<IOrderProcessor>().To<EmailOrderProcessor>()
            //    .WithConstructorArgument("settings", emailSettings);

            //kernel.Bind<IAuthProvider>().To<FormsAuthProvider>();
        }
    }
}
