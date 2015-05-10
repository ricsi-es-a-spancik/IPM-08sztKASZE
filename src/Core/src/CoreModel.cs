using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;

using Core.src.InterFaces;
using DomainModel.Model;
using DomainModel.Model.Player;
using Core.src.Utils;

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

        public event EventHandler<Int32> LaunchGame;

        public CoreModel()
        {
            registeredGames = 0;
            Games = new ObservableCollection<GameAppWrapper>();
        }

        public void register(IGameApp game, String GameName)
        {
            GameAppWrapper newGameWrapper = new GameAppWrapper(game, registeredGames, GameName);
            newGameWrapper.LaunchGameCommand = new DelegateCommand(
                param => { LaunchGame(this,(Int32)param);
            });
            ++registeredGames;
            Games.Add(newGameWrapper);
        }
    }
}
