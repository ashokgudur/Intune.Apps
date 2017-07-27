using System;
using Android.App;
using Android.OS;
using Android.Widget;
using Android.Views;
using Android.Content;
using Intune.Shared.Model;

namespace Intune.Android
{
    [Activity(Label = "Contact - Intune")]
    public class ContactActivity : Activity
    {
        Contact _contact = null;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.Contact);

            var contactId = Intent.GetIntExtra("ContactId", 0);
            _contact = IntuneService.GetContact(contactId);

            if (_contact.IsNew)
                Title = "New Contact - Intune";
            else
                Title = string.Format("{0} - Intune", _contact.Name);

            fillForm();

            var okButton = FindViewById<Button>(Resource.Id.contactOkButton);
            okButton.Click += OkButton_Click; ;
        }

        public override bool OnCreateOptionsMenu(IMenu menu)
        {
            MenuInflater.Inflate(Resource.Menu.contact_menus, menu);
            return base.OnCreateOptionsMenu(menu);
        }

        public override bool OnPrepareOptionsMenu(IMenu menu)
        {
            var chatMenuItem = menu.FindItem(Resource.Id.contact_menu_chat);
            chatMenuItem.SetEnabled(_contact.HasIntune());

            var accountsMenuItem = menu.FindItem(Resource.Id.contact_menu_accounts);
            accountsMenuItem.SetVisible(!_contact.IsNew);

            var shareContactMenuItem = menu.FindItem(Resource.Id.contact_menu_share);
            shareContactMenuItem.SetVisible(!_contact.IsNew);

            return base.OnPrepareOptionsMenu(menu);
        }

        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            switch (item.ItemId)
            {
                case Resource.Id.contact_menu_accounts:
                    showAccountsActivity();
                    break;
                case Resource.Id.contact_menu_chat:
                    showMessageBoardActivity();
                    break;
                case Resource.Id.contact_menu_share:
                    //TODO: Contact sharing?
                    //showContactActivity(0);
                    break;
                default:
                    break;
            }

            return base.OnOptionsItemSelected(item);
        }

        private void OkButton_Click(object sender, EventArgs e)
        {
            var result = FindViewById<TextView>(Resource.Id.contactResultTextView);

            var mobile = FindViewById<EditText>(Resource.Id.contactMobileEditText);
            var mobileNumber = mobile.Text.Trim();
            if (!string.IsNullOrWhiteSpace(mobileNumber))
            {
                var mnv = new MobileNumberValidator(mobile.Text.Trim());
                if (!mnv.IsValid())
                {
                    result.Text = "Mobile number not valid!";
                    return;
                }

                mobileNumber = mnv.GetFullMobileNumber();
            }

            var fullName = FindViewById<EditText>(Resource.Id.contactNameEditText);
            var email = FindViewById<EditText>(Resource.Id.contactEmailEditText);
            var address = FindViewById<EditText>(Resource.Id.contactAddressEditText);

            _contact.Name = fullName.Text;
            _contact.Email = email.Text.Trim().ToLower();
            _contact.Mobile = mobileNumber;
            _contact.Address = address.Text;

            if (!_contact.IsValid())
            {
                result.Text = "Please enter all the details...";
                return;
            }

            if (_contact.IsNew)
            {
                _contact.UserId = Intent.GetIntExtra("LoginUserId", 0);
                _contact.CreatedOn = DateTime.Now;
            }

            if (_contact.IsNew)
                result.Text = "Adding new contact...";
            else
                result.Text = "Updating contact...";

            try
            {
                if (_contact.IsNew)
                    _contact = IntuneService.AddContact(_contact);
                else
                    _contact = IntuneService.UpdateContact(_contact);

                if (_contact == null)
                    result.Text = string.Format("Cannot save this contact!", _contact.Name);
                else
                    result.Text = string.Format("Contact {0} saved.", _contact.Name);
            }
            catch (Exception)
            {
                result.Text = string.Format("Cannot save this contact!", _contact.Name);
            }
        }

        private void fillForm()
        {
            var fullName = FindViewById<EditText>(Resource.Id.contactNameEditText);
            var email = FindViewById<EditText>(Resource.Id.contactEmailEditText);
            var mobile = FindViewById<EditText>(Resource.Id.contactMobileEditText);
            var address = FindViewById<EditText>(Resource.Id.contactAddressEditText);

            fullName.Text = _contact.Name;
            email.Text = _contact.Email;
            mobile.Text = _contact.Mobile;
            address.Text = _contact.Address;
        }

        private void showAccountsActivity()
        {
            var loginUserId = Intent.GetIntExtra("LoginUserId", 0);
            var loginUserName = Intent.GetStringExtra("LoginUserName");
            var accountsActivity = new Intent(this, typeof(AccountsActivity));
            accountsActivity.PutExtra("LoginUserId", loginUserId);
            accountsActivity.PutExtra("LoginUserName", loginUserName);
            accountsActivity.PutExtra("ContactId", _contact.Id);
            accountsActivity.PutExtra("ContactName", _contact.Name);
            StartActivity(accountsActivity);
        }

        private void showMessageBoardActivity()
        {
            var loginUserId = Intent.GetIntExtra("LoginUserId", 0);
            var loginUserName = Intent.GetStringExtra("LoginUserName");
            var messageBoardActivity = new Intent(this, typeof(ChatboardActivity));
            messageBoardActivity.PutExtra("ByUserId", loginUserId);
            messageBoardActivity.PutExtra("ToUserId", _contact.ContactUserId);
            StartActivity(messageBoardActivity);
        }
    }
}
