﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace LegyenOnIsMilliardosClassLibary.Commands;
public class RelayCommands : ICommand
{
    private readonly Action<object> _execute;
    private readonly Predicate<object> _canExecute;

    public event EventHandler CanExecuteChanged;

    public RelayCommands(Action<object> execute) : this(execute, null) { }

    public RelayCommands(Action<object> execute, Predicate<object> canExecute)
    {
        _execute = execute ?? throw new ArgumentNullException(nameof(execute));
        _canExecute = canExecute;
    }

    public bool CanExecute(object parameter)
    {
        return _canExecute == null || _canExecute(parameter);
    }

    public void Execute(object parameter)
    {
        _execute(parameter);
    }

    public void RaiseCanExecuteChanged()
    {
        CanExecuteChanged?.Invoke(this, EventArgs.Empty);
    }
}
