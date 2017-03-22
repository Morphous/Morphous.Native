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
using Android.Support.V7.Widget;

namespace Morphous.Native.Droid.UI.Elements
{
    public class TermPartViewHolder : ElementViewHolder<ITermPart>
    {
        private RecyclerView _recyclerView;

        public TermPartViewHolder(DisplayContext displayContext, ViewGroup container, ITermPart element) : base(displayContext, container, element)
        {
        }

        protected override void BindView(View view)
        {
            base.BindView(view);
            var adapter = new TermAdapater(DisplayContext, Element.ContentItems);

            _recyclerView = view.FindViewById<RecyclerView>(Resource.Id.recycler_view);
            _recyclerView.AddItemDecoration(new SimpleDivider(DisplayContext.Activity));
            _recyclerView.SetAdapter(adapter);
        }

        public override void Dispose()
        {
            base.Dispose();
            //TODO detach the bindings for each child item
        }



        private class TermAdapater : RecyclerView.Adapter
        {
            private readonly DisplayContext _displayContext;
            private readonly IList<IContentItem> _contentItems;

            public TermAdapater(DisplayContext displayContext, IList<IContentItem> contentItems)
            {
                _displayContext = displayContext;
                _contentItems = contentItems;
            }

            public override int ItemCount => _contentItems.Count;

            public override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType)
            {
                var itemView = _displayContext.Inflater.Inflate(Resource.Layout.view_content_item, parent, false);
                return new ContentItemHolder(itemView, _displayContext);
            }

            public override void OnBindViewHolder(RecyclerView.ViewHolder holder, int position)
            {
                var contentItemHolder = (ContentItemHolder)holder;
                contentItemHolder.Bind(_contentItems[position]);
            }
        }

        public class ContentItemHolder : RecyclerView.ViewHolder
        {
            private readonly DisplayContext _displayContext;

            private ContentItemViewHolder _contentItemViewHolder;

            public ContentItemHolder(View itemView, DisplayContext displayContext) : base(itemView)
            {
                _displayContext = displayContext;
            }

            public void Bind(IContentItem contentItem)
            {
                _contentItemViewHolder?.Dispose();

                var container = ItemView.FindViewById<ViewGroup>(Resource.Id.contentItem_container);

                _contentItemViewHolder = _displayContext.ViewHolderFactory.CreateContentItemViewHolder(container, contentItem);

                container.AddView(_contentItemViewHolder.View);
            }
        }
    }
}