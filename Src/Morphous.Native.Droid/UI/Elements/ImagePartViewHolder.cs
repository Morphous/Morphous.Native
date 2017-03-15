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
using FFImageLoading.Views;
using FFImageLoading;
using GalaSoft.MvvmLight.Messaging;
using Morphous.Native.Droid.Events;

namespace Morphous.Native.Droid.UI.Elements
{
    public class ImagePartViewHolder : ElementViewHolder<IImagePart>
    {
        private readonly IMessenger _messenger;

        public ImagePartViewHolder(Context context, LayoutInflater inflater, ViewGroup container, IImagePart element) : this(context, inflater, container, element, null)
        {
        }

        public ImagePartViewHolder(Context context, LayoutInflater inflater, ViewGroup container, IImagePart element, IMessenger messenger) : base(context, inflater, container, element)
        {
            _messenger = messenger ?? GalaSoft.MvvmLight.Messaging.Messenger.Default;
        }

        protected override void BindView(View view)
        {
            base.BindView(view);

            var imageView = view.FindViewById<ImageViewAsync>(Resource.Id.imagePart_image);
            imageView.ScaleToFit = true;

            if (Element.Zone.ContentItem.DisplayType == "Summary")
            {
                imageView.Click += ImageView_Click;
            }

            ImageService.Instance.LoadUrl($"http://192.168.0.22:96{Element.Url}")
                .Retry(3, 200)
                .Into(imageView);
        }

        private void ImageView_Click(object sender, EventArgs e)
        {
            _messenger.Send(new ContentItemSelectedMessage(Element.Zone.ContentItem));
        }
    }
}