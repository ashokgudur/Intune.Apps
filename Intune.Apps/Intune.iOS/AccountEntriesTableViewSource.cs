using System;
using System.Collections.Generic;
using System.Linq;
using Foundation;
using Intune.Shared.Model;
using UIKit;

namespace Intune.iOS
{
    public class AccountEntryActionSheetActionExecutor : IActionSheetActionExecutor
    {
        public UIViewController Controller { get; set; }
        private AccountEntriesController parentController { get; set; }
        public Account account;
		public Entry entry;

		public AccountEntryActionSheetActionExecutor(UIViewController controller, 
                                                     Account account, Entry entry)
        {
            this.Controller = controller;
            this.account = account;
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
        private Account account;
        private List<Entry> entries;
        private AccountEntriesController parentController;

        public double TotalCreditQuantity { get; set; }
        public double TotalDebitQuantity { get; set; }
        public decimal TotalCreditAmount { get; set; }
        public decimal TotalDebitAmount { get; set; }

        public decimal BalanceAmount
        {
            get { return TotalCreditAmount - TotalDebitAmount; }
        }

        public double BalanceQuantity
        {
            get { return TotalCreditQuantity - TotalDebitQuantity; }
        }

        private void CalculateTotals()
        {
            var accountEntries = entries.Where(e => !e.IsVoid);

            foreach (var entry in accountEntries)
            {
                if (entry.TxnType == TxnType.Paid || entry.TxnType == TxnType.Issued)
                {
                    TotalCreditAmount += entry.Amount;
                    TotalCreditQuantity += entry.Quantity;
                }
                else
                {
                    TotalDebitAmount += entry.Amount;
                    TotalDebitQuantity += entry.Quantity;
                }
            }
        }

        public AccountEntriesTableViewSource(AccountEntriesController parentController, 
                                             Account account, List<Entry> entries)
        {
            this.parentController = parentController;
            this.entries = entries;
            this.account = account;
            CalculateTotals();
        }

        public override void RowSelected(UITableView tableView, NSIndexPath indexPath)
        {
            var entry = entries[indexPath.Row];
            tableView.DeselectRow(indexPath, true);
            ShowActionSheetAlert(entry);
        }

        private void ShowActionSheetAlert(Entry entry)
        {
            var actionExecutor = new AccountEntryActionSheetActionExecutor(parentController, account, entry);
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
            cell.UpdateAccountViewCell(account, entry);
            return cell;
        }

        public override nint RowsInSection(UITableView tableview, nint section)
        {
            return entries.Count;
        }
    }
}