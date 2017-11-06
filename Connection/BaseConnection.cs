﻿using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Connection
{
    /// <summary>
    /// Create connection to Db where T is some model
    /// </summary>
    /// <typeparam name="T">T is some model class </typeparam>
  
    public class BaseConnection<T> : HttpClient, IBaseFunction<T> where T : class

    {
        private static HttpClient httpClientConnection;
        private static HttpResponseMessage respMsg;
        private string user;
        private string password;
        private string token;

        public BaseConnection(HttpClient address, string user, string password)
        {
            httpClientConnection = new HttpClient();
            httpClientConnection.BaseAddress = new Uri(address.BaseAddress.AbsoluteUri);
            this.user = user;
            this.password = password;
           

               GetAccess(user, password);
        }
       
        private HttpStatusCode GetAccess(string user, string password)
        {
            var uri = httpClientConnection.BaseAddress.AbsoluteUri;
            var formContent = new FormUrlEncodedContent(new[]
            {
                new KeyValuePair<string,string>("grant_type","password"),
                new KeyValuePair<string,string>("username",user),
                new KeyValuePair<string,string>("password",password)
            });
            
           
            try
            {
                respMsg = httpClientConnection.PostAsync(String.Format("{0}{1}", httpClientConnection.BaseAddress, "Token"), formContent).Result;
                var resJson = respMsg.Content.ReadAsStringAsync();
                var jToken = JObject.Parse(resJson.Result);
                var token = jToken.GetValue("access_token").ToString();
                //this.token = token;
                httpClientConnection.DefaultRequestHeaders.Add("Authorization", "Bearer " + token);
                return HttpStatusCode.OK;
            }
            catch (Exception)
            {

                return HttpStatusCode.Unauthorized;
            }
              
            
        }


        public Task<string> GetAll(string url)
        {

            httpClientConnection.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
          
            try
            {
                
                 respMsg = httpClientConnection.GetAsync(httpClientConnection.BaseAddress.AbsoluteUri + "api" + "/"+url).Result;

                var result = respMsg.Content.ReadAsAsync<IEnumerable<T>>().Result;
                var jsonResult = Task.Run(() => JsonConvert.SerializeObject(result));

                return jsonResult;
            }
            catch (Exception e)
            {

                return Task.Run(() => String.Format("Unauthorized {0}",e.Message));
            }
            
        }

        public void PostClient(T clientData)
        {
            throw new NotImplementedException();
        }

        public void PutClient(string clienttoken, T clientData)
        {
            throw new NotImplementedException();
        }
        public void Delete(string clienttoken)
        {
            throw new NotImplementedException();
        }

        public T GetByParam(object param)
        {
            throw new NotImplementedException();
        }

        public T GetByParam(T param)
        {
            throw new NotImplementedException();
        }
    }
}
