﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Windows.Input;

namespace DomainModel.View
{
    interface IFourInARowField : INotifyPropertyChanged
    {
        String Player { get; }
    }

    interface IFourInARowInput : INotifyPropertyChanged
    {
        ICommand InputCommand { get; }
    }
}
