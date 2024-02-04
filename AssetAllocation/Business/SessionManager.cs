using CRM.Models;
using Microsoft.AspNetCore.Http;
using System.Web;
using Newtonsoft.Json;

namespace CRM.Business
{

    public static class SessionManager
    {
        public static void SetObjectInSession(this ISession session, string key, object value)
        {
            session.SetString(key, JsonConvert.SerializeObject(value));
        }

        public static T GetCustomObjectFromSession<T>(this ISession session, string key)
        {
            var value = session.GetString(key);
            return value == null ? default : JsonConvert.DeserializeObject<T>(value);
        }
    }
}
