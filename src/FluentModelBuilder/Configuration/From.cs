using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using FluentModelBuilder.Builder;
using FluentModelBuilder.Builder.Sources;

namespace FluentModelBuilder.Configuration
{
    /// <summary>
    /// Starting point for automodelbuilder configuration
    /// </summary>
    public static class From
    {
        /// <summary>
        /// Map classes from provided type source
        /// </summary>
        /// <param name="source">Type source to use</param>
        /// <returns>AutoModelBuilder</returns>
        public static AutoModelBuilder Source(ITypeSource source)
        {
            return new AutoModelBuilder().AddTypeSource(source);
        }

        /// <summary>
        /// Map classes from provided type source with supplied configuration
        /// </summary>
        /// <param name="source">Type source to use</param>
        /// <param name="configuration">Configuration to use</param>
        /// <returns>AutoModelBuilder</returns>
        public static AutoModelBuilder Source(ITypeSource source, IEntityAutoConfiguration configuration)
        {
            return new AutoModelBuilder(configuration).AddTypeSource(source);
        }

        /// <summary>
        /// Map classes from provided assemblies
        /// </summary>
        /// <param name="assemblies">Assemblies to scan</param>
        /// <returns>AutoModelBuilder</returns>
        public static AutoModelBuilder Assemblies(params Assembly[] assemblies)
        {
            return Source(new CombinedAssemblyTypeSource(assemblies.Select(x => new AssemblyTypeSource(x))));
        }

        /// <summary>
        /// Map classes from provided assemblies
        /// </summary>
        /// <param name="configuration">Configuration to use</param>
        /// <param name="assemblies">Assemblies to scan</param>
        /// <returns>AutoModelBuilder</returns>
        public static AutoModelBuilder Assemblies(IEntityAutoConfiguration configuration, params Assembly[] assemblies)
        {
            return Source(new CombinedAssemblyTypeSource(assemblies.Select(x => new AssemblyTypeSource(x))),
                configuration);
        }

        /// <summary>
        /// Map classes from provided assemblies
        /// </summary>
        /// <param name="configuration">Configuration to use</param>
        /// <param name="assemblies">Assemblies to scan</param>
        /// <returns>AutoModelBuilder</returns>
        public static AutoModelBuilder Assemblies(IEntityAutoConfiguration configuration,
            IEnumerable<Assembly> assemblies)
        {
            return Source(new CombinedAssemblyTypeSource(assemblies.Select(x => new AssemblyTypeSource(x))),
                configuration);
        }

        /// <summary>
        /// Map classes from provided assembly
        /// </summary>
        /// <param name="assembly">Assembly to scan</param>
        /// <returns></returns>
        public static AutoModelBuilder Assembly(Assembly assembly)
        {
            return Source(new AssemblyTypeSource(assembly));
        }

        /// <summary>
        /// Map classes from provided assembly with supplied configuration
        /// </summary>
        /// <param name="assembly">Assembly to scan</param>
        /// <param name="configuration">Configuration to use</param>
        /// <returns>AutoModelBuilder</returns>
        public static AutoModelBuilder Assembly(Assembly assembly, IEntityAutoConfiguration configuration)
        {
            return Source(new AssemblyTypeSource(assembly), configuration);
        }

        /// <summary>
        /// Map classes from the assembly containing <typeparam name="T"></typeparam>
        /// </summary>
        /// <typeparam name="T">Type contained in the required assembly</typeparam>
        /// <returns>AutoModelBuilder</returns>
        public static AutoModelBuilder AssemblyOf<T>()
        {
            return Assembly(typeof(T).GetTypeInfo().Assembly);
        }

        /// <summary>
        /// Map classes from the assembly containing <typeparam name="T"></typeparam> with supplied configuration
        /// </summary>
        /// <typeparam name="T">Type contained in the required assembly</typeparam>
        /// <param name="configuration">Configuration to use</param>
        /// <returns>AutoModelBuilder</returns>
        public static AutoModelBuilder AssemblyOf<T>(IEntityAutoConfiguration configuration)
        {
            return Assembly(typeof (T).GetTypeInfo().Assembly, configuration);
        }

        /// <summary>
        /// Map classes based on all manual specifications
        /// </summary>
        /// <remarks>
        /// You would use this if you didn't want to use any assembly or source by default, but rather
        /// manually configure entire AutoModelBuilder yourself.
        /// </remarks>
        /// <returns>AutoModelBuilder</returns>
        public static AutoModelBuilder Empty()
        {
            return new AutoModelBuilder();
        }

        /// <summary>
        /// Map classes based on all manual specifications with supplied configuration
        /// </summary>
        /// <param name="configuration">Configuration to use</param>
        /// <returns>AutoModelBuilder</returns>
        public static AutoModelBuilder Empty(IEntityAutoConfiguration configuration)
        {
            return new AutoModelBuilder(configuration);
        }

        ///// <summary>
        ///// Map classes from this assembly
        ///// </summary>
        ///// <returns>AutoModelBuilder</returns>
        //public static AutoModelBuilder ThisAssembly()
        //{
        //    return new AutoModelBuilder().AddEntitiesFromThisAssembly();
        //}

        ///// <summary>
        ///// Map classes from this assembly with supplied configuration
        ///// </summary>
        ///// <param name="configuration">Configuration to use</param>
        ///// <returns>AutoModelBuilder</returns>
        //public static AutoModelBuilder ThisAssembly(IEntityAutoConfiguration configuration)
        //{
        //    return new AutoModelBuilder(configuration).AddEntitiesFromThisAssembly();
        //}
    }
}