﻿using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Testnt.Common.Exceptions;
using Testnt.Main.Application.Common;
using Testnt.Main.Domain.Entity;
using Testnt.Main.Infrastructure.Data;

namespace Testnt.Main.Application.TestProjects.Command.Update
{
    public class UpdateTestProjectItemCommand : BaseRequest, IRequest
    {
        public Guid ProjectId { get; set; }
        public bool IsEnabled { get; set; }

        public class UpdateTestProjectItemCommandHandler : IRequestHandler<UpdateTestProjectItemCommand>
        {
            private readonly TestntDbContext context;
            public UpdateTestProjectItemCommandHandler(TestntDbContext context)
            {
                this.context = context;
            }

            public async Task<Unit> Handle(UpdateTestProjectItemCommand request, CancellationToken cancellationToken)
            {
                var project = await context.Projects
                    .Where(t => t.TenantId.Equals(request.TenantId))
                    .Where(t => t.Id.Equals(request.ProjectId))
                    .SingleOrDefaultAsync(cancellationToken);

                if (project == null)
                {
                    throw new EntityNotFoundException(nameof(TestProject), request.ProjectId);
                }

                project.IsEnabled = request.IsEnabled;

                context.Projects.Update(project);
                await context.SaveChangesAsync(cancellationToken);

                return Unit.Value;
            }
        }
    }

    

    public class UpdateTestProjectItemDto
    {
    }
}