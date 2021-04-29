using LibraryApplication.Application.Common.Interfaces;
using MediatR.Pipeline;
using Serilog;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace LibraryApplication.Application.Common.Behaviours
{
    public class LoggingBehaviour<TRequest> : IRequestPreProcessor<TRequest>
    {
        public Task Process(TRequest request, CancellationToken cancellationToken)
        {
            var requestName = typeof(TRequest).Name;

            Log.Information($"API Request: {requestName}");

            return Task.CompletedTask;
        }
    }
}
