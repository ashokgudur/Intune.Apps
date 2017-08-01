using Foundation;
using System;
using UIKit;

namespace Intune.iOS
{
    public partial class MainController : UIViewController
    {
        public MainController(IntPtr handle) : base(handle)
        {
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
			
            //TODO: userId would need to be passed from SignIn view
			const int userId = 1;
			var contacts = IntuneService.GetAllContacts(userId);
			ContactsTableView.Source = new ContactsTableViewSource(contacts);

			MainViewTabBar.ItemSelected += (object sender, UITabBarItemEventArgs e) =>
            {
                switch (e.Item.Tag)
                {
                    case 1: //Accounts
                        showAlert("Accounts");
                        break;
                    case 2: //Contacts
                        showContactsView();
                        break;
					case 3: //User profile
						showAlert("User Profile");
						break;
					case 4: //Logout
						showAlert("Logout");
						break;
					default:
                        break;
                }
            };
		}

        private void showContactsView()
        {
			////TODO: userId would need to be passed from SignIn view
			//const int userId = 1; 
   //         var contacts = IntuneService.GetAllContacts(userId);
			//ContactsTableView.Source = new ContactsTableViewSource(contacts);
		}

        private void showAlert(string tabName)
        {
            var alert = UIAlertController.Create($"Intune {tabName}",
                                     "Add new contact has been pressed.",
                                     UIAlertControllerStyle.Alert);
            alert.AddAction(UIAlertAction.Create("OK", UIAlertActionStyle.Default, null));
            PresentViewController(alert, true, null);
        }
    }
}
