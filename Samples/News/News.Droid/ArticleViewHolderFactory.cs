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
using Morphous.Native.Droid;
using Morphous.Native.Droid.Factories;
using Morphous.Native.Droid.UI.Elements;
using Morphous.Native.Models;

namespace News.Droid
{
    public class ArticleViewHolderFactory : DefaultViewHolderFactory
    {
        private readonly DisplayContext _displayContext;

        public ArticleViewHolderFactory(DisplayContext displayContext) : base(displayContext)
        {
            _displayContext = displayContext;
        }

        public override ElementViewHolder CreateElementViewHolder(ViewGroup container, IContentElement element)
        {
            if (element is IBodyPart)
            {
                return new WebViewBodyPartViewHolder(_displayContext, container, element as IBodyPart);
            }

            return base.CreateElementViewHolder(container, element);
        }
    }
}