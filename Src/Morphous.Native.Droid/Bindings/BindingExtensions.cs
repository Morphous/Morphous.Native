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
using System.Linq.Expressions;

namespace Morphous.Native.Droid.Bindings
{
    public static class BindingExtensions
    {
        public static ContentItemBinding SetContentBinding(
            this object source,
            Expression<Func<IContentItem>> sourcePropertyExpression,
            Expression<Func<View>> targetPropertyExpression)
        {
            return new ContentItemBinding(source, sourcePropertyExpression, targetPropertyExpression);
        }
    }
}