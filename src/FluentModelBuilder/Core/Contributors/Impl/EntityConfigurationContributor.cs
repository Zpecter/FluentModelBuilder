﻿using System;
using Microsoft.Data.Entity;
using Microsoft.Data.Entity.Metadata.Builders;

namespace FluentModelBuilder.Core.Contributors.Impl
{
    public class EntityConfigurationContributor<T> : IEntityContributor where T : class
    {

        private readonly Action<EntityTypeBuilder<T>> _entityAction;

        public EntityConfigurationContributor(Action<EntityTypeBuilder<T>> entityAction)
        {
            _entityAction = entityAction;
        }

        public void Contribute(ModelBuilder modelBuilder)
        {
            _entityAction(modelBuilder.Entity<T>());
        }
    }
}