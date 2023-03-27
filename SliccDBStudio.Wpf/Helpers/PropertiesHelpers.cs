using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SliccDBStudio.Helpers
{
    public static class PropertiesHelpers
    {
        public  static bool TryConvertProperties(List<Data.Property> properties, out Dictionary<string, string> propertiesDictionary)
        {
            var query = properties.GroupBy(x => x)
                .Where(g => g.Count() > 1)
                .ToDictionary(x => x.Key, y => y.Count());
            if (query.Count > 0)
            {
                propertiesDictionary = null;
                return false;
            }
            propertiesDictionary = properties.OrderBy(prop => prop.Name).ToDictionary(k => k.Name, v => v.Value);
            return true;
        }
    }
}
