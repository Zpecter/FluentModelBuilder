using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using FluentModelBuilder.Alterations;
using FluentModelBuilder.Builder;
using FluentModelBuilder.Builder.Sources;

namespace FluentModelBuilder.Configuration
{
    /// <summary>
    ///     Starting point for automodelbuilder configuration
    /// </summary>
    public static class From
    {

        #region Type Sources

        /// <summary>
        ///     Map classes from provided type source
        /// </summary>
        /// <param name="source">Type source to use</param>
        /// <returns>AutoModelBuilder</returns>
        public static AutoModelBuilder Source(ITypeSource source) => new AutoModelBuilder().AddTypeSource(source);

        /// <summary>
        ///     Map classes from provided type source
        /// </summary>
        /// <typeparam name="TSource">Type source to use</typeparam>
        /// <returns>AutoModelBuilder</returns>
        public static AutoModelBuilder Source<TSource>() where TSource : ITypeSource => new AutoModelBuilder().AddTypeSource<TSource>();

        /// <summary>
        ///     Map classes from provided type source with supplied configuration
        /// </summary>
        /// <param name="source">Type source to use</param>
        /// <param name="configuration">Configuration to use</param>
        /// <returns>AutoModelBuilder</returns>
        public static AutoModelBuilder Source(ITypeSource source, IEntityAutoConfiguration configuration)
            => new AutoModelBuilder(configuration).AddTypeSource(source);

        /// <summary>
        ///     Map classes from provided type source with supplied configuration
        /// </summary>
        /// <typeparam name="TSource">Type source to use</typeparam>
        /// <param name="configuration">Configuration to use</param>
        /// <returns>AutoModelBuilder</returns>
        public static AutoModelBuilder Source<TSource>(IEntityAutoConfiguration configuration)
            where TSource : ITypeSource => new AutoModelBuilder(configuration).AddTypeSource<TSource>();

        /// <summary>
        ///     Map classes from provided type source with supplied expression
        /// </summary>
        /// <param name="source">Type source to use</param>
        /// <param name="expression">Configuration to use</param>
        /// <returns>AutoModelBuilder</returns>
        public static AutoModelBuilder Source(ITypeSource source, Func<Type, bool> expression)
            => new AutoModelBuilder().AddTypeSource(source).Where(expression);

        /// <summary>
        ///     Map classes from provided type source with supplied expression
        /// </summary>
        /// <typeparam name="TSource">Type source to use</typeparam>
        /// <param name="expression">Configuration to use</param>
        /// <returns>AutoModelBuilder</returns>
        public static AutoModelBuilder Source<TSource>(Func<Type, bool> expression) where TSource : ITypeSource
            => new AutoModelBuilder().AddTypeSource<TSource>().Where(expression);

        #endregion

        #region Alterations

        public static AutoModelBuilder Alteration(IAutoModelBuilderAlteration alteration)
            => Empty().AddAlteration(alteration);

        public static AutoModelBuilder Alteration(IAutoModelBuilderAlteration alteration,
                IEntityAutoConfiguration configuration)
            => Empty(configuration).AddAlteration(alteration);

        public static AutoModelBuilder Alterations(IEnumerable<IAutoModelBuilderAlteration> alterations)
            => Empty().AddAlterations(alterations);

        public static AutoModelBuilder Alterations(IEnumerable<IAutoModelBuilderAlteration> alterations,
                IEntityAutoConfiguration configuration)
            => Empty(configuration).AddAlterations(alterations);

        public static AutoModelBuilder Alteration(Type type)
            => Empty().AddAlteration(type);

        public static AutoModelBuilder Alteration(Type type, IEntityAutoConfiguration configuration)
            => Empty(configuration).AddAlteration(type);

        public static AutoModelBuilder Alteration<TAlteration>() where TAlteration : IAutoModelBuilderAlteration
            => Empty().AddAlteration<TAlteration>();

        public static AutoModelBuilder Alteration<TAlteration>(IEntityAutoConfiguration configuration)
            where TAlteration : IAutoModelBuilderAlteration
        => Empty().AddAlteration<TAlteration>();

        #endregion

        #region Assemblies

        /// <summary>
        ///     Map classes from provided assemblies
        /// </summary>
        /// <param name="assemblies">Assemblies to scan</param>
        /// <returns>AutoModelBuilder</returns>
        public static AutoModelBuilder Assemblies(params Assembly[] assemblies)
            => Source(new CombinedAssemblyTypeSource(assemblies.Select(x => new AssemblyTypeSource(x))));

        /// <summary>
        ///     Map classes from provided assemblies
        /// </summary>
        /// <param name="configuration">Configuration to use</param>
        /// <param name="assemblies">Assemblies to scan</param>
        /// <returns>AutoModelBuilder</returns>
        public static AutoModelBuilder Assemblies(IEntityAutoConfiguration configuration, params Assembly[] assemblies)
            => Source(new CombinedAssemblyTypeSource(assemblies.Select(x => new AssemblyTypeSource(x))),
                configuration);

        /// <summary>
        ///     Map classes from provided assemblies
        /// </summary>
        /// <param name="configuration">Configuration to use</param>
        /// <param name="assemblies">Assemblies to scan</param>
        /// <returns>AutoModelBuilder</returns>
        public static AutoModelBuilder Assemblies(IEntityAutoConfiguration configuration,
            IEnumerable<Assembly> assemblies)
            => Source(new CombinedAssemblyTypeSource(assemblies.Select(x => new AssemblyTypeSource(x))),
                configuration);

        /// <summary>
        ///     Map classes from provided assembly
        /// </summary>
        /// <param name="assembly">Assembly to scan</param>
        /// <returns></returns>
        public static AutoModelBuilder Assembly(Assembly assembly) => Source(new AssemblyTypeSource(assembly));

        /// <summary>
        ///     Map classes from provided assembly with supplied configuration
        /// </summary>
        /// <param name="assembly">Assembly to scan</param>
        /// <param name="configuration">Configuration to use</param>
        /// <returns>AutoModelBuilder</returns>
        public static AutoModelBuilder Assembly(Assembly assembly, IEntityAutoConfiguration configuration)
            => Source(new AssemblyTypeSource(assembly), configuration);

        /// <summary>
        ///     Map classes from provided assembly with supplied expression
        /// </summary>
        /// <param name="assembly">Assembly to scan</param>
        /// <param name="expression">Expression to use to filter types</param>
        /// <returns>AutoModelBuilder</returns>
        public static AutoModelBuilder Assembly(Assembly assembly, Func<Type, bool> expression)
            => Source(new AssemblyTypeSource(assembly), expression);

        /// <summary>
        ///     Map classes from the assembly containing
        ///     <typeparam name="T"></typeparam>
        /// </summary>
        /// <typeparam name="T">Type contained in the required assembly</typeparam>
        /// <returns>AutoModelBuilder</returns>
        public static AutoModelBuilder AssemblyOf<T>() => Assembly(typeof (T).GetTypeInfo().Assembly);

        /// <summary>
        ///     Map classes from the assembly containing
        ///     <typeparam name="T"></typeparam>
        ///     with supplied configuration
        /// </summary>
        /// <typeparam name="T">Type contained in the required assembly</typeparam>
        /// <param name="configuration">Configuration to use</param>
        /// <returns>AutoModelBuilder</returns>
        public static AutoModelBuilder AssemblyOf<T>(IEntityAutoConfiguration configuration)
            => Assembly(typeof (T).GetTypeInfo().Assembly, configuration);

        /// <summary>
        ///     Map classes from the assembly containing
        ///     <typeparam name="T"></typeparam>
        ///     with supplied expression
        /// </summary>
        /// <typeparam name="T">Type contained in the required assembly</typeparam>
        /// <param name="expression">Expression to use</param>
        /// <returns>AutoModelBuilder</returns>
        public static AutoModelBuilder AssemblyOf<T>(Func<Type, bool> expression)
            => Assembly(typeof (T).GetTypeInfo().Assembly, expression);


        /// <summary>
        ///     Map classes from the assembly containing the provided type
        /// </summary>
        /// <param name="type">Type contained in the required assembly</param>
        /// <returns>AutoModelBuilder</returns>
        public static AutoModelBuilder AssemblyOf(Type type)
            => Assembly(type.GetTypeInfo().Assembly);

        /// <summary>
        ///     Map classes from the assembly containing the provided type
        ///     with supplied configuration
        /// </summary>
        /// <param name="type">Type contained in the required assembly</param>
        /// <param name="configuration">Configuration to use</param>
        /// <returns>AutoModelBuilder</returns>
        public static AutoModelBuilder AssemblyOf(Type type, IEntityAutoConfiguration configuration)
            => Assembly(type.GetTypeInfo().Assembly, configuration);

        /// <summary>
        ///     Map classes from the assembly containing the provided type
        ///     with supplied expression
        /// </summary>
        /// <param name="type">Type contained in the required assembly</param>
        /// <param name="expression">Expression to use</param>
        /// <returns>AutoModelBuilder</returns>
        public static AutoModelBuilder AssemblyOf(Type type, Func<Type, bool> expression)
            => Assembly(type.GetTypeInfo().Assembly, expression);

        #endregion

        #region Empty

        /// <summary>
        ///     Map classes based on all manual specifications
        /// </summary>
        /// <remarks>
        ///     You would use this if you didn't want to use any assembly or source by default, but rather
        ///     manually configure entire AutoModelBuilder yourself.
        /// </remarks>
        /// <returns>AutoModelBuilder</returns>
        public static AutoModelBuilder Empty() => new AutoModelBuilder();

        /// <summary>
        ///     Map classes based on all manual specifications with supplied configuration
        /// </summary>
        /// <param name="configuration">Configuration to use</param>
        /// <returns>AutoModelBuilder</returns>
        public static AutoModelBuilder Empty(IEntityAutoConfiguration configuration)
            => new AutoModelBuilder(configuration);

        #endregion

        /// <summary>
        ///     Map classes based on all manual specifications with supplied expression
        /// </summary>
        /// <param name="expression"></param>
        /// <returns></returns>
        public static AutoModelBuilder Expression(Func<Type, bool> expression) => Empty().Where(expression);
    }
}