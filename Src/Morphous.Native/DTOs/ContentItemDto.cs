using Newtonsoft.Json;
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
        [DataMember(Name = "id")]
        public int Id { get; set; }

        [DataMember(Name = "contentType")]
        public string ContentType { get; set; }

        [DataMember(Name = "displayType")]
        public string DisplayType { get; set; }

        [DataMember(Name = "zones")]
        public IList<ZoneDto> Zones { get; set; }
    }
}
