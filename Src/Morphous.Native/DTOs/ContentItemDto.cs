using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Morphous.Native.DTOs
{
    [DataContract]
    public class ContentItemDto
    {
        [DataMember(Name = "contentType")]
        public string ContentType { get; set; }

        [DataMember(Name = "displayType")]
        public string DisplayType { get; set; }

        [DataMember(Name = "zones")]
        public IList<ZoneDto> Zones { get; set; }
    }

    [DataContract]
    public class ZoneDto
    {
        [DataMember(Name = "name")]
        public string Name { get; set; }

        [DataMember(Name = "items")]
        public IList<ContentElementDto> Elements { get; set; }

    }

    [DataContract]
    public class ContentElementDto
    {
        [DataMember(Name = "type")]
        public string Type { get; set; }
    }
}
