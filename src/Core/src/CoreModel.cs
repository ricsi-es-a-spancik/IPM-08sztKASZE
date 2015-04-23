using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;

using Core.src.InterFaces;
using Core.src.Utils;
using Core.src.Structures;

namespace Core.src
{
    class CoreModel : ICore
    {
        protected Int32 registeredGames;
        public ObservableCollection<GameAppWrapper> Games
        {
            get;
            protected set;
        }

        public Player P1
        {
            get;
            set;
        }
        public Player P2
        {
            get;
            set;
        }

        public Int32 GameNum
        {
            get
            {
                return registeredGames;
            }
        }

        public CoreModel()
        {
            registeredGames = 0;
            Games = new ObservableCollection<GameAppWrapper>();
        }

        public void register(IGameApp game)
        {
            GameAppWrapper newGameWrapper = new GameAppWrapper(game, registeredGames);
            ++registeredGames;
            Games.Add(newGameWrapper);
        }
    }
}
