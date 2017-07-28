using Foundation;
using System;
using UIKit;
using System.Text.RegularExpressions;
using Intune.Shared.Model;

namespace Intune.iOS
{
    public partial class ResetPasswordController : UIViewController
    {
        public ResetPasswordController(IntPtr handle) : base(handle)
        {
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
			MessageLabel.Text = "";
			VerifyOtpView.Hidden = true;
            NewPasswordView.Hidden = true;
        }

        partial void SendVerificationCodeButton_TouchUpInside(UIButton sender)
        {
            try
            {
                var signInId = SignInIdTextField.Text.Trim();
                if (string.IsNullOrWhiteSpace(signInId))
                {
                    MessageLabel.Text = "Please enter your sign-in id.";
                    return;
                }

                MessageLabel.Text = "Sending verification code...";

                if (isSignInIdEmail(signInId))
                    IntuneService.SendEmailOtp(signInId);
                else
                {
                    var mnv = new MobileNumberValidator(signInId);
                    IntuneService.SendMobileOtp(mnv.GetIsdCodeWithoutPlus(), mnv.GetMobileNumberWithoutIsdCode());
                }

                MessageLabel.Text = "Verification code sent. Please verify.";
                VerifyOtpView.Hidden = false;
            }
            catch (Exception ex)
            {
                MessageLabel.Text = ex.Message;
            }
        }

        private bool isSignInIdEmail(string signInId)
        {
            var regex = new Regex("[A-Za-z.@]");
            return regex.IsMatch(signInId);
        }

        partial void VerifyOtpButton_TouchUpInside(UIButton sender)
        {
            try
            {
                var otp = VerificationCodeTextField.Text.Trim();
                if (string.IsNullOrWhiteSpace(otp))
                {
                    MessageLabel.Text = "Please enter verification code";
                    return;
                }

                MessageLabel.Text = "Verifying OTP...";

                var signInId = SignInIdTextField.Text.Trim();
				if (isSignInIdEmail(signInId))
                    IntuneService.VerifyEmailOtp(signInId, otp);
                else
                {
                    var mnv = new MobileNumberValidator(signInId);
                    IntuneService.VerifyMobileOtp(mnv.GetIsdCodeWithoutPlus(), mnv.GetMobileNumberWithoutIsdCode(), otp);
                }

                MessageLabel.Text = "Verification code OK. Please enter new password";
                NewPasswordView.Hidden = false;
            }
            catch (Exception ex)
            {
                MessageLabel.Text = ex.Message;
            }
        }

        partial void ResetPasswordButton_TouchUpInside(UIButton sender)
        {
			try
			{
				var newPassword = NewPasswordTextField.Text.Trim();
				if (string.IsNullOrWhiteSpace(newPassword))
				{
					MessageLabel.Text = "Please enter new password";
					return;
				}

                var signInId = SignInIdTextField.Text.Trim();
				if (!isSignInIdEmail(signInId))
				{
					var mnv = new MobileNumberValidator(signInId);
					signInId = mnv.GetFullMobileNumber();
				}

				var user = new User
				{
					Email = signInId,
					Password = newPassword,
                    SessionToken = VerificationCodeTextField.Text.Trim(),
				};

                IntuneService.ResetPassword(user);
				MessageLabel.Text = "Resetting password successful";
				NavigationController.PopViewController(true);
			}
			catch (Exception ex)
			{
				MessageLabel.Text = ex.Message;
			}
		}
    }
}