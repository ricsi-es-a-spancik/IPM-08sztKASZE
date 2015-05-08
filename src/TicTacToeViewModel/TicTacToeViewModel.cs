using System;
using System.Linq;
using System.Windows.Input;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Runtime.CompilerServices;
using DomainModel.View;
using DomainModel.Model.Player;
using DomainModel.Model.TicTacToe;

namespace TicTacToeViewModel
{
    public class TicTacToeViewModel : ITicTacToeViewModel
    {

        /// <summary>
        /// Command for ending the current game.
        /// </summary>
        public ICommand BackCommand { get; private set; }

        /// <summary>
        /// Viewmodel for the gameboard.
        /// </summary>
        public ObservableCollection<ITicTacToeField> Fields { get; private set; }

        /// <summary>
        /// Size of the board.
        /// </summary>
        public int Size { get { return 3; } }

        public string StatusText
        {
            get { throw new NotImplementedException(); }
        }

        /// <summary>
        /// Emited when a property is changed.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Game logic.
        /// </summary>
        private ITicTacToeModel _model;

        /// <summary>
        /// Initializes the current instance.
        /// </summary>
        /// <param name="m">Game logic.</param>
        public void Initialize(ITicTacToeModel m)
        {
            _model = m;
            _model.GameStarted += new EventHandler(Model_GameStarted);
            _model.FieldChanged += new EventHandler<FieldChangedEventArgs>(Model_FieldChanged);
            _model.GameOver += new EventHandler<GameOverEventArgs>(Model_GameOver);

            BackCommand = new DelegateCommand(param => OnGameExit());

            Fields = new ObservableCollection<ITicTacToeField>();

            _model.NewGame();
        }

        #region Private methods

        /// <summary>
        /// Initializes the viewmodel.
        /// </summary>
        private void InitBoard()
        {
            Fields.Clear();

            for (Int32 x = 0; x < 3; x++)
            {
                for (Int32 y = 0; y < 3; y++)
                {
                    Fields.Add(new TicTacToeField
                    {
                        Player = PlayerToField(_model[x, y]),
                        Col = x,
                        Row = y,
                        FieldChangedCommand = new DelegateCommand(param =>
                        {
                            _model.StepGame((param as TicTacToeField).Col, (param as TicTacToeField).Row);
                        })
                    });
                }
            }
        }

        /// <summary>
        /// Representing the player as a string.
        /// </summary>
        /// <param name="player">Player.</param>
        /// <returns>Appropriate string representation of the player.</returns>
        private String PlayerToField(Player player)
        {
            if (player == _model.PlayerOne)
                return "X";
            else if (player == _model.PlayerTwo)
                return "O";
            else
                return string.Empty;
        }

        #endregion

        #region Model event handlers

        /// <summary>
        /// Event handler for game starting.
        /// </summary>
        private void Model_GameStarted(object sender, EventArgs e)
        {
            InitBoard();
        }

        /// <summary>
        /// Event handler for field change event.
        /// </summary>
        private void Model_FieldChanged(object sender, FieldChangedEventArgs e)
        {
            Fields.FirstOrDefault(f => 
                { var field = f as TicTacToeField; return field.Col == e.Col && field.Row == e.Row;})
                .Player = PlayerToField(_model[e.Col, e.Row]);
            // lineáris keresés a megadott sorra, oszlopra, majd a játékos átírása
        }

        /// <summary>
        /// Event handler for game over event.
        /// </summary>
        private void Model_GameOver(object sender, GameOverEventArgs e)
        {
            throw new NotImplementedException();
        }

        #endregion

        #region Event methods

        /// <summary>
        /// 
        /// </summary>
        private void OnGameExit()
        {
            throw new NotImplementedException();
        }

        #endregion

        /// <summary>
        /// Emits property change event.
        /// </summary>
        /// <param name="propertyName">The name of the changed property.</param>
        protected virtual void OnPropertyChanged([CallerMemberName] String propertyName = null)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
