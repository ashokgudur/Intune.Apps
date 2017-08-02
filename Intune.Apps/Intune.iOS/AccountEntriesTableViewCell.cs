using Foundation;
using System;
using UIKit;
using Intune.Shared.Model;
using System.Globalization;

namespace Intune.iOS
{
    public partial class AccountEntriesTableViewCell : UITableViewCell
    {
        public AccountEntriesTableViewCell (IntPtr handle) : base (handle)
        {
        }

        public void UpdateAccountViewCell(Entry entry)
        {
			TxnDateLabel.Text = entry.TxnDate.ToString("dd-MMM-yyyy");
            TxnTypeLabel.Text = GetTxnType(entry);
            QuantityLabel.Text = entry.Quantity.ToString("#0");
            AmountLabel.Text = Math.Abs(entry.Amount).ToString("C2", CultureInfo.CurrentCulture);
            NotesLabel.Text = entry.Notes;
		}

		private string GetTxnType(Entry entry)
		{
            //TODO: need to get the role...
			//if (_role != UserAccountRole.Collaborator)
				//return entry.TxnType.ToString();

			if (entry.TxnType == TxnType.Paid || entry.TxnType == TxnType.Issued)
				return TxnType.Received.ToString();

			if (entry.Amount >= 0)
				return TxnType.Paid.ToString();
			else
				return TxnType.Issued.ToString();
		}
	}
}