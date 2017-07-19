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
using GalaSoft.MvvmLight.Helpers;
using Android.Text;
using Java.Lang;

namespace Morphous.Native.Droid.UI.Elements
{
    public class BodyPartViewHolder : ElementViewHolder<IBodyPart>
    {
        private TextView _htmlTextView;

        public BodyPartViewHolder(DisplayContext displayContext, ViewGroup container, IBodyPart element) : base(displayContext, container, element)
        {
        }

        protected override void BindView(View view)
        {
            base.BindView(view);
            _htmlTextView = view.FindViewById<TextView>(Resource.Id.bodyPart_html);
            Bindings.Add(this.SetBinding(() => Element.Html, () => _htmlTextView.TextFormatted).ConvertSourceToTarget(StringToHtml));
        }

        protected virtual ICharSequence StringToHtml(string arg)
        {
            var html = Html.FromHtml(arg);

            var end = html.Length();
            while (end > 0 && Character.IsWhitespace(html.CharAt(end - 1)))
            {
                end--;
            }

            return html.SubSequenceFormatted(0, end);
        }
    }
}