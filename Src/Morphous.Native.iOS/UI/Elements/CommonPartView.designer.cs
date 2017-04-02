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
	[Register ("CommonPartView")]
	partial class CommonPartView
	{
		[Outlet]
		UIKit.UILabel CreatedDateLabel { get; set; }

		[Outlet]
		UIKit.UILabel IdLabel { get; set; }

		[Outlet]
		UIKit.UILabel PublishedDateLabel { get; set; }

		[Outlet]
		UIKit.UILabel ResourceUrlLabel { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (IdLabel != null) {
				IdLabel.Dispose ();
				IdLabel = null;
			}

			if (ResourceUrlLabel != null) {
				ResourceUrlLabel.Dispose ();
				ResourceUrlLabel = null;
			}

			if (CreatedDateLabel != null) {
				CreatedDateLabel.Dispose ();
				CreatedDateLabel = null;
			}

			if (PublishedDateLabel != null) {
				PublishedDateLabel.Dispose ();
				PublishedDateLabel = null;
			}
		}
	}
}
