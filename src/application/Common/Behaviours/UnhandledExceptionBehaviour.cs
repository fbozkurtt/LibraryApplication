using MediatR;
using Serilog;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace LibraryApplication.Application.Common.Behaviours
{
    public class UnhandledExceptionBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    {
        public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
        {
            try
            {
                return await next();
            }
            catch (Exception ex)
            {
                var requestName = typeof(TRequest).Name;

                Log.Error(ex, "Library API Request: Unknown Exception for Request {Name} {@Request}", requestName, request);

                throw;
            }
        }
    }
}
