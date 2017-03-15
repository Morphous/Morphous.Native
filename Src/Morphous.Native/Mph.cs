using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Morphous.Native
{
    public static class Mph
    {
        private static string _baseUrl;
        public static string BaseUrl
        {
            get
            {
                if (_baseUrl == null)
                    throw new InvalidOperationException("The base url must be set 'Mph.BaseUrl'");
                return _baseUrl;
            }
            set { _baseUrl = value; }
        }
    }
}
