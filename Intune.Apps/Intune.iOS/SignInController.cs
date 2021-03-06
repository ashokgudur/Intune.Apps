﻿using System;
using System.Threading;
using System.Threading.Tasks;
using Intune.Shared.Model;
using UIKit;
using Xamarin.Auth;

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
            EnableAllButtons();
            MessageLabel.Text = "";
            SignInUser = null;
            SignInActivityIndicator.Hidden = true;
#if DEBUG
            SignInIdTextField.Text = "ashok.gudur@gmail.com";
            SignInPasswordTextField.Text = "ashokg";
#endif
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
            HookEventHandlers();
        }

        private void HookEventHandlers()
        {
            SignInButton.TouchUpInside += SignInButton_TouchUpInsideAsync;
            SignUpButton.TouchUpInside += SignUpButton_TouchUpInside;
            ForgotPasswordButton.TouchUpInside += ForgotPasswordButton_TouchUpInside;
        }

        public override void DidReceiveMemoryWarning()
        {
            base.DidReceiveMemoryWarning();
            // Release any cached data, images, etc that aren't in use.
        }

        async void SignInButton_TouchUpInsideAsync(object sender, EventArgs e)
        {
            try
            {
                SignInActivityIndicator.Hidden = false;
                SignInActivityIndicator.StartAnimating();

                DisplayStatusMessage("Login to your Intune...", UIColor.Blue);

                DisableAllButtons();
                var signInId = SignInIdTextField.Text;
                var password = SignInPasswordTextField.Text;
                var rememberSignIn = RememberSwitch.On;
                await Task.Run(() => LaunchMainController(signInId, password, rememberSignIn));

                SignInActivityIndicator.Hidden = true;
                SignInActivityIndicator.StopAnimating();
            }
            catch (Exception ex)
            {
                EnableAllButtons();
                DisplayStatusMessage(ex.Message, UIColor.Red);
            }
        }

        private void DisplayStatusMessage(String text, UIColor color)
        {
            MessageLabel.Text = text;
            MessageLabel.TextColor = color;
        }

        private void DisableAllButtons()
        {
            SignInButton.Enabled = false;
            SignUpButton.Enabled = false;
            ForgotPasswordButton.Enabled = false;
        }

        private void EnableAllButtons()
        {
            SignInButton.Enabled = true;
            SignUpButton.Enabled = true;
            ForgotPasswordButton.Enabled = true;
        }

        private void LaunchMainController(string signInId, string password, bool rememberSignIn)
        {
            var user = IntuneService.SignIn(signInId, password);
            if (user == null)
                BeginInvokeOnMainThread(() => MessageLabel.Text = "Cannot Login");
            else
            {
                SignInUser = user;

                if (rememberSignIn)
                    StoreSignInCredentials();

                BeginInvokeOnMainThread(() => NavigateToMainViewController());
            }
        }

        private void StoreSignInCredentials()
        {
            if (!Common.IsRunningOnDevice())
                return;

            var userAccount = new Xamarin.Auth.Account { Username = SignInUser.Email };
            userAccount.Properties.Add("Password", SignInUser.Password);
            var store = AccountStore.Create();
            store.Save(userAccount, Common.DeviceAccountStoreName);
        }

        private void NavigateToMainViewController()
        {
            var mainController = Storyboard.InstantiateViewController("MainController") as MainController;
            if (mainController == null)
                throw new Exception("Cannot find MainController");

            mainController.SignInUser = SignInUser;
            SignInActivityIndicator.StopAnimating();
            NavigationController.PresentViewController(mainController, true, null);
        }

        void ForgotPasswordButton_TouchUpInside(object sender, EventArgs e)
        {
            try
            {
                var resetPasswordView = Storyboard.InstantiateViewController("ResetPasswordController") as ResetPasswordController;
                if (resetPasswordView == null)
                    throw new Exception("Cannot find ResetPasswordController");

                Title = "Sign-in";
                NavigationController.PushViewController(resetPasswordView, true);
            }
            catch (Exception ex)
            {
                MessageLabel.Text = ex.Message;
            }
        }

        void SignUpButton_TouchUpInside(object sender, EventArgs e)
        {
            try
            {
                var signUpView = Storyboard.InstantiateViewController("SignUpController") as SignUpController;
                if (signUpView == null)
                    throw new Exception("Cannot find SignUpController");

                Title = "Sign-in";
                NavigationController.PushViewController(signUpView, true);
            }
            catch (Exception ex)
            {
                MessageLabel.Text = ex.Message;
            }
        }
    }
}
