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
        int? Id { get; }

        string ContentType { get; }

        string DisplayType { get; }

        IList<IZone> Zones { get; }

        TElement As<TElement>() where TElement : IContentElement;
    }

    public class ContentItem : ObservableObject, IContentItem
    {
        public int? Id => As<ICommonPart>()?.Id;

        public string ContentType { get; set; }
        
        public string DisplayType { get;  set; }

        public IList<IZone> Zones { get; } = new List<IZone>();

        public TElement As<TElement>() where TElement : IContentElement
        {
            return (TElement)Zones.SelectMany(z => z.Elements).FirstOrDefault(e => e is TElement);
        }
    }
}
