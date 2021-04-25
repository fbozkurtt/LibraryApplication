using LibraryApplication.Application.Common.Interfaces;
using LibraryApplication.Application.Common.Models;
using LibraryApplication.Domain.Events;
using MediatR;
using Serilog;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace LibraryApplication.Application.EventHandlers
{
    public class BookReturnedEventHandler : INotificationHandler<DomainEventNotification<BookReturnedEvent>>
    {
        public Task Handle(DomainEventNotification<BookReturnedEvent> notification, CancellationToken cancellationToken)
        {
            var domainEvent = notification.DomainEvent;

            Log.Information("Event occured: {DomainEvent}", domainEvent.GetType().Name);

            return Task.CompletedTask;
        }
    }
}
