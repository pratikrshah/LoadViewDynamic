using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace LoadViewDynamic
{
    public class MainWindowViewModel
    {
        object currentView;
        public RelayCommand SwitchViewCommand { get; set; }

        public MainWindowViewModel()
        {
            CurrentView = new OptionalView();
            SwitchViewCommand = new RelayCommand(mybtnCommandExecute, myCanbtnCommandExecute);
        }

        public object CurrentView
        {
            get { return this.currentView; }
            set
            {
                this.currentView = value;
                OnPropertyChanged("CurrentView");
            }
        }

        private void mybtnCommandExecute(object parameter)
        {
            SwitchView();
        }

        private bool myCanbtnCommandExecute(object parameter)
        {
            return true;
        }



        void SwitchView()
        {
            if (CurrentView is OptionalView)
                CurrentView = new SettingsView();
            else
                CurrentView = new OptionalView();
        }

        #region Implementation of INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
        #endregion
    }

    public class RelayCommand : ICommand
    {
        public delegate void ICommandExecute(object parameter);
        public delegate bool ICommandCanExecute(object parameter);

        private ICommandExecute _execute;
        private ICommandCanExecute _canexecute;

        public RelayCommand(ICommandExecute onExecuteMethod, ICommandCanExecute onCanExecuteMethod)
        {
            _execute = onExecuteMethod;
            _canexecute = onCanExecuteMethod;
        }

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public bool CanExecute(object parameter)
        {
            return _canexecute.Invoke(parameter);
        }

        public void Execute(object parameter)
        {
            _execute.Invoke(parameter);
        }
    }

    public class OptionalView { }

    public class SettingsView { }



}
