using System;
using System.Linq;
using System.Text;
using System.Windows.Input;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using DomainModel.View;

namespace TicTacToeViewModel
{
    public class TicTacToeViewModel : ITicTacToeViewModel
    {

        public ICommand BackCommand
        {
            get { throw new NotImplementedException(); }
        }

        public ObservableCollection<ITicTacToeField> Fields
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

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
