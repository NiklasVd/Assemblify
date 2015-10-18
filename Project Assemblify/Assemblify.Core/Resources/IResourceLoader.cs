using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assemblify.Core
{
    public interface IResourceLoader<T>
    {
        T Load(string path);
    }
}
