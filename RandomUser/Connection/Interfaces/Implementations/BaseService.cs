using PetaPoco;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RandomUser.Connection.Interfaces.Implementations
{
    public class BaseService
    {
        protected Database _db;
        public BaseService()
        {
            _db = new Database("DefaultConnection");
        }
    }
}