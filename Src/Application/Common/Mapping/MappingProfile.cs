using System.Reflection;
using AutoMapper;

namespace Application.Common.Mapping;

public class MappingProfile:Profile
{
    public MappingProfile()
    {
        ApplyMappingFromAssembly(Assembly.GetExecutingAssembly());
    }

   

    private void ApplyMappingFromAssembly(Assembly assembly)
    {
        //find from stackoverflow
        var mapFromType = typeof(IMapFrom<>);
        var mappingMethodName = nameof(IMapFrom<object>.Mapping);
        bool HasInterface(Type t) => t.IsGenericType && t.GetGenericTypeDefinition() == mapFromType;
        var types = assembly.GetExportedTypes().Where(t => t.GetInterfaces().Any(HasInterface)).ToList();
        var argumenttypes = new Type[] { typeof(Profile) };
        foreach (var type in types)
        {
            var instance = Activator.CreateInstance(type);
            var methodinfo = type.GetMethod(mappingMethodName);
            if (methodinfo!=null)
            {
                methodinfo.Invoke(instance, new object[] { this });
            }
            else
            {
                var interfaces = type.GetInterfaces().Where(HasInterface).ToList();
                if (interfaces.Count<=0)
                {
                    continue;
                }

                foreach (var interfacemethodinfo in interfaces.Select(@interface=>@interface.GetMethod(mappingMethodName,argumenttypes)))
                {
                    interfacemethodinfo?.Invoke(instance, new object[] {this });
                }
            }
        }
    }
}
