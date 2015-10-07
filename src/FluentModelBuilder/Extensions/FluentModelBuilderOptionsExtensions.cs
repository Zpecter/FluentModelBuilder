using System;
using System.Linq;
using FluentModelBuilder.Conventions;
using FluentModelBuilder.Conventions.Options;
using FluentModelBuilder.Options;
using Microsoft.Data.Entity.Metadata.Builders;

namespace FluentModelBuilder.Extensions
{
    public static class FluentModelBuilderOptionsExtensions
    {
        /// <summary>
        /// Adds entities from assemblies that correspond to given conditions
        /// </summary>
        /// <param name="options"><see cref="FluentModelBuilderOptions"/></param>
        /// <param name="optionsAction">Actions to apply to <see cref="EntityDiscoveryConventionOptions"/></param>
        /// <returns><see cref="FluentModelBuilderOptions"/></returns>
        public static FluentModelBuilderOptions AddEntities(this FluentModelBuilderOptions options, Action<EntityDiscoveryConventionOptions> optionsAction = null)
        {
            var convention = options.Conventions.FirstOrDefault(x => x is EntityDiscoveryConvention) as EntityDiscoveryConvention;
            if (convention == null)
            {
                convention = new EntityDiscoveryConvention();
                options.Conventions.AddFirst(convention);
            }
            
            optionsAction?.Invoke(convention.Options);
            return options;
        }
        
        /// <summary>
        /// Adds single entity to model
        /// </summary>
        /// <param name="options"><see cref="FluentModelBuilderOptions"/></param>
        /// <param name="type">Type of entity to add</param>
        /// <returns><see cref="FluentModelBuilderOptions"/></returns>
        public static FluentModelBuilderOptions AddEntity(this FluentModelBuilderOptions options, Type type)
        {
            options.AddConvention(new EntityConvention(type));
            return options;
        }

        /// <summary>
        /// Adds single entity to model
        /// </summary>
        /// <typeparam name="T">Type of entity to add</typeparam>
        /// <param name="options"><see cref="FluentModelBuilderOptions"/></param>
        /// <returns><see cref="FluentModelBuilderOptions"/></returns>
        public static FluentModelBuilderOptions AddEntity<T>(this FluentModelBuilderOptions options)
            where T : class
        {
            options.AddConvention(new EntityConvention(typeof (T)));
            return options;
        }

        /// <summary>
        /// Adds and configures single entity on model
        /// </summary>
        /// <typeparam name="T">Type of entity to add and configure</typeparam>
        /// <param name="options"><see cref="FluentModelBuilderOptions"/></param>
        /// <param name="action">Configuration to perform on entity</param>
        /// <returns><see cref="FluentModelBuilderOptions"/></returns>
        public static FluentModelBuilderOptions AddEntity<T>(this FluentModelBuilderOptions options,
            Action<EntityTypeBuilder<T>> action) where T : class
        {
            options.AddConvention(new EntityConfigurationConvention<T>(action));
            return options;
        }

        /// <summary>
        /// Adds a convention to <see cref="FluentModelBuilder"/>
        /// </summary>
        /// <param name="options"><see cref="FluentModelBuilderOptions"/></param>
        /// <param name="convention">Convention to add</param>
        /// <returns><see cref="FluentModelBuilderOptions"/></returns>
        public static FluentModelBuilderOptions AddConvention(this FluentModelBuilderOptions options,
            IModelBuilderConvention convention)
        {
            options.Conventions.AddLast(convention);
            return options;
        }

        /// <summary>
        /// Adds a strongly typed convention to <see cref="FluentModelBuilder"/>
        /// </summary>
        /// <typeparam name="T">Convention type to add</typeparam>
        /// <param name="options"><see cref="FluentModelBuilderOptions"/></param>
        /// <returns><see cref="FluentModelBuilderOptions"/></returns>
        public static FluentModelBuilderOptions AddConvention<T>(this FluentModelBuilderOptions options)
            where T : IModelBuilderConvention, new()
        {
            options.AddConvention(new T());
            return options;
        }

        /// <summary>
        /// Adds IEntityTypeOverrides to override model builder actions
        /// </summary>
        /// <param name="options"><see cref="FluentModelBuilderOptions"/></param>
        /// <param name="optionsAction">Actions to apply to <see cref="EntityTypeOverrideDiscoveryConventionOptions"/></param>
        /// <returns><see cref="FluentModelBuilderOptions"/></returns>
        public static FluentModelBuilderOptions AddOverrides(this FluentModelBuilderOptions options,
            Action<EntityTypeOverrideDiscoveryConventionOptions> optionsAction = null)
        {
            var convention =
                options.Conventions.FirstOrDefault(x => x is EntityTypeOverrideDiscoveryConvention) as
                    EntityTypeOverrideDiscoveryConvention;
            if (convention == null)
            {
                convention = new EntityTypeOverrideDiscoveryConvention();
                options.Conventions.AddLast(convention);
            }

            optionsAction?.Invoke(convention.Options);
            return options;
        }

        /// <summary>
        /// Adds single IEntityTypeOverride to model builder configuration
        /// </summary>
        /// <typeparam name="T">Type of IEntityTypeOverride to add</typeparam>
        /// <param name="options"><see cref="FluentModelBuilderOptions"/></param>
        /// <returns><see cref="FluentModelBuilderOptions"/></returns>
        public static FluentModelBuilderOptions AddOverride<T>(this FluentModelBuilderOptions options)
        {
            return options.AddConvention(new EntityTypeOverrideConvention<T>());
        }
    }
}