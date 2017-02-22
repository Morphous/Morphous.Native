using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Morphous.Native.DTOs
{
    [DataContract]
    public class BodyPartDto : ContentPartDto
    {
        [DataMember(Name = "html")]
        public string Html { get; set; }
    }
}
