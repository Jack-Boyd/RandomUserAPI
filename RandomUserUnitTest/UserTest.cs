using System;
using Newtonsoft.Json;
using RandomUser.Models;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Runtime.Serialization;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace RandomUserUnitTest
{
    [TestClass]
    public class UserTest
    {
        [TestMethod]
        public void TestGetUsers()
        {
            string URL = "https://localhost:44383/api/users";
            string usersResponseString = GetApi(URL);
            User[] users = JsonConvert.DeserializeObject<User[]>(usersResponseString);
            Assert.IsTrue(users != null && users.Length > 0);
        }
        [TestMethod]
        public void TestGetUserById()
        {
            string URL = "https://localhost:44383/api/users?id=5020";
            string usersResponseString = GetApi(URL);
            User user = JsonConvert.DeserializeObject<User>(usersResponseString);
            Assert.IsTrue(user != null);
        }
        [TestMethod]
        public void TestGetUserByNonExistentId()
        {
            string URL = "https://localhost:44383/api/users?id=4020";
            string usersResponseString = GetApi(URL);
            User user = JsonConvert.DeserializeObject<User>(usersResponseString);
            Assert.IsFalse(user != null);
        }
        [TestMethod]
        public void TestGetUsersFilterCorrect1()
        {
            string URL = "https://localhost:44383/api/users?limit=30&firstname=el";
            string usersResponseString = GetApi(URL);
            User[] users = JsonConvert.DeserializeObject<User[]>(usersResponseString);
            Assert.IsTrue(users != null && users.Length > 0);
        }
        [TestMethod]
        public void TestGetUsersFilterCorrect2()
        {
            string URL = "https://localhost:44383/api/users?limit=10&firstname=e&lastname=e";
            string usersResponseString = GetApi(URL);
            User[] users = JsonConvert.DeserializeObject<User[]>(usersResponseString);
            Assert.IsTrue(users != null && users.Length > 0);
        }
        [TestMethod]
        public void TestGetUsersFilterWrong()
        {
            string URL = "https://localhost:44383/api/users?limit=5&firstname=fail&lastname=fail";
            string usersResponseString = GetApi(URL);
            User[] users = JsonConvert.DeserializeObject<User[]>(usersResponseString);
            Assert.IsFalse(users != null && users.Length > 0);
        }
        [TestMethod]
        public void TestPutUser()
        {
            string URL = "https://localhost:44383/api/users?id=5020";
            string usersResponseString = GetApi(URL);
            User user = JsonConvert.DeserializeObject<User>(usersResponseString);
            var beforeTitle = user.Name.Title;
            user.Name.Title = user.Name.Title == "Mister" ? "Mrs" : "Mister";
            PutApi(user);
            usersResponseString = GetApi(URL);
            user = JsonConvert.DeserializeObject<User>(usersResponseString);
            Assert.IsTrue(!user.Name.Title.Equals(beforeTitle));

        }
        [TestMethod]
        public void TestPutNonExistentUser()
        {
            string URL = "https://localhost:44383/api/users?id=5020";
            string usersResponseString = GetApi(URL);
            User user = JsonConvert.DeserializeObject<User>(usersResponseString);
            var beforeTitle = user.Name.Title;
            user.UserId = 4001;
            PutApi(user);
            usersResponseString = GetApi(URL);
            user = JsonConvert.DeserializeObject<User>(usersResponseString);
            Assert.IsFalse(!user.Name.Title.Equals(beforeTitle));
        }
        [TestMethod]
        public void TestDeleteUser()
        {
            string URL = "https://localhost:44383/api/users";
            string usersBeforeResponseString = GetApi(URL);
            User[] usersBefore = JsonConvert.DeserializeObject<User[]>(usersBeforeResponseString);
            //Only works when there are users in the database. The more you test delete, the more likely the database will end up with no users in the end..
            DeleteApi(URL, usersBefore[0].UserId);
            string usersAfterResponseString = GetApi(URL);
            User[] usersAfter = JsonConvert.DeserializeObject<User[]>(usersAfterResponseString);
            Assert.IsTrue(usersBefore.Length == 0 || (usersBefore.Length > usersAfter.Length));
        }
        [TestMethod]
        public void TestDeleteNonExistentUser()
        {
            string URL = "https://localhost:44383/api/users";
            string usersBeforeResponseString = GetApi(URL);
            User[] usersBefore = JsonConvert.DeserializeObject<User[]>(usersBeforeResponseString);
            //Only works when there are users in the database. The more you test delete, the more likely the database will end up with no users in the end..
            DeleteApi(URL, 4001);
            string usersAfterResponseString = GetApi(URL);
            User[] usersAfter = JsonConvert.DeserializeObject<User[]>(usersAfterResponseString);
            Assert.IsFalse(usersBefore.Length > usersAfter.Length);
        }
        public string GetApi(string URL)
        {
            try
            {
                string responseString = string.Empty;
                ServicePointManager.Expect100Continue = true;
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(URL);
                request.Method = "GET";

                var response = (HttpWebResponse)request.GetResponse();
                using (var stream = response.GetResponseStream())
                {
                    using (var reader = new StreamReader(stream))
                    {
                        responseString = reader.ReadToEnd();
                    }
                };
                return responseString;
            }
            catch (Exception ex)
            {
                return "";
            }
        }

        public void PutApi(User update)
        {
            try
            {
                var httpWebRequest = (HttpWebRequest)WebRequest.Create("https://localhost:44383/api/users");
                httpWebRequest.ContentType = "application/json";
                httpWebRequest.Method = "PUT";
                using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
                {
                    string json = JsonConvert.SerializeObject(update); // Need to put data here to pass to the API.**
                    streamWriter.Write(json);
                }
                var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
                using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                {
                    var responseText = streamReader.ReadToEnd();
                }
            }
            catch (Exception ex)
            {

            }

        }

        public void DeleteApi(string URL, int id)
        {
            try
            {
                WebRequest request = WebRequest.Create(URL + "?id=" + id);
                request.Method = "DELETE";
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            }
            catch (Exception ex)
            {

            }
        }
        public void Serialize(Stream output, object input)
        {
            var ser = new DataContractSerializer(input.GetType());
            ser.WriteObject(output, input);
        }
    }
}
