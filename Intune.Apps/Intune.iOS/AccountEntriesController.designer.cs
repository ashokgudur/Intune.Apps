// WARNING
//
// This file has been generated automatically by Visual Studio from the outlets and
// actions declared in your storyboard file.
// Manual changes to this file will not be maintained.
//
using Foundation;
using System;
using System.CodeDom.Compiler;
using UIKit;

namespace Intune.iOS
{
    [Register ("AccountEntriesController")]
    partial class AccountEntriesController
    {
        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UITableView AccountEntriesTableView { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIBarButtonItem AddNewToolBarButton { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIBarButtonItem CloseToolBarButton { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIBarButtonItem CommentToolBarButton { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UINavigationBar NavigationBar { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIBarButtonItem RefreshToolBarButton { get; set; }

        void ReleaseDesignerOutlets ()
        {
            if (AccountEntriesTableView != null) {
                AccountEntriesTableView.Dispose ();
                AccountEntriesTableView = null;
            }

            if (AddNewToolBarButton != null) {
                AddNewToolBarButton.Dispose ();
                AddNewToolBarButton = null;
            }

            if (CloseToolBarButton != null) {
                CloseToolBarButton.Dispose ();
                CloseToolBarButton = null;
            }

            if (CommentToolBarButton != null) {
                CommentToolBarButton.Dispose ();
                CommentToolBarButton = null;
            }

            if (NavigationBar != null) {
                NavigationBar.Dispose ();
                NavigationBar = null;
            }

            if (RefreshToolBarButton != null) {
                RefreshToolBarButton.Dispose ();
                RefreshToolBarButton = null;
            }
        }
    }
}