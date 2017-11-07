using ActivityRegister.Utility;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Formatting;
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
        private URLLinks<T> directLinkToModel;
        private StatisticUtility statistic;
        private HttpMethod method;

        public BaseConnection(HttpClient address, string user, string password)
        {
            httpClientConnection = new HttpClient();
            statistic = new StatisticUtility();
           
            httpClientConnection.BaseAddress = new Uri(address.BaseAddress.AbsoluteUri);
            this.user = user;
            this.password = password;

            directLinkToModel = new URLLinks<T>(address.BaseAddress.AbsoluteUri);
            GetAccess(user, password);
           

        }
        
#region
       
        public StatisticUtility Statistic { get { return this.statistic; }set { this.statistic = value; } }

        #endregion
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
                respMsg = httpClientConnection.GetAsync(directLinkToModel.UriPath.BaseAddress.AbsoluteUri).Result;

                var method = respMsg.RequestMessage.Method;
                AllData(method);
                
                var result = respMsg.Content.ReadAsAsync<IEnumerable<T>>().Result;
                var jsonResult = Task.Run(() => JsonConvert.SerializeObject(result));
             

                return jsonResult;
            }
            catch (Exception e)
            {
                Statistic.GetError(e.Message);
                return Task.Run(() => String.Format("Unauthorized {0}",e.Message));
            }
            
        }

        public void PostClient(T clientData)
        {
            try
            {
                httpClientConnection.DefaultRequestHeaders.Accept.Clear();

                httpClientConnection.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
                
                string json = JsonConvert.SerializeObject(clientData);
                StringContent content = new StringContent(json, Encoding.UTF8, "application/json");

                respMsg = httpClientConnection.PostAsJsonAsync<T>("http://localhost:52281/api/clients/", clientData).Result;

                var method = respMsg.RequestMessage.Method;
                AllData(method);

                if (!respMsg.IsSuccessStatusCode)
                {
                    throw new Exception(respMsg.ReasonPhrase);
                }
            }
            catch (Exception e)
            {
                Statistic.GetError(e.Message);
                throw new Exception(e.Message);
            }
        }

        public void PutClient(string clienttoken, T clientData)
        {
            var method = respMsg.RequestMessage.Method;
            AllData(method);

            try
            {

            }
            catch (Exception e)
            {

                Statistic.GetError(e.Message);
                throw new Exception(e.Message);
            }
            throw new NotImplementedException();
        }
        public void Delete(string clienttoken)
        {
            var method = respMsg.RequestMessage.Method;
            AllData(method);

            try
            {

            }
            catch (Exception e)
            {

                Statistic.GetError(e.Message);
                throw new Exception(e.Message);
            }
        }

       
        public T GetByParam(string id)
        {
            var method = respMsg.RequestMessage.Method;
            AllData(method);

           
            try
            {
                respMsg = httpClientConnection.GetAsync(directLinkToModel.UriPath.BaseAddress.AbsoluteUri).Result;

                var result = respMsg.Content.ReadAsAsync<IEnumerable<T>>().Result;

                T model = Activator.CreateInstance<T>();

                foreach (var item in result)
                {
                   var t= item.GetType().GetProperty("Id").GetValue(item);
                    if (id==t.ToString())
                    {
                        model = item;
                    }
                }


                return model;

            }
            catch (Exception e)
            {

                Statistic.GetError(e.Message);
                throw new Exception(e.Message);
            }
        }
        private void AllData(HttpMethod method)
        {
            Statistic.GetRequestType(respMsg.RequestMessage.Method);
            Statistic.GetRequestType(method);
            Statistic.GetMethodName(this.GetType().Name);
            Statistic.GetDateOfRequest(DateTime.UtcNow);
            Statistic.GetMacAddress();
            Statistic.GetMachineName();
            
        }

        
    }
}
