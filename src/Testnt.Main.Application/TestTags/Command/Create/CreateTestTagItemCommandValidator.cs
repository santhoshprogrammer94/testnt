﻿using FluentValidation;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Testnt.Main.Infrastructure.Data;

namespace Testnt.Main.Application.TestTags.Command.Create
{
    public class CreateTestTagItemCommandValidator : AbstractValidator<CreateTestTagItemCommand>
    {
        private readonly TestntDbContext context;

        public CreateTestTagItemCommandValidator(TestntDbContext context)
        {
            this.context = context;
            RuleFor(v => v.Name)
                .MaximumLength(50)
                .NotEmpty()
                .WithMessage("Tag name is required.")
                .MustAsync((command, _, cancellation) => HaveUniqueName(command))
                .WithMessage("Project name already exists.")
                ;

            RuleFor(v => v.TenantId)
                .NotEmpty()
                .WithMessage("Tag id is missing.");

            RuleFor(v => v.ProjectId)
                .NotEmpty()
                .WithMessage("Project id is missing.");

        }

        private async Task<bool> HaveUniqueName(CreateTestTagItemCommand command)
        {
            var tagNameExistCheck = await context.TestTags
                .Where(t => t.TenantId.Equals(command.TenantId))
                .Where(t => t.ProjectId.Equals(command.ProjectId))
                .Where(p => p.Name.ToLower().Equals(command.Name.ToLower())).ToListAsync();
            return tagNameExistCheck.Count == 0;
        }
    }
}