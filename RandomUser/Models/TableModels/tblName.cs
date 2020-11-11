using PetaPoco;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RandomUser.Models.TableModels
{
    [TableName("tblUserName")]
    [PrimaryKey("NameId")]
    public class tblUserName
    {
        public int NameId { get; set; }
        public string Title { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}