using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace AmoSim2.Others
{
    public abstract class CommandViewModel
    {
        public CommandViewModel()
        {
            InjectViewModelToCommands();
        }

        private void InjectViewModelToCommands()
        {
            var vmType = this.GetType();
            var props = vmType.GetProperties().Cast<PropertyInfo>();

            var commandProps = props.Where(x => x.PropertyType.GetInterfaces().Contains(typeof(System.Windows.Input.ICommand)));
            var injectableCommands = commandProps.Where(x => x.GetValue(this)?.GetType().Equals(typeof(Command)) ?? false);
            var commandViewModelField = typeof(Command).GetField("<_viewModel>k__BackingField", BindingFlags.NonPublic | BindingFlags.Instance);

            foreach (var c in injectableCommands)
            {
                var command = c.GetValue(this);
                if (command != null)
                {
                    commandViewModelField?.SetValue(command, this);
                }
            }
        }
    }
}
