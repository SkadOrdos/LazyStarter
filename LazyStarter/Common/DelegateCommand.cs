using System;
using System.Windows.Input;
using System.Windows.Threading;

namespace LazyStarter
{
    /// <summary>
    /// Provides a ICommand implementation.
    /// </summary>
    public class DelegateCommand : ICommand
    {
        private readonly Action<Object> _command;
        private readonly Predicate<Object> _canExecute;


        /// <summary>
        /// Initializes a new instance of the  class.
        /// </summary>
        /// <param name="execute">The execute action.</param>
        /// <param name="canExecute">The can execute predicate.</param>
        public DelegateCommand(Action<Object> command, Predicate<Object> canExecute = null)
        {
            if (command == null)
            {
                throw new ArgumentNullException("command");
            }

            this._canExecute = canExecute;
            this._command = command;
        }


        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }


        /// <summary>
        /// Defines the method that determines whether the command can execute in its current state.
        /// </summary>
        /// <param name="parameter">Data used by the command. If the command does not require data to be passed, this object can be set to null.</param>
        /// <returns>
        /// True if this command can be executed, otherwise - false.
        /// </returns>
        public bool CanExecute(Object parameter)
        {
            return _canExecute == null ? true : _canExecute(parameter);
        }

             
        /// <summary>
        /// Defines the method to be called when the command is invoked.
        /// </summary>
        /// <param name="parameter">Data used by the command. If the command does not require data to be passed, this object can be set to null.</param>
        public void Execute(Object parameter)
        {
            if (CanExecute(parameter))
            {
                this._command(parameter);        
            }
        }
    }
}