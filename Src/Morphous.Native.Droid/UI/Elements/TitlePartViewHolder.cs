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
using Android.Support.V7.App;

namespace Morphous.Native.Droid.UI.Elements
{
    public class TitlePartViewHolder : ElementViewHolder<ITitlePart>
    {
        private TextView _textView;

        public TitlePartViewHolder(DisplayContext displayContext, ViewGroup container, ITitlePart element) : base(displayContext, container, element)
        {
        }

        protected override View CreateView()
        {
            var toolbar = Container as Android.Support.V7.Widget.Toolbar;
            var appCompatActivity = DisplayContext.Activity as AppCompatActivity;

            if (toolbar != null && appCompatActivity != null)
            {
                toolbar.Title = Element.Title;
                appCompatActivity.SetSupportActionBar(toolbar);
                return null;
            }

            return base.CreateView();
        }

        protected override void BindView(View view)
        {
            base.BindView(view);

            if (view != null)
            {
                _textView = view.FindViewById<TextView>(Resource.Id.titlePart_title);
                Bindings.Add(this.SetBinding(() => Element.Title, () => _textView.Text));
            }
        }
    }
}