using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

using Core.src.InterFaces;
using Core.src.Utils;
using DomainModel.Model;

using TicTacToeGameApp;


using System.Collections.ObjectModel;

namespace Core.src
{
    class CoreViewModel : BaseViewModel
    {
        protected CoreModel model;

        public event EventHandler<IGameApp> LaunchGame;

        public DelegateCommand LaunchGameCommand
        {
            get;
            protected set;
        }

        public ObservableCollection<GameAppWrapper> GameList
        {
            get
            {
                return model.Games;
            }
        }

        public Int32 GameNum
        {
            get
            {
                return model.GameNum;
            }
        }

        public CoreViewModel()
        {
            model = new CoreModel();

            model.LaunchGame += onGameLaunch;

            // Register Game applications

            model.register(new TicTacToeGameApp.TicTacToeGameApp(), "TicTacToe");

        }

        private void onGameLaunch(object sender,Int32 param)
        {
            Int32 Id = param;
            GameAppWrapper current = GameList[(int)Id];

            current.P1 = model.P1;
            current.P2 = model.P2;

            LaunchGame(this, current);
        }
    }
}
