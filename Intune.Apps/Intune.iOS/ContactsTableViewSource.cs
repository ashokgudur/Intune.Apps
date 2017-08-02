using System;
using System.Collections.Generic;
using Foundation;
using Intune.Shared.Model;
using UIKit;

namespace Intune.iOS
{
	internal class ContactActionSheetActionExecutor : IActionSheetActionExecutor
	{
		public UIViewController Controller { get; set; }
		private MainController mainController { get; set; }
        public Contact contact;

        public ContactActionSheetActionExecutor(UIViewController controller, Contact contact)
		{
			this.Controller = controller;
            this.contact = contact;
			mainController = controller as MainController;
		}

		public void Execute(string action)
		{
			switch (action)
			{
				case ContactRowActionOptions.ViewOrEdit:
                    mainController.DisplayContactController(contact);
					break;
                case ContactRowActionOptions.Chat:
					break;
				default:
					throw new Exception($"Invalid option '{action}'");
			}
		}
	}

	public class ContactRowActionOptions
	{
		public const string ViewOrEdit = "View/Edit";
		public const string Chat = "Chat";
	}

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
            tableView.DeselectRow(indexPath, true);
            ShowActionSheetAlert(contact);
		}

        private void ShowActionSheetAlert(Contact contact)
		{
            var actionExecutor = new ContactActionSheetActionExecutor(mainController, contact);
			var alert = ActionSheetAlert.Instance(actionExecutor);
			var options = new string[]
			{
				ContactRowActionOptions.ViewOrEdit,
				ContactRowActionOptions.Chat,
			};

			alert.Show("What to do you want to do?", options, contact.Name);
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