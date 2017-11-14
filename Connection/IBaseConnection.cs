using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Connection
{
    public interface IBaseFunction<T> : IDisposable, IConnection<T> where T : class
    {

        string GetAll(string url);
        void PostClient(T clientData);
        void PutClient(int id, T clientData);
        void Delete(int id);
    }
}
