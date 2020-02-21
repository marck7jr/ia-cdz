using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace CDZ.Common.Helpers
{
    public static class GenericHelper
    {
        public static T Clone<T>(this T obj)
        {
            using (var ms = new MemoryStream())
            {
                var formatter = new BinaryFormatter();
                formatter.Serialize(ms, obj);
                ms.Position = 0;

                return (T)formatter.Deserialize(ms);
            }
        }
    }
}
