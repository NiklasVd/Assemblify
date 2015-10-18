using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assemblify.Core
{
    public class ResourceDictionary<T>
    {
        private IResourceLoader<T> resourceLoader;
        private readonly Dictionary<string, T> resources;

        public T this [string nameKey]
        {
            get { return resources[nameKey]; }
        }

        public ResourceDictionary(IResourceLoader<T> resourceLoader)
        {
            this.resourceLoader = resourceLoader;
            resources = new Dictionary<string, T>();
        }

        public void Load(string path)
        {
            var nameKey = path;
            var resourceValue = resourceLoader.Load(path);

            resources.Add(nameKey, resourceValue);
        }
        public bool Remove(string nameKey)
        {
            return resources.Remove(nameKey);
        }
    }
}
