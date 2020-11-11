using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using RandomUser.Models;
using RandomUser.Models.TableModels;

namespace RandomUser.Connection.Interfaces
{
    public interface IUserNameService : IService<tblUserName>
    {
        List<tblUserName> GetNamesOfRange(List<int> ids);
    }
}