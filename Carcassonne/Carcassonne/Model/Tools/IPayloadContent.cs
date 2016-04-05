using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Carcassonne.Model.Tools
{
    /// <summary>
    /// Ez az interfész byte tömbbe, és abból való konverzióra szolgáló eljárásokat szab meg.
    /// </summary>
    public interface IPayloadContent
    {
        void ReadContent(byte[] payloadContent);
        void WriteContent(Stream contentStream);
    }
}
