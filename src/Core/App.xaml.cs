using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using Core.src;
using Core.src.Utils;
using DomainModel.Contracts;
using Microsoft.Practices.Unity;
using Microsoft.Practices.Unity.Configuration;
using DomainModel;


namespace Core
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private MainWindow _window;
        private CoreViewModel _viewmodel;

        private GameAppWrapper _currentGame;

        private IScoreStore _store;

        public App() 
        {
 
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            using (var container = new UnityContainer().LoadConfiguration() )
            {
                this._store = container.Resolve<IScoreStore>();
            }

            _viewmodel = new CoreViewModel();
            _window = new MainWindow();

            _viewmodel.LaunchGame += LaunchNewGame;

            _window.DataContext = _viewmodel;
            _window.Show();
        }

        private void LaunchNewGame(object sender, src.InterFaces.IGameApp e)
        {
            GameAppWrapper wrap = (GameAppWrapper)e;
            if (this._currentGame == null)
            {
                _window.Close();
                this._currentGame = wrap;
                this._currentGame.GameOver += GameOver;
                wrap.Initialize();
            }
            else
            {
                throw new InvalidOperationException("A game is still running!");
            }
        }

        private void GameOver(object sender, EventArgs e)
        {
            this._currentGame.GameOver -= GameOver;

            LeaderboardEntry board = new LeaderboardEntry{
                UserName = "test",
                GameType = GameTypes.TicTacToe,
                Date = DateTime.Now,
                GameSessionId = "session1",
                Score = (int)this._currentGame.P1.Score
            };

            _store.AddLeaderboardEntryAsync(board);

            this._currentGame = null;

            _window.Show();
        }
    }
}
