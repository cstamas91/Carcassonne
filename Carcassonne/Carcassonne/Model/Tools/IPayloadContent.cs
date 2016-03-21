using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Carcassonne.Model.Tools
{
    public interface IPayloadContent<T>
    {
        T ReadContent();
        byte[] WriteContent();
    }
}
