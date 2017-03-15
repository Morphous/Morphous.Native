using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Morphous.Native.Models
{
    public interface IImagePart : IContentPart
    {
        string Url { get; }
        string AlternateText { get; }
        int Width { get; }
        int Height { get; }
    }

    public class ImagePart : ContentPart, IImagePart
    {
        public string Url { get; internal set; }
        public string AlternateText { get; internal set; }
        public int Width { get; internal set; }
        public int Height { get; internal set; }
    }
}

