using System;
using System.Collections.Generic;
using Foundation;
using Intune.Shared.Model;
using UIKit;

namespace Intune.iOS
{
    internal class AccountsTableViewSource : UITableViewSource
    {
        private List<Account> accounts;

        public AccountsTableViewSource(List<Account> accounts)
        {
            this.accounts = accounts;
        }

        public override UITableViewCell GetCell(UITableView tableView, NSIndexPath indexPath)
        {
            var cell = tableView.DequeueReusableCell("AccountsTableViewCellId", indexPath)
                                as AccountsTableViewCell;

            var account = accounts[indexPath.Row];

            cell.UpdateAccountViewCell(account);
            return cell;
        }

        public override nint RowsInSection(UITableView tableview, nint section)
        {
            return accounts.Count;
        }
    }
}