using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using RandomUser.Connection.Interfaces;
using RandomUser.Connection.Interfaces.Implementations;
using RandomUser.Models;
using RandomUser.Models.TableModels;
namespace RandomUser.Connection.Services
{
    public class UserNameService
    {
        private static readonly IUserNameService uns = new UserNameDbService();
        public static List<tblUserName> GetAll()
        {
            return uns.GetAll();
        }
        public static tblUserName GetById(int id)
        {
            return uns.GetById(id);
        }
        public static void Update(tblUserName name)
        {
            uns.Update(name);
        }
        public static void Delete(int id)
        {
            uns.Delete(id);
        }
        public static List<tblUserName> GetNamesOfRange(List<int> ids)
        {
            return uns.GetNamesOfRange(ids);
        }
    }
}