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
using Morphous.Native.Droid.UI.Elements;
using Morphous.Native.Models;
using GalaSoft.MvvmLight.Messaging;
using Morphous.Native.Droid.UI;

namespace Morphous.Native.Droid.Factories
{
    public class DefaultViewHolderFactory : IViewHolderFactory
    {
        private readonly DisplayContext _displayContext;

        public DefaultViewHolderFactory(DisplayContext displayContext)
        {
            _displayContext = displayContext;
        }
        
        public virtual ContentItemViewHolder CreateContentItemViewHolder(ViewGroup container, IContentItem contentItem)
        {
            return new ContentItemViewHolder(_displayContext, container, contentItem);
        }

        public virtual ElementViewHolder CreateElementViewHolder(ViewGroup container, IContentElement element)
        {
            if (element is ICommonPart)
            {
                return new CommonPartViewHolder(_displayContext, container, element as ICommonPart);
            }
            else if (element is ITitlePart)
            {
                return new TitlePartViewHolder(_displayContext, container, element as ITitlePart);
            }
            else if (element is IBodyPart)
            {
                return new BodyPartViewHolder(_displayContext, container, element as IBodyPart);
            }
            else if (element is ITermPart)
            {
                return new TermPartViewHolder(_displayContext, container, element as ITermPart);
            }
            else if (element is ITaxonomyPart)
            {
                return new TaxonomyPartViewHolder(_displayContext, container, element as ITaxonomyPart);
            }
            else if (element is IImagePart)
            {
                return new ImagePartViewHolder(_displayContext, container, element as IImagePart);
            }
            else if (element is IBooleanField)
            {
                return new BooleanFieldViewHolder(_displayContext, container, element as IBooleanField);
            }
            else if (element is IMediaField)
            {
                return new MediaFieldViewHolder(_displayContext, container, element as IMediaField);
            }
            else
            {
                return new ElementViewHolder<IContentElement>(_displayContext, container, element);
            }
        }
    }
}