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
using Morphous.Native.Droid.Bindings;
using Morphous.Native.Droid.Factories;

namespace Morphous.Native.Droid.UI.Elements
{
    class MediaFieldViewHolder : ElementViewHolder<IMediaField>
    {
        private readonly IViewHolderFactory _viewHolderFactory;

        private ContentItemViewHolder _contentItemViewHolder;

        public MediaFieldViewHolder(Context context, LayoutInflater inflater, ViewGroup container, IViewHolderFactory viewHolderFactory, IMediaField element) : base(context, inflater, container, element)
        {
            _viewHolderFactory = viewHolderFactory;
        }

        protected override View CreateView()
        {
            _contentItemViewHolder = _viewHolderFactory.CreateContentItemViewHolder(Context, Inflater, Container, Element.Media);
            return _contentItemViewHolder.View;
        }

        public override void Dispose()
        {
            base.Dispose();
            _contentItemViewHolder?.Dispose();
        }
    }
}