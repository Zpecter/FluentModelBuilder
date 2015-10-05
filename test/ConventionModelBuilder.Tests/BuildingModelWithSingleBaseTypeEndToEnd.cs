﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ConventionModelBuilder.Conventions.Options.Extensions;
using ConventionModelBuilder.Extensions;
using ConventionModelBuilder.Options.Extensions;
using ConventionModelBuilder.TestTarget;
using Microsoft.Data.Entity;
using Microsoft.Framework.DependencyInjection;
using Xunit;

namespace ConventionModelBuilder.Tests
{
    public class BuildingModelWithSingleBaseTypeEndToEnd : IClassFixture<BuildingModelWithSingleBaseTypeEndToEnd.Fixture>
    {
        public class Fixture
        {
            public DbContext Context;

            public Fixture()
            {
                var collection = new ServiceCollection();
                collection.AddEntityFramework().AddDbContext<DbContext>(o =>
                {
                    o.BuildModelUsingConventions(c =>
                    {
                        c.AddEntities(e => e.WithBaseType<EntityBase>().FromAssemblyContaining<NotAnEntity>());
                    });
                });
                Context = collection.BuildServiceProvider().GetService<DbContext>();
            }
        }

        private readonly Fixture _fixture;

        public BuildingModelWithSingleBaseTypeEndToEnd(Fixture fixture)
        {
            _fixture = fixture;
        }

        [Fact]
        public void DoesNotAddAbstractEntitiesToModel()
        {
            Assert.False(_fixture.Context.Model.EntityTypes.Any(x => x.ClrType == typeof(EntityBase)));
        }

        [Fact]
        public void AddsEntitiesToModel()
        {
            Assert.True(_fixture.Context.Model.EntityTypes.Any(x => x.ClrType == typeof (EntityOne)));
            Assert.True(_fixture.Context.Model.EntityTypes.Any(x => x.ClrType == typeof (EntityTwo)));
        }

        [Fact]
        public void AddsPropertiesToModel()
        {
            Assert.True(_fixture.Context.Model.EntityTypes.Any(c => c.GetProperties().Any(p => p.Name == "IgnoredInOverride")));
            Assert.True(_fixture.Context.Model.EntityTypes.Any(c => c.GetProperties().Any(p => p.Name == "NotIgnored")));
            Assert.True(_fixture.Context.Model.EntityTypes.Any(c => c.GetProperties().Any(p => p.Name == "Id")));
        }
    }
}
