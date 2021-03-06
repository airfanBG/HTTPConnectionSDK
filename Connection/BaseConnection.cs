﻿using ActivityRegister.DbConnection;
using ActivityRegister.Models;
using ActivityRegister.Utility;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

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
        private IStatistic statistic;
        private StatisticDbConnection db;
        private EntityStatistic entity;

        public BaseConnection(HttpClient address, string user, string password)
        {
            httpClientConnection = new HttpClient();
           
            statistic = new StatisticUtility();
            entity = new EntityStatistic();
            db = new StatisticDbConnection();

            httpClientConnection.BaseAddress = new Uri(address.BaseAddress.AbsoluteUri);
            this.user = user;
            this.password = password;

            directLinkToModel = new URLLinks<T>(address.BaseAddress.AbsoluteUri);
            GetAccess(user, password);

            
        }
        
#region
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
                entity.Error = HttpStatusCode.Unauthorized.ToString();
                db.SaveChanges();
                return HttpStatusCode.Unauthorized;
            }
              
            
        }

        /// <summary>
        /// Return all data from current Entity
        /// </summary>
        /// <param name="entityName">Entity</param>
        /// <returns></returns>
        public string GetAll(string entityName)
        {

            httpClientConnection.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
            
            try
            {
                respMsg = httpClientConnection.GetAsync(directLinkToModel.UriPath.BaseAddress.AbsoluteUri).Result;

                var requestMethod = respMsg.RequestMessage.Method;
                var functionName = System.Reflection.MethodBase.GetCurrentMethod().Name;
                
                var result = respMsg.Content.ReadAsAsync<IEnumerable<T>>();
                var jsonResult = Task.Run(() => JsonConvert.SerializeObject(result)).Result;
             
                AllData(requestMethod, functionName);

                return jsonResult;
            }
            catch (Exception e)
            {
                entity.Error = e.Message;
                
db.SaveChanges();
                return String.Format("Unauthorized {0}",e.Message);
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
                    entity.Error = respMsg.RequestMessage.ToString();
                    db.SaveChanges();
                    throw new Exception(respMsg.ReasonPhrase);
                }
            }
            catch (Exception e)
            {

                entity.Error = e.Message;
              
                db.SaveChangesAsync();
                throw new Exception(e.Message);
            }
        }
        
        public void PutClient(int id, T clientData)
        {
            var requestMethod = respMsg.RequestMessage.Method;
            var functionName = System.Reflection.MethodBase.GetCurrentMethod().Name;
            
            httpClientConnection.DefaultRequestHeaders.Accept.Clear();
            httpClientConnection.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
 
            try
            {
                var content = new StringContent(Task.Run(()=> JsonConvert.SerializeObject(clientData)).Result, Encoding.UTF8, "application/json");
               
                httpClientConnection.PutAsJsonAsync(directLinkToModel.UriPath.BaseAddress.AbsoluteUri + "/" + id, content);

                AllData(requestMethod, functionName);
            }
            catch (Exception e)
            {

                entity.Error = e.Message;

                db.SaveChangesAsync();
                throw new Exception(e.Message);
            }
           
        }
       
        public void Delete(int id)
        {
            var requestMethod = respMsg.RequestMessage.Method;
            var functionName = System.Reflection.MethodBase.GetCurrentMethod().Name;
            
            try
            {
                AllData(requestMethod, functionName);
                
                var stat= httpClientConnection.DeleteAsync(directLinkToModel.UriPath.BaseAddress.AbsoluteUri + "/" + id).Result;
                var reqMess = stat.IsSuccessStatusCode;

                if (reqMess==false||stat.ReasonPhrase=="Internal Server Error")
                {
                    throw new Exception(stat.ReasonPhrase);
                }

            }
            catch (Exception e)
            {
                entity.Error = e.Message;

                db.SaveChangesAsync();
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
                        break;
                    }
                }


                return model;

            }
            catch (Exception e)
            {
                entity.Error = e.Message;

                db.SaveChangesAsync();
                throw new Exception(e.Message);
            }
        }
        #endregion
        private void AllData(HttpMethod requestMethod, string functionName)
        {

            entity.MachineId =statistic.GetMacAddress();
            entity.DateOfRequest = statistic.GetDateOfRequest(DateTime.UtcNow);
            entity.Error = statistic.Error;
            entity.RequestType = statistic.GetMethodName(requestMethod.Method);
            entity.ComputerName = statistic.GetMachineName()[0];
            entity.UserName = statistic.GetMachineName()[1];
            entity.RequestMethod = statistic.GetMethodName(functionName);
           

            db.Entity.Add(entity);
            db.SaveChanges();
        }

        
    }
}
