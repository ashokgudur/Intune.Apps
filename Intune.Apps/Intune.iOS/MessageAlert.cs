using System;
using UIKit;

namespace Intune.iOS
{
    public class MessageAlert
    {
        private UIViewController controller;

        public static MessageAlert Instance(UIViewController controller)
        {
            return new MessageAlert(controller);
        }

        private MessageAlert(UIViewController controller)
        {
            this.controller = controller;
        }

        public void Show(string message)
        {
            var alert = UIAlertController.Create("Intune", message, UIAlertControllerStyle.Alert);
            alert.AddAction(UIAlertAction.Create("OK", UIAlertActionStyle.Default, null));
            controller.PresentViewController(alert, true, null);
        }
    }
}
