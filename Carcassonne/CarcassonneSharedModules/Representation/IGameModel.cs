using CarcassonneSharedModules.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace CarcassonneSharedModules.Representation
{
    public interface IGameModel
    {
        GameTable GameTable { get; }
        ScoreHandler ScoreHandler { get; }
        IEnumerable<Player> Players { get; }
        GameStateDescriptor State { get; }
    }

}
