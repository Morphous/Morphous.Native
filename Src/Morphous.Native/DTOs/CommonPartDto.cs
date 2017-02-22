using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Morphous.Native.DTOs
{
    [DataContract]
    public class CommonPartDto : ContentElementDto
    {        
        [DataMember(Name = "id")]
        public int Id { get; set; }

        [DataMember(Name = "resourceUrl")]
        public string ResourceUrl { get; set; }

        [DataMember(Name = "createdUtc")]
        public string CreatedUtc { get; set; }

        [DataMember(Name = "publishedUtc")]
        public string PublishedUtc { get; set; }
    }
}
