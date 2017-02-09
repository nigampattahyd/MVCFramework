using System.Web;
using Microsoft.Practices.Unity;
using CRMHub.Interface;
using CRMHub.Repository;
using System;

namespace CRM.App_Start
{
    public class UnityConfig
    {
        #region Unity Container
        private static Lazy<IUnityContainer> container = new Lazy<IUnityContainer>(() =>
        {
            var container = new UnityContainer();
            RegisterTypes(container);
            return container;
        });

        /// <summary>
        /// Gets the configured Unity container.
        /// </summary>
        public static IUnityContainer GetConfiguredContainer()
        {
            return container.Value;
        }

        #endregion
        public static void RegisterTypes(IUnityContainer container)
        {
            container.RegisterType<IClientRepository, ClientRepository>();

            container.RegisterType<ICompanyRepository, CompanyRepository>();
            container.RegisterType<IOfficeRepository, OfficeRepository>();
            container.RegisterType<ICommonRepository, CommonRepository>();
            container.RegisterType<ISystemSetupRepository, SystemSetupRepository>();
            container.RegisterType<IOfficeLevelAccessRepository, OfficeLevelAccessRepository>();
            container.RegisterType<IRoleRepository, RoleRepository>();
            container.RegisterType<IContactsRepository,ContactRepository>();
            container.RegisterType<IUserRepository, UserRepository>();
            container.RegisterType<ICommonRepository, CommonRepository>();
            container.RegisterType<IDashBoardSetUpRepository, DashBoardSetUpRepository>();
            container.RegisterType<ILeadRepository, LeadRepository>();
            container.RegisterType<ICompanyNewsRepository, CompanyNewsRepository>();
            container.RegisterType<IErrorLogRepository, ErrorLogRepository>();
            container.RegisterType<IMenuRepository,MenuRepository>();
            container.RegisterType<IUserMasterRepository, UserMasterRepository>();
            container.RegisterType<ICreateClientsRepository, CreateClientsRepository>();
            container.RegisterType<ICustomFieldsRepository, CustomFieldsRepository>();
           // container.RegisterType<ISalesRepository, SalesRepository>();
            container.RegisterType<IDashboardData, DashboardDataRepository>();
            container.RegisterType<IProjectRepository, ProjectRepository>();
            container.RegisterType<IItemRepository, ItemRepository>();
            container.RegisterType<IInvoiceRepository, InvoiceRepository>();
            container.RegisterType<IOpportunitiesRepository, OpportunitiesRepository>();
          

        }
    }
}