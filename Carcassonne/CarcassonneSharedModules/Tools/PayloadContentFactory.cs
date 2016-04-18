using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarcassonneSharedModules.Tools
{
    /// <summary>
    /// Generikus Factory osztály ami IPayloadContentet implementáló osztályok példányának létrehozására szolgál.
    /// A kliens a hálózati üzenetben kapott byte tömbből előállítja a játék állapotának leíróját, ehhez készítünk példányokat.
    /// </summary>
    /// <typeparam name="T">Üres ktrorral rendelkező IPayloadContentet implementáló osztály.</typeparam>
    public static class PayloadContentFactory<T>
        where T : IPayloadContent, new()
    {
        /// <summary>
        /// Statikus metódus egy IPayloadContentet implementáló osztály példányának létrehozására a kapott byte array alapján.
        /// </summary>
        /// <param name="payloadContent">Byte array, ami a készítendő példány értékeit tartalmazza.</param>
        /// <returns>Generikus paraméter típus egy példánya.</returns>
        public static T Create(byte[] payloadContent)
        {
            T item = new T();
            item.ReadContent(payloadContent);

            return item;
        }
    }
}
