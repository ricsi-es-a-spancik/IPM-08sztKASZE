using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Collections.ObjectModel;
using Core.src;
using Core.src.ViewModels;
using Core.src.Utils;
using DomainModel.Contracts;
using Microsoft.Practices.Unity;
using Microsoft.Practices.Unity.Configuration;
using DomainModel;
using DomainModel.Model;


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
        private ScoreWindow _score;
        private ScoreViewModel _scorevm;

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
            _viewmodel.showScoreEvent += showScore;

            _window.DataContext = _viewmodel;
            _window.Show();
        }

        private async void showScore(object sender, Int32 e)
        {
            _score = new ScoreWindow();

            IEnumerable<LeaderboardEntry> entries = new List<LeaderboardEntry>();

            switch (e)
            {
                case 1:
                    entries = await _store.GetLeaderboardAsync(GameTypes.TicTacToe, Scopes.AllTime);
                    break;
                case 2:
                    entries = await _store.GetLeaderboardAsync(GameTypes.NineMensMorris, Scopes.AllTime);
                    break;
                case 3:
                    entries = await _store.GetLeaderboardAsync(GameTypes.ConnectFour, Scopes.AllTime);
                    break;
            }

            _scorevm = new ScoreViewModel(entries);
            _scorevm.BackToMenu += showMainWindow;

            _score.DataContext = _scorevm;
            _score.Show();

            _window.Hide();
        }

        private void showMainWindow(object sender, EventArgs e)
        {
            ScoreViewModel snd = (ScoreViewModel)sender;
            snd.BackToMenu -= showMainWindow;
            _score.Close();
            _window.Show();
        }

        private void LaunchNewGame(object sender, IGameApp e)
        {
            GameAppWrapper wrap = (GameAppWrapper)e;
            if (this._currentGame == null)
            {
                _window.Hide();
                this._currentGame = wrap;
                this._currentGame.GameExit += GameExit;
                wrap.Initialize();
                wrap.OpenWindow();
            }
            else
            {
                throw new InvalidOperationException("A game is still running!");
            }
        }

        private void GameExit(object sender, EventArgs e)
        {
            this._currentGame.GameExit -= GameExit;

            InputDialog dia = new InputDialog();
            InputDialogViewModel diavm = new InputDialogViewModel(dia);
            dia.DataContext = diavm;
            diavm.CloseDialog += CloseDialog;

            dia.ShowDialog();

            if (diavm.result == true)
            {
                String name = dia.Input.Text;
                if ((int)this._currentGame.P1.Score > (int)this._currentGame.P2.Score)
                {
                    LeaderboardEntry board = new LeaderboardEntry();
                    board.UserName = name;
                    board.GameType = GameTypes.TicTacToe;
                    board.Date = DateTime.Now;
                    board.GameSessionId = "session1";
                    board.Score = (int)this._currentGame.P1.Score;

                    _store.AddLeaderboardEntryAsync(board);
                }
                else
                {
                    LeaderboardEntry board = new LeaderboardEntry();
                    board.UserName = name;
                    board.GameType = GameTypes.TicTacToe;
                    board.Date = DateTime.Now;
                    board.GameSessionId = "session1";
                    board.Score = (int)this._currentGame.P2.Score;

                    _store.AddLeaderboardEntryAsync(board);
                }
            }

            this._currentGame.CloseWindow();

            this._currentGame = null;

            _window.Show();
        }

        private void CloseDialog(object sender, EventArgs e)
        {
            ((InputDialogViewModel)sender).dia.Close();
        }
    }
}
