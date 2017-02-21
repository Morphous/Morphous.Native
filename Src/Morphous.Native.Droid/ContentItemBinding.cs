using Android.App;
using Android.Content;
using Android.Views;
using Android.Widget;
using GalaSoft.MvvmLight.Helpers;
using Morphous.Native.Models;
using System;
using System.Linq.Expressions;

namespace Morphous.Native.Droid
{
    public class ContentItemBinding : Binding<IContentItem, View>
    {
        private readonly WeakReference<Context> _weakContext;
        private readonly Func<IContentItem> _sourcePropertyFunc;
        private readonly Func<View> _targetPropertyFunc;

        public ContentItemBinding(
            object source,
            Context context,
            Expression<Func<IContentItem>> sourcePropertyExpression,
            Expression<Func<View>> targetPropertyExpression)
            : base(
                source,
                sourcePropertyExpression,
                null,
                null,
                BindingMode.OneWay,
                null,
                null)
        {
            _weakContext = new WeakReference<Context>(context);
            _sourcePropertyFunc = sourcePropertyExpression.Compile();
            _targetPropertyFunc = targetPropertyExpression.Compile();
            this.WhenSourceChanges(Update);
        }

        private void Update()
        {
            var contentItem = _sourcePropertyFunc();
            var view = _targetPropertyFunc();

            if (contentItem == null)
                return;

            Context context;
            if (!_weakContext.TryGetTarget(out context))
                return;

            foreach (var zone in contentItem.Zones)
            {
                var zoneLayout = view.FindViewById<ViewGroup>(context.Resources.GetIdentifier(zone.Name, "id", context.PackageName));

                if (zoneLayout != null)
                {
                    foreach (var element in zone.Elements)
                    {
                        var textView = new TextView(context);
                        textView.Text = element.Type;

                        zoneLayout.AddView(textView);
                    }
                }
            }
        }
    }
}