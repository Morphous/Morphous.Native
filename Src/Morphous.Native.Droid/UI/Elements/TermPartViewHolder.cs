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

namespace Morphous.Native.Droid.UI.Elements
{
    public class TermPartViewHolder : ElementViewHolder<ITermPart>
    {

        public TermPartViewHolder(Context context, LayoutInflater inflater, ViewGroup container, ITermPart element) : base(context, inflater, container, element)
        {
        }

        protected override void BindView(View view)
        {
            base.BindView(view);

        }
    }
}