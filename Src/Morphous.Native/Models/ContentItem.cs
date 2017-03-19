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
        int Id { get; }

        string ContentType { get; }

        string DisplayType { get; }

        IList<string> Alternates { get; }

        IList<IContentZone> Zones { get; }

        TElement As<TElement>() where TElement : IContentElement;
    }

    public class ContentItem : ObservableObject, IContentItem
    {
        public int Id { get; internal set; }

        public string ContentType { get; internal set; }
        
        public string DisplayType { get; internal set; }

        public IList<string> Alternates { get; } = new List<string>();

        public IList<IContentZone> Zones { get; } = new List<IContentZone>();

        public TElement As<TElement>() where TElement : IContentElement
        {
            return (TElement)Zones.SelectMany(z => z.Elements).FirstOrDefault(e => e is TElement);
        }
    }
}
