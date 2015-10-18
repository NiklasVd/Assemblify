using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assemblify.Core
{
    public class SceneResourceLoader : IResourceLoader<Scene>
    {
        public Scene Load(string path)
        {
            var fileContent = File.ReadAllText(path);
            return JsonConvert.DeserializeObject<Scene>(fileContent);
        }
    }
}
