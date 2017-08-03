using System;
using System.Collections.Generic;
using Foundation;
using Intune.Shared.Model;
using UIKit;

namespace Intune.iOS
{
    public class AccountSharingTableViewSource : UITableViewSource
    {
        private List<Contact> contacts;
        private AccountController Controller;

        public AccountSharingTableViewSource(AccountController controller, List<Contact> contacts)
        {
            this.Controller = controller;
            this.contacts = contacts;
        }

        public override void RowSelected(UITableView tableView, NSIndexPath indexPath)
        {
            var contact = contacts[indexPath.Row];
            tableView.DeselectRow(indexPath, true);
        }

        public override UITableViewCell GetCell(UITableView tableView, NSIndexPath indexPath)
        {
            var contact = contacts[indexPath.Row];
            var cell = tableView.DequeueReusableCell("AccountSharingTableViewCellId", indexPath) as AccountSharingTableViewCell;
            cell.FillTableViewCell(contact);
            return cell;
        }

        public override nint RowsInSection(UITableView tableview, nint section)
        {
            return contacts.Count;
        }
    }
}
