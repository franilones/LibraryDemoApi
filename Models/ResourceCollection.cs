using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LibraryDemoApi.Models
{
    public class ResourceCollection<T> : Resource where T : Resource
    {
        public List<T> Values { get; set; }
        public ResourceCollection(List<T> values)
        {
            Values = values;
        }
    }
}
