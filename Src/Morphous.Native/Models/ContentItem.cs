using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Morphous.Native.Models
{
    public interface IContentItem
    {
        string ContentType { get; }

        string DisplayType { get; }

        IList<IZone> Zones { get; }
    }

    public class ContentItem : ObservableObject, IContentItem
    {
        public string ContentType { get; set; }
        
        public string DisplayType { get;  set; }

        public IList<IZone> Zones { get; } = new List<Zone>();
    }
}
