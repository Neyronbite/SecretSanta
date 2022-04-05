using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecretSanta.BLL.Configuration
{
    static class AutoMapperExtensions
    {
        public static T MapTo<T>(this object obj)
        {
            return AutoMapperConfig.Instance.Map<T>(obj);
        }
        public static IEnumerable<T> MapTo<T>(this IEnumerable<object> objs)
        {
            foreach (var obj in objs)
            {
                yield return AutoMapperConfig.Instance.Map<T>(obj);
            }
        }
    }
}
