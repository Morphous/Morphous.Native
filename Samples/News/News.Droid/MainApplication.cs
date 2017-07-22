using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Morphous.Native;
using GalaSoft.MvvmLight.Messaging;
using Morphous.Native.Messges;
using Morphous.Native.Models;
using Morphous.Native.Droid.Messages;

namespace News.Droid
{
    [Application(Theme = "@style/MyTheme")]
    public class MainApplication : Application
    {
        public MainApplication(IntPtr handle, JniHandleOwnership ownerShip) : base(handle, ownerShip)
        {
        }

        public override void OnCreate()
        {
            base.OnCreate();
            Mph.BaseUrl = "https://morphousnews.azurewebsites.net";

            Messenger.Default.Register<ContentItemCreatedMessage>(this, message =>
            {
                if (message.ContentItem.ContentType == "Article" && message.ContentItem.DisplayType == "Detail")
                {
                    var mediaContent = message.ContentItem.As<MediaField>().Media;
                    mediaContent?.As<ImagePart>().Alternates.Insert(0, "ArticleImage");
                }
            });

            Messenger.Default.Register<ContentItemDisplayingMessage>(this, message =>
            {
                if (message.DisplayContext.RootContentItem.ContentType == "Article" && message.DisplayContext.RootContentItem.DisplayType == "Detail")
                {
                    message.DisplayContext.ViewHolderFactory = new ArticleViewHolderFactory(message.DisplayContext);
                }
            });
        }
    }
}