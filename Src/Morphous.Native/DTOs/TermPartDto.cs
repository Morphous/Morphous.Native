using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Morphous.Native.DTOs
{
    [DataContract]
    public class TermPartDto : ContentPartDto
    {
        [DataMember(Name = "items")]
        public IList<ContentItemDto> ContentItems { get; set; } = new List<ContentItemDto>();
    }
}
