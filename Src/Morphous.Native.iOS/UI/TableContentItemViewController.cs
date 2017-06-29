// This file has been autogenerated from a class added in the UI designer.

using System;

using Foundation;
using GalaSoft.MvvmLight.Helpers;
using GalaSoft.MvvmLight.Messaging;
using Morphous.Native.iOS.Bindings;
using Morphous.Native.iOS.Factories;
using Morphous.Native.ViewModels;
using UIKit;

namespace Morphous.Native.iOS
{
	public partial class TableContentItemViewController : UIViewController
	{
        private IContentItemViewModel ViewModel { get; set; }

        private Binding _contentItemBinding;

        public virtual int ContentItemId { get; set; }

		public TableContentItemViewController (IntPtr handle) : base (handle)
		{
		}

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
            ViewModel = ContentItemViewModel.Create(ContentItemId);

            _contentItemBinding = this.SetBinding(() => ViewModel.ContentItem).WhenSourceChanges(Update);
        }

        public override void ViewWillAppear(bool animated)
        {
            base.ViewWillAppear(animated);
            TableView.DeselectRow(TableView.IndexPathForSelectedRow, true);
        }

        void Update()
        {
            if (ViewModel.ContentItem == null)
                return;


            var displayContext = new DisplayContext();
            displayContext.ViewController = this;
            displayContext.Messenger = Messenger.Default;
            displayContext.RootView = TableView;
            displayContext.RootContentItem = ViewModel.ContentItem;
            displayContext.ViewFactory = new DefaultViewFactory(displayContext);

            foreach (var zone in ViewModel.ContentItem.Zones)
            {
                var zoneProp = typeof(TableContentItemViewController).GetProperty(zone.Name, System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
                var zoneView = zoneProp?.GetValue(this, null) as UIView;

                if (zoneView != null)
                {
                    foreach (var element in zone.Elements)
                    {
                        var elementView = displayContext.ViewFactory.CreateElementView(element);

                        if (elementView != null)
                        {
                            if (zoneView is UIStackView)
                            {
                                ((UIStackView)zoneView).AddArrangedSubview(elementView);
                            }
                            else
                            {
                                zoneView.AddSubview(elementView);
                            }
                        }
                    }
                }
            }
        }
   }
}