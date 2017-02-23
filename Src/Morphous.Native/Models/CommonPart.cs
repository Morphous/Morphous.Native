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
        DateTime CreatedDate { get; }
        DateTime PublishedDate { get; }
    }

    public class CommonPart : ContentPart, ICommonPart
    {
        public int Id { get; internal set; }
        public string ResourceUrl { get; internal set; }
        public DateTime CreatedDate { get; internal set; }
        public DateTime PublishedDate { get; internal set; }
    }
}
