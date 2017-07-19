using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Morphous.Native.Models
{
    public interface ITermPart : IContentPart
    {
        IList<IContentItem> ContentItems { get; }
    }

    public class TermPart : ContentPart, ITermPart
    {
        public IList<IContentItem> ContentItems { get; } = new List<IContentItem>();
    }
}
