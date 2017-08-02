using Foundation;
using System;
using UIKit;
using Intune.Shared.Model;

namespace Intune.iOS
{
    public partial class MainController : UIViewController
    {
        private const int accountsViewTagId = 1;
        private const int contactsViewTagId = 2;
        private const int userProfileViewTagId = 3;
        private const int logoutViewTagId = 4;
		private UIRefreshControl AccountsRefreshControl;
        private UIRefreshControl ContactsRefreshControl;

		public User SignInUser { get; set; }

        public MainController(IntPtr handle) : base(handle)
        {
        }

        public override void ViewWillAppear(bool animated)
        {
            base.ViewWillAppear(animated);

            if (MainViewTabBar.SelectedItem.Tag == accountsViewTagId)
                SetAccountsTableViewSource();
            else if (MainViewTabBar.SelectedItem.Tag == contactsViewTagId)
                SetContactsTableViewSource();
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
            AddRefreshControls();
            HookEventHandlers();
            LayoutViewContent();
            SetContactsTableViewSource();
            MainViewTabBar.SelectedItem = AccountsTabBarItem;
            DisplayAccountsView();
        }

        private void HookEventHandlers()
        {
            AddNewToolBarButton.Clicked += AddNewToolBarButton_Clicked;
            RefreshToolBarButton.Clicked += RefreshToolBarButton_Clicked;
            MainViewTabBar.ItemSelected += MainViewTabBar_ItemSelected;
        }

        private void LayoutViewContent()
        {
            var contentViewFrame = ContentView.Frame;
            contentViewFrame.X = 0;
            contentViewFrame.Y = 0;
            ContactsTableView.Frame = contentViewFrame;
            AccountsTableView.Frame = contentViewFrame;
        }

        void AddRefreshControls()
        {
            if (!UIDevice.CurrentDevice.CheckSystemVersion(6, 0))
                return;

            AccountsRefreshControl = new UIRefreshControl();
            AccountsRefreshControl.ValueChanged += (sender, e) => { RunAccountsRefreshControl(); };
            AccountsTableView.Add(AccountsRefreshControl);

			ContactsRefreshControl = new UIRefreshControl();
			ContactsRefreshControl.ValueChanged += (sender, e) => { RunContactsRefreshControl(); };
			ContactsTableView.Add(ContactsRefreshControl);
		}

        private void DisplayContactsView()
        {
            this.MainViewNavigationBar.TopItem.Title = "Intune - Contacts";
            AccountsTableView.Hidden = true;
            ContactsTableView.Hidden = false;
        }

        private void DisplayAccountsView()
        {
            this.MainViewNavigationBar.TopItem.Title = "Intune - Accounts";
            ContactsTableView.Hidden = true;
            AccountsTableView.Hidden = false;
        }

        private void NavigateToSignInView()
        {
            var storyboard = UIStoryboard.FromName("Main", NSBundle.MainBundle);
            var signInController = (UIViewController)storyboard.InstantiateViewController("SignInController");
            DismissViewController(true, null);
            PresentViewController(signInController, true, null);
        }

        public void SetAccountsTableViewSource()
        {
            //TODO: contactId would come from contacts to display accounts of a given contact
            const int contactId = 0;
            var accounts = IntuneService.GetAllAccounts(SignInUser.Id, contactId);
            AccountsTableView.Source = new AccountsTableViewSource(this, accounts);
        }

        public void SetContactsTableViewSource()
        {
            var contacts = IntuneService.GetAllContacts(SignInUser.Id);
            ContactsTableView.Source = new ContactsTableViewSource(this, contacts);
        }

        private void MainViewTabBar_ItemSelected(object sender, UITabBarItemEventArgs e)
        {
            switch (e.Item.Tag)
            {
                case accountsViewTagId:
                    DisplayAccountsView();
                    break;
                case contactsViewTagId:
                    DisplayContactsView();
                    break;
                case userProfileViewTagId:
                    ShowAlert("User Profile...");
                    break;
                case logoutViewTagId:
                    NavigateToSignInView();
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private void AddNewToolBarButton_Clicked(object sender, EventArgs e)
        {
            try
            {
                if (MainViewTabBar.SelectedItem.Tag == accountsViewTagId)
                    DisplayAccountController(new Account());
                else if (MainViewTabBar.SelectedItem.Tag == contactsViewTagId)
                    DisplayContactController(new Contact());
            }
            catch (Exception ex)
            {
                ShowAlert(ex.Message);
            }
        }

        public void DisplayAccountController(Account account)
        {
            var accountController = Storyboard.InstantiateViewController("AccountController") as AccountController;
            if (accountController == null)
                throw new Exception("Could not find the view controller by Id: 'AccountController'");

            accountController.SignInUser = SignInUser;
            accountController.Account = account;
            PresentViewController(accountController, true, null);
        }

        public void DisplayContactController(Contact contact)
        {
            var contactController = Storyboard.InstantiateViewController("ContactController") as ContactController;
            if (contactController == null)
                throw new Exception("Could not find the view controller by Id: 'ContactController'");

            contactController.SignInUser = SignInUser;
            contactController.Contact = contact;
            PresentViewController(contactController, true, null);
        }

        private void RefreshToolBarButton_Clicked(object sender, EventArgs e)
        {
            try
            {
                RefreshList();
            }
            catch (Exception ex)
            {
                ShowAlert(ex.Message);
            }
        }

        void RunAccountsRefreshControl()
        {
            InvokeOnMainThread(() => { AccountsRefreshControl.BeginRefreshing(); });
			SetAccountsTableViewSource();
            InvokeOnMainThread(() => { AccountsRefreshControl.EndRefreshing(); });
        }

		void RunContactsRefreshControl()
		{
			InvokeOnMainThread(() => { ContactsRefreshControl.BeginRefreshing(); });
			SetContactsTableViewSource();
			InvokeOnMainThread(() => { ContactsRefreshControl.EndRefreshing(); });
		}

		private void RefreshList()
        {
            if (MainViewTabBar.SelectedItem.Tag == accountsViewTagId)
                SetAccountsTableViewSource();
            else if (MainViewTabBar.SelectedItem.Tag == contactsViewTagId)
                SetContactsTableViewSource();
        }

        private void ShowAlert(string message)
        {
            //TODO: make this centralized alert to be used throught the app
            var alert = UIAlertController.Create("Intune", message, UIAlertControllerStyle.Alert);
            alert.AddAction(UIAlertAction.Create("OK", UIAlertActionStyle.Default, null));
            PresentViewController(alert, true, null);
        }
    }
}
