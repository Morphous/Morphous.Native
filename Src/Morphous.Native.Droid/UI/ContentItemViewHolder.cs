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
using Morphous.Native.Droid.Messages;
using Morphous.Native.Droid.Factories;
using GalaSoft.MvvmLight.Messaging;
using Morphous.Native.Droid.UI.Elements;

namespace Morphous.Native.Droid.UI
{
    public class ContentItemViewHolder : IDisposable
    {
        private readonly List<ElementViewHolder> _elementViewHolders = new List<ElementViewHolder>();

        protected DisplayContext DisplayContext { get; }
        protected ViewGroup Container { get; }
        protected IContentItem ContentItem { get; }

        public ContentItemViewHolder(
            DisplayContext displayContext,
            ViewGroup container,
            IContentItem contentItem)
        {
            DisplayContext = displayContext;
            Container = container;
            ContentItem = contentItem;
        }

        private View _view;
        public View View
        {
            get
            {
                if (_view == null)
                {
                    _view = CreateView();
                }

                return _view;
            }
        }

        protected virtual View CreateView()
        {
            var contentItemView = GetTemplate();

            foreach (var zone in ContentItem.Zones)
            {
                var zoneLayout = contentItemView.FindViewById<ViewGroup>(DisplayContext.Context.Resources.GetIdentifier(zone.Name, "id", DisplayContext.Context.PackageName));

                if (zoneLayout != null)
                {
                    foreach (var element in zone.Elements)
                    {
                        var elementViewHolder = DisplayContext.ViewHolderFactory.CreateElementViewHolder(zoneLayout, element);
                        _elementViewHolders.Add(elementViewHolder);
                        zoneLayout.AddView(elementViewHolder.View);
                    }
                }
            }

            if (ContentItem.DisplayType == "Summary" && contentItemView.Clickable)
            {
                contentItemView.Click += (object sender, EventArgs e) => DisplayContext.Messenger.Send(new ContentItemSelectedMessage(ContentItem));
            }

            return contentItemView;
        }

        private View GetTemplate()
        {
            foreach (var alternate in ContentItem.Alternates)
            {
                var layoutId = DisplayContext.Context.Resources.GetIdentifier(alternate.ToLower(), "layout", DisplayContext.Context.PackageName);
                if (layoutId > 0)
                {
                    return DisplayContext.Inflater.Inflate(layoutId, Container, false);
                }
            }

            throw new InflateException("Couldn't find any content item templates to inflate.");
        }

        public void Dispose()
        {
            foreach (var elementViewHolder in _elementViewHolders)
            {
                elementViewHolder.Dispose();
            }
        }
    }
}