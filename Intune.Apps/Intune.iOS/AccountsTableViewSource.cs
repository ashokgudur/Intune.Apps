using System;
using System.Collections.Generic;
using Foundation;
using Intune.Shared.Model;
using UIKit;

namespace Intune.iOS
{
    public class AccountsTableViewSource : UITableViewSource
    {
        private List<Account> accounts;
        private MainController mainController;

        public AccountsTableViewSource(MainController mainController, List<Account> accounts)
        {
            this.mainController = mainController;
            this.accounts = accounts;
        }

        public override void RowSelected(UITableView tableView, NSIndexPath indexPath)
        {
            var account = accounts[indexPath.Row];
            tableView.DeselectRow(indexPath, true);
			mainController.DisplayAccountEntriesController(account);
        }

        public override UITableViewCell GetCell(UITableView tableView, NSIndexPath indexPath)
        {
            var account = accounts[indexPath.Row];
            var cell = tableView.DequeueReusableCell("AccountsTableViewCellId", indexPath) as AccountsTableViewCell;
            cell.UpdateAccountViewCell(account);
            return cell;
        }

        public override nint RowsInSection(UITableView tableview, nint section)
        {
            return accounts.Count;
        }
    }
}
