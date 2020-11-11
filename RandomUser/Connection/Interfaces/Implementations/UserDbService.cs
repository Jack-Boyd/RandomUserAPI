using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using RandomUser.Models;
using RandomUser.Models.TableModels;

namespace RandomUser.Connection.Interfaces.Implementations
{
    public class UserDbService : BaseService, IUserService
    {
        public UserDbService()
            : base()
        {
        }
        public List<tblUser> GetAll()
        {
            return _db.Fetch<tblUser>("WHERE 1=1").ToList();
        }
        public tblUser GetById(int id)
        {
            return _db.Fetch<tblUser>("WHERE UserId = @0", id).FirstOrDefault();
        }

        public void Update(tblUser user)
        {
            _db.Update(user);
        }
        public void Delete(int id)
        {
            var sql = "DELETE from tblUser where UserId = @0";
            _db.Execute(sql, id);
        }
        public List<tblUser> GetAllWithSearch(int? limit = 0, string firstName = "", string lastName = "")
        {
            bool hasLimit = limit > 0;
            bool hasFirstNameSearch = !string.IsNullOrEmpty(firstName);
            bool hasLastNameSearch = !string.IsNullOrEmpty(lastName);
            bool hasNameSearch = hasFirstNameSearch || hasLastNameSearch;

            var sql = "";
            List<tblUser> users = new List<tblUser>();
            if (hasNameSearch) { 
                if (hasFirstNameSearch && !hasLastNameSearch) {
                    sql = hasLimit ?
                        "SELECT TOP (@0) tblUser.* FROM tblUser INNER JOIN tblUserName on tblUser.NameId = tblUserName.NameId WHERE tblUserName.FirstName LIKE '%" + firstName + "%'" :
                        "SELECT tblUser.* FROM tblUser INNER JOIN tblUserName on tblUser.NameId = tblUserName.NameId WHERE tblUserName.FirstName LIKE '%" + firstName + "%'";
                } 
                else if (!hasFirstNameSearch && hasLastNameSearch) {
                    sql = hasLimit ?
                        "SELECT TOP (@0) tblUser.* FROM tblUser INNER JOIN tblUserName on tblUser.NameId = tblUserName.NameId WHERE tblUserName.LastName LIKE '%" + lastName + "%'" :
                        "SELECT tblUser.* FROM tblUser INNER JOIN tblUserName on tblUser.NameId = tblUserName.NameId WHERE tblUserName.LastName LIKE '%" + lastName + "%'";
                }
                else if (hasFirstNameSearch && hasLastNameSearch) {
                    sql = hasLimit ?
                        "SELECT TOP (@0) tblUser.* FROM tblUser INNER JOIN tblUserName on tblUser.NameId = tblUserName.NameId WHERE tblUserName.FirstName LIKE '%" + firstName + "%' OR tblUserName.LastName LIKE '%" + lastName + "%'" :
                        "SELECT tblUser.* FROM tblUser INNER JOIN tblUserName on tblUser.NameId = tblUserName.NameId WHERE tblUserName.FirstName LIKE '%" + firstName + "%' OR tblUserName.LastName LIKE '%" + lastName + "%'";
                }
                users = hasLimit ? _db.Query<tblUser>(sql, limit).ToList() : _db.Query<tblUser>(sql).ToList();
            } 
            else {
                if (hasLimit) {
                    sql = "SELECT TOP (@0) * FROM tblUser";
                    users = _db.Query<tblUser>(sql, limit).ToList();
                } 
                else
                {
                    sql = "SELECT * FROM tblUser";
                    users = _db.Query<tblUser>(sql).ToList();
                }
            }
            return users;
        }
    }
}