using System;
using RestSharp;
using System.Net;
using Newtonsoft.Json;
using System.Collections.Generic;
using Intune.Shared.Model;

namespace Intune.Android
{
    public static class IntuneService
    {
        const string intuneServerUri = @"http://intune-1.apphb.com/";

        public static User SignIn(string signInId, string password)
        {
            var user = new User { Email = signInId, Password = password };
            var body = JsonConvert.SerializeObject(user);
            var request = new RestRequest(@"api/user/signin/", Method.POST);
            request.AddParameter("text/json", body, ParameterType.RequestBody);
            var client = new RestClient(intuneServerUri);
            var response = client.Execute<User>(request);
            if (response.StatusCode == HttpStatusCode.OK)
                return response.Data;

            return null;
        }

        public static User GetUserById(int userId)
        {
            var request = new RestRequest(@"api/user/id/", Method.GET);
            request.AddParameter("userId", userId);
            var client = new RestClient(intuneServerUri);
            var response = client.Execute<User>(request);
            if (response.StatusCode == HttpStatusCode.OK)
                return response.Data;

            return null;
        }

        public static User GetUserBySignInId(string signInId)
        {
            var request = new RestRequest(@"api/user/signinid/", Method.GET);
            request.AddParameter("signinid", signInId);
            var client = new RestClient(intuneServerUri);
            var response = client.Execute<User>(request);
            if (response.StatusCode == HttpStatusCode.OK)
                return response.Data;

            return null;
        }

        public static User RegiterUser(User user)
        {
            var body = JsonConvert.SerializeObject(user);
            var request = new RestRequest(@"api/user/register/", Method.POST);
            request.AddParameter("text/json", body, ParameterType.RequestBody);
            var client = new RestClient(intuneServerUri);
            var response = client.Execute<User>(request);
            if (response.StatusCode == HttpStatusCode.OK)
                return response.Data;

            return null;
        }

        public static User UpdateUser(User user)
        {
            var body = JsonConvert.SerializeObject(user);
            var request = new RestRequest(@"api/user/update/", Method.POST);
            request.AddParameter("text/json", body, ParameterType.RequestBody);
            var client = new RestClient(intuneServerUri);
            var response = client.Execute<User>(request);
            if (response.StatusCode == HttpStatusCode.OK)
                return response.Data;

            return null;
        }

        public static void ResetPassword(User user)
        {
            var body = JsonConvert.SerializeObject(user);
            var request = new RestRequest(@"api/user/resetpassword/", Method.POST);
            request.AddParameter("text/json", body, ParameterType.RequestBody);
            var client = new RestClient(intuneServerUri);
            var response = client.Execute<User>(request);
            if (response.StatusCode != HttpStatusCode.OK)
                throw new Exception("Cannot reset your password.");
        }

        public static List<Account> GetAllAccounts(int userId, int contactId)
        {
            var request = new RestRequest(@"api/account/allaccounts/", Method.GET);
            request.AddParameter("userId", userId);
            request.AddParameter("contactId", contactId);
            var client = new RestClient(intuneServerUri);
            var response = client.Execute<List<Account>>(request);
            if (response.StatusCode == HttpStatusCode.OK)
                return response.Data;

            return null;
        }

        public static Entry GetAccountEntry(int entryId)
        {
            var request = new RestRequest(@"api/account/entry/", Method.GET);
            request.AddParameter("entryId", entryId);
            var client = new RestClient(intuneServerUri);
            var response = client.Execute<Entry>(request);
            if (response.StatusCode == HttpStatusCode.OK)
                return response.Data;

            return null;
        }

        public static List<Entry> GetAccountEntries(int accountId)
        {
            var request = new RestRequest(@"api/account/entries/", Method.GET);
            request.AddParameter("accountId", accountId);
            var client = new RestClient(intuneServerUri);
            var response = client.Execute<List<Entry>>(request);
            if (response.StatusCode == HttpStatusCode.OK)
                return response.Data;

            return null;
        }

        public static Contact GetContact(int contactId)
        {
            var request = new RestRequest(@"api/contact/contact/", Method.GET);
            request.AddParameter("contactId", contactId);
            var client = new RestClient(intuneServerUri);
            var response = client.Execute<Contact>(request);
            if (response.StatusCode == HttpStatusCode.OK)
                return response.Data;

            return null;
        }

        public static List<Contact> GetAllContacts(int userId)
        {
            var request = new RestRequest(@"api/contact/allcontacts/", Method.GET);
            request.AddParameter("userId", userId);
            var client = new RestClient(intuneServerUri);
            var response = client.Execute<List<Contact>>(request);
            if (response.StatusCode == HttpStatusCode.OK)
                return response.Data;

            return null;
        }

        public static void AddAccountSharing(int accountId, UserAccountShareRole[] accountShares)
        {
            string accountSharingApiUri = @"api/account/sharing";
            string param = string.Format("/?accountId={0}", accountId);
            string accountSharingApiUriString = string.Format("{0}{1}", accountSharingApiUri, param);
            var body = JsonConvert.SerializeObject(accountShares);
            var request = new RestRequest(accountSharingApiUriString, Method.POST);
            request.AddParameter("text/json", body, ParameterType.RequestBody);
            var client = new RestClient(intuneServerUri);
            var response = client.Execute(request);
            if (response.StatusCode != HttpStatusCode.OK)
                throw new Exception("Cannot share account with contacts");
        }

        public static List<Contact> GetAccountSharedContacts(int userId, int accountId)
        {
            var request = new RestRequest(@"api/account/sharedcontacts/", Method.GET);
            request.AddParameter("userId", userId);
            request.AddParameter("accountId", accountId);
            var client = new RestClient(intuneServerUri);
            var response = client.Execute<List<Contact>>(request);
            if (response.StatusCode == HttpStatusCode.OK)
                return response.Data;

            return null;
        }

        public static List<int> GetAccountUsers(int accountId, UserAccountRole role)
        {
            var request = new RestRequest(@"api/account/account/users/", Method.GET);
            request.AddParameter("accountId", accountId);
            request.AddParameter("role", (int)role);
            var client = new RestClient(intuneServerUri);
            var response = client.Execute<List<int>>(request);
            if (response.StatusCode == HttpStatusCode.OK)
                return response.Data;

            return null;
        }

        public static Account AddAccount(Account account)
        {
            var body = JsonConvert.SerializeObject(account);
            var request = new RestRequest(@"api/account/create/", Method.POST);
            request.AddParameter("text/json", body, ParameterType.RequestBody);
            var client = new RestClient(intuneServerUri);
            var response = client.Execute<Account>(request);
            if (response.StatusCode == HttpStatusCode.OK)
                return response.Data;

            return null;
        }

        public static Entry AddAccountEntry(Entry entry)
        {
            var body = JsonConvert.SerializeObject(entry);
            var request = new RestRequest(@"api/account/addentry/", Method.POST);
            request.AddParameter("text/json", body, ParameterType.RequestBody);
            var client = new RestClient(intuneServerUri);
            var response = client.Execute<Entry>(request);
            if (response.StatusCode == HttpStatusCode.OK)
                return response.Data;

            return null;
        }

        public static Contact AddContact(Contact contact)
        {
            var body = JsonConvert.SerializeObject(contact);
            var request = new RestRequest(@"api/contact/create/", Method.POST);
            request.AddParameter("text/json", body, ParameterType.RequestBody);
            var client = new RestClient(intuneServerUri);
            var response = client.Execute<Contact>(request);
            if (response.StatusCode == HttpStatusCode.OK)
                return response.Data;

            return null;
        }

        public static Account UpdateAccount(Account account)
        {
            var body = JsonConvert.SerializeObject(account);
            var request = new RestRequest(@"api/account/update/", Method.POST);
            request.AddParameter("text/json", body, ParameterType.RequestBody);
            var client = new RestClient(intuneServerUri);
            var response = client.Execute<Account>(request);
            if (response.StatusCode == HttpStatusCode.OK)
                return response.Data;

            return null;
        }

        public static Contact UpdateContact(Contact contact)
        {
            var body = JsonConvert.SerializeObject(contact);
            var request = new RestRequest(@"api/contact/update/", Method.POST);
            request.AddParameter("text/json", body, ParameterType.RequestBody);
            var client = new RestClient(intuneServerUri);
            var response = client.Execute<Contact>(request);
            if (response.StatusCode == HttpStatusCode.OK)
                return response.Data;

            return null;
        }

        public static List<Comment> GetContactComments(int byUserId, int toUserId)
        {
            var request = new RestRequest(@"api/comment/contact/allcomments/", Method.GET);
            request.AddParameter("byUserId", byUserId);
            request.AddParameter("toUserId", toUserId);
            var client = new RestClient(intuneServerUri);
            var response = client.Execute<List<Comment>>(request);
            if (response.StatusCode == HttpStatusCode.OK)
                return response.Data;

            return null;
        }

        public static List<Comment> GetAccountComments(int accountId, int userId)
        {
            var request = new RestRequest(@"api/comment/account/allcomments/", Method.GET);
            request.AddParameter("accountId", accountId);
            request.AddParameter("userId", userId);
            var client = new RestClient(intuneServerUri);
            var response = client.Execute<List<Comment>>(request);
            if (response.StatusCode == HttpStatusCode.OK)
                return response.Data;

            return null;
        }

        public static List<Comment> GetEntryComments(int entryId, int userId)
        {
            var request = new RestRequest(@"api/comment/entry/allcomments/", Method.GET);
            request.AddParameter("entryId", entryId);
            request.AddParameter("userId", userId);
            var client = new RestClient(intuneServerUri);
            var response = client.Execute<List<Comment>>(request);
            if (response.StatusCode == HttpStatusCode.OK)
                return response.Data;

            return null;
        }

        public static Comment AddComment(Comment comment)
        {
            var body = JsonConvert.SerializeObject(comment);
            var request = new RestRequest(@"api/comment/addcomment/", Method.POST);
            request.AddParameter("text/json", body, ParameterType.RequestBody);
            var client = new RestClient(intuneServerUri);
            var response = client.Execute<Comment>(request);
            if (response.StatusCode == HttpStatusCode.OK)
                return response.Data;

            return null;
        }

        public static void SendEmailOtp(string emailAddress)
        {
            var request = new RestRequest(@"api/email/otp/send/", Method.GET);
            request.AddParameter("emailAddress", emailAddress);
            var client = new RestClient(intuneServerUri);
            var response = client.Execute(request);
            if (response.StatusCode != HttpStatusCode.OK)
                throw new Exception("Cannot send email verification code");
        }

        public static void VerifyEmailOtp(string emailAddress, string otp)
        {
            var request = new RestRequest(@"api/email/otp/verify/", Method.GET);
            request.AddParameter("emailAddress", emailAddress);
            request.AddParameter("otp", otp);
            var client = new RestClient(intuneServerUri);
            var response = client.Execute(request);
            if (response.StatusCode != HttpStatusCode.OK)
                throw new Exception("Cannot verify email verification code");
        }

        public static void SendMobileOtp(string isdCode, string mobileNumber)
        {
            var countryIsdCode = isdCode.Replace("+", "");
            var request = new RestRequest(@"api/mobile/otp/send/", Method.GET);
            request.AddParameter("isdCode", countryIsdCode);
            request.AddParameter("mobileNumber", mobileNumber);
            var client = new RestClient(intuneServerUri);
            var response = client.Execute(request);
            if (response.StatusCode != HttpStatusCode.OK)
                throw new Exception("Cannot send mobile verification code");
        }

        public static void VerifyMobileOtp(string isdCode, string mobileNumber, string otp)
        {
            var countryIsdCode = isdCode.Replace("+", "");
            var request = new RestRequest(@"api/mobile/otp/verify/", Method.GET);
            request.AddParameter("isdCode", countryIsdCode);
            request.AddParameter("mobileNumber", mobileNumber);
            request.AddParameter("otp", otp);
            var client = new RestClient(intuneServerUri);
            var response = client.Execute(request);
            if (response.StatusCode != HttpStatusCode.OK)
                throw new Exception("Cannot verify mobile verification code");
        }

        public static List<Country> GetCountryIsdCodes()
        {
            var request = new RestRequest(@"api/country/isdcodes/", Method.GET);
            var client = new RestClient(intuneServerUri);
            var response = client.Execute<List<Country>>(request);
            if (response.StatusCode == HttpStatusCode.OK)
                return response.Data;

            return null;
        }

        public static bool IsCountryIsdCodeValid(string isdCode)
        {
            var countryIsdCode = isdCode.Replace("+", "");
            var request = new RestRequest(@"api/country/isdcode/validate/", Method.GET);
            request.AddParameter("isdCode", countryIsdCode);
            var client = new RestClient(intuneServerUri);
            var response = client.Execute(request);
            return response.StatusCode == HttpStatusCode.OK;
        }
    }
}
