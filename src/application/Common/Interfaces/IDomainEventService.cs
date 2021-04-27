using LibraryApplication.Domain.Common;
using System.Threading.Tasks;

namespace LibraryApplication.Application.Common.Interfaces
{
    public interface IDomainEventService
    {
        Task Publish(DomainEvent domainEvent);
    }
}
