using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Newtonsoft.Json;

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

        private void SanitizeUrl(ref string input)
        {
            int index = input.IndexOf("?");
            if (index > 0)
                input = input.Substring(0, index);
        }

        // GET api/values/5
        public string Get(string user, string url, string msg)
        {
            SanitizeUrl(ref url);

            var userId = GetOrAddUser(user);
            var urlId = GetOrAddUrl(url);
            var msgId = AddMessage(msg);
            AddUserMsg(userId, urlId, msgId);
            return "value";
        }

        public string Get(string url)
        {
            SanitizeUrl(ref url);

            var msgList = GetMsgsForUrl(url);
            var json = JsonConvert.SerializeObject(msgList);
            return json;
        }

        class MessageInfo
        {
            public string message;
            public string username;
            public string time;
        }

        private List<MessageInfo> GetMsgsForUrl(string url)
        {
            var msgList = new List<MessageInfo>();

            var dbUrl = fcDb.urls.FirstOrDefault(x => x.value == url);
            
            if(dbUrl==null)
                return msgList;

            var urlUserMsgs = fcDb.urlUserMsgs.Where(x => x.urlId == dbUrl.id).OrderByDescending(x=>x.id).Take(10);

            foreach (var uum in urlUserMsgs)
            {
                msgList.Add(new MessageInfo()
                {
                    message = fcDb.msgs.FirstOrDefault(x => x.id == uum.msgId).value,
                    time = fcDb.msgs.FirstOrDefault(x => x.id == uum.msgId).timeStamp.ToString(),
                    username = fcDb.users.FirstOrDefault(x => x.id == uum.userId).value
                });
            }

            msgList.Reverse();

            return msgList;
        }


        private int AddMessage(string msg)
        {
            var newMsg = fcDb.msgs.Add(new msg()
            {
                value = msg,
                timeStamp = DateTime.Now
            });

            fcDb.SaveChanges();
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

            fcDb.SaveChanges();
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

            fcDb.SaveChanges();
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

            fcDb.SaveChanges();
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
