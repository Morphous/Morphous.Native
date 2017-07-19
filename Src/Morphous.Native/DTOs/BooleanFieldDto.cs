using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Morphous.Native.DTOs
{
    [DataContract]
    public class BooleanFieldDto : ContentFieldDto
    {
        [DataMember(Name = "value")]
        public bool Value { get; set; }
    }
}
