using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using TicTacToeModel;
using TicTacToeViewModel;
using DomainModel.Model.Player;
using DomainModel.Model.TicTacToe;
using AI;

namespace TicTacToeTest
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private TicTacToeModel.TicTacToeModel _model;
        private TicTacToeViewModel.TicTacToeViewModel _viewModel;
        private TicTacToeView2.TicTacToeWindow _window;

        /// <summary>
        /// Alkalmazás példányosítása.
        /// </summary>
        public App()
        {
            Startup += new StartupEventHandler(App_Startup);
        }

        /// <summary>
        /// Alkalmazás indulásának eseménykezelője.
        /// </summary>
        private void App_Startup(object sender, StartupEventArgs e)
        {
            _model = new TicTacToeModel.TicTacToeModel();
            _model.Initialize(new Human(), new Computer(new AlphaBetaAI()));
            _model.GameOver += new EventHandler<GameOverEventArgs>(Model_GameOver);

            _viewModel = new TicTacToeViewModel.TicTacToeViewModel();
            _viewModel.Initialize(_model);

            _model.NewGame();

            _window = new TicTacToeView2.TicTacToeWindow();
            _window.DataContext = _viewModel;
            _window.Show();
        }

        #region Model event handlers

        /// <summary>
        /// Játék megnyerésének eseménykezelése.
        /// </summary>
        private void Model_GameOver(object sender, GameOverEventArgs e)
        {
            if(e.Winner == _model.PlayerOne)
                MessageBox.Show("A kereszt játékos győzött!", "Játék vége!", MessageBoxButton.OK, MessageBoxImage.Asterisk);
            else if (e.Winner == _model.PlayerTwo)
                MessageBox.Show("A kör játékos győzött!", "Játék vége!", MessageBoxButton.OK, MessageBoxImage.Asterisk);
            else 
                MessageBox.Show("A játék döntetlen!", "Játék vége!", MessageBoxButton.OK, MessageBoxImage.Asterisk);

            _model.NewGame();
        }

        #endregion
    }
}
