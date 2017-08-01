using System;
using System.Collections.Generic;
using Foundation;
using Intune.Shared.Model;
using UIKit;

namespace Intune.iOS
{
    public class ContactsTableViewSource : UITableViewSource
    {
        private List<Contact> contacts;
        private MainController mainController;

        public ContactsTableViewSource(MainController mainController, List<Contact> contacts)
        {
            this.mainController = mainController;
            this.contacts = contacts;
        }

        public override void RowSelected(UITableView tableView, NSIndexPath indexPath)
        {
            var contact = contacts[indexPath.Row];
            mainController.DisplayContactController(contact);
            tableView.DeselectRow(indexPath, true);
        }

        public override UITableViewCell GetCell(UITableView tableView, NSIndexPath indexPath)
        {
            var contact = contacts[indexPath.Row];
            var cell = tableView.DequeueReusableCell("ContactsTableViewCellId", indexPath) as ContactsTableViewCell;
            cell.UpdateContactViewCell(contact);
            return cell;
        }

        public override nint RowsInSection(UITableView tableview, nint section)
        {
            return contacts.Count;
        }
    }
}