﻿using Android.App;
using Android.Widget;
using Android.OS;
using Android.Content;
using Android.Views;
using System.Threading;
using Android.Graphics;
using Android.Support.Design.Widget;
using Android.Views.InputMethods;
using Android.Util;
using System.Text.RegularExpressions;
using Xamarin.Auth;
using Intune.Shared.Model;

namespace Intune.Android
{
    [Activity(Label = "Intune", Icon = "@drawable/icon")]
    public class SignInActivity : Activity
    {
        View _rootView;
        TextInputLayout _idLayout;
        TextInputEditText _idEditText;
        TextInputLayout _passwordLayout;
        TextInputEditText _passwordEditText;

        protected override void OnCreate(Bundle bundle)
        {
            SetTheme(Resource.Style.SignInTheme);

            base.OnCreate(bundle);
            SetContentView(Resource.Layout.SignIn);

            Title = "Welcome to Intune";

            _rootView = FindViewById<View>(Resource.Id.loginRootLinearLayout);

            _idLayout = FindViewById<TextInputLayout>(Resource.Id.signInIdInputLayout);
            _idEditText = FindViewById<TextInputEditText>(Resource.Id.signInIdTextInputEditText);

            _passwordLayout = FindViewById<TextInputLayout>(Resource.Id.signInPasswordInputLayout);
            _passwordEditText = FindViewById<TextInputEditText>(Resource.Id.signInPasswordTextInputEditText);

            var robotoTypeface = Typeface.CreateFromAsset(Application.Context.Assets, "fonts/Roboto-Regular.ttf");
            _idEditText.Typeface = robotoTypeface;
            _passwordEditText.Typeface = robotoTypeface;

            var signInMessageTextView = FindViewById<TextView>(Resource.Id.signInMessageTextView);
            signInMessageTextView.PaintFlags = PaintFlags.UnderlineText;

            var signInButton = FindViewById<Button>(Resource.Id.signInButton);
            signInButton.Click += SignInButton_Click;

#if DEBUG
            _idEditText.Text = "ashok.gudur@gmail.com";
            _passwordEditText.Text = "ashokg";
#endif
        }

        public override bool OnCreateOptionsMenu(IMenu menu)
        {
            MenuInflater.Inflate(Resource.Menu.login_menus, menu);
            return base.OnCreateOptionsMenu(menu);
        }

        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            switch (item.ItemId)
            {
                case Resource.Id.login_menu_sign_up:
                    signUpMenu_Click();
                    break;
                case Resource.Id.login_menu_forgot:
                    forgotPasswordMenu_Click();
                    break;
                default:
                    break;
            }

            return base.OnOptionsItemSelected(item);
        }

        public override void OnBackPressed() { }

        private void forgotPasswordMenu_Click()
        {
            StartActivity(typeof(ResetPasswordActivity));
        }

        private void signUpMenu_Click()
        {
            StartActivity(typeof(SignUpActivity));
        }

        private string getSignInId()
        {
            return _idEditText.Text.Trim().ToLower();
        }

        private bool isIdEmail()
        {
            var regex = new Regex("[A-Za-z.@]");
            return regex.IsMatch(getSignInId());
        }

        private void SignInButton_Click(object sender, System.EventArgs e)
        {
            hideKeyboard();

            _idLayout.ErrorEnabled = false;
            if (string.IsNullOrWhiteSpace(getSignInId()))
            {
                _idLayout.ErrorEnabled = true;
                _idLayout.Error = "mobile or email is required";
                Snackbar.Make(_rootView, "Please enter mobile or email.", Snackbar.LengthLong)
                        .SetAction("OK", (v) =>
                        {
                            _idLayout.ErrorEnabled = false;
                            _idEditText.RequestFocus();
                        })
                        .Show();
                return;
            }

            var isIdValid = false;
            if (isIdEmail())
                isIdValid = Patterns.EmailAddress.Matcher(getSignInId()).Matches();
            else
                isIdValid = new MobileNumberValidator(getSignInId()).IsValid();

            if (!isIdValid)
            {
                _idLayout.ErrorEnabled = true;
                _idLayout.Error = "Valid mobile or email is required";
                Snackbar.Make(_rootView, "Enter valid mobile or email address.", Snackbar.LengthLong)
                        .SetAction("OK", (v) =>
                        {
                            _idLayout.ErrorEnabled = false;
                            _idEditText.RequestFocus();
                        })
                        .Show();
                return;
            }

            _passwordLayout.ErrorEnabled = false;
            if (string.IsNullOrWhiteSpace(_passwordEditText.Text.Trim()))
            {
                _passwordLayout.ErrorEnabled = true;
                _passwordLayout.Error = "Password is required";
                Snackbar.Make(_rootView, "Please enter password", Snackbar.LengthLong)
                        .SetAction("Clear", (v) =>
                        {
                            _passwordLayout.ErrorEnabled = false;
                            _passwordEditText.Text = string.Empty;
                            _passwordEditText.RequestFocus();
                        })
                        .Show();
                return;
            }

            var signId = getSignInId();
            if (!isIdEmail())
            {
                var mobileValidator = new MobileNumberValidator(signId);
                signId = mobileValidator.GetFullMobileNumber();
            }

            Snackbar.Make(_rootView, "Logging into Intune...", Snackbar.LengthIndefinite).Show();
            var us = new IntuneUserService(this, signId, _passwordEditText.Text);
            ThreadPool.QueueUserWorkItem(o => us.SignIn());
        }

        private void hideKeyboard()
        {
            var imm = GetSystemService(Context.InputMethodService) as InputMethodManager;
            imm.HideSoftInputFromWindow(CurrentFocus.WindowToken, HideSoftInputFlags.NotAlways);
        }

        private class IntuneUserService
        {
            Activity _activity;
            string _signInId;
            string _password;

            public IntuneUserService(Activity activity, string signInId, string password)
            {
                _activity = activity;
                _signInId = signInId;
                _password = password;
            }

            public void SignIn()
            {
                var rootView = _activity.FindViewById<View>(Resource.Id.loginRootLinearLayout);
                var user = IntuneService.SignIn(_signInId, _password);
                if (user == null)
                {
                    Snackbar.Make(rootView, "Cannot login!!!", Snackbar.LengthLong)
                    .SetAction("OK", (v) => { })
                    .Show();
                    return;
                }

                var rememberMeCheckBox = _activity.FindViewById<CheckBox>(Resource.Id.signInRememberMeCheckBox);
                if (rememberMeCheckBox.Checked)
                    saveSignInCredentials();
                else
                    deleteSavedSignInCredentials();

                Snackbar.Make(rootView, "Loading accounts...", Snackbar.LengthLong).Show();
                showAccountsActivity(user);
            }

            private void saveSignInCredentials()
            {
                var userAccount = new Xamarin.Auth.Account { Username = _signInId };
                userAccount.Properties.Add("Password", _password);
                var store = AccountStore.Create();
                store.Save(userAccount, "IntuneTechnologiesApp");
            }

            private void deleteSavedSignInCredentials()
            {
                var store = AccountStore.Create();
                var userAccount = new Xamarin.Auth.Account { Username = _signInId };
                store.Delete(userAccount, "IntuneTechnologiesApp");
            }

            private void showAccountsActivity(User user)
            {
                var accountsActivity = new Intent(_activity, typeof(AccountsActivity));
                accountsActivity.PutExtra("LoginUserId", user.Id);
                accountsActivity.PutExtra("LoginUserName", user.Name);
                accountsActivity.PutExtra("LoginUserSignInId", user.Email);
                _activity.StartActivity(accountsActivity);
            }
        }
    }
}
