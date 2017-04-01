// WARNING
//
// This file has been generated automatically by Xamarin Studio to store outlets and
// actions made in the UI designer. If it is removed, they will be lost.
// Manual changes to this file may not be handled correctly.
//
using Foundation;
using System.CodeDom.Compiler;

namespace MainSample.iOS
{
	[Register ("MyContentItem")]
	partial class MyContentItem
	{
		[Outlet]
		UIKit.UIStackView Content { get; set; }

		[Outlet]
		UIKit.UIStackView Header { get; set; }

		[Outlet]
		UIKit.UIStackView HeroImage { get; set; }

		[Outlet]
		UIKit.UIStackView Meta { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (HeroImage != null) {
				HeroImage.Dispose ();
				HeroImage = null;
			}

			if (Header != null) {
				Header.Dispose ();
				Header = null;
			}

			if (Content != null) {
				Content.Dispose ();
				Content = null;
			}

			if (Meta != null) {
				Meta.Dispose ();
				Meta = null;
			}
		}
	}
}
