using RestSharp;
using System;

namespace AntPlus
{
    public class Ant
    {
        public string GetLoginURL(string ClientID, string redirectURL, string State)
        {
            string AuthURL = "";
            AuthURL = "https://ant.aliceblueonline.com/oauth2/auth?response_type=code&client_id=" + ClientID + "&redirect_uri=" + redirectURL + "&scope=orders&state=" + State + "&access_type=authorization_code";
            return AuthURL;
        }

        public string AccessToken(string Code, string ClientID, string redirectURL, string ClientSecret)
        {
            string AccessToken = "";
            string encoder = ClientID + ":" + ClientSecret;
            var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(encoder);
            string basicauthentication = System.Convert.ToBase64String(plainTextBytes);
            var client = new RestClient("https://ant.aliceblueonline.com/oauth2/token");
            var request = new RestRequest(Method.POST);
            request.AddHeader("Authorization", "Basic " + basicauthentication + "");
            request.AddHeader("Content-Type", "application/x-www-form-urlencoded");
            request.AddParameter("undefined", "grant_type=authorization_code&code=" + Code + "&client_id=" + ClientID + "&client_secret_post=" + ClientSecret + "&redirect_uri=" + redirectURL + "&authorization_response=authorization_response", ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);
            AccessToken = response.Content;
            return AccessToken;

        }
    }
}
