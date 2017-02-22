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
    [JsonConverter(typeof(ElementConverter))]
    public class ContentElementDto
    {
        [DataMember(Name = "type")]
        public string Type { get; set; }
    }
}
