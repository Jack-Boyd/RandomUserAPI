using PetaPoco;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RandomUser.Models.TableModels
{
    [TableName("tblUser")]
    [PrimaryKey("UserId")]
    public class tblUser
    {
        public int UserId { get; set; }
        public int NameId { get; set; }
        public string Email { get; set; }
        public string Gender { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string PhoneNumber { get; set; }
        public string ThumbnailImage { get; set; }
        public string MainImage { get; set; }
    }
}