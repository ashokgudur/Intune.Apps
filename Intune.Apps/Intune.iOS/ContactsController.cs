using Foundation;
using System;
using UIKit;

namespace Intune.iOS
{
    public partial class ContactsController : UITableViewController
    {
        public ContactsController (IntPtr handle) : base (handle)
        {
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            var contacts = IntuneService.GetAllContacts(1);
            ContactsTableView.Source = new ContactsTableViewSource(contacts);

        }
    }
}