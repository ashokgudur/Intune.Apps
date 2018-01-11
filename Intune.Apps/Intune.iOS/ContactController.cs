using Foundation;
using System;
using UIKit;
using Intune.Shared.Model;

namespace Intune.iOS
{
    public partial class ContactController : UIViewController
    {
        public User SignInUser { get; set; }
        public Contact Contact { get; set; }

        public ContactController(IntPtr handle) : base(handle)
        {
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
            MessageLabel.Text = "";
            SetViewTitle();
            FillControls();
            SaveToolBarButton.Clicked += SaveToolBarButton_Clicked;
            ChatToolBarButton.Clicked += ChatToolBarButton_Clicked;
            CancelToolBarButton.Clicked += CancelToolBarButton_Clicked;
        }

        private void FillControls()
        {
            FullNameTextField.Text = Contact.Name;
            EmailTextField.Text = Contact.Email;
            MobileTextField.Text = Contact.Mobile;
            AddressTextField.Text = Contact.Address;
        }

        private void SetViewTitle()
        {
            if (Contact.IsNew)
                NavigationBar.TopItem.Title = "New Contact";
            else
                NavigationBar.TopItem.Title = Contact.Name;
        }

        void ChatToolBarButton_Clicked(object sender, EventArgs e)
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
                if (Contact.IsNew)
                    IntuneService.AddContact(Contact);
                else
                    IntuneService.UpdateContact(Contact);

                DismissViewController(false, null);
            }
            catch (Exception ex)
            {
                MessageLabel.Text = ex.Message;
            }
        }

        private void FillObject()
        {
            Contact.Name = FullNameTextField.Text.Trim();
            Contact.Email = EmailTextField.Text.Trim();
            Contact.Mobile = MobileTextField.Text.Trim();
            Contact.Address = AddressTextField.Text.Trim();
            Contact.CreatedOn = DateTime.Now;
            Contact.UserId = SignInUser.Id;
        }

        private void ValidateUserInput()
        {
            if (string.IsNullOrWhiteSpace(FullNameTextField.Text.Trim()))
            {
                throw new ArgumentException("Enter full name");
            }

            if (string.IsNullOrWhiteSpace(EmailTextField.Text.Trim()))
            {
                throw new ArgumentException("Enter email address");
            }

            if (string.IsNullOrWhiteSpace(MobileTextField.Text.Trim()))
            {
                throw new ArgumentException("Enter mobile number");
            }

            if (string.IsNullOrWhiteSpace(AddressTextField.Text.Trim()))
            {
                throw new ArgumentException("Enter address");
            }
        }
    }
}