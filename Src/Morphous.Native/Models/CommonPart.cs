using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Morphous.Native.Models
{
    public interface ICommonPart : IContentElement
    {
        int Id { get; }
        string ResourceUrl { get; }
        string CreatedUtc { get; }
        string PublishedUtc { get; }
    }

    public class CommonPart : ContentPart, ICommonPart
    {
        public int Id { get; internal set; }
        public string ResourceUrl { get; internal set; }
        public string CreatedUtc { get; internal set; }
        public string PublishedUtc { get; internal set; }
    }
}
