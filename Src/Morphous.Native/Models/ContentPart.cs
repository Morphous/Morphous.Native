using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Morphous.Native.Models
{
    public interface IContentPart : IContentElement
    {
    }

    public class ContentPart : ContentElement, IContentPart
    {
    }
}
