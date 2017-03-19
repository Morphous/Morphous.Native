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
using Morphous.Native.Droid.UI.Elements;
using GalaSoft.MvvmLight.Messaging;
using Morphous.Native.Droid.UI;

namespace Morphous.Native.Droid.Factories
{
    public interface IViewHolderFactory
    {
        ContentItemViewHolder CreateContentItemViewHolder(Context context, LayoutInflater inflater, ViewGroup zoneLayout, IContentItem contentItem);
        ElementViewHolder CreateElementViewHolder(Context context, LayoutInflater inflater, ViewGroup zoneLayout, IContentElement element);
    }
}