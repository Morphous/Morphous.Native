using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Morphous.Native.DTOs
{
    [DataContract]
    public class TitlePartDto : ContentElementDto
    {
        [DataMember(Name = "title")]
        public string Title { get; set; }
    }
}
