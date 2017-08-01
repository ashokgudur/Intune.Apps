using Foundation;
using System;
using UIKit;

namespace Intune.iOS
{
    public partial class ContactsController : UITableViewController
    {
        public ContactsController(IntPtr handle) : base(handle)
        {
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            var contacts = IntuneService.GetAllContacts(1);
            ContactsTableView.Source = new ContactsTableViewSource(contacts);

        }

        //partial void AddContactToolBarButton_Activated(UIBarButtonItem sender)
        //{
        //    var alert = UIAlertController.Create("Intune Contacts",
        //                             "Add new contact has been pressed.",
        //                             UIAlertControllerStyle.Alert);
        //    alert.AddAction(UIAlertAction.Create("OK", UIAlertActionStyle.Default, null));
        //    PresentViewController(alert, true, null);
        //}
    }
}