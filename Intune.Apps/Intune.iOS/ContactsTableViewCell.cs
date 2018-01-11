using Foundation;
using System;
using UIKit;
using Intune.Shared.Model;

namespace Intune.iOS
{
    public partial class ContactsTableViewCell : UITableViewCell
    {
        public ContactsTableViewCell (IntPtr handle) : base (handle)
        {
        }

        internal void UpdateContactViewCell(Contact contact)
        {
            FullNameLabel.Text = contact.Name;
            MobileLabel.Text = contact.Mobile;
            AddressLabel.Text = contact.Address;
            IsIntunedLabel.Hidden = !contact.HasIntune();
        }
    }
}