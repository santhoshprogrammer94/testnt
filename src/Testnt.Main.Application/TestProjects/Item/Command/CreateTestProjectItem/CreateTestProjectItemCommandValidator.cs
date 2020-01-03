﻿
using FluentValidation;
using FluentValidation.Validators;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;
using Testnt.Main.Infrastructure.Data;

namespace Testnt.Main.Application.TestProjects.Item.Command.CreateTestProjectItem
{
    public class CreateTestProjectCommandValidator : AbstractValidator<CreateTestProjectItemCommand>
    {
        private readonly TestntDbContext context;

        public CreateTestProjectCommandValidator(TestntDbContext context)
        {
            this.context = context;
            RuleFor(v => v.Name)
                .MaximumLength(50)
                .NotEmpty()
                .WithMessage("Project name is required.")
                .MustAsync((name, cancellation) => HaveUniqueName(name))
                .WithMessage("Project name already exists.")
                ;
           
        }

        private async Task<bool> HaveUniqueName(string projectName)
        {
            var projectNameExistCheck = await context.Projects.Where(p => p.Name.ToLower().Equals(projectName.ToLower())).ToListAsync();
            return projectNameExistCheck.Count == 0;
        }
    }
}
