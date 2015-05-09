using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using DomainModel.View;

namespace TicTacToeViewModel
{
    public class TicTacToeField : ITicTacToeField
    {
        /// <summary>
        /// Represents which of the playeres has marked the current field (empty if none of them).
        /// </summary>
        private String _player;

        /// <summary>
        /// Gets or sets the player.
        /// </summary>
        public String Player
        {
            get { return _player; }
            set
            {
                if (_player != value)
                {
                    _player = value;
                    OnPropertyChanged();
                }
            }
        }

        /// <summary>
        /// Gets or sets the current field's column index.
        /// </summary>
        public Int32 Col { get; set; }

        /// <summary>
        /// Gets or sets the current field's row index.
        /// </summary>
        public Int32 Row { get; set; }

        /// <summary>
        /// Gets or sets the command which changes the state of the field.
        /// </summary>
        public ICommand FieldChangeCommand { get; set; }

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

        /// <summary>
        /// Emited when a property is changed.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;
    }
}
