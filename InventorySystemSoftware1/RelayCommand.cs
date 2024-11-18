/**************************************************************************************************
 * Project Name    : Employee Management System
 * File Name       : RelayCommand.cs
 * Description     : Implements a relay command that simplifies the ICommand interface for binding 
 *                   commands in ViewModels. Enables binding of UI actions to methods without 
 *                   needing custom command classes.
 * 
 * Author          : [Aaryaka P Nath]
 * Date Created    : [yyyy-MM-dd]
 ***************************************************************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace InventorySystemSoftware1
{
    public class RelayCommand : ICommand
    {
        private readonly Action _execute;
        private readonly Func<bool> _canExecute;

        public RelayCommand(Action execute, Func<bool> canExecute = null)
        {
            _execute = execute ?? throw new ArgumentNullException(nameof(execute));
            _canExecute = canExecute;
        }
        //

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }
        //
        public bool CanExecute(object parameter)
        {
            return _canExecute == null || _canExecute();
        }

        public void Execute(object parameter)
        {
            _execute();
        }
    }
}
