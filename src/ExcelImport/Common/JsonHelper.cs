using Newtonsoft.Json;

namespace WebMain.Common
{
    public class JsonHelper
    {
        /// <summary>
        /// obj转json
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static string ToJsonStr<T>(T obj) where T : class,new()
        {
            return JsonConvert.SerializeObject(obj, typeof(T), Formatting.Indented, null);
        }

        /// <summary>
        /// obj转json
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static string ToJsonStr(object obj)
        {
            return JsonConvert.SerializeObject(obj);
        }

        /// <summary>
        /// json转obj
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static T ToObj<T>(string str) where T : class,new()
        {
            return JsonConvert.DeserializeObject(str, typeof(T)) as T;
        }
    }
}