# HTTPConnectionSDK Connection.dll creates a base connection to Web.Api server.  Only thing you have to do is to have a model for Db, and Web.Api address. 
            HttpClient cl = new HttpClient();
            cl.BaseAddress = new Uri("http://localhost:0000");
           
            BaseConnection<Clients> conn = new BaseConnection<Clients>(cl, "airfan", "123456");
