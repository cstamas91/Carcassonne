using CarcassonneSharedModules.Representation;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarcassonneSharedModules.Tools
{
    /// <summary>
    /// A hálózati üzenet létrehozásáért felelős.
    /// </summary>
    public class PayloadContentHandler
    {
        private readonly IGameModel model;
        public PayloadContentHandler(IGameModel model)
        {
            this.model = model;
        }

        public byte[] GetPayload()
        {
            using (var contentStream = new MemoryStream())
            {
                model.State.WriteContent(contentStream);

                return contentStream.ToArray();
            }
        }
    }   
}
