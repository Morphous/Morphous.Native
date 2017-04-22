// WARNING
//
// This file has been generated automatically by Xamarin Studio to store outlets and
// actions made in the UI designer. If it is removed, they will be lost.
// Manual changes to this file may not be handled correctly.
//
using Foundation;
using System.CodeDom.Compiler;

namespace News.iOS
{
	[Register ("ContentItemArticleView")]
	partial class ContentItemArticleView
	{
		[Outlet]
		UIKit.UIStackView Header { get; set; }

		[Outlet]
		UIKit.UIStackView HeroImage { get; set; }
		
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
		}
	}
}
