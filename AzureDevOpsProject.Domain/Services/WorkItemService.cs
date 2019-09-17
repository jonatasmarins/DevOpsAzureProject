using AzureDevOpsProject.Domain.Entities;
using AzureDevOpsProject.Domain.Repositories;
using Flunt.Notifications;
using Flunt.Validations;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AzureDevOpsProject.Domain.Services
{
    public class WorkItemService : Notifiable
    {
        public IEnumerable<WorkItem> ConvertToWorkItem(IEnumerable<Microsoft.TeamFoundation.WorkItemTracking.WebApi.Models.WorkItem> workItensExternalService)
        {
            List<WorkItem> workItens = new List<WorkItem>();

            foreach (var item in workItensExternalService)
            {
                DateTime createdDate = DateTime.MinValue;
                if (!DateTime.TryParse(item.Fields["System.CreatedDate"].ToString(), out createdDate))
                {
                    AddNotification("System.CreatedDate", "Não foi possível converter o valor para o tipo DateTime");
                }

                //[System.Id], [System.WorkItemType], [System.Title], [System.CreatedDate]
                WorkItem workItem = new WorkItem(
                    item.Id.Value.ToString(),
                    item.Fields["System.WorkItemType"].ToString(),
                    item.Fields["System.Title"].ToString(),
                    createdDate
                );

                workItens.Add(workItem);
            }

            return workItens;
        }
    }
}
