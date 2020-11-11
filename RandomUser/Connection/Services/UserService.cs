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
    public class UserService
    {
        private static readonly IUserService us = new UserDbService();
        public static tblUser GetById(int id)
        {
            return us.GetById(id);
        }
        public static List<tblUser> GetAll()
        {
            return us.GetAll();
        }
        public static void Update(tblUser user)
        {
            us.Update(user);
        }
        public static void Delete(int id)
        {
            us.Delete(id);
        }
        public static List<tblUser> GetAllWithSearch(int? limit = 0, string firstName = "", string lastName = "")
        {
            return us.GetAllWithSearch(limit, firstName, lastName);
        }
    }
}