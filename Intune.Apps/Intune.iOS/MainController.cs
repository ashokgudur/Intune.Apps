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

        public User SignInUser { get; set; }

        public MainController(IntPtr handle) : base(handle)
        {
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            HookEventHandlers();
            LayoutViewContent();
            SetContactsTableViewSource();
            SetAccountsTableViewSource();
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

        private void SetAccountsTableViewSource()
        {
            //TODO: contactId would come from contacts to display accounts of a given contact
            const int contactId = 0;
            var accounts = IntuneService.GetAllAccounts(SignInUser.Id, contactId);
            AccountsTableView.Source = new AccountsTableViewSource(accounts);
        }

        private void SetContactsTableViewSource()
        {
            var contacts = IntuneService.GetAllContacts(SignInUser.Id);
            ContactsTableView.Source = new ContactsTableViewSource(contacts);
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
                    DisplayAccountController();
                else if (MainViewTabBar.SelectedItem.Tag == contactsViewTagId)
                    DisplayContactController();
            }
            catch (Exception ex)
            {
                ShowAlert(ex.Message);
            }
        }

        private void DisplayAccountController()
        {
            var accountController = this.Storyboard
                                    .InstantiateViewController("AccountController")
                                    as AccountController;
            if (accountController != null)
            {
                accountController.SignInUser = SignInUser;
                accountController.Account = new Account();
				PresentViewController(accountController, true, null);
                SetAccountsTableViewSource();
            }
        }

        private void DisplayContactController()
        {
            var contactController = this.Storyboard
                                    .InstantiateViewController("ContactController")
                                    as ContactController;
            if (contactController != null)
            {
                contactController.SignInUser = SignInUser;
                contactController.Contact = new Contact();
				PresentViewController(contactController, true, null);
                SetContactsTableViewSource();
            }
        }

        private void RefreshToolBarButton_Clicked(object sender, EventArgs e)
        {
            try
            {
                if (MainViewTabBar.SelectedItem.Tag == accountsViewTagId)
                    SetAccountsTableViewSource();
                else if (MainViewTabBar.SelectedItem.Tag == contactsViewTagId)
                    SetContactsTableViewSource();
            }
            catch (Exception ex)
            {
                ShowAlert(ex.Message);
            }
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
