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
using Morphous.Native;

namespace MainSample.Droid
{
    [Application(Theme = "@style/MyTheme")]
    public class MainApplication : Application
    {

        private List<WeakReference<object>> _weakObjects = new List<WeakReference<object>>();

        public MainApplication(IntPtr handle, JniHandleOwnership ownerShip) : base(handle, ownerShip)
        {
        }

        public override void OnCreate()
        {
            base.OnCreate();
            Mph.BaseUrl = "http://192.168.1.25:96";
        }

        public void SetObject(object someObject)
        {
            _weakObjects.Add(new WeakReference<object>(someObject));
        }

        public void GarbageCollect()
        {
            object someObject;

            foreach(var weakObject in _weakObjects)
            {
                if (!weakObject.TryGetTarget(out someObject))
                {
                    Console.WriteLine("its gone");
                }
            }


            GC.Collect();
        }
    }
}