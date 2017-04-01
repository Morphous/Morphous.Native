using System;
using Morphous.Native.Models;
using UIKit;

namespace Morphous.Native.iOS
{
    public class ElementView<TElement> : ElementView where TElement : class, IContentElement
    {
        public TElement Element { get; private set; }

        public ElementView(IntPtr handle) : base (handle)
        {
        }

        public override void SetElement(IContentElement contentElement)
        {
            if (contentElement is TElement)
            {
                Element = (TElement)contentElement;
                Bind();
            }
            else
            {
                throw new InvalidCastException($"Tried to create a {GetType().Name} with a {contentElement.GetType().Name}");
            }
        }
    }

    public abstract class ElementView : UIView
    {
        public ElementView(IntPtr handle) : base (handle)
        {
        }

        public abstract void SetElement(IContentElement contentElement);

        protected virtual void Bind()
        {
        }
    }
}
