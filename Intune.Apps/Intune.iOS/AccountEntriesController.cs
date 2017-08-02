using Foundation;
using System;
using UIKit;
using Intune.Shared.Model;

namespace Intune.iOS
{
    public partial class AccountEntriesController : UIViewController
    {
        private UIRefreshControl RefreshControl;

        public User SignInUser { get; set; }
        public Account Account { get; set; }

        public AccountEntriesController(IntPtr handle) : base(handle)
        {
        }

        public override void ViewWillAppear(bool animated)
        {
            base.ViewWillAppear(animated);
            SetAccountEntriesTableViewSource();
        }

        public void SetAccountEntriesTableViewSource()
        {
            var entries = IntuneService.GetAccountEntries(Account.Id);
            AccountEntriesTableView.Source = new AccountEntriesTableViewSource(this, entries);
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
            NavigationBar.TopItem.Title = Account.Name;
            AddRefreshControls();
            HookEventHandlers();
            SetAccountEntriesTableViewSource();
        }

        void AddRefreshControls()
        {
            if (!UIDevice.CurrentDevice.CheckSystemVersion(6, 0))
                return;

            RefreshControl = new UIRefreshControl();
            RefreshControl.ValueChanged += (sender, e) => { RunRefreshControl(); };
            AccountEntriesTableView.Add(RefreshControl);
        }

        void RunRefreshControl()
        {
            InvokeOnMainThread(() => { RefreshControl.BeginRefreshing(); });
            SetAccountEntriesTableViewSource();
            InvokeOnMainThread(() => { RefreshControl.EndRefreshing(); });
        }

        private void HookEventHandlers()
        {
            CloseToolBarButton.Clicked += CloseToolBarButton_Clicked;
            AddNewToolBarButton.Clicked += AddNewToolBarButton_Clicked;
            RefreshToolBarButton.Clicked += RefreshToolBarButton_Clicked;
            CommentToolBarButton.Clicked += CommentToolBarButton_Clicked;
        }

        void CloseToolBarButton_Clicked(object sender, EventArgs e)
        {
            DismissViewController(true, null);
        }

        private void CommentToolBarButton_Clicked(object sender, EventArgs e)
        {
            try
            {
                throw new NotImplementedException();
            }
            catch (Exception ex)
            {
                MessageAlert.Instance(this).Show(ex.Message);
            }
        }

        private void AddNewToolBarButton_Clicked(object sender, EventArgs e)
        {
            try
            {
                DisplayAccountEntryController(new Entry());
            }
            catch (Exception ex)
            {
                MessageAlert.Instance(this).Show(ex.Message);
            }
        }

        private void RefreshToolBarButton_Clicked(object sender, EventArgs e)
        {
            try
            {
                SetAccountEntriesTableViewSource();
            }
            catch (Exception ex)
            {
                MessageAlert.Instance(this).Show(ex.Message);
            }
        }

        public void DisplayAccountEntryController(Entry entry)
        {
            //         var controller = Storyboard.InstantiateViewController("AccountEntryController") as AccountEntryController;
            //if (controller == null)
            //	throw new Exception("Could not find 'AccountEntryController'");

            //controller.SignInUser = SignInUser;
            //controller.Contact = contact;
            //PresentViewController(controller, true, null);
        }
    }
}
