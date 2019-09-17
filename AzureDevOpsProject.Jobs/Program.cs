using AzureDevOpsProject.Domain.Entities;
using AzureDevOpsProject.Domain.Repositories;
using AzureDevOpsProject.Domain.Services;
using AzureDevOpsProject.Infra.UnitOfWork;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace AzureDevOpsProject.Jobs
{
    public class Program
    {
        private static Uri _uri;
        private static string _personalAccessToken;
        private static string _project;

        public static void Main(string[] args)
        {
            #region Configuration

            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json");

            IConfiguration config = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", true, true)
                .Build();

            #endregion

            #region Proprieties

            List<Microsoft.TeamFoundation.WorkItemTracking.WebApi.Models.WorkItem> workItensExternal = new List<Microsoft.TeamFoundation.WorkItemTracking.WebApi.Models.WorkItem>();
            IEnumerable<Domain.Entities.WorkItem> workItens = new List<Domain.Entities.WorkItem>();
            Project project = new Project();

            #endregion

            // Get Project in DataBase
            using (IUnitOfWork uow = new UnitOfWork(config))
            {
                Task.Run(async () =>
                {
                    project = await uow.projectRepository.Get();
                }).Wait();
            }

            if (project != null)
            {
                // Get WorkItem Azure
                Task.Run(async () =>
                {
                    Console.WriteLine("Acessando Azure DevOps");
                    WorkItemServiceExternal workItemServiceExternal = new WorkItemServiceExternal();
                    workItensExternal = await workItemServiceExternal.RunGetTasksQuery(project);
                    Console.WriteLine("WorkItens Recuperado com sucesso");
                }).Wait();


                Console.WriteLine("Convertendo objeto externo para entity Work Item");
                WorkItemService workItemService = new WorkItemService();
                workItens = workItemService.ConvertToWorkItem(workItensExternal);

                // Save Work Item in Data Base
                Console.WriteLine("Salvando os Work Itens no banco de dados");

                using (IUnitOfWork uow = new UnitOfWork(config))
                {
                    Task.Run(async () =>
                    {
                        await uow.workItemRepository.Delete();
                        await uow.workItemRepository.Creates(workItens);
                    }).Wait();
                }

                Console.WriteLine("Dados salvos com sucesso !");
            }
            else
            {
                Console.WriteLine("Não foi possível carregar configuração do projeto azure dev ops");
            }

            Console.ReadKey();
        }
    }
}