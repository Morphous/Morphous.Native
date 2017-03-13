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
using GalaSoft.MvvmLight.Messaging;
using Morphous.Native.Droid.Events;
using GalaSoft.MvvmLight.Helpers;

namespace Morphous.Native.Droid.UI.Elements
{
    public class TermPartViewHolder : ElementViewHolder<ITermPart>
    {
        private readonly IMessenger _messenger;

        private RecyclerView _recyclerView;

        public TermPartViewHolder(Context context, LayoutInflater inflater, ViewGroup container, ITermPart element) : this(context, inflater, container, element, null)
        {
        }

        public TermPartViewHolder(Context context, LayoutInflater inflater, ViewGroup container, ITermPart element, IMessenger messenger) : base(context, inflater, container, element)
        {
            _messenger = messenger ?? GalaSoft.MvvmLight.Messaging.Messenger.Default;
        }

        protected override void BindView(View view)
        {
            base.BindView(view);
            var adapter = new TermAdapater(Inflater, Element.ContentItems);
            adapter.ChildItemSelected += Adapter_ChildItemSelected;

            _recyclerView = view.FindViewById<RecyclerView>(Resource.Id.recycler_view);
            _recyclerView.SetAdapter(adapter);
        }

        private void Adapter_ChildItemSelected(object sender, IContentItem e)
        {
            _messenger.Send(new ContentItemSelectedMessage(e));
        }

        public override void Dispose()
        {
            base.Dispose();
            //TODO detach the bindings for each child item
        }



        private class TermAdapater : RecyclerView.Adapter
        {
            private readonly LayoutInflater _inflater;
            private readonly IList<IContentItem> _contentItems;

            public event EventHandler<IContentItem> ChildItemSelected;

            public TermAdapater(LayoutInflater inflater, IList<IContentItem> contentItems)
            {
                _inflater = inflater;
                _contentItems = contentItems;
            }

            public override int ItemCount => _contentItems.Count;

            public override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType)
            {
                var itemView = _inflater.Inflate(Resource.Layout.view_content_item, parent, false);
                var viewHolder = new ContentItemHolder(itemView);

                itemView.Click += (object sender, EventArgs e) =>
                {
                    var contentItem = _contentItems[viewHolder.AdapterPosition];
                    ChildItemSelected(sender, contentItem);
                };

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
            private Binding _binding;

            public IContentItem ContentItem { get; set; }

            public ContentItemHolder(View itemView) : base(itemView)
            {
            }

            public void Bind(IContentItem contentItem)
            {
                ContentItem = contentItem;
                
                if (_binding != null)
                {
                    _binding.Detach();
                }

                _binding = this.SetContentBinding(() => this.ContentItem, () => ItemView);
            }
        }
    }
}