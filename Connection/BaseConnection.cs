using ActivityRegister.DbConnection;
using ActivityRegister.Utility;
using Database;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Net.Http.Headers;
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
        private StatisticUtility stat;
        private StatisticDbConnection db;

        public BaseConnection(HttpClient address, string user, string password)
        {
            httpClientConnection = new HttpClient();
           
            stat = new StatisticUtility();

            httpClientConnection.BaseAddress = new Uri(address.BaseAddress.AbsoluteUri);
            this.user = user;
            this.password = password;

            directLinkToModel = new URLLinks<T>(address.BaseAddress.AbsoluteUri);
            GetAccess(user, password);

            
        }
        
#region
       
        public StatisticUtility Statistic { get { return this.stat; }set { this.stat = value; } }

       
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

                var requestMethod = respMsg.RequestMessage.Method;
                var functionName = System.Reflection.MethodBase.GetCurrentMethod().Name;

               
                
                var result = respMsg.Content.ReadAsAsync<IEnumerable<T>>().Result;
                var jsonResult = Task.Run(() => JsonConvert.SerializeObject(result));
             
                AllData(requestMethod, functionName);

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

                respMsg = httpClientConnection.PostAsJsonAsync<T>(directLinkToModel.UriPath.BaseAddress.AbsoluteUri, clientData).Result;

                var requestMethod = respMsg.RequestMessage.Method;
                var functionName = System.Reflection.MethodBase.GetCurrentMethod().Name;

                AllData(requestMethod, functionName);

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
        
        public void PutClient(int id, T clientData)
        {
            var requestMethod = respMsg.RequestMessage.Method;
            var functionName = System.Reflection.MethodBase.GetCurrentMethod().Name;

            AllData(requestMethod, functionName);

            httpClientConnection.DefaultRequestHeaders.Accept.Clear();
            httpClientConnection.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
 
            try
            {
                var content = new StringContent(JsonConvert.SerializeObject(clientData), Encoding.UTF8, "application/json");
               
                var t = httpClientConnection.PutAsJsonAsync(directLinkToModel.UriPath.BaseAddress.AbsoluteUri + "/" + id, content).Result;
            }
            catch (Exception e)
            {

                Statistic.GetError(e.Message);
                throw new Exception(e.Message);
            }
           
        }
        public void Delete(int id)
        {
            var requestMethod = respMsg.RequestMessage.Method;
            var functionName = System.Reflection.MethodBase.GetCurrentMethod().Name;

            AllData(requestMethod, functionName);

            var uri = String.Format(directLinkToModel.UriPath.BaseAddress.AbsoluteUri + "/" + id);

            respMsg = httpClientConnection.DeleteAsync(uri).Result;
            
           
            try
            {
            }
            catch (Exception e)
            {

                Statistic.GetError(e.Message);
                throw new Exception(e.Message);
            }
        }

       
        public T GetByParam(int id)
        {
            var requestMethod = respMsg.RequestMessage.Method;
            var functionName = System.Reflection.MethodBase.GetCurrentMethod().Name;

            AllData(requestMethod, functionName);

            try
            {

                respMsg = httpClientConnection.GetAsync(directLinkToModel.UriPath.BaseAddress.AbsoluteUri).Result;

                var result = respMsg.Content.ReadAsAsync<IEnumerable<T>>().Result;

                T model = Activator.CreateInstance<T>();

                foreach (var item in result)
                {
                   var t= item.GetType().GetProperty("Id").GetValue(item);
                    if (id==(int)t)
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
        #endregion
        private void AllData(HttpMethod requestMethod, string functionName)
        {
             ActivityRegister.Models.Statistic res = new ActivityRegister.Models.Statistic();
            res.MachineId = stat.GetMacAddress();
            res.DateOfRequest = stat.GetDateOfRequest(DateTime.UtcNow);
            res.Error = stat.Error;
            res.RequestType = stat.GetMethodName(requestMethod.Method);
            res.ComputerName = stat.GetMachineName()[0];
            res.UserName = stat.GetMachineName()[1];
            res.RequestModel = stat.GetMethodName(functionName);
            db = new StatisticDbConnection();
            db.Statistics.Add(res);
            db.SaveChanges();
        }

        
    }
}
