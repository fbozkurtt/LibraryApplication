using LibraryApplication.Application.Common.Models;
using LibraryApplication.Domain.Events;
using MediatR;
using Serilog;
using System.Threading;
using System.Threading.Tasks;

namespace LibraryApplication.Application.EventHandlers
{
    public class BookRemovedEventHandler : INotificationHandler<DomainEventNotification<BookRemovedEvent>>
    {
        public Task Handle(DomainEventNotification<BookRemovedEvent> notification, CancellationToken cancellationToken)
        {
            var domainEvent = notification.DomainEvent;

            Log.Information("Event occured: {DomainEvent}", domainEvent.GetType().Name);

            return Task.CompletedTask;
        }
    }
}
