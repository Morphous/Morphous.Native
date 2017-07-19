using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Morphous.Native.Models
{
    public interface IContentField : IContentElement
    {
        string Name { get; }
    }

    public class ContentField : ContentElement, IContentField
    {
        public string Name { get; internal set; }
    }
}
