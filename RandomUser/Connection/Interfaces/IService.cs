using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RandomUser.Connection.Interfaces
{
    public interface IService<T>
    {
        T GetById(int id);
        List<T> GetAll();
        void Update(T user);
        void Delete(int id);
    }
}
