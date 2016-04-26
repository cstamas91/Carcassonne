using System;
using System.IO;
using CarcassonneSharedModules.Tools;

namespace CarcassonneServer.Model.Representation
{
    public abstract class BaseElement : IPayloadContent
    {
        virtual public Position Position { get; set; }


        virtual public void ReadContent(byte[] payloadContent)
        {
        }

        virtual public void WriteContent(Stream contentStream)
        {
        }
    }
}
