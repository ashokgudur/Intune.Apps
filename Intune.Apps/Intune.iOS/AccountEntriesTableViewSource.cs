using System;
using System.Collections.Generic;
using Foundation;
using Intune.Shared.Model;
using UIKit;

namespace Intune.iOS
{
    public class AccountEntryActionSheetActionExecutor : IActionSheetActionExecutor
    {
        public UIViewController Controller { get; set; }
        private AccountEntriesController parentController { get; set; }
        public Entry entry;

        public AccountEntryActionSheetActionExecutor(UIViewController controller, Entry entry)
        {
            this.Controller = controller;
            this.entry = entry;
            parentController = controller as AccountEntriesController;
        }

        public void Execute(string action)
        {
            switch (action)
            {
                case AccountEntryRowActionOptions.View:
                    parentController.DisplayAccountEntryController(entry);
                    break;
                case AccountEntryRowActionOptions.Void:
                    parentController.VoidAccountEntry(entry);
                    break;
                case AccountEntryRowActionOptions.Comment:
                    parentController.CommentOnAccountEntry(entry);
                    break;
                default:
                    throw new Exception($"Invalid option '{action}'");
            }
        }
    }

    public class AccountEntryRowActionOptions
    {
        public const string View = "View";
        public const string Void = "Void";
        public const string Comment = "Comment";
    }

    public class AccountEntriesTableViewSource : UITableViewSource
    {
        private List<Entry> entries;
        private AccountEntriesController parentController;

        public AccountEntriesTableViewSource(AccountEntriesController parentController, List<Entry> entries)
        {
            this.parentController = parentController;
            this.entries = entries;
        }

        public override void RowSelected(UITableView tableView, NSIndexPath indexPath)
        {
            var entry = entries[indexPath.Row];
            tableView.DeselectRow(indexPath, true);
            ShowActionSheetAlert(entry);
        }

        private void ShowActionSheetAlert(Entry entry)
        {
            var actionExecutor = new AccountEntryActionSheetActionExecutor(parentController, entry);
            var alert = ActionSheetAlert.Instance(actionExecutor);
            var options = new string[] { AccountEntryRowActionOptions.Comment };

            if (!entry.IsVoid)
                options = new string[]
                {
                    AccountEntryRowActionOptions.View,
                    AccountEntryRowActionOptions.Void,
                    AccountEntryRowActionOptions.Comment,
                };

            alert.Show("What to do you want to do?", options, entry.Notes);
        }

        public override UITableViewCell GetCell(UITableView tableView, NSIndexPath indexPath)
        {
            var entry = entries[indexPath.Row];
            var cell = tableView.DequeueReusableCell("AccountEntriesTableViewCellId", indexPath) as AccountEntriesTableViewCell;
            cell.UpdateAccountViewCell(entry);
            return cell;
        }

        public override nint RowsInSection(UITableView tableview, nint section)
        {
            return entries.Count;
        }
    }
}