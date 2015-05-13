using System;
using System.Windows.Input;

namespace DomainModel
{
    /// <summary>
    /// General command type.
    /// </summary>
    public class DelegateCommand : ICommand
    {
        private readonly Action<Object> _execute; // lambda which executes the action
        private readonly Func<Object, Boolean> _canExecute; // lambda which checks the precondition of the execution

        /// <summary>
        /// Executability event.
        /// </summary>
        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        /// <summary>
        /// Initializes a new instance.
        /// </summary>
        /// <param name="execute">Executable activity.</param>
        public DelegateCommand(Action<Object> execute) : this(null, execute) { }

        /// <summary>
        /// Initializes a new instance.
        /// </summary>
        /// <param name="canExecute">Precondition.</param>
        /// <param name="execute">Executable activity.</param>
        public DelegateCommand(Func<Object, Boolean> canExecute, Action<Object> execute)
        {
            if (execute == null)
            {
                throw new ArgumentNullException("execute");
            }

            _execute = execute;
            _canExecute = canExecute;
        }

        /// <summary>
        /// Checking precondition.
        /// </summary>
        /// <param name="parameter">Parameter of the activity.</param>
        /// <returns>True if the activity can be executed.</returns>
        public Boolean CanExecute(Object parameter)
        {
            return _canExecute == null ? true : _canExecute(parameter);
        }

        /// <summary>
        /// Execution of the activity.
        /// </summary>
        /// <param name="parameter">Parameter of the activity.</param>
        public void Execute(Object parameter)
        {
            if (!CanExecute(parameter))
            {
                throw new InvalidOperationException("Command execution is disabled.");
            }
            _execute(parameter);
        }
    }
}
