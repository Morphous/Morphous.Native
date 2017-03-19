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

        protected Context Context { get; }
        protected LayoutInflater Inflater { get; }
        protected ViewGroup Container { get; }
        protected IViewHolderFactory ViewHolderFactory { get; }
        protected IMessenger Messenger { get; }
        protected IContentItem ContentItem { get; }

        public ContentItemViewHolder(
            Context context, 
            LayoutInflater inflater, 
            ViewGroup container, 
            IViewHolderFactory viewHolderFactory,
            IMessenger messenger,
            IContentItem contentItem)
        {
            Context = context;
            Inflater = inflater;
            Container = container;
            ViewHolderFactory = viewHolderFactory;
            Messenger = messenger;
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
                var zoneLayout = contentItemView.FindViewById<ViewGroup>(Context.Resources.GetIdentifier(zone.Name, "id", Context.PackageName));

                if (zoneLayout != null)
                {
                    foreach (var element in zone.Elements)
                    {
                        var elementViewHolder = ViewHolderFactory.CreateElementViewHolder(Context, Inflater, zoneLayout, element);
                        _elementViewHolders.Add(elementViewHolder);
                        zoneLayout.AddView(elementViewHolder.View);
                    }
                }
            }

            if (ContentItem.DisplayType == "Summary" && contentItemView.Clickable)
            {
                contentItemView.Click += (object sender, EventArgs e) => Messenger.Send(new ContentItemSelectedMessage(ContentItem));
            }

            return contentItemView;
        }

        private View GetTemplate()
        {
            foreach (var alternate in ContentItem.Alternates)
            {
                var layoutId = Context.Resources.GetIdentifier(alternate.ToLower(), "layout", Context.PackageName);
                if (layoutId > 0)
                {
                    return Inflater.Inflate(layoutId, Container, false);
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