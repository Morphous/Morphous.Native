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
using Morphous.Native.Droid.Bindings;

namespace Morphous.Native.Droid.UI.Elements
{
    public class TermPartViewHolder : ElementViewHolder<ITermPart>
    {
        private RecyclerView _recyclerView;

        public TermPartViewHolder(Context context, LayoutInflater inflater, ViewGroup container, ITermPart element) : base(context, inflater, container, element)
        {
        }

        protected override void BindView(View view)
        {
            base.BindView(view);

            var linearLayoutParams = Container.LayoutParameters as LinearLayout.LayoutParams;

            if (linearLayoutParams != null)
            {
                linearLayoutParams.Height = 0;
                linearLayoutParams.Weight = 1;
            }

            _recyclerView = view.FindViewById<RecyclerView>(Resource.Id.recycler_view);
            _recyclerView.SetAdapter(new TermAdapater(Inflater, Element.ContentItems));
        }

        private class TermAdapater : RecyclerView.Adapter
        {
            private readonly LayoutInflater _inflater;
            private readonly IList<IContentItem> _contentItems;

            public TermAdapater(LayoutInflater inflater, IList<IContentItem> contentItems)
            {
                _inflater = inflater;
                _contentItems = contentItems;
            }

            public override int ItemCount => _contentItems.Count;

            public override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType)
            {
                var itemView = _inflater.Inflate(Resource.Layout.ChildContentItem, parent, false);
                var viewHolder = new ContentItemHolder(itemView);
                
                return viewHolder;
            }

            public override void OnBindViewHolder(RecyclerView.ViewHolder holder, int position)
            {
                var contentItemHolder = (ContentItemHolder)holder;
                contentItemHolder.Bind(_contentItems[position]);
            }
        }

        public class ContentItemHolder : RecyclerView.ViewHolder
        {
            public IContentItem ContentItem { get; set; }

            public ContentItemHolder(View itemView) : base(itemView)
            {
            }

            public void Bind(IContentItem contentItem)
            {
                ContentItem = contentItem;

                //TODO store binding
                this.SetContentBinding(() => this.ContentItem, () => ItemView);
            }
        }
    }
}