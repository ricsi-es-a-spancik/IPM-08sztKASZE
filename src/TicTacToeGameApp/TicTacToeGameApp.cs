using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using DomainModel.Model;
using DomainModel.Model.Player;
using DomainModel.Model.TicTacToe;
using DomainModel.View;

namespace TicTacToeGameApp
{
    public class TicTacToeGameApp : IGameApp
    {
        public event EventHandler GameExit;

        private ITicTacToeModel _model;
        private ITicTacToeViewModel _viewModel;
        private Window _window;

        public void Initialize(Player p1, Player p2)
        {
            _model = Activator.CreateInstanceFrom("TicTacToeModel.dll", "TicTacToeModel.TicTacToeModel")
                .Unwrap() as ITicTacToeModel;
            _model.Initialize(new Human(), new Human());
            _model.GameOver += new EventHandler<GameOverEventArgs>(Model_GameOver);

            _viewModel = Activator.CreateInstanceFrom("TicTacToeViewModel.dll", "TicTacToeViewModel.TicTacToeViewModel")
                .Unwrap() as ITicTacToeViewModel;
            _viewModel.Initialize(_model);
            //_viewModel.BackCommand = new DelegateCommand(p => ViewModel_GameExit());

            _model.NewGame();

            _window = Activator.CreateInstanceFrom("TicTacToeView.dll", "TicTacToeView.TicTacToeWindow").Unwrap() as Window;
            _window.DataContext = _viewModel;
        }

        public void OpenWindow()
        {
            if(_window != null)
                _window.Show();
        }

        public void CloseWindow()
        {
            if (_window != null)
                _window.Close();
        }

        private void Model_GameOver(object sender, GameOverEventArgs e)
        {
            if (e.Winner == _model.PlayerOne)
                MessageBox.Show("A kereszt játékos győzött!", "Játék vége!", MessageBoxButton.OK, MessageBoxImage.Asterisk);
            else if (e.Winner == _model.PlayerTwo)
                MessageBox.Show("A kör játékos győzött!", "Játék vége!", MessageBoxButton.OK, MessageBoxImage.Asterisk);
            else
                MessageBox.Show("A játék döntetlen!", "Játék vége!", MessageBoxButton.OK, MessageBoxImage.Asterisk);

            _model.NewGame();
        }

        private void ViewModel_GameExit()
        {
            if (GameExit != null)
                GameExit(this, null);
        }
    }
}
