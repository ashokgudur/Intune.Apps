using System;

using UIKit;

namespace Intune.iOS
{
    public partial class SignInController : UIViewController
    {
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
            // Perform any additional setup after loading the view, typically from a nib.
            MessageLabel.Text = "";
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
                    MessageLabel.Text = user.Name;
            }
            catch (Exception ex)
            {
                MessageLabel.Text = ex.Message;
            }
        }

        partial void ForgotPasswordButton_TouchUpInside(UIButton sender)
        {
            try
            {
                var resetPassword = this.Storyboard
                                        .InstantiateViewController("ResetPasswordController") 
                                        as ResetPasswordController;
				if (resetPassword != null)
				{
                    this.Title = "Sign-in";
                    this.NavigationController.PushViewController(resetPassword, true);
				}
            }
            catch (Exception ex)
            {
                MessageLabel.Text = ex.Message;
            }
        }
    }
}
