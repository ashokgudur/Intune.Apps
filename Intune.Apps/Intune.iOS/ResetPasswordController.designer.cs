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
    [Register ("ResetPasswordController")]
    partial class ResetPasswordController
    {
        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UILabel MessageLabel { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UILabel NewPasswordLabel { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UITextField NewPasswordTextField { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIView NewPasswordView { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIButton ResetPasswordButton { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UILabel ResetPasswordTitle { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIButton SendVerificationCodeButton { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UILabel SigninIdLabel { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UITextField SignInIdTextField { get; set; }

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

        [Action ("ResetPasswordButton_TouchUpInside:")]
        [GeneratedCode ("iOS Designer", "1.0")]
        partial void ResetPasswordButton_TouchUpInside (UIKit.UIButton sender);

        [Action ("SendVerificationCodeButton_TouchUpInside:")]
        [GeneratedCode ("iOS Designer", "1.0")]
        partial void SendVerificationCodeButton_TouchUpInside (UIKit.UIButton sender);

        [Action ("VerifyOtpButton_TouchUpInside:")]
        [GeneratedCode ("iOS Designer", "1.0")]
        partial void VerifyOtpButton_TouchUpInside (UIKit.UIButton sender);

        void ReleaseDesignerOutlets ()
        {
            if (MessageLabel != null) {
                MessageLabel.Dispose ();
                MessageLabel = null;
            }

            if (NewPasswordLabel != null) {
                NewPasswordLabel.Dispose ();
                NewPasswordLabel = null;
            }

            if (NewPasswordTextField != null) {
                NewPasswordTextField.Dispose ();
                NewPasswordTextField = null;
            }

            if (NewPasswordView != null) {
                NewPasswordView.Dispose ();
                NewPasswordView = null;
            }

            if (ResetPasswordButton != null) {
                ResetPasswordButton.Dispose ();
                ResetPasswordButton = null;
            }

            if (ResetPasswordTitle != null) {
                ResetPasswordTitle.Dispose ();
                ResetPasswordTitle = null;
            }

            if (SendVerificationCodeButton != null) {
                SendVerificationCodeButton.Dispose ();
                SendVerificationCodeButton = null;
            }

            if (SigninIdLabel != null) {
                SigninIdLabel.Dispose ();
                SigninIdLabel = null;
            }

            if (SignInIdTextField != null) {
                SignInIdTextField.Dispose ();
                SignInIdTextField = null;
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