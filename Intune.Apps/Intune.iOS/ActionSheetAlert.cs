using System;
using UIKit;

namespace Intune.iOS
{
    public interface IActionSheetActionExecutor
    {
        UIViewController Controller { get; set; }
		void Execute(string action);
    }

    public class ActionSheetAlert
    {
        public const string CancelOption = "Cancel";
        private IActionSheetActionExecutor actionExecutor { get; set; }

        public static ActionSheetAlert Instance(IActionSheetActionExecutor actionExecutor)
        {
            return new ActionSheetAlert(actionExecutor);
        }

        private ActionSheetAlert(IActionSheetActionExecutor actionExecutor)
        {
            this.actionExecutor = actionExecutor;
        }

        public void Show(string message, string[] options, string title = "Intune")
        {
            var alert = UIAlertController.Create(title, message, UIAlertControllerStyle.ActionSheet);

            foreach (var option in options)
            {
                if (option == "Cancel")
                    throw new Exception("Invalid option. 'Cancel' option will be added by default.");

                alert.AddAction(UIAlertAction.Create(option, UIAlertActionStyle.Default,
                                                 (action) => actionExecutor.Execute(action.Title)));
            }

            alert.AddAction(UIAlertAction.Create(CancelOption, UIAlertActionStyle.Cancel, null));

            var popover = alert.PopoverPresentationController;
            if (popover != null)
            {
                popover.SourceView = actionExecutor.Controller.View;
                popover.PermittedArrowDirections = UIPopoverArrowDirection.Up;
            }

            actionExecutor.Controller.PresentViewController(alert, true, null);
        }
    }
}
