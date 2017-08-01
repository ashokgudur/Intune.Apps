using Foundation;
using System;
using UIKit;
using Intune.Shared.Model;

namespace Intune.iOS
{
    public partial class AccountController : UIViewController
    {
        public User SignInUser { get; set; }
        public Account Account { get; set; }

        public AccountController(IntPtr handle) : base(handle)
        {
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
			MessageLabel.Text = "";
			SetViewTitle();
            SaveButton.TouchUpInside += SaveButton_TouchUpInside;
            CancelButton.TouchUpInside += CancelButton_TouchUpInside;
        }

        private void SetViewTitle()
        {
            if (Account.IsNew)
                AccountTitle.Text = "New Account";
            else
                AccountTitle.Text = $"Account - {Account.Name}";
        }

        void CancelButton_TouchUpInside(object sender, EventArgs e)
        {
            DismissViewController(true, null);
        }

        void SaveButton_TouchUpInside(object sender, EventArgs e)
        {
            try
            {
                ValidateUserInput();
                IntuneService.AddAccount(FillObject());
                DismissViewController(false, null);
            }
            catch (Exception ex)
            {
                MessageLabel.Text = ex.Message;
            }
        }

        private Account FillObject()
        {
            return new Account
            {
                Name = NameTextField.Text.Trim(),
                UserId = SignInUser.Id,
                AddedOn = DateTime.Now,
            };
        }

        private void ValidateUserInput()
        {
            if (string.IsNullOrWhiteSpace(NameTextField.Text.Trim()))
            {
                throw new ArgumentException("Enter account name");
            }
        }
    }
}