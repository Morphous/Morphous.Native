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

namespace MainSample.Droid
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
            Mph.BaseUrl = "http://192.168.0.13:96";

            Messenger.Default.Register<ContentItemCreatedMessage>(this, message =>
            {
                if (message.ContentItem.ContentType == "Article" && message.ContentItem.DisplayType == "Detail")
                {
                    var mediaContent = message.ContentItem.As<MediaField>().Media;
                    mediaContent.As<ImagePart>().Alternates.Insert(0, "ArticleImage");
                }
            });

            Messenger.Default.Register<ContentItemDisplayingMessage>(this, message =>
            {
                if (message.DisplayContext.RootContentItem.ContentType == "Article")
                {
                }
                var test = message.DisplayContext.RootContentItem.ContentType;
            });
        }
    }
}