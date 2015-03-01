//==========================================================================
//
// Author:  Nick Landry
// Title:   Senior Technical Evangelist - Microsoft US DX - NY Metro
// Twitter: @ActiveNick
// Blog:    www.AgeofMobility.com
//
// Copyright (c) Microsoft Corporation. All rights reserved.
//
// Disclaimer: Portions of this code may been simplified to demonstrate
// useful application development techniques and enhance readability.
// As such they may not necessarily reflect best practices in enterprise 
// development, and/or may not include all required safeguards.
// 
// This code and information are provided "as is" without warranty of any
// kind, either expressed or implied, including but not limited to the
// implied warranties of merchantability and/or fitness for a particular
// purpose.
//
// To learn more about Universal Windows app development using Cortana
// and the Speech SDK, watch the full-day course for free on
// Microsoft Virtual Acdemy (MVA) at http://aka.ms/cortanamva
//
//==========================================================================
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace BingAPILibrary
{
    public class Translator
    {
        // To learn more about app development with the Bing translate services, visit
        // http://www.bing.com/dev/en-us/translator

        // Microsoft Translator API in Azure data Market (up to 2M characters / montn free):
        // http://datamarket.azure.com/dataset/bing/microsofttranslator

        // You need to obtain a valid application key from the Azure Marketplace,
        // see http://msdn.microsoft.com/en-us/library/hh454950.aspx for details
        // THE TRANSLATOR CALLS WILL FAIL UNTIL YOU REPLACE THE TWO VALUES BELOW WITH
        // YOUR OWN ID & SECRET. sEE THE LINKS ABOVE FOR INSTRUCTIONS
        private readonly string _clientId = "XXXXX-INSERT YOUR CLIENT ID HERE-XXXXX";
        private readonly string _clientSecret = "XXXXX-INSERT YOUR CLIENT SECRET HERE-XXXXX";

        private readonly Uri _dataMarketAddress = new Uri("https://datamarket.accesscontrol.windows.net/v2/OAuth2-13");

        // We use a single HttpClient instance for all requests to Azure Marketplace
        private HttpClient _client = new HttpClient();

        public async Task<string> TranslateString(string strSource, string language)
        {
            string auth = await GetAzureDataMarketToken();

            //_client.DefaultRequestHeaders.Add("Authorization", auth);
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", auth);
            string RequestUri = "http://api.microsofttranslator.com/v2/Http.svc/Translate?text=" + 
                                System.Net.WebUtility.UrlEncode(strSource) + 
                                "&to=" + language;
            try
            {
                string strTranslated = await _client.GetStringAsync(RequestUri);
                System.Xml.Linq.XDocument xTranslation = System.Xml.Linq.XDocument.Parse(strTranslated);
                string strTransText = xTranslation.Root.FirstNode.ToString();
                if (strTransText == strSource)
                    return "";
                else
                    return strTransText;
            }
            catch (Exception ex)
            {
                // TO DO: Do something with the exception
                return "";
            }
        }

        private async Task<string> GetAzureDataMarketToken()
        {
            // First we issue async HTML form POST request to the Azure Marketplace to obtain an Access Token. 
            // See http://msdn.microsoft.com/en-us/library/hh454950.aspx for details

            // Create form parameters that we will send to data market.
            Dictionary<string, string> properties = new Dictionary<string, string>
            {
                { "grant_type", "client_credentials" },
                { "client_id",  _clientId},
                { "client_secret", _clientSecret },
                { "scope", "http://api.microsofttranslator.com" }
            };

            FormUrlEncodedContent authentication = new FormUrlEncodedContent(properties);
            HttpResponseMessage dataMarketResponse = await _client.PostAsync(_dataMarketAddress, authentication);
            string dmResponse;

            // If client authentication failed then we get a JSON response from Azure Market Place
            if (!dataMarketResponse.IsSuccessStatusCode)
            {
                //JToken error = await dataMarketResponse.Content.ReadAsAsync<JToken>();
                dmResponse = await dataMarketResponse.Content.ReadAsStringAsync();
                JToken error = Newtonsoft.Json.JsonConvert.DeserializeObject<JToken>(dmResponse);

                string errorType = error.Value<string>("error");
                string errorDescription = error.Value<string>("error_description");
                throw new HttpRequestException(string.Format("Azure market place request failed: {0} {1}", errorType, errorDescription));
            }

            // Get the access token to attach to the original request from the response body
            //AdmAccessToken accessToken = await dataMarketResponse.Content.ReadAsAsync<AdmAccessToken>();
            dmResponse = await dataMarketResponse.Content.ReadAsStringAsync();
            AdmAccessToken accessToken = Newtonsoft.Json.JsonConvert.DeserializeObject<AdmAccessToken>(dmResponse);

            return accessToken.access_token;
        }
    }

    /// <summary>
    /// This class describes an Access Token from Azure Data Market 
    /// </summary>
    public class AdmAccessToken
    {
        public string access_token { get; set; }

        public string token_type { get; set; }

        public string expires_in { get; set; }

        public string scope { get; set; }
    }
}
