using Foundation;
using System;
using UIKit;
using Intune.Shared.Model;
using System.Globalization;

namespace Intune.iOS
{
    public partial class AccountEntriesController : UIViewController
    {
        private UIRefreshControl RefreshControl;

        public User SignInUser { get; set; }
        public Account Account { get; set; }

        public AccountEntriesController(IntPtr handle) : base(handle)
        {
        }

        public override void ViewWillAppear(bool animated)
        {
            base.ViewWillAppear(animated);
            SetAccountEntriesTableViewSource();
        }

		public override void ViewDidLoad()
        {
            base.ViewDidLoad();
            NavigationBar.TopItem.Title = Account.Name;
            AddRefreshControls();
            HookEventHandlers();
            SetAccountEntriesTableViewSource();
        }

        void AddRefreshControls()
        {
            if (!UIDevice.CurrentDevice.CheckSystemVersion(6, 0))
                return;

            RefreshControl = new UIRefreshControl();
            RefreshControl.ValueChanged += (sender, e) => { RunRefreshControl(); };
            AccountEntriesTableView.Add(RefreshControl);
        }

        void RunRefreshControl()
        {
            InvokeOnMainThread(() => { RefreshControl.BeginRefreshing(); });
            SetAccountEntriesTableViewSource();
            InvokeOnMainThread(() => { RefreshControl.EndRefreshing(); });
        }

        private void HookEventHandlers()
        {
            CloseToolBarButton.Clicked += CloseToolBarButton_Clicked;
            AddNewToolBarButton.Clicked += AddNewToolBarButton_Clicked;
            CommentToolBarButton.Clicked += CommentToolBarButton_Clicked;
        }

		public void SetAccountEntriesTableViewSource()
		{
			var entries = IntuneService.GetAccountEntries(Account.Id);
            AccountEntriesTableView.Source = new AccountEntriesTableViewSource(this, Account, entries);
			DisplayAccountSummary();
		}

		private void DisplayAccountSummary()
		{
			var accountsAdapter = AccountEntriesTableView.Source as AccountEntriesTableViewSource;

			TotalPaidLabel.Text = $"TOTAL {getTotalsTxnType("Paid")}";
			TotalPaidQuantityLabel.Text = accountsAdapter.TotalCreditQuantity.ToString("#0");
			TotalPaidAmountLabel.Text = Math.Abs(accountsAdapter.TotalCreditAmount).ToString("C2", CultureInfo.CurrentCulture);

			TotalReceivedLabel.Text = $"TOTAL {getTotalsTxnType("Rcvd")}";
			TotalReceivedQuantityLabel.Text = accountsAdapter.TotalDebitQuantity.ToString("#0");
			TotalReceivedAmountLabel.Text = Math.Abs(accountsAdapter.TotalDebitAmount).ToString("C2", CultureInfo.CurrentCulture);

			TotalBalanceLabel.Text = $"TOTAL {GetBalanceTitle(accountsAdapter.BalanceAmount)}";
			TotalBalanceQuantityLabel.Text = accountsAdapter.BalanceQuantity.ToString("#0");
			TotalBalanceAmountLabel.Text = Math.Abs(accountsAdapter.BalanceAmount).ToString("C2", CultureInfo.CurrentCulture);
		}

		private String GetBalanceTitle(decimal balanceAmount)
		{
			return balanceAmount == 0
				? "Zero"
				: balanceAmount > 0
					? getBalanceTitle("Rcvbl")
					: getBalanceTitle("Paybl");
		}

		private string getTotalsTxnType(string ofType)
		{
			if (Account.Role == UserAccountRole.Collaborator)
				return ofType == "Paid" ? "RECEIVED" : "PAID";
			else
				return ofType == "Paid" ? "PAID" : "RECEIVED";
		}

		private string getBalanceTitle(string ofType)
		{
			if (Account.Role == UserAccountRole.Collaborator)
				return ofType == "Rcvbl" ? "PAYABLE" : "RECEIVABLE";
			else
				return ofType == "Rcvbl" ? "RECEIVABLE" : "PAYABLE";
		}

		void CloseToolBarButton_Clicked(object sender, EventArgs e)
        {
            DismissViewController(true, null);
        }

        private void CommentToolBarButton_Clicked(object sender, EventArgs e)
        {
            try
            {
                //TODO: ...
                throw new NotImplementedException();
            }
            catch (Exception ex)
            {
                MessageAlert.Instance(this).Show(ex.Message);
            }
        }

        private void AddNewToolBarButton_Clicked(object sender, EventArgs e)
        {
            try
            {
                DisplayAccountEntryController(new Entry());
            }
            catch (Exception ex)
            {
                MessageAlert.Instance(this).Show(ex.Message);
            }
        }

        public void VoidAccountEntry(Entry entry)
        {
            try
            {
                VoidEntry(entry);
				SetAccountEntriesTableViewSource();
			}
            catch (Exception ex)
            {
                MessageAlert.Instance(this).Show(ex.Message);
            }
        }

        private void VoidEntry(Entry entry)
        {
            var voidEntry = new Entry
            {
                UserId = SignInUser.Id,
                AccountId = entry.AccountId,
                Notes = ComposeVoidNotes(entry),
                TxnType = MakeVoidTxnType(entry),
                TxnDate = DateTime.Today,
                Quantity = entry.Quantity,
                Amount = entry.Amount,
                VoidId = entry.Id,
            };

            IntuneService.AddAccountEntry(voidEntry);
        }

        private TxnType MakeVoidTxnType(Entry entry)
        {
            if (entry.TxnType == TxnType.Paid || entry.TxnType == TxnType.Issued)
                return TxnType.Received;
            else if (entry.Amount > 0)
                return TxnType.Paid;
            else
                return TxnType.Issued;
        }

        private string ComposeVoidNotes(Entry entry)
        {
            return string.Format("Void of {0} on {1} of Qty: {2} and {3}",
                entry.Notes, entry.TxnDate.ToShortDateString(), entry.Quantity,
                entry.Amount.ToString("C2", CultureInfo.CurrentCulture));
        }

        public void CommentOnAccountEntry(Entry entry)
        {
            //TODO: ...
            try
            {
                throw new NotImplementedException();
            }
            catch (Exception ex)
            {
                MessageAlert.Instance(this).Show(ex.Message);
            }
        }

        public void DisplayAccountEntryController(Entry entry)
        {
            var controller = Storyboard.InstantiateViewController("AccountEntryController") as AccountEntryController;
            if (controller == null)
                throw new Exception("Could not find 'AccountEntryController'");

            controller.SignInUser = SignInUser;
            controller.Account = Account;
            controller.Entry = entry;
            PresentViewController(controller, true, null);
        }
    }
}
