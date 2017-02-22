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

namespace Morphous.Native.Droid.Factories
{
    public interface IElementViewHolderFactory
    {
        ElementViewHolder Create(Context context, LayoutInflater inflater, ViewGroup zoneLayout, IContentElement element);
    }
}