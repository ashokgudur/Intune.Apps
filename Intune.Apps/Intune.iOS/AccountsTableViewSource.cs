using System;
using System.Collections.Generic;
using Foundation;
using Intune.Shared.Model;
using UIKit;

namespace Intune.iOS
{
    public class AccountActionSheetActionExecutor : IActionSheetActionExecutor
    {
        public UIViewController Controller { get; set; }
        private MainController mainController { get; set; }
        public Account account;

        public AccountActionSheetActionExecutor(UIViewController controller, Account account)
        {
            this.Controller = controller;
            this.account = account;
            mainController = controller as MainController;
        }

        public void Execute(string action)
        {
            switch (action)
            {
                case AccountRowActionOptions.EditOrShare:
					mainController.DisplayAccountController(account);
					break;
                case AccountRowActionOptions.Entries:
					mainController.DisplayAccountEntriesController(account);
                    break;
                case AccountRowActionOptions.Comment:
					mainController.DisplayAccountCommentController(account);
					break;
                default:
                    throw new Exception($"Invalid option '{action}'");
            }
        }
    }

    public class AccountRowActionOptions
    {
        public const string EditOrShare = "Edit/Share";
        public const string Entries = "Entries";
        public const string Comment = "Comment";

    }

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
            ShowActionSheetAlert(account);
        }

        private void ShowActionSheetAlert(Account account)
        {
            var actionExecutor = new AccountActionSheetActionExecutor(mainController, account);
            var alert = ActionSheetAlert.Instance(actionExecutor);
            var options = new string[]
            {
                AccountRowActionOptions.EditOrShare,
                AccountRowActionOptions.Entries,
                AccountRowActionOptions.Comment,
            };

            alert.Show("What to do you want to do?", options, account.Name);
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
