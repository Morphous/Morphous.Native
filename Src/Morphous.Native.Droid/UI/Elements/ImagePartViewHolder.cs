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
        public ImagePartViewHolder(Context context, LayoutInflater inflater, ViewGroup container, IImagePart element) : this(context, inflater, container, element, null)
        {
        }

        protected override void BindView(View view)
        {
            base.BindView(view);
            var imageView = view.FindViewById<ImageViewAsync>(Resource.Id.imagePart_image);

            ImageService.Instance.LoadUrl($"http://192.168.0.22:96{Element.Url}")
                .Retry(3, 200)
                .Into(imageView);
        }
    }
}