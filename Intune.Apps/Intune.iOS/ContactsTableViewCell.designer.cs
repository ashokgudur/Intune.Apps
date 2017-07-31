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
    [Register ("ContactsTableViewCell")]
    partial class ContactsTableViewCell
    {
        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UILabel AddressLabel { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UILabel FullNameLabel { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UILabel IsIntunedLabel { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UILabel MobileLabel { get; set; }

        void ReleaseDesignerOutlets ()
        {
            if (AddressLabel != null) {
                AddressLabel.Dispose ();
                AddressLabel = null;
            }

            if (FullNameLabel != null) {
                FullNameLabel.Dispose ();
                FullNameLabel = null;
            }

            if (IsIntunedLabel != null) {
                IsIntunedLabel.Dispose ();
                IsIntunedLabel = null;
            }

            if (MobileLabel != null) {
                MobileLabel.Dispose ();
                MobileLabel = null;
            }
        }
    }
}