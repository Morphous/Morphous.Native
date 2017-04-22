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
	[Register ("TableContentItemViewController")]
	partial class TableContentItemViewController
	{
		[Outlet]
		UIKit.UIView Content { get; set; }

		[Outlet]
		UIKit.UIView Header { get; set; }

		[Outlet]
		UIKit.UIView Meta { get; set; }

		[Outlet]
		UIKit.UITableView TableView { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (Content != null) {
				Content.Dispose ();
				Content = null;
			}

			if (Header != null) {
				Header.Dispose ();
				Header = null;
			}

			if (Meta != null) {
				Meta.Dispose ();
				Meta = null;
			}

			if (TableView != null) {
				TableView.Dispose ();
				TableView = null;
			}
		}
	}
}
