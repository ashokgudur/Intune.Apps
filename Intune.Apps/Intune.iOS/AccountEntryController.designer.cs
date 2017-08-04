// WARNING
//
// This file has been generated automatically by Visual Studio from the outlets and
// actions declared in your storyboard file.
// Manual changes to this file will not be maintained.
//
using Foundation;
using System;
using System.CodeDom.Compiler;
using UIKit;

namespace Intune.iOS
{
    [Register ("AccountEntryController")]
    partial class AccountEntryController
    {
        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UILabel AmountLabel { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UITextField AmountTextField { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIBarButtonItem CancelToolBarButton { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIBarButtonItem CommentToolBarButton { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UILabel MessageLabel { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UINavigationBar NavigationBar { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UILabel NotesLabel { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UITextField NotesTextField { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UILabel QuantityLabel { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UITextField QuantityTextField { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIBarButtonItem SaveToolBarButton { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UILabel TxnDateLabel { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIDatePicker TxnDatePicker { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UILabel TxnTypeLabel { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UISegmentedControl TxnTypeSegement { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIBarButtonItem VoidToolBarButton { get; set; }

        void ReleaseDesignerOutlets ()
        {
            if (AmountLabel != null) {
                AmountLabel.Dispose ();
                AmountLabel = null;
            }

            if (AmountTextField != null) {
                AmountTextField.Dispose ();
                AmountTextField = null;
            }

            if (CancelToolBarButton != null) {
                CancelToolBarButton.Dispose ();
                CancelToolBarButton = null;
            }

            if (CommentToolBarButton != null) {
                CommentToolBarButton.Dispose ();
                CommentToolBarButton = null;
            }

            if (MessageLabel != null) {
                MessageLabel.Dispose ();
                MessageLabel = null;
            }

            if (NavigationBar != null) {
                NavigationBar.Dispose ();
                NavigationBar = null;
            }

            if (NotesLabel != null) {
                NotesLabel.Dispose ();
                NotesLabel = null;
            }

            if (NotesTextField != null) {
                NotesTextField.Dispose ();
                NotesTextField = null;
            }

            if (QuantityLabel != null) {
                QuantityLabel.Dispose ();
                QuantityLabel = null;
            }

            if (QuantityTextField != null) {
                QuantityTextField.Dispose ();
                QuantityTextField = null;
            }

            if (SaveToolBarButton != null) {
                SaveToolBarButton.Dispose ();
                SaveToolBarButton = null;
            }

            if (TxnDateLabel != null) {
                TxnDateLabel.Dispose ();
                TxnDateLabel = null;
            }

            if (TxnDatePicker != null) {
                TxnDatePicker.Dispose ();
                TxnDatePicker = null;
            }

            if (TxnTypeLabel != null) {
                TxnTypeLabel.Dispose ();
                TxnTypeLabel = null;
            }

            if (TxnTypeSegement != null) {
                TxnTypeSegement.Dispose ();
                TxnTypeSegement = null;
            }

            if (VoidToolBarButton != null) {
                VoidToolBarButton.Dispose ();
                VoidToolBarButton = null;
            }
        }
    }
}