using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Morphous.Native.Models;

namespace Morphous.Native.Droid.Events
{
    public class ContentItemSelectedMessage
    {
        public IContentItem ContentItem { get; }

        public ContentItemSelectedMessage(IContentItem contentItem)
        {
            ContentItem = contentItem;
        }
    }
}