using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace Ecommerce.Abstractions.Helper
{
    public static class SessionHelper
    {
        public static void SetObjectAsJson(this ISession session,string key,object value)
        {
            session.SetString(key, JsonConvert.SerializeObject(value, Formatting.None,
                        new JsonSerializerSettings()
                        {
                            ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                        }));
          
           

        }

        public static T GetObjectFromJson<T> (this ISession session , String key)
        {
            var value = session.GetString(key);
            return value == null ? default : JsonConvert.DeserializeObject<T>(value);
        }
    }
}
