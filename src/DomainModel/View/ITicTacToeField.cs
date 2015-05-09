using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Windows.Input;

namespace DomainModel.View
{
    public interface ITicTacToeField : INotifyPropertyChanged
    {
        String Player { get; set; }

        ICommand FieldChangeCommand { get; set; }
    }
}
