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
    [Register ("SignUpController")]
    partial class SignUpController
    {
        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UILabel FullNameLabel { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UITextField FullNameTextField { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UILabel LoginIdLabel { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UITextField LoginIdTextField { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UILabel MessageLabel { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UILabel PasswordLabel { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UITextField PasswordTextField { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UILabel SignUpAgreementLabel { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIButton SignUpButton { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UILabel SignUpTitle { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UITextField VerificationCodeTextField { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UILabel VerificationOtpLabel { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIButton VerifyOtpButton { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIView VerifyOtpView { get; set; }

        [Action ("SignUpButton_TouchUpInside:")]
        [GeneratedCode ("iOS Designer", "1.0")]
        partial void SignUpButton_TouchUpInside (UIKit.UIButton sender);

        [Action ("VerifyOtpButton_TouchUpInside:")]
        [GeneratedCode ("iOS Designer", "1.0")]
        partial void VerifyOtpButton_TouchUpInside (UIKit.UIButton sender);

        void ReleaseDesignerOutlets ()
        {
            if (FullNameLabel != null) {
                FullNameLabel.Dispose ();
                FullNameLabel = null;
            }

            if (FullNameTextField != null) {
                FullNameTextField.Dispose ();
                FullNameTextField = null;
            }

            if (LoginIdLabel != null) {
                LoginIdLabel.Dispose ();
                LoginIdLabel = null;
            }

            if (LoginIdTextField != null) {
                LoginIdTextField.Dispose ();
                LoginIdTextField = null;
            }

            if (MessageLabel != null) {
                MessageLabel.Dispose ();
                MessageLabel = null;
            }

            if (PasswordLabel != null) {
                PasswordLabel.Dispose ();
                PasswordLabel = null;
            }

            if (PasswordTextField != null) {
                PasswordTextField.Dispose ();
                PasswordTextField = null;
            }

            if (SignUpAgreementLabel != null) {
                SignUpAgreementLabel.Dispose ();
                SignUpAgreementLabel = null;
            }

            if (SignUpButton != null) {
                SignUpButton.Dispose ();
                SignUpButton = null;
            }

            if (SignUpTitle != null) {
                SignUpTitle.Dispose ();
                SignUpTitle = null;
            }

            if (VerificationCodeTextField != null) {
                VerificationCodeTextField.Dispose ();
                VerificationCodeTextField = null;
            }

            if (VerificationOtpLabel != null) {
                VerificationOtpLabel.Dispose ();
                VerificationOtpLabel = null;
            }

            if (VerifyOtpButton != null) {
                VerifyOtpButton.Dispose ();
                VerifyOtpButton = null;
            }

            if (VerifyOtpView != null) {
                VerifyOtpView.Dispose ();
                VerifyOtpView = null;
            }
        }
    }
}