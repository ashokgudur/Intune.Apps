// WARNING
//
// This file has been generated automatically by Visual Studio from the outlets and
// actions declared in your storyboard file.
// Manual changes to this file will not be maintained.
//
using Foundation;
using System;
using System.CodeDom.Compiler;

namespace Intune.iOS
{
    [Register ("SignInController")]
    partial class SignInController
    {
        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIButton ForgotPasswordButton { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UILabel IntuneLoginInTitle { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UILabel MessageLabel { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIButton SignInButton { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UILabel SignInIdLabel { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UITextField SignInIdTextField { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UILabel SignInPasswordLabel { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UITextField SignInPasswordTextField { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIView SignInView { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIButton SignUpButton { get; set; }

        [Action ("ForgotPasswordButton_TouchUpInside:")]
        [GeneratedCode ("iOS Designer", "1.0")]
        partial void ForgotPasswordButton_TouchUpInside (UIKit.UIButton sender);

        [Action ("SignInButton_TouchUpInside:")]
        [GeneratedCode ("iOS Designer", "1.0")]
        partial void SignInButton_TouchUpInside (UIKit.UIButton sender);

        [Action ("SignUpButton_TouchUpInside:")]
        [GeneratedCode ("iOS Designer", "1.0")]
        partial void SignUpButton_TouchUpInside (UIKit.UIButton sender);

        void ReleaseDesignerOutlets ()
        {
            if (ForgotPasswordButton != null) {
                ForgotPasswordButton.Dispose ();
                ForgotPasswordButton = null;
            }

            if (IntuneLoginInTitle != null) {
                IntuneLoginInTitle.Dispose ();
                IntuneLoginInTitle = null;
            }

            if (MessageLabel != null) {
                MessageLabel.Dispose ();
                MessageLabel = null;
            }

            if (SignInButton != null) {
                SignInButton.Dispose ();
                SignInButton = null;
            }

            if (SignInIdLabel != null) {
                SignInIdLabel.Dispose ();
                SignInIdLabel = null;
            }

            if (SignInIdTextField != null) {
                SignInIdTextField.Dispose ();
                SignInIdTextField = null;
            }

            if (SignInPasswordLabel != null) {
                SignInPasswordLabel.Dispose ();
                SignInPasswordLabel = null;
            }

            if (SignInPasswordTextField != null) {
                SignInPasswordTextField.Dispose ();
                SignInPasswordTextField = null;
            }

            if (SignInView != null) {
                SignInView.Dispose ();
                SignInView = null;
            }

            if (SignUpButton != null) {
                SignUpButton.Dispose ();
                SignUpButton = null;
            }
        }
    }
}