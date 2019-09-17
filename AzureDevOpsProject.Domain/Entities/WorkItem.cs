using AzureDevOpsProject.Shared.Entities;
using Dapper.Contrib.Extensions;
using Flunt.Validations;
using System;

namespace AzureDevOpsProject.Domain.Entities
{
    [Table("WorkItem")]
    public class WorkItem : Entity
    {
        public WorkItem()
        {

        }

        public WorkItem(string code, string type, string title, DateTime createDate)
        {
            Code = code;
            Type = type;
            Title = title;
            CreateDate = createDate;
        }

        public string Code { get; set; }
        public string Type { get; set; }
        public string Title { get; set; }
        public DateTime CreateDate { get; set; }

        public override void Validate()
        {
            AddNotifications(new Contract()
                .Requires()
                .IsNullOrEmpty(Code, "Code", "Não pode ser vazio")
                .IsNullOrEmpty(Type, "Type", "Não pode ser vazio")
                .IsNullOrEmpty(Title, "Title", "Não pode ser vazio")
                .IsLowerOrEqualsThan(CreateDate, DateTime.MinValue, "CreateDate", "Data inválida")
            );
        }
    }
}
