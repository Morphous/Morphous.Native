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
        private Binding _htmlBinding;

        public BodyPartViewHolder(Context context, LayoutInflater inflater, ViewGroup container, IBodyPart element) : base(context, inflater, container, element)
        {
        }

        protected override void BindView(View view)
        {
            base.BindView(view);
            _htmlTextView = view.FindViewById<TextView>(Resource.Id.bodyPart_html);
            _htmlBinding = this.SetBinding(() => Element.Html, () => _htmlTextView.TextFormatted).ConvertSourceToTarget(StringToHtml);
        }

        private ICharSequence StringToHtml(string arg)
        {
            return Html.FromHtml(arg);
        }
    }
}