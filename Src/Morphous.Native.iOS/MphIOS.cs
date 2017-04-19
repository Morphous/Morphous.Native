using System;
using System.Collections.Generic;
using Foundation;
using Morphous.Native.iOS.UI;
using Morphous.Native.Models;
using UIKit;

namespace Morphous.Native.iOS
{
    public enum ContentItemViewControllerType
    {
        Default,
        Table
    }

    public static class MphIOS
    {
        private static readonly IDictionary<string, Func<int, UIViewController>> _typesDictionary = new Dictionary<string, Func<int, UIViewController>>();


        public static void AddViewControllerSelector(string contentType, ContentItemViewControllerType contentItemViewControllerType)
        {
            switch (contentItemViewControllerType)
            {
                case ContentItemViewControllerType.Default:
                    AddViewControllerSelector(contentType, (id) => ContentItemViewController(id));
                    break;
                case ContentItemViewControllerType.Table:
                    AddViewControllerSelector(contentType, (id) => TableContentItemViewController(id));
                    break;
            }
        }

        public static void AddViewControllerSelector(string contentType, Func<int, UIViewController> factory)
        {
             _typesDictionary[contentType] = factory;
        }


        public static UIViewController ContentItemViewController(IContentItem contentItem)
        {
            if (_typesDictionary.ContainsKey(contentItem.ContentType))
            {
                return _typesDictionary[contentItem.ContentType](contentItem.Id);
            }
            else
            {
                return ContentItemViewController(contentItem.Id);
            }
        }



        public static UIStoryboard ContentStoryboard => UIStoryboard.FromName("Content", NSBundle.MainBundle);

        public static ContentItemViewController ContentItemViewController()
        {
            return (ContentItemViewController)ContentStoryboard.InstantiateViewController("ContentItemViewController");
        }

        public static ContentItemViewController ContentItemViewController(int contentItemId)
        {
            var viewController = ContentItemViewController();
            viewController.ContentItemId = contentItemId;
            return viewController;
        }

        public static TableContentItemViewController TableContentItemViewController()
        {
            return (TableContentItemViewController)ContentStoryboard.InstantiateViewController("TableContentItemViewController");
        }

        public static TableContentItemViewController TableContentItemViewController(int contentItemId)
        {
        	var viewController = TableContentItemViewController();
        	viewController.ContentItemId = contentItemId;
        	return viewController;
        }
    }
}
