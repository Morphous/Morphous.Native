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

namespace Morphous.Native.Droid.UI.Elements
{
    public class CommonPartViewHolder : ElementViewHolder<ICommonPart>
    {
        private TextView _idTextView;
        private TextView _resourceUrlTextView;
        private TextView _createdDateTextView;
        private TextView _publishedDateTextView;

        public CommonPartViewHolder(DisplayContext displayContext, ViewGroup container, ICommonPart element) : base(displayContext, container, element)
        {
        }

        protected override void BindView(View view)
        {
            base.BindView(view);
            _idTextView = view.FindViewById<TextView>(Resource.Id.commonPart_id);
            _resourceUrlTextView = view.FindViewById<TextView>(Resource.Id.commonPart_resourceUrl);
            _createdDateTextView = view.FindViewById<TextView>(Resource.Id.commonPart_createdDate);
            _publishedDateTextView = view.FindViewById<TextView>(Resource.Id.commonPart_publishedDate);

            Bindings.Add(this.SetBinding(() => Element.Id, () => _idTextView.Text));
            Bindings.Add(this.SetBinding(() => Element.ResourceUrl, () => _resourceUrlTextView.Text));
            Bindings.Add(this.SetBinding(() => Element.CreatedDate, () => _createdDateTextView.Text).ConvertSourceToTarget(FormatDate));
            Bindings.Add(this.SetBinding(() => Element.PublishedDate, () => _publishedDateTextView.Text).ConvertSourceToTarget(FormatDate));
        }

        protected virtual string FormatDate(DateTime arg)
        {
            return arg.ToString("dd MMM yyyy");
        }
    }
}