using OANDAV20.REST20.TradeLibrary.DataTypes.Communications.Requests;
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading.Tasks;

namespace OANDAV20.REST20
{
   /// Best Practices Notes
   /// 
   /// Keep alive is on by default
   public partial class Rest20
   {
      // Convenience helpers
      private static string Server(EServer server) { return Credentials.GetDefaultCredentials().GetServer(server); }
      private static string AccessToken { get { return Credentials.GetDefaultCredentials().AccessToken; } }

      private static string GetCommaSeparatedList(List<string> items)
      {
         StringBuilder result = new StringBuilder();
         foreach (var item in items)
         {
            result.Append(item + ",");
         }
         return result.ToString().Trim(',');
      }

      /// <summary>
      /// Used primarily for test purposes, this sends a request with the expectation of getting an error
      /// </summary>
      /// <param name="request">The request to send</param>
      /// <returns>null if the request succeeds.  response body as string if the request fails</returns>
      public static async Task<string> MakeErrorRequest(Request request)
      {
         string requestString = Server(EServer.Account) + request.GetRequestString();
         return await MakeErrorRequest(requestString);
      }

      /// <summary>
      /// Used for tests, this request avoids exceptions for normal errors, ignores successful responses and just returns error strings
      /// </summary>
      /// <param name="requestString">the request to make</param>
      /// <returns>null if request is successful, the error response if it fails</returns>
      private static async Task<string> MakeErrorRequest(string requestString)
      {
         HttpClient Account = new HttpClient();
         HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, requestString);
         request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", AccessToken);

         try
         {
            using (var response = await Account.SendAsync(request))
            {
               if (response.IsSuccessStatusCode)
               {
                  // request succeeded -- return null
                  return null;
               }
               else
               {
                  return await response.Content.ReadAsStringAsync();
               }
            }
         }
         catch (WebException ex)
         {
            var response = (HttpWebResponse)ex.Response;
            var stream = new StreamReader(response.GetResponseStream());
            var result = stream.ReadToEnd();
            return result;
         }
      }

      /// <summary>
      /// Primary (internal) request handler
      /// </summary>
      /// <typeparam name="T">The response type</typeparam>
      /// <param name="requestString">the request to make</param>
      /// <param name="method">method for the request (defaults to GET)</param>
      /// <param name="requestParams">optional parameters (note that if provided, it's assumed the requestString doesn't contain any)</param>
      /// <returns>response via type T</returns>
      private static async Task<T> MakeRequestAsync<T>(string requestString, string method = "GET", Dictionary<string, string> requestParams = null)
      {
         if (requestParams != null && requestParams.Count > 0)
         {
            var parameters = CreateParamString(requestParams);
            requestString = requestString + "?" + parameters;
         }
         HttpWebRequest request = WebRequest.CreateHttp(requestString);
         request.Headers[HttpRequestHeader.Authorization] = "Bearer " + AccessToken;
         request.Headers[HttpRequestHeader.AcceptEncoding] = "gzip, deflate";
         request.Method = method;

         try
         {
            using (WebResponse response = await request.GetResponseAsync())
            {
               var serializer = new DataContractJsonSerializer(typeof(T));
               var stream = GetResponseStream(response);
               T result = (T)serializer.ReadObject(stream);
               return result;
            }
         }
         catch (WebException ex)
         {
            var stream = GetResponseStream(ex.Response);
            var reader = new StreamReader(stream);
            var result = reader.ReadToEnd();
            throw new Exception(result);
         }
      }

      private static Stream GetResponseStream(WebResponse response)
      {
         var stream = response.GetResponseStream();
         if (response.Headers["Content-Encoding"] == "gzip")
         {  // if we received a gzipped response, handle that
            stream = new GZipStream(stream, CompressionMode.Decompress);
         }
         else if (response.Headers["Content-Encoding"] == "deflate")
         {  // if we received a deflated response, handle that
            stream = new DeflateStream(stream, CompressionMode.Decompress);
         }
         return stream;
      }

      /// <summary>
      /// Secondary (internal) request handler. differs from primary in that parameters are placed in the body instead of the request string
      /// </summary>
      /// <typeparam name="T">response type</typeparam>
      /// <param name="method">method to use (usually POST or PATCH)</param>
      /// <param name="requestParams">the parameters to pass in the request body</param>
      /// <param name="requestString">the request to make</param>
      /// <returns>response, via type T</returns>
      private static async Task<T> MakeRequestWithParamString<T>(string method, Dictionary<string, string> requestParams, string requestString)
      {
         // Create the body
         var paramString = CreateParamString(requestParams);
         HttpWebRequest request = WebRequest.CreateHttp(requestString);
         request.Headers[HttpRequestHeader.Authorization] = "Bearer " + AccessToken;
         request.Method = method;
         request.ContentType = "application/x-www-form-urlencoded";

         using (var writer = new StreamWriter(await request.GetRequestStreamAsync()))
         {
            // Write the body
            await writer.WriteAsync(paramString);
         }

         // Handle the response
         try
         {
            using (WebResponse response = await request.GetResponseAsync())
            {
               var serializer = new DataContractJsonSerializer(typeof(T));
               return (T)serializer.ReadObject(response.GetResponseStream());
            }
         }
         catch (WebException ex)
         {
            var response = (HttpWebResponse)ex.Response;
            var stream = new StreamReader(response.GetResponseStream());
            var result = stream.ReadToEnd();
            throw new Exception(result);
         }
      }

      /// <summary>
      /// Secondary (internal) request handler. differs from primary in that parameters are placed in the body instead of the request string
      /// </summary>
      /// <typeparam name="T">response type</typeparam>
      /// <param name="method">method to use (usually POST or PATCH)</param>
      /// <param name="requestParams">the parameters to pass in the request body</param>
      /// <param name="requestString">the request to make</param>
      /// <returns>response, via type T</returns>
      private static async Task<T> MakeRequestWithJSONBody<T, P>(string method, P requestParams, string requestString)
      {
         // Create the body
         var requestBody = CreateJSONBody(requestParams);
         HttpWebRequest request = WebRequest.CreateHttp(requestString);
         request.Headers[HttpRequestHeader.Authorization] = "Bearer " + AccessToken;
         request.Method = method;
         request.ContentType = "application/json";

         using (var writer = new StreamWriter(await request.GetRequestStreamAsync()))
         {
            // Write the body
            await writer.WriteAsync(requestBody);
         }

         // Handle the response
         try
         {
            using (WebResponse response = await request.GetResponseAsync())
            {
               var serializer = new DataContractJsonSerializer(typeof(T));
               return (T)serializer.ReadObject(response.GetResponseStream());
            }
         }
         catch (WebException ex)
         {
            var response = (HttpWebResponse)ex.Response;

            // oco: if you want to get fancy, you can hook up an ErrorResponseFactory here
            // and use it to serialize the error object for storage, etc.

            var stream = new StreamReader(response.GetResponseStream());
            var result = stream.ReadToEnd();
            throw new Exception(result);
         }
      }

      /// <summary>
      /// Helper function to create the parameter string out of a dictionary of parameters
      /// </summary>
      /// <param name="requestParams">the parameters to convert</param>
      /// <returns>string containing all the parameters for use in requests</returns>
      private static string CreateParamString(Dictionary<string, string> requestParams)
      {
         string requestBody = "";
         foreach (var pair in requestParams)
         {
            requestBody += WebUtility.UrlEncode(pair.Key) + "=" + WebUtility.UrlEncode(pair.Value) + "&";
         }
         requestBody = requestBody.Trim('&');
         return requestBody;
      }

      /// <summary>
      /// Helper function to create the parameter string out of a dictionary of parameters
      /// </summary>
      /// <param name="requestParams">the parameters to convert</param>
      /// <returns>string containing all the parameters for use in requests</returns>
      private static string CreateJSONBody<P>(P obj, bool simpleDictionary = false)
      {
         // trap this in case of forgetting
         if (typeof(P) == typeof(IDictionary<string, string>))
         {
            simpleDictionary = true;
         }
         if (typeof(P) == typeof(IDictionary<string, object>))
         {
            simpleDictionary = true;
         }


         var settings = new DataContractJsonSerializerSettings();
         settings.UseSimpleDictionaryFormat = simpleDictionary;

         var jsonSerializer = new DataContractJsonSerializer(typeof(P), settings);
         using (var ms = new MemoryStream())
         {
            jsonSerializer.WriteObject(ms, obj);
            var msBytes = ms.ToArray();
            return Encoding.UTF8.GetString(msBytes, 0, msBytes.Length);
         }
      }
   }
}
