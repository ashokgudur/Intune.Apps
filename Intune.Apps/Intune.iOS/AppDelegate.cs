﻿using System.Linq;
using Foundation;
using Intune.Shared.Model;
using UIKit;
using Xamarin.Auth;

namespace Intune.iOS
{
    // The UIApplicationDelegate for the application. This class is responsible for launching the
    // User Interface of the application, as well as listening (and optionally responding) to application events from iOS.
    [Register("AppDelegate")]
    public class AppDelegate : UIApplicationDelegate
    {
        // class-level declarations

        public override UIWindow Window
        {
            get;
            set;
        }

        public override bool FinishedLaunching(UIApplication application, NSDictionary launchOptions)
        {
            Window = new UIWindow(UIScreen.MainScreen.Bounds);
            var rootViewController = GetRootViewController();
            Window.RootViewController = new UINavigationController(rootViewController);
            Window.MakeKeyAndVisible();
            return true;
        }

        private UIViewController GetRootViewController()
        {
            var storyboard = UIStoryboard.FromName("Main", NSBundle.MainBundle);
            if (SignIn(GetSignInCredentials()))
                return storyboard.InstantiateViewController("MainController");
            else
                return storyboard.InstantiateViewController("SignInController");
        }

        private bool SignIn(User credentials)
        {
            var user = IntuneService.SignIn(credentials.Email, credentials.Password);
            return user != null;
        }

        private User GetSignInCredentials()
        {
            var store = AccountStore.Create();
            var storedAccounts = store.FindAccountsForService(Common.DeviceAccountStoreName);
            if (storedAccounts.Count() == 0)
            {
                return new User
                {
                    Email = "SystemWakeUpEmail",
                    Password = "SystemWakeUpPassword"
                };
            }

            var storedAccount = storedAccounts.ToArray()[0];
            return new User
            {
                Email = storedAccount.Username,
                Password = storedAccount.Properties["Password"]
            };
        }

        public override void OnResignActivation(UIApplication application)
        {
            // Invoked when the application is about to move from active to inactive state.
            // This can occur for certain types of temporary interruptions (such as an incoming phone call or SMS message) 
            // or when the user quits the application and it begins the transition to the background state.
            // Games should use this method to pause the game.
        }

        public override void DidEnterBackground(UIApplication application)
        {
            // Use this method to release shared resources, save user data, invalidate timers and store the application state.
            // If your application supports background exection this method is called instead of WillTerminate when the user quits.
        }

        public override void WillEnterForeground(UIApplication application)
        {
            // Called as part of the transiton from background to active state.
            // Here you can undo many of the changes made on entering the background.
        }

        public override void OnActivated(UIApplication application)
        {
            // Restart any tasks that were paused (or not yet started) while the application was inactive. 
            // If the application was previously in the background, optionally refresh the user interface.
        }

        public override void WillTerminate(UIApplication application)
        {
            // Called when the application is about to terminate. Save data, if needed. See also DidEnterBackground.
        }
    }
}

