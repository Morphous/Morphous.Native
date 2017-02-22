using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Morphous.Native.Models
{
    public interface IBodyPart : IContentPart
    {
        string Html { get; }
    }

    public class BodyPart : ContentPart, IBodyPart
    {
        public string Html { get; internal set; }
    }
}
