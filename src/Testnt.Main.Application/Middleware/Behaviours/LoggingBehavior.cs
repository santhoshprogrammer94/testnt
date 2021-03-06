﻿using MediatR;
using Microsoft.Extensions.Logging;
using System.Threading;
using System.Threading.Tasks;
using Testnt.Common.Interface;

namespace Testnt.Main.Application.Middleware.Behaviours
{
    public class LoggingBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    {
        private readonly ILogger<TRequest> logger;
        private readonly ICurrentUserService currentUserService;

        public LoggingBehavior(ILogger<TRequest> logger, ICurrentUserService currentUserService)
        {
            this.logger = logger;
            this.currentUserService = currentUserService;
        }
        public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
        {
            var name = typeof(TRequest).Name;

            logger.LogInformation("Handling command {@Request} by {@UserName} from tenant {@TenantId}",
                name, currentUserService.Name, currentUserService.TenantId);

            var response = await next();

            logger.LogInformation("Done handling command: {@Request} by {@UserName}",
                name, currentUserService.Name);

            return response;
        }
    }
}
