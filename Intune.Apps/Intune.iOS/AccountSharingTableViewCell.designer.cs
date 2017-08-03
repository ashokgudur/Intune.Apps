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
    [Register ("AccountSharingTableViewCell")]
    partial class AccountSharingTableViewCell
    {
        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UILabel ContactNameLabel { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UISegmentedControl PermissionSegement { get; set; }

        void ReleaseDesignerOutlets ()
        {
            if (ContactNameLabel != null) {
                ContactNameLabel.Dispose ();
                ContactNameLabel = null;
            }

            if (PermissionSegement != null) {
                PermissionSegement.Dispose ();
                PermissionSegement = null;
            }
        }
    }
}