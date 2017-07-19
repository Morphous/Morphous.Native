using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Morphous.Native.Models
{
    public interface ITitlePart : IContentPart
    {
        string Title { get; }
    }

    public class TitlePart : ContentPart, ITitlePart
    {
        public string Title { get; internal set; }
    }
}
