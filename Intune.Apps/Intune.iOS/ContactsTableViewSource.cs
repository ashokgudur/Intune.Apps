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

        public ContactsTableViewSource(List<Contact> contacts)
        {
            this.contacts = contacts;
        }

        public override UITableViewCell GetCell(UITableView tableView, NSIndexPath indexPath)
        {
            var cell = tableView.DequeueReusableCell("ContactsTableViewCellId", indexPath) as ContactsTableViewCell ;
            var contact = contacts[indexPath.Row];
            cell.UpdateContactViewCell(contact);
            return cell;
        }

        public override nint RowsInSection(UITableView tableview, nint section)
        {
            return contacts.Count;
        }
    }
}