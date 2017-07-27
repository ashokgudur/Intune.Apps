using System;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Views;
using Android.Widget;
using Intune.Shared.Model;

namespace Intune.Android
{
    [Activity(Label = "Accounts - Intune")]
    public class AccountsActivity : Activity
    {
        AccountsAdapter _accountsAdapter = null;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.Accounts);

            var contactId = Intent.GetIntExtra("ContactId", 0);
            if (contactId == 0)
            {
                var loginUserName = Intent.GetStringExtra("LoginUserName");
                Title = string.Format("{0} - Accounts", loginUserName);
            }
            else
            {
                var contactName = Intent.GetStringExtra("ContactName");
                Title = string.Format("{0} - Accounts", contactName);
            }

            var accountsListView = FindViewById<ListView>(Resource.Id.accountsListView);
            accountsListView.ItemClick +=
                (object sender, AdapterView.ItemClickEventArgs e) =>
                {
                    var obj = accountsListView.GetItemAtPosition(e.Position);
                    var account = ((JavaObjectWrapper<Account>)obj).Obj;
                    showAccountEntriesActivity(account);
                };
        }

        private void refreshList()
        {
            var loginUserId = Intent.GetIntExtra("LoginUserId", 0);
            var contactId = Intent.GetIntExtra("ContactId", 0);
            _accountsAdapter = new AccountsAdapter(this, loginUserId, contactId);
            var accountsListView = FindViewById<ListView>(Resource.Id.accountsListView);
            accountsListView.Adapter = _accountsAdapter;
        }

        protected override void OnResume()
        {
            base.OnResume();
            refreshList();
        }

        public override bool OnCreateOptionsMenu(IMenu menu)
        {
            MenuInflater.Inflate(Resource.Menu.accounts_menus, menu);
            return base.OnCreateOptionsMenu(menu);
        }

        public override bool OnPrepareOptionsMenu(IMenu menu)
        {
            var userProfileMenuItem = menu.FindItem(Resource.Id.accounts_menu_profile);
            userProfileMenuItem.SetEnabled(false);

            var contactId = Intent.GetIntExtra("ContactId", 0);
            if (contactId != 0)
            {
                var logoutMenuItem = menu.FindItem(Resource.Id.accounts_menu_logout);
                logoutMenuItem.SetEnabled(false);
            }

            return base.OnPrepareOptionsMenu(menu);
        }

        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            switch (item.ItemId)
            {
                case Resource.Id.accounts_menu_refresh:
                    refreshList();
                    break;
                case Resource.Id.accounts_menu_logout:
                    performLogout();
                    break;
                case Resource.Id.accounts_menu_profile:
                    showUserProfileActivity();
                    break;
                case Resource.Id.accounts_menu_contacts:
                    showContactsActivity();
                    break;
                case Resource.Id.accounts_menu_new:
                    showAccountActivity();
                    break;
                default:
                    break;
            }

            return base.OnOptionsItemSelected(item);
        }

        public override void OnBackPressed() { }

        private void performLogout()
        {
            Finish();
            deleteSavedSignInCredentials();
            var signInActivity = new Intent(this, typeof(SignInActivity));
            signInActivity.SetFlags(ActivityFlags.ClearTop | ActivityFlags.NewTask);
            StartActivity(signInActivity);
        }

        private void deleteSavedSignInCredentials()
        {
            var store = Xamarin.Auth.AccountStore.Create();
            var signInId = Intent.GetStringExtra("LoginUserSignInId");
            var userAccount = new Xamarin.Auth.Account { Username = signInId };
            store.Delete(userAccount, "IntuneTechnologiesApp");
        }

        private void showUserProfileActivity()
        {
            var loginUserId = Intent.GetIntExtra("LoginUserId", 0);
            var loginUserName = Intent.GetStringExtra("LoginUserName");
            var userProfileActivity = new Intent(this, typeof(UserProfileActivity));
            userProfileActivity.PutExtra("LoginUserId", loginUserId);
            userProfileActivity.PutExtra("LoginUserName", loginUserName);
            StartActivity(userProfileActivity);
        }

        private void showContactsActivity()
        {
            var loginUserId = Intent.GetIntExtra("LoginUserId", 0);
            var loginUserName = Intent.GetStringExtra("LoginUserName");
            var contactsActivity = new Intent(this, typeof(ContactsActivity));
            contactsActivity.PutExtra("LoginUserId", loginUserId);
            contactsActivity.PutExtra("LoginUserName", loginUserName);
            StartActivity(contactsActivity);
        }

        private void showAccountActivity()
        {
            var loginUserId = Intent.GetIntExtra("LoginUserId", 0);
            var accountActivity = new Intent(this, typeof(AccountActivity));
            accountActivity.PutExtra("LoginUserId", loginUserId);
            StartActivity(accountActivity);
        }

        private void showAccountEntriesActivity(Account account)
        {
            var loginUserId = Intent.GetIntExtra("LoginUserId", 0);
            var accountEntriesActivity = new Intent(this, typeof(AccountEntriesActivity));
            accountEntriesActivity.PutExtra("LoginUserId", loginUserId);
            accountEntriesActivity.PutExtra("AccountId", account.Id);
            accountEntriesActivity.PutExtra("AccountName", account.Name);
            accountEntriesActivity.PutExtra("AccountRole", (int)account.Role);
            StartActivity(accountEntriesActivity);
        }
    }
}