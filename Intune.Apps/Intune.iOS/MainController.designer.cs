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
    [Register ("MainController")]
    partial class MainController
    {
        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UITabBarItem AccountsTabBarItem { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UITabBarItem ContactsTabBarItem { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UITableView ContactsTableView { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UITabBarItem LogoutTabBarItem { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UITabBar MainViewTabBar { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UITabBarItem ProfileTabBarItem { get; set; }

        void ReleaseDesignerOutlets ()
        {
            if (AccountsTabBarItem != null) {
                AccountsTabBarItem.Dispose ();
                AccountsTabBarItem = null;
            }

            if (ContactsTabBarItem != null) {
                ContactsTabBarItem.Dispose ();
                ContactsTabBarItem = null;
            }

            if (ContactsTableView != null) {
                ContactsTableView.Dispose ();
                ContactsTableView = null;
            }

            if (LogoutTabBarItem != null) {
                LogoutTabBarItem.Dispose ();
                LogoutTabBarItem = null;
            }

            if (MainViewTabBar != null) {
                MainViewTabBar.Dispose ();
                MainViewTabBar = null;
            }

            if (ProfileTabBarItem != null) {
                ProfileTabBarItem.Dispose ();
                ProfileTabBarItem = null;
            }
        }
    }
}