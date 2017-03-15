using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Morphous.Native.Models
{
    public interface IMediaField : IContentField
    {
        IContentItem Media { get; }
    }

    public class MediaField : ContentField, IMediaField
    {
        public IContentItem Media { get; internal set; }
    }
}
