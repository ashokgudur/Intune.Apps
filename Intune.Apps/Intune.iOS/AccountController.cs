using Foundation;
using System;
using UIKit;
using Intune.Shared.Model;
using System.Collections.Generic;
using System.Linq;

namespace Intune.iOS
{
    public partial class AccountController : UIViewController
    {
        public User SignInUser { get; set; }
        public Account Account { get; set; }
        private List<Contact> contacts; 

        public AccountController(IntPtr handle) : base(handle)
        {
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
            MessageLabel.Text = "";
            SetViewTitle();
            NameTextField.Text = Account.Name;
            SetAccountSharingTableViewSource();
            SaveToolBarButton.Clicked += SaveToolBarButton_Clicked;
            CommentToolBarButton.Clicked += CommentToolBarButton_Clicked; ;
            CancelToolBarButton.Clicked += CancelToolBarButton_Clicked;
        }

        public void SetAccountSharingTableViewSource()
        {
            contacts = IntuneService.GetAccountSharedContacts(SignInUser.Id, Account.Id);
            AccountSharingTableView.Source = new AccountSharingTableViewSource(this, contacts);
        }

        private void SetViewTitle()
        {
            if (Account.IsNew)
                NavigationBar.TopItem.Title = "New Account";
            else
                NavigationBar.TopItem.Title = Account.Name;
        }

        void CommentToolBarButton_Clicked(object sender, EventArgs e)
        {
            //TODO: ...
            try
            {
                throw new NotImplementedException();
            }
            catch (Exception ex)
            {
                MessageLabel.Text = ex.Message;
            }
        }

        void CancelToolBarButton_Clicked(object sender, EventArgs e)
        {
            DismissViewController(true, null);
        }

        void SaveToolBarButton_Clicked(object sender, EventArgs e)
        {
            try
            {
                ValidateUserInput();
                FillObject();
                if (Account.IsNew)
                    IntuneService.AddAccount(Account);
                else
                    IntuneService.UpdateAccount(Account);

                SaveAccountSharing();
                DismissViewController(false, null);
            }
            catch (Exception ex)
            {
                MessageLabel.Text = ex.Message;
            }
        }

        private void SaveAccountSharing()
        {
			var accountShares = new List<UserAccountShareRole>();
			var sharedUsers = contacts.Where(c => c.ContactUserId > 0 &&
	                 						 c.AccountSharedRole != UserAccountRole.Owner).ToArray();
			foreach (var sharedUser in sharedUsers)
				accountShares.Add(
					new UserAccountShareRole
					{
						UserId = sharedUser.ContactUserId,
						Role = sharedUser.AccountSharedRole
					});

			IntuneService.AddAccountSharing(Account.Id, accountShares.ToArray());
		}

        private void FillObject()
        {
            Account.Name = NameTextField.Text.Trim();
            Account.UserId = SignInUser.Id;
            Account.AddedOn = DateTime.Now;
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