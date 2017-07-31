using Foundation;
using System;
using UIKit;
using Intune.Shared.Model;
using System.Globalization;

namespace Intune.iOS
{
    public partial class AccountsTableViewCell : UITableViewCell
    {
        public AccountsTableViewCell(IntPtr handle) : base(handle)
        {
        }

        internal void UpdateAccountViewCell(Account account)
        {
            NameLabel.Text = account.Name;
            BalanceLabel.Text = Math.Abs(account.Balance)
                .ToString("C2", CultureInfo.CurrentCulture);
            PermissionLabel.Text = $"You are {account.Role}";

            BalanceTypeLabel.Text = $"Balance is {getBalanceType(account)}";
        }

        private string getBalanceType(Account account)
        {
            if (account.Balance == 0)
            {
                if (account.HasEntries)
                    return "++";
                else
                    return "NA";
            }
            else if (account.Balance > 0)

                return getBalanceTitle(account, "Receivable");
            else
                return getBalanceTitle(account, "Payable");

        }

        private string getBalanceTitle(Account account, string ofType)
        {
            if (account.Role == UserAccountRole.Collaborator)
                return ofType == "Receivable" ? "Payable" : "Receivable";
            else
                return ofType == "Receivable" ? "Receivable" : "Payable";
        }
    }
}