﻿using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Testnt.Main.Infrastructure.Data;

namespace Testnt.Main.Application.TestProjects.List.Query.GetTestProjectList
{
    public class GetTestProjectListQueryHandler : IRequestHandler<GetTestProjectListQuery, GetTestProjectListVm>
    {
        private readonly TestntDbContext context;
        private readonly IMapper mapper;

        public GetTestProjectListQueryHandler(TestntDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        public async Task<GetTestProjectListVm> Handle(GetTestProjectListQuery request, CancellationToken cancellationToken)
        {
            var projects = await context.Projects
                .ProjectTo<GetTestProjectListDto>(mapper.ConfigurationProvider)
                //.OrderBy(t => t.)
                .ToListAsync(cancellationToken);

            var vm = new GetTestProjectListVm
            {
                Data = projects,
                Count = projects.Count
            };

            return vm;
        }
    }
}
