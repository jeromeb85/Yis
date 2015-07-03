using System;
using System.Windows.Input;

namespace Yis.Framework.Presentation.Commanding
{
    public class Command : ICommand
    {
        #region Constructors + Destructors

        /// <summary>
        /// Constructeur
        /// </summary>
        /// <param name="execute">Méthode d'exécution de la commande</param>
        public Command(Action execute)
            : this(execute, null)
        {
        }

        /// <summary>
        /// Constructeur
        /// </summary>
        /// <param name="execute">   Méthode d'exécution de la commande</param>
        /// <param name="canExecute">Méthode permettant de savoir si la commande peut être exécutée</param>
        public Command(Action execute, Func<bool> canExecute)
        {
            if (execute == null)
                throw new ArgumentNullException("execute", "Le délégué execute ne peut pas être nul");

            _execute = execute;
            _canExecute = canExecute;
        }

        #endregion Constructors + Destructors

        #region Fields

        private Func<bool> _canExecute;
        private Action _execute;

        #endregion Fields

        #region Events

        public event EventHandler CanExecuteChanged
        {
            add
            {
                if (_canExecute != null)
                {
                    CommandManager.RequerySuggested += value;
                }
            }

            remove
            {
                if (_canExecute != null)
                {
                    CommandManager.RequerySuggested -= value;
                }
            }
        }

        #endregion Events

        #region Methods

        //protected virtual void OnCanExecuteChanged(EventArgs e)
        //{
        //    EventHandler handler = CanExecuteChanged;
        //    if (handler != null)
        //    {
        //        handler(this, e);
        //    }
        //}
        public bool CanExecute(object parameter)
        {
            return _canExecute == null ? true : _canExecute();
        }

        public void Execute(object parameter)
        {
            _execute();
        }

        public void RaiseCanExecuteChanged()
        {
            CommandManager.InvalidateRequerySuggested();
        }

        #endregion Methods
    }

    public class Command<T> : ICommand
    {
        #region Constructors + Destructors

        /// <summary>
        /// Constructeur
        /// </summary>
        /// <param name="execute">Méthode d'exécution de la commande</param>
        public Command(Action<T> execute)
            : this(execute, null)
        {
        }

        /// <summary>
        /// Constructeur
        /// </summary>
        /// <param name="execute">   Méthode d'exécution de la commande</param>
        /// <param name="canExecute">Méthode permettant de savoir si la commande peut être exécutée</param>
        public Command(Action<T> execute, Func<T, bool> canExecute)
        {
            if (execute == null)
                throw new ArgumentNullException("execute", "Le délégué execute ne peut pas être nul");

            _execute = execute;
            _canExecute = canExecute;
        }

        #endregion Constructors + Destructors

        #region Fields

        private Func<T, bool> _canExecute;
        private Action<T> _execute;

        #endregion Fields

        //public void RaiseCanExecuteChanged()
        //{
        //    OnCanExecuteChanged(EventArgs.Empty);
        //}

        //protected virtual void OnCanExecuteChanged(EventArgs e)
        //{
        //    EventHandler handler = CanExecuteChanged;
        //    if (handler != null)
        //    {
        //        handler(this, e);
        //    }
        //}

        #region Events

        public event EventHandler CanExecuteChanged
        {
            add
            {
                if (_canExecute != null)
                {
                    CommandManager.RequerySuggested += value;
                }
            }

            remove
            {
                if (_canExecute != null)
                {
                    CommandManager.RequerySuggested -= value;
                }
            }
        }

        #endregion Events

        #region Methods

        public bool CanExecute(object parameter)
        {
            return _canExecute == null ? true : _canExecute((T)parameter);
        }

        public void Execute(object parameter)
        {
            _execute((T)parameter);
        }

        #endregion Methods
    }
}