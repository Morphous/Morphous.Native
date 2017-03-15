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

namespace Morphous.Native.Droid.UI.Elements
{
    class MediaFieldViewHolder : ElementViewHolder<IMediaField>
    {
        public MediaFieldViewHolder(Context context, LayoutInflater inflater, ViewGroup container, IMediaField element) : base(context, inflater, container, element)
        {
        }

        protected override View CreateView()
        {
            return Inflater.Inflate(Resource.Layout.view_content_item, Container, false);
        }

        protected override void BindView(View view)
        {
            base.BindView(view);
            Bindings.Add(this.SetContentBinding(() => Element.Media, () => view));
        }
    }
}