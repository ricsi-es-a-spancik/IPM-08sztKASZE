using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Windows.Input;
using System.Collections.ObjectModel;
using DomainModel.Model.TicTacToe;

namespace DomainModel.View
{
    public interface ITicTacToeViewModel : INotifyPropertyChanged
    {
        void Initialize(ITicTacToeModel m);

        ICommand BackCommand { get; set; }

        ObservableCollection<ITicTacToeField> Fields { get; }

        Int32 Size { get; }

        String StatusText { get; }
    }
}
