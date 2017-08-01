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
            SaveButton.TouchUpInside += SaveButton_TouchUpInside;
            CancelButton.TouchUpInside += CancelButton_TouchUpInside;
        }

        private void SetViewTitle()
        {
            if (Contact.IsNew)
                ContactTitle.Text = "New Contact";
            else
                ContactTitle.Text = $"Contact - {Contact.Name}";
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
                IntuneService.AddContact(FillObject());
                DismissViewController(false, null);
            }
            catch (Exception ex)
            {
                MessageLabel.Text = ex.Message;
            }
        }

        private Contact FillObject()
        {
            return new Contact
            {
                Name = FullNameTextField.Text.Trim(),
                Email = EmailTextField.Text.Trim(),
                Mobile = MobileTextField.Text.Trim(),
                Address = AddressTextField.Text.Trim(),
                CreatedOn = DateTime.Now,
                UserId = SignInUser.Id,
            };
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