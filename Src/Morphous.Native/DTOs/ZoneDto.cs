using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Morphous.Native.DTOs
{
    [DataContract]
    public class ZoneDto
    {
        [DataMember(Name = "name")]
        public string Name { get; set; }

        [DataMember(Name = "items")]
        public IList<ContentElementDto> Elements { get; set; }

    }
}
