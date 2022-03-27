using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace AmoSim2.Others
{
    public sealed class Command : ICommand
    {
        private Action<object, CommandViewModel> _executeAction = null;
        private Func<object, CommandViewModel, bool> _canExecuteAction = (o, vm) => true;

        private CommandViewModel _viewModel { get; set; }

        public Command(Action<object, CommandViewModel> pExecute, Func<object, CommandViewModel, bool> pCanExecute = null)
        {
            _executeAction = pExecute;

            if (pCanExecute != null)
            {
                _canExecuteAction = pCanExecute;
            }

            if (_executeAction == null) throw new ArgumentNullException("An execution function must be provided!");
        }

        #region ICommand Members  

        public bool CanExecute(object parameter)
        {
            return _canExecuteAction(parameter, _viewModel);
        }
        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public void Execute(object parameter)
        {
            _executeAction(parameter, _viewModel);
        }
        #endregion
    }
}
