using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Carcassonne.Model.Tools
{
    public static class PayloadWriter<T>
        where T : IPayloadContent<T>
    {
        public static byte[] Write(T content)
        {
            using (var sw = new StreamWriter(new MemoryStream()))
            {
                sw.Write(content.WriteContent());

                var payloadArray = new byte[sw.BaseStream.Length];
                (sw.BaseStream as MemoryStream).WriteTo(new MemoryStream(payloadArray));
                return payloadArray;
            }
        }
    }
}
