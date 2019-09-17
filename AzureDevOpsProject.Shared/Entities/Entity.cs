using Dapper.Contrib.Extensions;
using Flunt.Notifications;
using System;

namespace AzureDevOpsProject.Shared.Entities
{
    public abstract class Entity : Notifiable
    {
        protected Entity()
        {
            Id = Guid.NewGuid();
        }

        [Key]
        public Guid Id { get; set; }

        public abstract void Validate();
    }
}
