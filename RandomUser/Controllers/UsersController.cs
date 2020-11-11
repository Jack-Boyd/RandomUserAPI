using RandomUser.Connection.Services;
using RandomUser.Models;
using RandomUser.Models.TableModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace RandomUser.Controllers
{
    public class UsersController : ApiController
    {
        public IHttpActionResult Get(int? limit = null, string firstName = null, string lastName = null)
        {
            List<tblUser> dbUsers = UserService.GetAllWithSearch(limit, firstName, lastName);
            List<tblUserName> dbNames = UserNameService.GetNamesOfRange(dbUsers.Select(s => s.NameId).ToList());
            List<User> users = dbUsers
                .Select(s => new User()
                {
                    UserId = s.UserId,
                    Name = new Name()
                    {
                        Title = dbNames.Where(x => x.NameId == s.NameId).FirstOrDefault().Title,
                        FirstName = dbNames.Where(x => x.NameId == s.NameId).FirstOrDefault().FirstName,
                        LastName = dbNames.Where(x => x.NameId == s.NameId).FirstOrDefault().LastName
                    },
                    Email = s.Email,
                    Gender = s.Gender,
                    DateOfBirth = s.DateOfBirth,
                    PhoneNumber = s.PhoneNumber,
                    ThumbnailImage = s.ThumbnailImage,
                    MainImage = s.MainImage
                }).ToList<User>();
            if (users.Count == 0)
            {
                return Ok();
            }

            return Ok(users);
        }
        public IHttpActionResult GetUserById(int id)
        {
            tblUser dbUser = UserService.GetById(id);
            User user = new User();
            if (dbUser != null)
            {
                tblUserName dbName = UserNameService.GetById(dbUser.NameId);
                user = new User()
                {
                    UserId = dbUser.UserId,
                    Name = new Name()
                    {
                        Title = dbName.Title,
                        FirstName = dbName.FirstName,
                        LastName = dbName.LastName
                    },
                    Email = dbUser.Email,
                    Gender = dbUser.Gender,
                    DateOfBirth = dbUser.DateOfBirth,
                    PhoneNumber = dbUser.PhoneNumber,
                    ThumbnailImage = dbUser.ThumbnailImage,
                    MainImage = dbUser.MainImage
                };
                if (user == null)
                {
                    return Ok();
                }
            } 
            else
            {
                return Ok();
            }

            return Ok(user);
        }



        public IHttpActionResult Put([FromBody]User value)
        {
            if (!ModelState.IsValid)
                return BadRequest("Not a valid model");

            tblUser dbUser = UserService.GetById(value.UserId);
            tblUserName dbName = UserNameService.GetById(dbUser.NameId);
            try
            {
                if (dbName != null)
                {
                    dbName.Title = value.Name.Title;
                    dbName.FirstName = value.Name.FirstName;
                    dbName.LastName = value.Name.LastName;

                    UserNameService.Update(dbName);
                }
                else
                {
                    return BadRequest("No User matches provided ID");
                }

                if (dbUser != null)
                {
                    dbUser.Email = value.Email;
                    dbUser.Gender = value.Gender;
                    dbUser.DateOfBirth = Convert.ToDateTime(value.DateOfBirth);
                    dbUser.PhoneNumber = value.PhoneNumber;
                    dbUser.ThumbnailImage = value.ThumbnailImage;
                    dbUser.MainImage = value.MainImage;

                    UserService.Update(dbUser);
                }
                else
                {
                    return BadRequest("No User matches provided ID");
                }
            }
            catch (Exception ex)
            {
                return BadRequest("Not a valid call");
            }
            return Ok("Success");
        }

        public IHttpActionResult Delete(int id)
        {
            if (id <= 0)
                return BadRequest("Not a valid student id");

            tblUser dbUser = UserService.GetById(id);
            tblUserName dbName = UserNameService.GetById(dbUser.NameId);

            try
            {

                if (dbUser != null)
                {
                    UserService.Delete(id);
                }
                else
                {
                    return BadRequest("No User matches provided ID");
                }
                if (dbName != null)
                {
                    UserNameService.Delete(dbUser.NameId);
                }
                else
                {
                    return BadRequest("No User matches provided ID");
                }
            }
            catch (Exception ex)
            {
                return BadRequest("Not a valid call");
            }

            return Ok("Success");
        }
    }
}
