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
using GalaSoft.MvvmLight.Messaging;
using Morphous.Native.Droid.Factories;
using Morphous.Native.Droid.UI;

namespace Morphous.Native.Droid
{
    public class DisplayContext
    {
        public Activity Activity { get; set; }
        public LayoutInflater Inflater { get; set; }
        public ViewGroup RootContainer { get; set; }
        public IMessenger Messenger { get; set; }
        public IViewHolderFactory ViewHolderFactory { get; set;}
        public IContentItem RootContentItem { get; set; }

        public ContentItemViewHolder RootContentItemViewHolder()
        {
            return ViewHolderFactory.CreateContentItemViewHolder(RootContainer, RootContentItem);
        }
    }
}