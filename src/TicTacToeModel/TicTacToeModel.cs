using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DomainModel.Model.AI;
using DomainModel.Model.Player;
using DomainModel.Model.TicTacToe;


namespace TicTacToeModel
{
    public class TicTacToeModel : ITicTacToeModel
    {
        #region Private fields

        private Player currentPlayer;

        private Player[,] gameTable; 
        private int stepNumber;

        #endregion

        #region Public properties

        /// <summary>
        /// Gets the number of steps of the current game.
        /// </summary>
        public int StepNumber { get { return stepNumber; } }

        /// <summary>
        /// Gets the player who is on turn.
        /// </summary>
        public Player CurrentPlayer { get { return currentPlayer; } }

        public Player PlayerOne { get; private set; }
        public Player PlayerTwo { get; private set; }

        /// <summary>
        /// Gets the given field's value.
        /// </summary>
        /// <param name="col">Column index.</param>
        /// <param name="row">Row index.</param>
        /// <returns>The player whose mark is on the given field.</returns>
        public Player this[int col, int row]
        {
            get
            {
                if (col < 0 || col > gameTable.GetLength(0))
                    throw new ArgumentException("Bad column index.", "x");
                if (row < 0 || row > gameTable.GetLength(1))
                    throw new ArgumentException("Bad row index.", "y");

                return gameTable[col, row];
            }
        }

        #endregion

        #region Events

        /// <summary>
        ///  Emited when the game has started.
        /// </summary>
        public event EventHandler GameStarted;

        /// <summary>
        /// Emited when the game is over.
        /// </summary>
        public event EventHandler<GameOverEventArgs> GameOver;

        /// <summary>
        /// Emited when a field of the board is changed.
        /// </summary>
        public event EventHandler<FieldChangedEventArgs> FieldChanged;


        #endregion

        #region Public methods

        /// <summary>
        /// Initializes the TicTacToe model instance.
        /// </summary>
        /// <param name="p1">First player.</param>
        /// <param name="p2">Second player.</param>
        public void Initialize(Player p1, Player p2)
        {
            PlayerOne = p1;
            PlayerTwo = p2;

            InitializeComputerPlayer(PlayerOne);
            InitializeComputerPlayer(PlayerTwo);
            
            gameTable = new Player[3, 3];
        }

        /// <summary>
        /// Checks whether the given player is a Computer player an initializes the AI logic for it.
        /// </summary>
        /// <param name="player">The player to be checked.</param>
        private void InitializeComputerPlayer(Player player)
        {
            if(player is Computer)
            {
                Computer cPlayer = player as Computer;
                cPlayer.AI.setChildrenFunction(ChildrenFunction);
                cPlayer.AI.setEvaluationFunction(EvaluationFunction);
            }
        }

        /// <summary>
        /// Starts a new game
        /// </summary>
        public void NewGame()
        {
            for (int col = 0; col < gameTable.GetLength(0); ++col)
                for (int row = 0; row < gameTable.GetLength(1); ++row)
                {
                    gameTable[col, row] = null; //default value for all fields
                }

            stepNumber = 0;
            currentPlayer = PlayerOne; //first player starts

            OnGameStarted();
        }

        /// <summary>
        /// Steps the game with marking the given field by the current player.
        /// </summary>
        /// <param name="col">Column index.</param>
        /// <param name="row">Row index.</param>
        public void StepGame(int col, int row)
        {
            if (col < 0 || col > gameTable.GetLength(0))
                throw new ArgumentOutOfRangeException("x", "Bad column index.");
            if (row < 0 || row > gameTable.GetLength(1))
                throw new ArgumentOutOfRangeException("y", "Bad row index.");
            if (stepNumber >= 9)
                throw new InvalidOperationException("Game is over!");
            if (gameTable[col, row] != null)
                return;
                //throw new InvalidOperationException("Field is not empty!");

            gameTable[col, row] = currentPlayer; //mark selected field
            OnFieldChanged(col, row, currentPlayer); //emit field change event

            ++stepNumber;

            currentPlayer = currentPlayer == PlayerOne ? PlayerTwo : PlayerOne; //next player

            CheckGame();
        }

        #endregion

        #region Private methods

        /// <summary>
        /// Checking game state.
        /// </summary>
        private void CheckGame()
        {
            //finding the winner
            Player winner = CheckWinner();
            
            if (winner != null) // no winner
            {
                ++winner.Score;
                OnGameOver(winner); // emit game over event with winner player param
            }
            else if (stepNumber == 9) // it's a draft
            {
                OnGameOver(null); // emit game over event with no winnner
            }
        }

        /// <summary>
        /// Checks the game state and returns the player who won the game if it exists.
        /// </summary>
        private Player CheckWinner()
        {
            Player winner = null;

            for (int i = 0; i < 3; ++i)
            {
                if (gameTable[i, 0] != null && gameTable[i, 0] == gameTable[i, 1] && gameTable[i, 1] == gameTable[i, 2])
                    winner = gameTable[i, 0];
            }
            for (int i = 0; i < 3; ++i)
            {
                if (gameTable[0, i] != null && gameTable[0, i] == gameTable[1, i] && gameTable[1, i] == gameTable[2, i])
                    winner = gameTable[0, i];
            }
            if (gameTable[0, 0] != null && gameTable[0, 0] == gameTable[1, 1] && gameTable[1, 1] == gameTable[2, 2])
                winner = gameTable[0, 0];
            if (gameTable[0, 2] != null && gameTable[0, 2] == gameTable[1, 1] && gameTable[1, 1] == gameTable[2, 0])
                winner = gameTable[0, 2];

            return winner;
        }

        #endregion

        #region Event triggers

        /// <summary>
        /// Emits game start event.
        /// </summary>
        private void OnGameStarted()
        {
            if (GameStarted != null)
                GameStarted(this, EventArgs.Empty);
        }

        /// <summary>
        /// Emits game over event.
        /// </summary>
        private void OnGameOver(Player player)
        {
            if (GameOver != null)
                GameOver(this, new GameOverEventArgs(player));
        }

        /// <summary>
        /// Emits field changed event.
        /// </summary>
        /// <param name="col">Column index.</param>
        /// <param name="row">Row index.</param>
        /// <param name="player">The player that marked the field.</param>
        private void OnFieldChanged(int col, int row, Player player)
        {
            if (FieldChanged != null)
                FieldChanged(this, new FieldChangedEventArgs(col, row, player));
        }

        #endregion

        /// <summary>
        /// Game logic state type used by AI component.
        /// </summary>
        private class State : IState 
        {
            /// <summary>
            /// Initializes a new instance.
            /// </summary>
            public State() 
            { 
                GameTable = new Player[3, 3]; 
            }

            /// <summary>
            /// Initializes a new instance.
            /// </summary>
            /// <param name="gameTable">The current gametable.</param>
            /// <param name="currentPlayer">The player who is on turn.</param>
            public State(Player[,] gameTable, Player currentPlayer) 
            { 
                GameTable = gameTable;
                CurrentPlayer = currentPlayer;
            }

            /// <summary>
            /// Current gametable.
            /// </summary>
            public Player[,] GameTable { get; set; }

            /// <summary>
            /// The player who is on turn.
            /// </summary>
            public Player CurrentPlayer { get; set; }
        }

        /// <summary>
        /// Game logic step type used by AI component.
        /// </summary>
        private class Step : IStep 
        { 
            /// <summary>
            /// Initializes a new instance.
            /// </summary>
            /// <param name="col">Column of the field to be marked.</param>
            /// <param name="row">Row of the field to be marked.</param>
            public Step(int col, int row)
            {
                Col = col;
                Row = row;
            }

            /// <summary>
            /// Column of the field to be marked.
            /// </summary>
            public int Col { get; set; } 
            
            /// <summary>
            /// Row of the field to be marked.
            /// </summary>
            public int Row { get; set; } 
        }

        /// <summary>
        /// Generates the next possible states. It is used by the AI logic.
        /// </summary>
        /// <param name="abstractState">The state whose children should be generated.</param>
        /// <returns>The list of the children.</returns>
        private List<Tuple<IState, IStep>> ChildrenFunction(IState abstractState)
        {
            List<Tuple<IState, IStep>> ret = new List<Tuple<IState, IStep>>();
            State state = abstractState as State;
            for (int col = 0; col < 3; ++col)
            {
                for (int row = 0; row < 3; ++row)
                {
                    if (state.GameTable[col,row] == null)
                    {
                        State newState = new State();
                        Array.Copy(state.GameTable, newState.GameTable, state.GameTable.Length);
                        newState.GameTable[col, row] = state.CurrentPlayer;
                        newState.CurrentPlayer = state.CurrentPlayer;
                        ret.Add(new Tuple<IState, IStep>(newState, new Step(col, row)));
                    }
                }
            }

            return ret;
        }

        /// <summary>
        /// Evaluates the quality of the given state. It is used by the AI logic.
        /// </summary>
        /// <param name="abstractState">The state whose quality should be evaluated.</param>
        /// <returns>An integer value indicating the quality of the state.</returns>
        private int EvaluationFunction(IState abstractState)
        {
            State state = abstractState as State;
            if(state.CurrentPlayer == CheckWinner())
                return 10;
            else if(CheckWinner() == null)
                return 0;
            else
                return -10;
        }
    }
}
