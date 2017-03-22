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
using GalaSoft.MvvmLight.Helpers;

namespace Morphous.Native.Droid.UI.Elements
{
    public class BooleanFieldViewHolder : ElementViewHolder<IBooleanField>
    {
        private TextView _nameTextView;
        private TextView _valueTextView;

        public BooleanFieldViewHolder(DisplayContext displayContext, ViewGroup container, IBooleanField element) : base(displayContext, container, element)
        {
        }

        protected override void BindView(View view)
        {
            base.BindView(view);
            _nameTextView = view.FindViewById<TextView>(Resource.Id.booleanField_name);
            _valueTextView = view.FindViewById<TextView>(Resource.Id.booleanField_value);
            Bindings.Add(this.SetBinding(() => Element.Name, () => _nameTextView.Text));
            Bindings.Add(this.SetBinding(() => Element.Value, () => _valueTextView.Text));
        }
    }
}