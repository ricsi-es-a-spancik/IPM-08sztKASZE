using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Windows.Input;
using System.Collections.ObjectModel;

namespace DomainModel.View
{
    interface FourInARowViewModel : INotifyPropertyChanged
    {
        ICommand BackCommand { get; }

        ObservableCollection<IFourInARowField> Fields { get; }

        ObservableCollection<IFourInARowInput> Inputs { get; }

        Int32 Size { get; }

        String StatusText { get; }
    }
}
