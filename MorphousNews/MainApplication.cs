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

namespace MorphousNews
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
            Mph.BaseUrl = "http://192.168.0.18:96/";

            Messenger.Default.Register<ContentItemCreatedMessage>(this, message =>
            {
                if(message.ContentItem.ContentType == "Article" && message.ContentItem.DisplayType == "Detail")
                {
                    var media = message.ContentItem.As<IMediaField>().Media;
                    media.As<IImagePart>().Alternates.Insert(0, "ArticleImage");
                }
            });
        }
    }
}