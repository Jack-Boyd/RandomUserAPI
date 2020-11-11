using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using RandomUser.Models;
using RandomUser.Models.TableModels;

namespace RandomUser.Connection.Interfaces.Implementations
{
    public class UserNameDbService : BaseService, IUserNameService
    {
        public UserNameDbService()
    : base()
        {
        }
        public List<tblUserName> GetAll()
        {
            return _db.Fetch<tblUserName>("WHERE 1=1").ToList();
        }
        public tblUserName GetById(int id)
        {
            return _db.Fetch<tblUserName>("WHERE NameId = @0", id).FirstOrDefault();
        }

        public void Update(tblUserName name)
        {
            _db.Update(name);
        }
        public void Delete(int id)
        {
            var sql = "DELETE from tblUserName where NameId = @0";
            _db.Execute(sql, id);
        }
        public List<tblUserName> GetNamesOfRange(List<int> ids)
        {
            var sql = PetaPoco.Sql.Builder.Select("*").From("tblUserName").Where("NameId in (@idsToFind)", new { idsToFind = ids });
            return ids.Count > 0 ? _db.Query<tblUserName>(sql).ToList() : null;
        }
    }
}