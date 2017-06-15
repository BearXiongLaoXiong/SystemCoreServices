using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace System.Framework.Common
{
    public static class JsonHelper
    {
        static JsonHelper()
        {
            var datetimeConverter = new IsoDateTimeConverter { DateTimeFormat = "yyyy-MM-dd HH:mm:ss" };

            var jsonSettings = new JsonSerializerSettings
            {
                MissingMemberHandling = Newtonsoft.Json.MissingMemberHandling.Ignore,
                NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore,
                ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
            };
            jsonSettings.Converters.Add(datetimeConverter);
        }

        /// <summary>
        /// 将指定的对象序列化成 JSON 数据。
        /// </summary>
        /// <param name="obj">要序列化的对象。</param>
        /// <returns></returns>
        public static string ToJson(this object obj)
        {
            return JsonConvert.SerializeObject(obj);
            //return JsonConvert.SerializeObject(obj, Formatting.None, JsonSettings);
        }

        /// <summary>
        /// 将指定的 JSON 数据反序列化成指定对象。
        /// </summary>
        /// <typeparam name="T">对象类型。</typeparam>
        /// <param name="json">JSON 数据。</param>
        /// <returns></returns>
        public static T FromJson<T>(this string json)
        {
            return JsonConvert.DeserializeObject<T>(json);
            //return JsonConvert.DeserializeObject<T>(json, JsonSettings);
        }


        /// <summary>
        /// 将指定的文件对象序列化成 JSON 数据。
        /// </summary>
        /// <param name="obj">要序列化的对象。</param>
        /// <param name="file">文件fullName</param>
        /// <returns></returns>
        public static void ToJson(this object obj, string file)
        {
            using (var fs = File.Open(file, FileMode.Create, FileAccess.Read))
            using (var sw = new StreamWriter(fs))
            using (var jw = new JsonTextWriter(sw))
            {
                new JsonSerializer().Serialize(jw, obj);
            }
        }

        /// <summary>
        /// 将指定的 JSON 数据反序列化成指定对象。
        /// </summary>
        /// <typeparam name="T">对象类型。</typeparam>
        /// <param name="json">JSON 数据。</param>
        /// <param name="file">文件fullName</param>
        /// <returns></returns>
        public static T FromJson<T>(this string json, string file)
        {
            using (var fs = File.Open(file, FileMode.Create, FileAccess.Read))
            using (var sw = new StreamReader(fs))
            using (var jr = new JsonTextReader(sw))
            {
               return new JsonSerializer().Deserialize<T>(jr);
            }
        }
    }
}
