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

namespace Morphous.Native.Droid.UI.Elements
{
    public class ImagePartViewHolder : ElementViewHolder<IImagePart>
    {
        public ImagePartViewHolder(DisplayContext displayContext, ViewGroup container, IImagePart element) : base(displayContext, container, element)
        {
        }

        protected override void BindView(View view)
        {
            base.BindView(view);
            var imageView = view.FindViewById<ImageViewAsync>(Resource.Id.imagePart_image);

            ImageService.Instance.LoadUrl($"{Mph.BaseUrl}{Element.Url}")
                .Retry(3, 200)
                .Into(imageView);
        }
    }
}