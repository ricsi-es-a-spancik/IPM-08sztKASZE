using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;


using DomainModel.Model;
using DomainModel.Model.Player;
using DomainModel.Model.AI;
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
            protected set;
        }
        public Player P2
        {
            get;
            protected set;
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
            P1 = new Human();
            P2 = new Human();
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

        public void setAI(IArtificalIntelligence AI)
        {
            P2 = new Computer(AI);
        }

        public void removeAI()
        {
            P2 = new Human();
        }
    }
}
