using System.Threading.Tasks;
using Android.App;
using Android.Content;
using Android.Support.V7.App;
using Xamarin.Auth;
using System.Linq;
using Intune.ApiGateway;
using Intune.ApiGateway.Model;

namespace Intune.Android
{
    [Activity(Theme = "@style/IntuneTheme.Splash", MainLauncher = true, NoHistory = true)]
    public class SplashActivity : AppCompatActivity
    {
        protected override void OnResume()
        {
            base.OnResume();
            var startupWork = new Task(() => { SimulateStartup(); });
            startupWork.Start();
        }

        async void SimulateStartup()
        {
            var store = AccountStore.Create();
            var storeAccounts = store.FindAccountsForService("IntuneTechnologiesApp");
            if (storeAccounts.Count() == 0)
            {
                await IntuneService.SignIn("SystemWakeUpEmail", "SystemWakeUpPassword");
                StartActivity(new Intent(Application.Context, typeof(SignInActivity)));
                return;
            }

            var signInAccount = storeAccounts.ToArray()[0];
            var signInId = signInAccount.Username;
            var password = signInAccount.Properties["Password"];
            var user = await IntuneService.SignIn(signInId, password);
            if (user == null)
                StartActivity(new Intent(Application.Context, typeof(SignInActivity)));
            else
                showAccountsActivity(user);
        }

        private void showAccountsActivity(User user)
        {
            var accountsActivity = new Intent(this, typeof(AccountsActivity));
            accountsActivity.PutExtra("LoginUserId", user.Id);
            accountsActivity.PutExtra("LoginUserName", user.Name);
            accountsActivity.PutExtra("LoginUserSignInId", user.Email);
            StartActivity(accountsActivity);
        }

        public override void OnBackPressed() { }
    }
}