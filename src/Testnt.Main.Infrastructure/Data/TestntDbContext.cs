﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using Testnt.Common.Interface;
using Testnt.Main.Domain.Entity;
using Testnt.Main.Domain.Entity.TestSessionEntity;

namespace Testnt.Main.Infrastructure.Data
{
    public class TestntDbContext : DbContext
    {
        private readonly ICurrentUserService currentUserService;
        private readonly IDateTimeService dateTimeService;

        public TestntDbContext(DbContextOptions<TestntDbContext> options, ICurrentUserService currentUserService, IDateTimeService dateTimeService)
           : base(options)
        {
            this.currentUserService = currentUserService;
            this.dateTimeService = dateTimeService;
        }

        public DbSet<Project> Projects { get; set; }
        public DbSet<Scenario> Scenarios { get; set; }
        public DbSet<Feature> Features { get; set; }
        public DbSet<Session> Sessions { get; set; }
        public DbSet<UserProfile> UserProfiles { get; set; }
        public DbSet<Tag> Tags { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Project>()
                .HasIndex(u => u.Name);

            // many to many mapping for test outline and tags
            modelBuilder.Entity<TagLink>()
                .HasKey(bc => new { bc.ScenarioId, bc.TagId });
            modelBuilder.Entity<TagLink>()
                .HasOne(bc => bc.Scenario)
                .WithMany(b => b.Tags)
                .HasForeignKey(bc => bc.TagId);
            modelBuilder.Entity<TagLink>()
                .HasOne(bc => bc.Tag)
                .WithMany(c => c.TagLinks)
                .HasForeignKey(bc => bc.ScenarioId);


            // many to many mapping for test project and user
            modelBuilder.Entity<ProjectUser>()
                .HasKey(bc => new { bc.ProjectId, bc.UserProfileId });
            modelBuilder.Entity<ProjectUser>()
                .HasOne(bc => bc.UserProfile)
                .WithMany(b => b.Projects)
                .HasForeignKey(bc => bc.ProjectId);
            modelBuilder.Entity<ProjectUser>()
                .HasOne(bc => bc.Project)
                .WithMany(c => c.Members)
                .HasForeignKey(bc => bc.UserProfileId);


            modelBuilder.Entity<Scenario>()
                .OwnsMany(p => p.Steps);

            // reference https://haacked.com/archive/2019/07/29/query-filter-by-interface/
            // Note the .Where(t => t.BaseType == null) clause here. 
            // Query filters may only be applied to the root entity type of an inheritance hierarchy. 
            // This clause ensures we don’t try to apply a filter on a non-root type.
            var entityTypes = modelBuilder.Model.GetEntityTypes()
                .Where(t => t.BaseType == null)
                .Where(t => t.IsOwned() == false);

            // add tenant id filter and index in all queries
            // ref https://stackoverflow.com/questions/45812459/ef-core-2-apply-hasqueryfilter-for-all-entity
            // ref https://stackoverflow.com/questions/38178439/expression-to-compare-guid-properties-versus-string-values-and-then-translate-to
            foreach (var entityType in entityTypes)
            {
                var tenantIdProperty = entityType.FindProperty("TenantId");
                
                if (tenantIdProperty != null && tenantIdProperty.ClrType == typeof(Guid))
                {
                    var parameter = Expression.Parameter(entityType.ClrType, "entity");
                    var propertyAccessExpr = Expression.MakeMemberAccess(parameter, tenantIdProperty.PropertyInfo);
                    var guidExpr = Expression.Constant(currentUserService.TenantId);
                    var body = Expression.Equal(propertyAccessExpr, guidExpr);

                    var lambda = Expression.Lambda(body, parameter);

                    entityType.SetQueryFilter(lambda);

                    entityType.AddIndex(tenantIdProperty);
                    entityType.AddForeignKey(tenantIdProperty, entityType.FindPrimaryKey(), entityType);
                }
            }
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            InjectBaseActivity();
            return base.SaveChangesAsync(cancellationToken);
        }

        public override int SaveChanges()
        {
            InjectBaseActivity();
            return base.SaveChanges();
        }

        public override int SaveChanges(bool acceptAllChangesOnSuccess)
        {
            InjectBaseActivity();
            return base.SaveChanges(acceptAllChangesOnSuccess);
        }


        private void InjectBaseActivity()
        {
            foreach (var entry in ChangeTracker.Entries<BaseEntity>())
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        if (entry.Entity.TenantId != currentUserService.TenantId)
                        {
                            entry.Entity.TenantId = currentUserService.TenantId;
                        }
                        entry.Entity.CreatedBy = currentUserService.Name;
                        entry.Entity.Created = dateTimeService.Now;
                        break;
                    case EntityState.Modified:
                        if (entry.Entity.TenantId != currentUserService.TenantId)
                        {
                            entry.Entity.TenantId = currentUserService.TenantId;
                        }
                        entry.Entity.CreatedBy = currentUserService.Name;
                        entry.Entity.Created = dateTimeService.Now;
                        break;
                }
            }
        }
    }
}
