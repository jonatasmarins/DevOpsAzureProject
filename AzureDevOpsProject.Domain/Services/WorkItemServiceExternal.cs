using AzureDevOpsProject.Domain.Entities;
using Microsoft.TeamFoundation.WorkItemTracking.WebApi;
using Microsoft.TeamFoundation.WorkItemTracking.WebApi.Models;
using Microsoft.VisualStudio.Services.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AzureDevOpsProject.Domain.Services
{
    public class WorkItemServiceExternal
    {
        public async Task<List<Microsoft.TeamFoundation.WorkItemTracking.WebApi.Models.WorkItem>>
            RunGetTasksQuery(Project project)
        {
            VssBasicCredential credentials = new VssBasicCredential("", project.PersonalAccessToken);

            //create a wiql object and build our query
            Wiql wiql = new Wiql()
            {
                Query = "Select " +
                            " [System.Id], [System.WorkItemType], [System.Title], [System.CreatedDate] " +
                        "From WorkItems " +
                        "Where " +
                        "[System.TeamProject] = '" + project.Name.ToString() + "' " +
                        "And [System.State] <> 'Closed' " +
                        "Order By [State] Asc, [Changed Date] Desc"
            };

            //create instance of work item tracking http client
            using (WorkItemTrackingHttpClient workItemTrackingHttpClient = new WorkItemTrackingHttpClient(new Uri(project.Url.ToString()), credentials))
            {
                //execute the query to get the list of work items in the results
                WorkItemQueryResult workItemQueryResult = await workItemTrackingHttpClient.QueryByWiqlAsync(wiql);

                //some error handling                
                if (workItemQueryResult.WorkItems.Count() != 0)
                {
                    //need to get the list of our work item ids and put them into an array
                    List<int> list = new List<int>();
                    foreach (var item in workItemQueryResult.WorkItems)
                    {
                        list.Add(item.Id);
                    }
                    int[] arr = list.ToArray();

                    //build a list of the fields we want to see
                    string[] fields = new string[4];
                    fields[0] = "System.Id";
                    fields[1] = "System.WorkItemType";
                    fields[2] = "System.Title";
                    fields[3] = "System.CreatedDate";

                    //get work items for the ids found in query
                    var workItems = await workItemTrackingHttpClient.GetWorkItemsAsync(arr, fields, workItemQueryResult.AsOf);
                    Console.WriteLine("Query Results: {0} items found", workItems.Count);

                    return workItems;
                }

                return null;
            }
        }
    }
}
