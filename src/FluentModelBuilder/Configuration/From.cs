using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using FluentModelBuilder.AutoModelBuilder;
using FluentModelBuilder.AutoModelBuilder.Sources;

namespace FluentModelBuilder.Configuration
{
    public static class From
    {
        public static AutoModelBuilder.AutoModelBuilder Source(ITypeSource source)
        {
            return new AutoModelBuilder.AutoModelBuilder().AddTypeSource(source);
        }

        public static AutoModelBuilder.AutoModelBuilder Source(ITypeSource source, IEntityAutoConfiguration configuration)
        {
            return new AutoModelBuilder.AutoModelBuilder(configuration).AddTypeSource(source);
        }

        public static AutoModelBuilder.AutoModelBuilder Assemblies(params Assembly[] assemblies)
        {
            return Source(new CombinedAssemblyTypeSource(assemblies.Select(x => new AssemblyTypeSource(x))));
        }

        public static AutoModelBuilder.AutoModelBuilder Assemblies(IEntityAutoConfiguration configuration, params Assembly[] assemblies)
        {
            return Source(new CombinedAssemblyTypeSource(assemblies.Select(x => new AssemblyTypeSource(x))),
                configuration);
        }

        public static AutoModelBuilder.AutoModelBuilder Assemblies(IEntityAutoConfiguration configuration,
            IEnumerable<Assembly> assemblies)
        {
            return Source(new CombinedAssemblyTypeSource(assemblies.Select(x => new AssemblyTypeSource(x))),
                configuration);
        }

        public static AutoModelBuilder.AutoModelBuilder Assembly(Assembly assembly)
        {
            return Source(new AssemblyTypeSource(assembly));
        }

        public static AutoModelBuilder.AutoModelBuilder Assembly(Assembly assembly, IEntityAutoConfiguration configuration)
        {
            return Source(new AssemblyTypeSource(assembly), configuration);
        }

        public static AutoModelBuilder.AutoModelBuilder AssemblyOf<T>()
        {
            return Assembly(typeof(T).GetTypeInfo().Assembly);
        }

        public static AutoModelBuilder.AutoModelBuilder AssemblyOf<T>(IEntityAutoConfiguration configuration)
        {
            return Assembly(typeof (T).GetTypeInfo().Assembly, configuration);
        }
    }
}