using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace Commands
{
    //Allgemeine generische Commandklasse, welche individuell befüllt werden kann.

    //ICommand ermöglicht dieser Klasse, als COmmand verwendet zu werden
    public class CustomCommand : ICommand
    {
        //Delegates zum Speichern der Logik
        public Action<object> ExecuteMethode { get; set; }
        public Func<object, bool> CanExecuteMethode { get; set; }

        //Konstruktor
        public CustomCommand(Action<object> exe, Func<object, bool> can = null)
        {
            ExecuteMethode = exe;

            if (can == null) CanExecuteMethode = p => true;
            else CanExecuteMethode = can;
        }

        //Anmeldung des Commands im CommandManager
        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        //Bedingung für die Ausführung
        public bool CanExecute(object parameter)
        {
            return CanExecuteMethode(parameter);
        }

        //Aktion bei Ausführung
        public void Execute(object parameter)
        {
            ExecuteMethode(parameter);
        }
    }
}
