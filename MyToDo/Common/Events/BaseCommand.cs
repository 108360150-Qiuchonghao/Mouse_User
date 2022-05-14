using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MyToDo.Common.Events
{
    public class BaseCommand<T> : ICommand
    {
        #region Variables

        private readonly Action<T> _execute;
        private readonly Func<T, bool> _canExecute;
        public event EventHandler CanExecuteChanged;

        #endregion

        #region Methods
        public BaseCommand(Action<T> execute) : this(execute, null) { }

        public BaseCommand(Action<T> execute, Func<T, bool> canExecute)
        {
            if (execute == null)
            {
                throw new ArgumentNullException("execute");
            }

            _execute = execute;
            _canExecute = canExecute;
        }

        public void OnCanExecuteChanged()
        {
            if (CanExecuteChanged != null)
            {
                CanExecuteChanged(this, EventArgs.Empty);
            }
        }

        public bool CanExecute(object parameter)
        {
            return _canExecute == null || _canExecute((T)parameter);
        }

        public void Execute(object parameter)
        {
            if (CanExecute(parameter) && _execute != null)
            {
                T param = default(T);
                try
                {
                    param = (T)parameter;
                }
                catch { }

                _execute(param);
            }
        }

        #endregion
    }
}
