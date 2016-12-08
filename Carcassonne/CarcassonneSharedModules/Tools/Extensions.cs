using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarcassonneSharedModules.Tools
{
    /// <summary>
    /// Kiegészítő segédfüggvények osztálya.
    /// </summary>
    public static class Extensions
    {
        public static IEnumerable<T> GetMemberEnumeration<T>()
            where T : struct
        {
            return Enum.GetValues(typeof(T)) as IEnumerable<T>;
        }

        public static T GetNext<T>(this T item)
            where T : struct
        {
            if (!item.GetType().IsEnum)
                throw new ArgumentException("Az item változónak Enum típusúnak kell lennie");

            var values = (T[])Enum.GetValues(item.GetType());
            int indexOfItem = Array.IndexOf(values, item);
            return values.Length == indexOfItem + 1 ? values[0] : values[indexOfItem + 1];
        }
        /// <summary>
        /// Short típusú érték Stream-be való írásának elkülönítése.
        /// </summary>
        /// <param name="stream">Stream adatfolyam, amibe az értéket írni akarjuk.</param>
        /// <param name="content">Short típusú adat, amit a Stream-be írni akaunk.</param>
        public static void WriteShort(this Stream stream, short content)
        {
            var byteRep = BitConverter.GetBytes(content);
            stream.Write(byteRep, 0, byteRep.Length);
        }
        /// <summary>
        /// String típusú érték Streambe való írásának elkülönítése.
        /// </summary>
        /// <param name="stream">Stream adatfolyam, amibe az értéket írni akarjuk.</param>
        /// <param name="content">Sring típusú adat, amit a Stream-be írni akaunk.</param>
        public static void WriteString(this Stream stream, string content)
        {
            using (var streamWriter = new StreamWriter(stream, Encoding.Default, 4, true))
                streamWriter.Write(string.Format("{0}\n", content));
        }

        public static byte[] ReadToChar(this Stream stream, char ch, int num = 1)
        {
            var str = string.Empty;

            var ms = (stream as MemoryStream);
            using (var stringStream = new MemoryStream())
            {
                var buff = new byte[2];
                ms.Read(buff, 0, 1);
                var c = BitConverter.ToChar(buff, 0);
                int i = 0;

                while (i < num)
                {
                    stringStream.Write(buff, 0, 1);
                    ms.Read(buff, 0, 1);
                    c = BitConverter.ToChar(buff, 0);

                    if (c == ch)
                        i++;
                }

                return stringStream.ToArray();
            }
        }
    }
}
