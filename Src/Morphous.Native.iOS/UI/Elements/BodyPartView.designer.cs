// WARNING
//
// This file has been generated automatically by Xamarin Studio to store outlets and
// actions made in the UI designer. If it is removed, they will be lost.
// Manual changes to this file may not be handled correctly.
//
using Foundation;
using System.CodeDom.Compiler;

namespace Morphous.Native.iOS
{
	[Register ("BodyPartView")]
	partial class BodyPartView
	{
		[Outlet]
		UIKit.UILabel HtmlLabel { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (HtmlLabel != null) {
				HtmlLabel.Dispose ();
				HtmlLabel = null;
			}
		}
	}
}
