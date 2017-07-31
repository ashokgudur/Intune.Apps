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
    [Register ("AccountsTableViewCell")]
    partial class AccountsTableViewCell
    {
        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UILabel BalanceLabel { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UILabel BalanceTypeLabel { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UILabel NameLabel { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UILabel PermissionLabel { get; set; }

        void ReleaseDesignerOutlets ()
        {
            if (BalanceLabel != null) {
                BalanceLabel.Dispose ();
                BalanceLabel = null;
            }

            if (BalanceTypeLabel != null) {
                BalanceTypeLabel.Dispose ();
                BalanceTypeLabel = null;
            }

            if (NameLabel != null) {
                NameLabel.Dispose ();
                NameLabel = null;
            }

            if (PermissionLabel != null) {
                PermissionLabel.Dispose ();
                PermissionLabel = null;
            }
        }
    }
}