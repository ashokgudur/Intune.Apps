using Foundation;
using System;
using UIKit;

namespace Intune.iOS
{
    public partial class AccountrsController : UITableViewController
    {
        public AccountrsController (IntPtr handle) : base (handle)
        {
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
            var accounts = IntuneService.GetAllAccounts(1, 0);
            AccountsTableView.Source = new AccountsTableViewSource(accounts);
        }
    }
}