using System;
using Intune.Shared.Model;
using UIKit;

namespace Intune.iOS
{
    public partial class SignInController : UIViewController
    {
        private User SignInUser { get; set; }

        protected SignInController(IntPtr handle) : base(handle)
        {
            // Note: this .ctor should not contain any initialization logic.
        }

        public override void ViewWillAppear(bool animated)
        {
            base.ViewWillAppear(animated);
            Title = "Intune";
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
            MessageLabel.Text = "";
            SignInUser = null;
#if DEBUG
            SignInIdTextField.Text = "ashok.gudur@gmail.com";
            SignInPasswordTextField.Text = "ashokg";
#endif
        }

        public override void DidReceiveMemoryWarning()
        {
            base.DidReceiveMemoryWarning();
            // Release any cached data, images, etc that aren't in use.
        }

        partial void SignInButton_TouchUpInside(UIButton sender)
        {
            try
            {
                var signInId = SignInIdTextField.Text;
                var password = SignInPasswordTextField.Text;
                var user = IntuneService.SignIn(signInId, password);
                if (user == null)
                    MessageLabel.Text = "Cannot Login";
                else
                {
                    SignInUser = user;
                    navigateToMainViewController();
                }
            }
            catch (Exception ex)
            {
                MessageLabel.Text = ex.Message;
            }
        }

        private void navigateToMainViewController()
        {
            var mainController = this.Storyboard
                                 .InstantiateViewController("MainController")
                                    as MainController;
            if (mainController != null)
            {
                mainController.SignInUser = SignInUser;
                this.NavigationController.PresentViewController(mainController, true, null);
            }
        }

        partial void ForgotPasswordButton_TouchUpInside(UIButton sender)
        {
            try
            {
                var resetPasswordView = this.Storyboard
                                        .InstantiateViewController("ResetPasswordController")
                                        as ResetPasswordController;
                if (resetPasswordView != null)
                {
                    this.Title = "Sign-in";
                    this.NavigationController.PushViewController(resetPasswordView, true);
                }
            }
            catch (Exception ex)
            {
                MessageLabel.Text = ex.Message;
            }
        }

        partial void SignUpButton_TouchUpInside(UIButton sender)
        {
            try
            {
                var signUpView = this.Storyboard
                                        .InstantiateViewController("SignUpController")
                                        as SignUpController;
                if (signUpView != null)
                {
                    this.Title = "Sign-in";
                    this.NavigationController.PushViewController(signUpView, true);
                }
            }
            catch (Exception ex)
            {
                MessageLabel.Text = ex.Message;
            }
        }
    }
}
