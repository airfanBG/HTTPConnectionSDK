using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Connection
{
    public interface IBaseFunction<T> : IDisposable, IConnection<T> where T : class
    {

        Task<string> GetAll();
        T GetByParam(object param);
        T GetByParam(T param);
        void PostClient(T clientData);
        void PutClient(string clienttoken, T clientData);
        void Delete(string clienttoken);
    }
}
