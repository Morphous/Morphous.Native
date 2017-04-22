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
using Morphous.Native.Droid.UI.Elements;
using Morphous.Native.Models;
using Android.Webkit;

namespace News.Droid
{
    class WebViewBodyPartViewHolder : ElementViewHolder<IBodyPart>
    {
        public WebViewBodyPartViewHolder(DisplayContext displayContext, ViewGroup container, IBodyPart element) : base(displayContext, container, element)
        {
        }

        protected override void BindView(View view)
        {
            var webView = view.FindViewById<WebView>(Resource.Id.webView);
            webView.LoadData(Element.Html, "text/html", "UTF-8");
        }
    }
}