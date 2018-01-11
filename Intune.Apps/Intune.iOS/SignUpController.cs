using Foundation;
using System;
using UIKit;
using System.Text.RegularExpressions;
using Intune.Shared.Model;

namespace Intune.iOS
{
    public partial class SignUpController : UIViewController
    {
        public SignUpController (IntPtr handle) : base (handle)
        {
        }

        public override void ViewWillAppear(bool animated)
        {
            base.ViewWillAppear(animated);
            MessageLabel.Text = "";
            VerifyOtpView.Hidden = true;
        }

        partial void SignUpButton_TouchUpInside(UIButton sender)
        {
            try
            {
                LoginIdTextField.ResignFirstResponder();

                var loginId = LoginIdTextField.Text.Trim();
                if (string.IsNullOrWhiteSpace(loginId))
                {
                    MessageLabel.Text = "Please enter a login id.";
                    return;
                }

                var fullName = FullNameTextField.Text.Trim();
                if (string.IsNullOrWhiteSpace(fullName))
                {
                    MessageLabel.Text = "Please enter your full name.";
                    return;
                }

                var password = PasswordTextField.Text.Trim();
                if (string.IsNullOrWhiteSpace(password))
                {
                    MessageLabel.Text = "Please enter a password.";
                    return;
                }

                MessageLabel.Text = "Sending verification code...";

                if (isLoginIdEmail())
                    IntuneService.SendEmailOtp(loginId);
                else
                {
                    var mnv = new MobileNumberValidator(loginId);
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

        private bool isLoginIdEmail()
        {
            var loginId = LoginIdTextField.Text.Trim();
            var regex = new Regex("[A-Za-z.@]");
            return regex.IsMatch(loginId);
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

                var loginId = LoginIdTextField.Text.Trim();
                if (isLoginIdEmail())
                    IntuneService.VerifyEmailOtp(loginId, otp);
                else
                {
                    var mnv = new MobileNumberValidator(loginId);
                    IntuneService.VerifyMobileOtp(mnv.GetIsdCodeWithoutPlus(), mnv.GetMobileNumberWithoutIsdCode(), otp);
                }

                MessageLabel.Text = "Verification code OK.";
                registerUser();
            }
            catch (Exception ex)
            {
                MessageLabel.Text = ex.Message;
            }
        }

        private string getMobileNumber()
        {
            //TODO: Dummy number for mobile when email is used
            if (isLoginIdEmail())
                return "+910000000000";
            else
            {
                var mnv = new MobileNumberValidator(LoginIdTextField.Text.Trim());
                return mnv.GetFullMobileNumber();
            }
        }

        private void registerUser()
        {
            try
            {
                var loginId = LoginIdTextField.Text.Trim();
                if (!isLoginIdEmail())
                {
                    var mnv = new MobileNumberValidator(loginId);
                    loginId = mnv.GetFullMobileNumber();
                }

                var user = new User
                {
                    Name = FullNameTextField.Text.Trim(),
                    Email = loginId,
                    Mobile = getMobileNumber(),
                    Password = PasswordTextField.Text.Trim(),
                    CreatedOn = DateTime.Now, //TODO: UTC date time?
                };

                user = IntuneService.RegiterUser(user);
                if (user == null)
                    MessageLabel.Text = "Cannot register user!";
                else
                {
                    MessageLabel.Text = $"{user.Name} is registered.";
					NavigationController.PopViewController(true);
				}
            }
            catch (Exception ex)
            {
                MessageLabel.Text = ex.Message;
            }
        }
    }
}
