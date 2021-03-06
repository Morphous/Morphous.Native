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
using GalaSoft.MvvmLight.Messaging;
using GalaSoft.MvvmLight.Helpers;
using Morphous.Native.Droid.Messages;

namespace Morphous.Native.Droid.UI.Elements
{
    public class TaxonomyPartViewHolder : ElementViewHolder<ITaxonomyPart>
    {
        private RecyclerView _recyclerView;

        public TaxonomyPartViewHolder(DisplayContext displayContext, ViewGroup container, ITaxonomyPart element) : base(displayContext, container, element)
        {
        }

        protected override void BindView(View view)
        {
            base.BindView(view);
            var adapter = new TermsAdapater(DisplayContext.Inflater, Element.Terms);
            adapter.TermSelected += Adapter_TermSelected;

            _recyclerView = view.FindViewById<RecyclerView>(Resource.Id.recycler_view);
            _recyclerView.AddItemDecoration(new SimpleDivider(DisplayContext.Activity));
            _recyclerView.SetAdapter(adapter);
        }

        private void Adapter_TermSelected(object sender, ITaxonomyItem e)
        {
            var termWrapper = new TermWrapper { Id = e.Id };
            DisplayContext.Messenger.Send(new ContentItemSelectedMessage(termWrapper));
        }

        public override void Dispose()
        {
            base.Dispose();
            //TODO detach the bindings for each child item
        }


        private class TermsAdapater : RecyclerView.Adapter
        {
            private readonly LayoutInflater _inflater;
            private readonly IList<ITaxonomyItem> _terms;

            public event EventHandler<ITaxonomyItem> TermSelected;

            public TermsAdapater(LayoutInflater inflater, IList<ITaxonomyItem> terms)
            {
                _inflater = inflater;
                _terms = terms;
            }

            public override int ItemCount => _terms.Count;

            public override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType)
            {
                var itemView = _inflater.Inflate(Resource.Layout.TaxonomyItem, parent, false);
                var viewHolder = new ContentItemHolder(itemView);

                itemView.Click += (object sender, EventArgs e) =>
                {
                    var term = _terms[viewHolder.AdapterPosition];
                    TermSelected(sender, term);
                };

                return viewHolder;
            }

            public override void OnBindViewHolder(RecyclerView.ViewHolder holder, int position)
            {
                var contentItemHolder = (ContentItemHolder)holder;
                contentItemHolder.Bind(_terms[position]);
            }
        }

        public class ContentItemHolder : RecyclerView.ViewHolder
        {
            private TextView _titleTextView;
            private Binding _binding;

            public ITaxonomyItem Term { get; set; }

            public ContentItemHolder(View itemView) : base(itemView)
            {
                _titleTextView = itemView.FindViewById<TextView>(Resource.Id.taxonomyItem_title);
            }

            public void Bind(ITaxonomyItem term)
            {
                Term = term;
                
                if (_binding != null)
                {
                    _binding.Detach();
                }

                _binding = this.SetBinding(() => Term.Title, () => _titleTextView.Text);
            }
        }
        

        public class TermWrapper : IContentItem
        {
            public int Id { get; set; }

            public string ContentType => throw new NotImplementedException();

            public string DisplayType => throw new NotImplementedException();

            public IList<IContentZone> Zones => throw new NotImplementedException();

            public IList<string> Alternates => throw new NotImplementedException();

            public TElement As<TElement>() where TElement : IContentElement
            {
                throw new NotImplementedException();
            }
        }
    }
}