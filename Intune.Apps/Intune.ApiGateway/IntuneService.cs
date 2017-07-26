using System;
using System.Net;
using Newtonsoft.Json;
using System.Collections.Generic;
using Intune.ApiGateway.Model;
using System.Threading.Tasks;
using System.Net.Http;
using System.Text;

namespace Intune.ApiGateway
{
    public static class IntuneService
    {
        const string intuneServerUri = @"http://intune-1.apphb.com/";
        private static Uri baseUri = new Uri(intuneServerUri);

        public async static Task<User> SignIn(string signInId, string password)
        {
            var user = new User { Email = signInId, Password = password };
            var body = JsonConvert.SerializeObject(user);
            var uri = new Uri(baseUri, @"api/user/signin/");
            var content = new StringContent(body, Encoding.UTF8, "application/json");
            var client = new HttpClient();
            var response = await client.PostAsync(uri, content);
            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<User>(result);
            }

            return null;
        }

        public static User GetUserById(int userId)
        {
            throw new NotImplementedException();

            //var request = new RestRequest(@"api/user/id/", Method.GET);
            //request.AddParameter("userId", userId);
            //var client = new RestClient(intuneServerUri);
            //IRestResponse response = null;
            //Task.Run(async () => { response = await getResponseAsync(client, request); }).Wait();
            //return response.StatusCode == HttpStatusCode.OK
            //        ? JsonConvert.DeserializeObject<User>(response.Content)
            //        : null;
        }

        public static User GetUserBySignInId(string signInId)
        {
            throw new NotImplementedException();

            //var request = new RestRequest(@"api/user/signinid/", Method.GET);
            //request.AddParameter("signinid", signInId);
            //var client = new RestClient(intuneServerUri);
            //IRestResponse response = null;
            //Task.Run(async () => { response = await getResponseAsync(client, request); }).Wait();
            //return response.StatusCode == HttpStatusCode.OK
            //        ? JsonConvert.DeserializeObject<User>(response.Content)
            //        : null;
        }

        public static User RegiterUser(User user)
        {
            throw new NotImplementedException();

            //var body = JsonConvert.SerializeObject(user);
            //var request = new RestRequest(@"api/user/register/", Method.POST);
            //request.AddParameter("text/json", body, ParameterType.RequestBody);
            //var client = new RestClient(intuneServerUri);
            //IRestResponse response = null;
            //Task.Run(async () => { response = await getResponseAsync(client, request); }).Wait();
            //return response.StatusCode == HttpStatusCode.OK
            //        ? JsonConvert.DeserializeObject<User>(response.Content)
            //        : null;
        }

        public static User UpdateUser(User user)
        {
            throw new NotImplementedException();

            //var body = JsonConvert.SerializeObject(user);
            //var request = new RestRequest(@"api/user/update/", Method.POST);
            //request.AddParameter("text/json", body, ParameterType.RequestBody);
            //var client = new RestClient(intuneServerUri);
            //IRestResponse response = null;
            //Task.Run(async () => { response = await getResponseAsync(client, request); }).Wait();
            //return response.StatusCode == HttpStatusCode.OK
            //        ? JsonConvert.DeserializeObject<User>(response.Content)
            //        : null;
        }

        public static void ResetPassword(User user)
        {
            throw new NotImplementedException();

            //var body = JsonConvert.SerializeObject(user);
            //var request = new RestRequest(@"api/user/resetpassword/", Method.POST);
            //request.AddParameter("text/json", body, ParameterType.RequestBody);
            //var client = new RestClient(intuneServerUri);
            //IRestResponse response = null;
            //Task.Run(async () => { response = await getResponseAsync(client, request); }).Wait();
            //if (response.StatusCode != HttpStatusCode.OK)
            //    throw new Exception("Cannot reset your password.");
        }

        public static List<Account> GetAllAccounts(int userId, int contactId)
        {
            throw new NotImplementedException();

            //var request = new RestRequest(@"api/account/allaccounts/", Method.GET);
            //request.AddParameter("userId", userId);
            //request.AddParameter("contactId", contactId);
            //var client = new RestClient(intuneServerUri);
            //IRestResponse response = null;
            //Task.Run(async () => { response = await getResponseAsync(client, request); }).Wait();
            //return response.StatusCode == HttpStatusCode.OK
            //        ? JsonConvert.DeserializeObject<List<Account>>(response.Content)
            //        : null;
        }

        public static Entry GetAccountEntry(int entryId)
        {
            throw new NotImplementedException();

            //var request = new RestRequest(@"api/account/entry/", Method.GET);
            //request.AddParameter("entryId", entryId);
            //var client = new RestClient(intuneServerUri);
            //IRestResponse response = null;
            //Task.Run(async () => { response = await getResponseAsync(client, request); }).Wait();
            //return response.StatusCode == HttpStatusCode.OK
            //        ? JsonConvert.DeserializeObject<Entry>(response.Content)
            //        : null;
        }

        public static List<Entry> GetAccountEntries(int accountId)
        {
            throw new NotImplementedException();

            //var request = new RestRequest(@"api/account/entries/", Method.GET);
            //request.AddParameter("accountId", accountId);
            //var client = new RestClient(intuneServerUri);
            //IRestResponse response = null;
            //Task.Run(async () => { response = await getResponseAsync(client, request); }).Wait();
            //return response.StatusCode == HttpStatusCode.OK
            //        ? JsonConvert.DeserializeObject<List<Entry>>(response.Content)
            //        : null;
        }

        public static Contact GetContact(int contactId)
        {
            throw new NotImplementedException();

            //var request = new RestRequest(@"api/contact/contact/", Method.GET);
            //request.AddParameter("contactId", contactId);
            //var client = new RestClient(intuneServerUri);
            //IRestResponse response = null;
            //Task.Run(async () => { response = await getResponseAsync(client, request); }).Wait();
            //return response.StatusCode == HttpStatusCode.OK
            //        ? JsonConvert.DeserializeObject<Contact>(response.Content)
            //        : null;
        }

        public static List<Contact> GetAllContacts(int userId)
        {
            throw new NotImplementedException();

            //var request = new RestRequest(@"api/contact/allcontacts/", Method.GET);
            //request.AddParameter("userId", userId);
            //var client = new RestClient(intuneServerUri);
            //IRestResponse response = null;
            //Task.Run(async () => { response = await getResponseAsync(client, request); }).Wait();
            //return response.StatusCode == HttpStatusCode.OK
            //        ? JsonConvert.DeserializeObject<List<Contact>>(response.Content)
            //        : null;
        }

        public static void AddAccountSharing(int accountId, UserAccountShareRole[] accountShares)
        {
            throw new NotImplementedException();

            //string accountSharingApiUri = @"api/account/sharing";
            //string param = string.Format("/?accountId={0}", accountId);
            //string accountSharingApiUriString = string.Format("{0}{1}", accountSharingApiUri, param);
            //var body = JsonConvert.SerializeObject(accountShares);
            //var request = new RestRequest(accountSharingApiUriString, Method.POST);
            //request.AddParameter("text/json", body, ParameterType.RequestBody);
            //var client = new RestClient(intuneServerUri);
            //IRestResponse response = null;
            //Task.Run(async () => { response = await getResponseAsync(client, request); }).Wait();
            //if (response.StatusCode != HttpStatusCode.OK)
            //    throw new Exception("Cannot share account with contacts");
        }

        public static List<Contact> GetAccountSharedContacts(int userId, int accountId)
        {
            throw new NotImplementedException();

            //var request = new RestRequest(@"api/account/sharedcontacts/", Method.GET);
            //request.AddParameter("userId", userId);
            //request.AddParameter("accountId", accountId);
            //var client = new RestClient(intuneServerUri);
            //IRestResponse response = null;
            //Task.Run(async () => { response = await getResponseAsync(client, request); }).Wait();
            //return response.StatusCode == HttpStatusCode.OK
            //        ? JsonConvert.DeserializeObject<List<Contact>>(response.Content)
            //        : null;
        }

        public static List<int> GetAccountUsers(int accountId, UserAccountRole role)
        {
            throw new NotImplementedException();

            //var request = new RestRequest(@"api/account/account/users/", Method.GET);
            //request.AddParameter("accountId", accountId);
            //request.AddParameter("role", (int)role);
            //var client = new RestClient(intuneServerUri);
            //IRestResponse response = null;
            //Task.Run(async () => { response = await getResponseAsync(client, request); }).Wait();
            //return response.StatusCode == HttpStatusCode.OK
            //        ? JsonConvert.DeserializeObject<List<int>>(response.Content)
            //        : null;
        }

        public static Account AddAccount(Account account)
        {
            throw new NotImplementedException();

            //var body = JsonConvert.SerializeObject(account);
            //var request = new RestRequest(@"api/account/create/", Method.POST);
            //request.AddParameter("text/json", body, ParameterType.RequestBody);
            //var client = new RestClient(intuneServerUri);
            //IRestResponse response = null;
            //Task.Run(async () => { response = await getResponseAsync(client, request); }).Wait();
            //return response.StatusCode == HttpStatusCode.OK
            //        ? JsonConvert.DeserializeObject<Account>(response.Content)
            //        : null;
        }

        public static Entry AddAccountEntry(Entry entry)
        {
            throw new NotImplementedException();

            //var body = JsonConvert.SerializeObject(entry);
            //var request = new RestRequest(@"api/account/addentry/", Method.POST);
            //request.AddParameter("text/json", body, ParameterType.RequestBody);
            //var client = new RestClient(intuneServerUri);
            //IRestResponse response = null;
            //Task.Run(async () => { response = await getResponseAsync(client, request); }).Wait();
            //return response.StatusCode == HttpStatusCode.OK
            //        ? JsonConvert.DeserializeObject<Entry>(response.Content)
            //        : null;
        }

        public static Contact AddContact(Contact contact)
        {
            throw new NotImplementedException();

            //var body = JsonConvert.SerializeObject(contact);
            //var request = new RestRequest(@"api/contact/create/", Method.POST);
            //request.AddParameter("text/json", body, ParameterType.RequestBody);
            //var client = new RestClient(intuneServerUri);
            //IRestResponse response = null;
            //Task.Run(async () => { response = await getResponseAsync(client, request); }).Wait();
            //return response.StatusCode == HttpStatusCode.OK
            //        ? JsonConvert.DeserializeObject<Contact>(response.Content)
            //        : null;
        }

        public static Account UpdateAccount(Account account)
        {
            throw new NotImplementedException();

            //var body = JsonConvert.SerializeObject(account);
            //var request = new RestRequest(@"api/account/update/", Method.POST);
            //request.AddParameter("text/json", body, ParameterType.RequestBody);
            //var client = new RestClient(intuneServerUri);
            //IRestResponse response = null;
            //Task.Run(async () => { response = await getResponseAsync(client, request); }).Wait();
            //return response.StatusCode == HttpStatusCode.OK
            //        ? JsonConvert.DeserializeObject<Account>(response.Content)
            //        : null;
        }

        public static Contact UpdateContact(Contact contact)
        {
            throw new NotImplementedException();

            //var body = JsonConvert.SerializeObject(contact);
            //var request = new RestRequest(@"api/contact/update/", Method.POST);
            //request.AddParameter("text/json", body, ParameterType.RequestBody);
            //var client = new RestClient(intuneServerUri);
            //IRestResponse response = null;
            //Task.Run(async () => { response = await getResponseAsync(client, request); }).Wait();
            //return response.StatusCode == HttpStatusCode.OK
            //        ? JsonConvert.DeserializeObject<Contact>(response.Content)
            //        : null;
        }

        public static List<Comment> GetContactComments(int byUserId, int toUserId)
        {
            throw new NotImplementedException();

            //var request = new RestRequest(@"api/comment/contact/allcomments/", Method.GET);
            //request.AddParameter("byUserId", byUserId);
            //request.AddParameter("toUserId", toUserId);
            //var client = new RestClient(intuneServerUri);
            //IRestResponse response = null;
            //Task.Run(async () => { response = await getResponseAsync(client, request); }).Wait();
            //return response.StatusCode == HttpStatusCode.OK
            //        ? JsonConvert.DeserializeObject<List<Comment>>(response.Content)
            //        : null;
        }

        public static List<Comment> GetAccountComments(int accountId, int userId)
        {
            throw new NotImplementedException();

            //var request = new RestRequest(@"api/comment/account/allcomments/", Method.GET);
            //request.AddParameter("accountId", accountId);
            //request.AddParameter("userId", userId);
            //var client = new RestClient(intuneServerUri);
            //IRestResponse response = null;
            //Task.Run(async () => { response = await getResponseAsync(client, request); }).Wait();
            //return response.StatusCode == HttpStatusCode.OK
            //        ? JsonConvert.DeserializeObject<List<Comment>>(response.Content)
            //        : null;
        }

        public static List<Comment> GetEntryComments(int entryId, int userId)
        {
            throw new NotImplementedException();

            //var request = new RestRequest(@"api/comment/entry/allcomments/", Method.GET);
            //request.AddParameter("entryId", entryId);
            //request.AddParameter("userId", userId);
            //var client = new RestClient(intuneServerUri);
            //IRestResponse response = null;
            //Task.Run(async () => { response = await getResponseAsync(client, request); }).Wait();
            //return response.StatusCode == HttpStatusCode.OK
            //        ? JsonConvert.DeserializeObject<List<Comment>>(response.Content)
            //        : null;
        }

        public static Comment AddComment(Comment comment)
        {
            throw new NotImplementedException();

            //var body = JsonConvert.SerializeObject(comment);
            //var request = new RestRequest(@"api/comment/addcomment/", Method.POST);
            //request.AddParameter("text/json", body, ParameterType.RequestBody);
            //var client = new RestClient(intuneServerUri);
            //IRestResponse response = null;
            //Task.Run(async () => { response = await getResponseAsync(client, request); }).Wait();
            //return response.StatusCode == HttpStatusCode.OK
            //        ? JsonConvert.DeserializeObject<Comment>(response.Content)
            //        : null;
        }

        public static void SendEmailOtp(string emailAddress)
        {
            throw new NotImplementedException();

            //var request = new RestRequest(@"api/email/otp/send/", Method.GET);
            //request.AddParameter("emailAddress", emailAddress);
            //var client = new RestClient(intuneServerUri);
            //IRestResponse response = null;
            //Task.Run(async () => { response = await getResponseAsync(client, request); }).Wait();
            //if (response.StatusCode != HttpStatusCode.OK)
            //    throw new Exception("Cannot send email verification code");
        }

        public static void VerifyEmailOtp(string emailAddress, string otp)
        {
            throw new NotImplementedException();

            //var request = new RestRequest(@"api/email/otp/verify/", Method.GET);
            //request.AddParameter("emailAddress", emailAddress);
            //request.AddParameter("otp", otp);
            //var client = new RestClient(intuneServerUri);
            //IRestResponse response = null;
            //Task.Run(async () => { response = await getResponseAsync(client, request); }).Wait();
            //if (response.StatusCode != HttpStatusCode.OK)
            //    throw new Exception("Cannot verify email verification code");
        }

        public static void SendMobileOtp(string isdCode, string mobileNumber)
        {
            throw new NotImplementedException();

            //var countryIsdCode = isdCode.Replace("+", "");
            //var request = new RestRequest(@"api/mobile/otp/send/", Method.GET);
            //request.AddParameter("isdCode", countryIsdCode);
            //request.AddParameter("mobileNumber", mobileNumber);
            //var client = new RestClient(intuneServerUri);
            //IRestResponse response = null;
            //Task.Run(async () => { response = await getResponseAsync(client, request); }).Wait();
            //if (response.StatusCode != HttpStatusCode.OK)
            //    throw new Exception("Cannot send mobile verification code");
        }

        public static void VerifyMobileOtp(string isdCode, string mobileNumber, string otp)
        {
            throw new NotImplementedException();

            //var countryIsdCode = isdCode.Replace("+", "");
            //var request = new RestRequest(@"api/mobile/otp/verify/", Method.GET);
            //request.AddParameter("isdCode", countryIsdCode);
            //request.AddParameter("mobileNumber", mobileNumber);
            //request.AddParameter("otp", otp);
            //var client = new RestClient(intuneServerUri);
            //IRestResponse response = null;
            //Task.Run(async () => { response = await getResponseAsync(client, request); }).Wait();
            //if (response.StatusCode != HttpStatusCode.OK)
            //    throw new Exception("Cannot verify mobile verification code");
        }

        public static List<Country> GetCountryIsdCodes()
        {
            throw new NotImplementedException();

            //var request = new RestRequest(@"api/country/isdcodes/", Method.GET);
            //var client = new RestClient(intuneServerUri);
            //IRestResponse response = null;
            //Task.Run(async () => { response = await getResponseAsync(client, request); }).Wait();
            //return response.StatusCode == HttpStatusCode.OK
            //        ? JsonConvert.DeserializeObject<List<Country>>(response.Content)
            //        : null;
        }

        public static bool IsCountryIsdCodeValid(string isdCode)
        {
            throw new NotImplementedException();

            //var countryIsdCode = isdCode.Replace("+", "");
            //var request = new RestRequest(@"api/country/isdcode/validate/", Method.GET);
            //request.AddParameter("isdCode", countryIsdCode);
            //var client = new RestClient(intuneServerUri);
            //IRestResponse response = null;
            //Task.Run(async () => { response = await getResponseAsync(client, request); }).Wait();
            //return response.StatusCode == HttpStatusCode.OK;
        }
    }
}
