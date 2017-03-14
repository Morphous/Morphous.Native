using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Morphous.Native.Models
{
    public interface ITaxonomyPart : IContentPart
    {
        IList<ITaxonomyItem> Terms { get; }
    }

    public interface ITaxonomyItem
    {
        int Id { get; }

        string Title { get; }

        string DisplayUrl { get; }

        IList<ITaxonomyItem> Terms { get; }
    }

    public class TaxonomyPart : ContentPart, ITaxonomyPart
    {
        public IList<ITaxonomyItem> Terms { get; internal set; }
    }

    public class TaxonomyItem : ITaxonomyItem
    {
        public int Id { get; internal set; }

        public string Title { get; internal set; }

        public string DisplayUrl { get; internal set; }

        public IList<ITaxonomyItem> Terms { get; internal set; }
    }
}
