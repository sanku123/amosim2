using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace AmoSim2.Others
{
    public class Command : ICommand
    {
        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        private Action<object> _executeAction = null;
        private Func<object, bool> _canExecuteFunc = (o) => true;

        public Command(Action<object> pExecuteAction, Func<object, bool> pCanExecuteFunc = null)
        {
            _executeAction = pExecuteAction;

            if (pCanExecuteFunc != null)
            {
                _canExecuteFunc = pCanExecuteFunc;
            }

            if (_executeAction == null)
            {
                throw new ArgumentNullException("Die ExecuteAction muss gesetzt sein!");
            }
        }

        public bool CanExecute(object parameter)
        {
            return _canExecuteFunc(parameter);
        }

        public void Execute(object parameter)
        {
            _executeAction(parameter);
        }
    }
}