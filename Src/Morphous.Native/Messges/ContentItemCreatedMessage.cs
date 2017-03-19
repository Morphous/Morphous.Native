using Morphous.Native.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Morphous.Native.Messges
{
    public class ContentItemCreatedMessage
    {
        public IContentItem ContentItem { get; }

        public ContentItemCreatedMessage(IContentItem contentItem)
        {
            ContentItem = contentItem;
        }
    }
}