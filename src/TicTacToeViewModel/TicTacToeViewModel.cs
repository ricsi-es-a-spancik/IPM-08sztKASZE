using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DomainModel.View;

namespace TicTacToeViewModel
{
    public class TicTacToeViewModel : ITicTacToeViewModel
    {

        public System.Windows.Input.ICommand BackCommand
        {
            get { throw new NotImplementedException(); }
        }

        public System.Collections.ObjectModel.ObservableCollection<ITicTacToeField> Fields
        {
            get { throw new NotImplementedException(); }
        }

        public int Size
        {
            get { throw new NotImplementedException(); }
        }

        public string StatusText
        {
            get { throw new NotImplementedException(); }
        }

        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
    }
}
