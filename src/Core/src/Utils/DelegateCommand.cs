using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Core.src.Utils
{
    class DelegateCommand : ICommand
    {
        private readonly Action<Object> _execute;
        private readonly Func<Object, Boolean> _canExecute;

        public DelegateCommand(Action<Object> exec) : this(null, exec) { }
        public DelegateCommand(Func<Object,Boolean> canExecute,Action<Object> exec)
        {
            if (exec == null)
                throw new ArgumentNullException("No executeable object");

            _execute = exec;
            _canExecute = canExecute;
        }

        public bool CanExecute(object parameter)
        {
            return _canExecute == null ? true : _canExecute(parameter);
        }

        public event EventHandler CanExecuteChanged;

        public void Execute(object parameter)
        {
            if(!CanExecute(parameter))
            {
                throw new InvalidOperationException("The command condition is not filled");
            }

            _execute(parameter);
        }

        public void RaiseCanExecuteChanged()
        {
            if (CanExecuteChanged != null)
                CanExecuteChanged(this, EventArgs.Empty);
        }
    }
}
