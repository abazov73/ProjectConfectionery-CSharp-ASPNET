using ConfectioneryContracts.StoragesContracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ConfectioneryFileImplement.Implements
{
    public class BackUpInfo : IBackUpInfo
    {
        public readonly DataFileSingleton _source;

        public BackUpInfo()
        {
            _source = DataFileSingleton.GetInstance();
        }

        public List<T>? GetList<T>() where T : class, new()
        {
            Type type = typeof(T);
            var list = _source.GetType().GetProperties(BindingFlags.Instance | BindingFlags.Public)
                .FirstOrDefault(x => x.PropertyType.IsGenericType && x.PropertyType.GetGenericArguments().First() == type)?.GetValue(_source);
            return (List<T>?)list;
        }

        public Type? GetTypeByModelInterface(string modelInterfaceName)
        {
            var assembly = typeof(BackUpInfo).Assembly;
            var types = assembly.GetTypes();
            foreach (var type in types)
            {
                if (type.IsClass && type.GetInterface(modelInterfaceName) != null)
                {
                    return type;
                }
            }
            return null;
        }
    }
}
