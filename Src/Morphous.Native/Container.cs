using Morphous.Native.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Morphous.Native
{
    public class Container
    {
        public static IContentRequester ContentRequester { get; } = new ContentRequester();
    }
}
