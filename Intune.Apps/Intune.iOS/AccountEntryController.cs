using Foundation;
using System;
using UIKit;
using Intune.Shared.Model;
using System.Globalization;

namespace Intune.iOS
{
    public partial class AccountEntryController : UIViewController
    {
        public User SignInUser { get; set; }
        public Account Account { get; set; }
        public Entry Entry { get; set; }

        public AccountEntryController(IntPtr handle) : base(handle)
        {
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
            MessageLabel.Text = "";
            TxnDatePicker.Enabled = Entry.IsNew;
            SetToolBarStatus();
            HookEventHandlers();
            SetViewTitle();
            FillControls();
        }

        private void SetToolBarStatus()
        {
            SaveToolBarButton.Enabled = Entry.IsNew;
            VoidToolBarButton.Enabled = !Entry.IsNew && !Entry.IsVoid;
        }

        private void HookEventHandlers()
        {
            CancelToolBarButton.Clicked += CancelToolBarButton_Clicked;
            VoidToolBarButton.Clicked += VoidToolBarButton_Clicked;
            SaveToolBarButton.Clicked += SaveToolBarButton_Clicked;
            CommentToolBarButton.Clicked += CommentToolBarButton_Clicked;
        }

        private void SetViewTitle()
        {
            if (Entry.IsNew)
                NavigationBar.TopItem.Title = "New Entry";
            else
                NavigationBar.TopItem.Title = Entry.Notes;
        }

        private void FillControls()
        {
            TxnTypeSegement.SelectedSegment = (int)Entry.TxnType;
            TxnDatePicker.Date = DateTimeToNSDate(Entry.TxnDate);
            QuantityTextField.Text = Entry.Quantity.ToString("#0");
            AmountTextField.Text = Math.Abs(Entry.Amount).ToString("#0.00");
            NotesTextField.Text = Entry.Notes;
        }

        void CancelToolBarButton_Clicked(object sender, EventArgs e)
        {
            DismissViewController(true, null);
        }

        void VoidToolBarButton_Clicked(object sender, EventArgs e)
        {
            try
            {
                VoidEntry();
                DismissViewController(false, null);
            }
            catch (Exception ex)
            {
                MessageLabel.Text = ex.Message;
            }
        }

        private void VoidEntry()
        {
            var voidEntry = new Entry
            {
                UserId = SignInUser.Id,
                AccountId = Entry.AccountId,
                Notes = ComposeVoidNotes(),
                TxnType = MakeVoidTxnType(),
                TxnDate = DateTime.Today,
                Quantity = Entry.Quantity,
                Amount = Entry.Amount,
                VoidId = Entry.Id,
            };

            IntuneService.AddAccountEntry(voidEntry);
        }

        private TxnType MakeVoidTxnType()
        {
            if (Entry.TxnType == TxnType.Paid || Entry.TxnType == TxnType.Issued)
                return TxnType.Received;
            else if (Entry.Amount > 0)
                return TxnType.Paid;
            else
                return TxnType.Issued;
        }

        private string ComposeVoidNotes()
        {
            return string.Format("Void of {0} on {1} of Qty: {2} and {3}",
                Entry.Notes, Entry.TxnDate.ToShortDateString(), Entry.Quantity,
                Entry.Amount.ToString("C2", CultureInfo.CurrentCulture));
        }

        void SaveToolBarButton_Clicked(object sender, EventArgs e)
        {
            try
            {
                FillObject();

                if (!Entry.IsValid())
                    throw new ArgumentException("Empty entry cannot be saved");

                IntuneService.AddAccountEntry(Entry);
                DismissViewController(false, null);
            }
            catch (Exception ex)
            {
                MessageLabel.Text = ex.Message;
            }
        }

        void CommentToolBarButton_Clicked(object sender, EventArgs e)
        {
            try
            {
                //TODO: ...
                DismissViewController(false, null);
            }
            catch (Exception ex)
            {
                MessageLabel.Text = ex.Message;
            }
        }

        private void FillObject()
        {
            Entry.TxnType = (TxnType)(int)TxnTypeSegement.SelectedSegment;
            var txnDate = NSDateToDateTime(TxnDatePicker.Date);
            Entry.TxnDate = txnDate.Date;
            Entry.Quantity = Double.Parse(QuantityTextField.Text.Trim());
            Entry.Amount = Decimal.Parse(AmountTextField.Text.Trim());
            Entry.Notes = NotesTextField.Text.Trim();
            Entry.AccountId = Account.Id;
            Entry.UserId = SignInUser.Id;
        }

        private DateTime NSDateToDateTime(NSDate date)
        {
            DateTime reference = TimeZone.CurrentTimeZone.ToLocalTime(
                new DateTime(2001, 1, 1, 0, 0, 0));
            return reference.AddSeconds(date.SecondsSinceReferenceDate);
        }

        private NSDate DateTimeToNSDate(DateTime date)
        {
            DateTime reference = TimeZone.CurrentTimeZone.ToLocalTime(
                new DateTime(2001, 1, 1, 0, 0, 0));
            return NSDate.FromTimeIntervalSinceReferenceDate(
                (date - reference).TotalSeconds);
        }
    }
}
