using Foundation;
using System;
using UIKit;
using Intune.Shared.Model;

namespace Intune.iOS
{
    public partial class AccountSharingTableViewCell : UITableViewCell
    {
        public AccountSharingTableViewCell(IntPtr handle) : base(handle)
        {
        }

        public void FillTableViewCell(Contact contact)
        {
            ContactNameLabel.Text = contact.Name;
            PermissionSegement.Enabled = contact.HasIntune();

            if (contact.HasIntune())
                PermissionSegement.SelectedSegment = (int)contact.AccountSharedRole;
            else
                PermissionSegement.SelectedSegment = 0;

            PermissionSegement.RemoveTarget(null, null, UIControlEvent.ValueChanged);
            PermissionSegement.ValueChanged += (sender, e) =>
            {
                contact.AccountSharedRole = (UserAccountRole)(int)PermissionSegement.SelectedSegment;
            };
        }
    }
}
