using Carcassonne.Model.Representation;
using Carcassonne.Model.Tools;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarcassonneTester
{
    class Program
    {
        static void Main(string[] args)
        {
            List<Player> ls = new List<Player>() { new Player(0, "Tomi"), new Player(1, "pwnzor") };
            InitContent ic = new InitContent(ls);
            using (var stream = new MemoryStream())
            {
                ic.WriteContent(stream);

                var readIC = PayloadContentFactory<InitContent>.Create(stream.ToArray());
                foreach (var playa in readIC.Players)
                    Console.WriteLine(playa);

                Console.ReadKey();
            }
        }
    }
}

