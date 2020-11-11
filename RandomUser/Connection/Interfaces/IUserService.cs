using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RandomUser.Models;
using RandomUser.Models.TableModels;

namespace RandomUser.Connection.Interfaces
{
    public interface IUserService : IService<tblUser>
    {
        List<tblUser> GetAllWithSearch(int? limit, string firstName, string lastName);
    }
}
