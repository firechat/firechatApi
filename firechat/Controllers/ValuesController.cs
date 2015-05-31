using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace firechat.Controllers
{
    //[Authorize]
    public class ValuesController : ApiController
    {
        firechatDb fcDb = new firechatDb();
       
        // GET api/values
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/values/5
        public string Get(string user, string url, string msg)
        {
            var userId = GetOrAddUser(user);
            var urlId = GetOrAddUrl(url);
            var msgId = AddMessage(msg);
            AddUserMsg(userId, urlId, msgId);
            fcDb.SaveChanges();
            return "value";
        }

        private int AddMessage(string msg)
        {
            var newMsg = fcDb.msgs.Add(new msg()
            {
                value = msg
            });

            return newMsg.id;
        }


        private void AddUserMsg(int userId, int urlId, int msgId)
        {
            if (!fcDb.users.Any(x => x.id == userId))
                return;

            if(!fcDb.urls.Any(x => x.id == urlId))
                return;

            var newUrlMsg = fcDb.urlUserMsgs.Add(new urlUserMsg()
            {
                urlId = urlId,
                msgId = msgId,
                userId = userId
            });
        }

        private int GetOrAddUser(string user)
        {
            var users = fcDb.users;
            if (users.Any(x => x.value == user))
            {
                return users.First(x => x.value == user).id;
            }

            //create a new user
            var newUser = users.Add(new user()
            {
                value = user
            });

            return newUser.id;
        }

        private int GetOrAddUrl(string url)
        {
            var urls = fcDb.urls;
            if (urls.Any(x => x.value == url))
            {
                return urls.First(x => x.value == url).id;
            }

            //create a new user
            var newUrl = urls.Add(new url()
            {
                value = url
            });

            return newUrl.id;
        }

        // POST api/values
        public void Post([FromBody]string value)
        {
        }

        // PUT api/values/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        public void Delete(int id)
        {
        }
    }
}
