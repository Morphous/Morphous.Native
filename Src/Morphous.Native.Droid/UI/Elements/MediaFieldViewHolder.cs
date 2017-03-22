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
        private ContentItemViewHolder _contentItemViewHolder;

        public MediaFieldViewHolder(DisplayContext displayContext, ViewGroup container, IMediaField element) : base(displayContext, container, element)
        {
        }

        protected override void BindView(View view)
        {
            base.BindView(view);
            var mediaContainer = view.FindViewById<ViewGroup>(Resource.Id.mediaField_media);

            _contentItemViewHolder = DisplayContext.ViewHolderFactory.CreateContentItemViewHolder(Container, Element.Media);

            mediaContainer.AddView(_contentItemViewHolder.View);
        }

        public override void Dispose()
        {
            base.Dispose();
            _contentItemViewHolder?.Dispose();
        }
    }
}