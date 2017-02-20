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

namespace MainSample.Droid
{
    [Application(Theme = "@style/MyTheme")]
    public class MainApplication : Application
    {
        public MainApplication(IntPtr handle, JniHandleOwnership ownerShip) : base(handle, ownerShip)
        {
        }
    }
}