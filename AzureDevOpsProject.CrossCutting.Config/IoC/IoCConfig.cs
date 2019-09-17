using AzureDevOpsProject.ApplicationService;
using AzureDevOpsProject.Domain.Repositories;
using AzureDevOpsProject.Domain.Services.Inteface;
using AzureDevOpsProject.Infra.Context;
using AzureDevOpsProject.Infra.UnitOfWork;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace AzureDevOpsProject.CrossCutting.Config
{
    public static class IoCConfig
    {
        public static void Config(IServiceCollection services, IConfiguration configuration)
        {
            #region [ Commom ]

            //services.AddTransient<IHttpContextAccessor, HttpContextAccessor>();

            #endregion


            #region [ Job ]

            #endregion

            #region [ Application ]

            services.AddTransient(typeof(IWorkItemApplicationService), typeof(WorkItemApplicationService));
            services.AddTransient(typeof(IProjectApplicationService), typeof(ProjectApplicationService));

            #endregion            

            #region [ Repository ]            

            services.AddScoped<IUnitOfWork, UnitOfWork>(service => new UnitOfWork(configuration));
            services.AddScoped<IDbContextOptions, DbContextOptions<DbContextSqlServer>>();

            #endregion
        }
    }
}
