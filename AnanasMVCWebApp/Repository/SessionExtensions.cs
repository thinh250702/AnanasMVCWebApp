using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Diagnostics;

namespace AnanasMVCWebApp.Repository {
    public static class SessionExtensions {
        public static void SetJson(this ISession session, string key, object value) {
            Debug.WriteLine("From Session: " + JsonConvert.SerializeObject(value));
            session.SetString(key, JsonConvert.SerializeObject(value));
        }
        public static T GetJson<T>(this ISession session, string key) {
            var sessionData = session.GetString(key);
            return sessionData == null ? default(T) : JsonConvert.DeserializeObject<T>(sessionData);
        }
    }
}
