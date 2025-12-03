using System;
using System.Collections.Generic;

namespace patterns.Command
{
    public class CommandInvoker
    {
        private Stack<ICommand> _commandHistory = new Stack<ICommand>();
        private Dictionary<char, ICommand> _keyBindings = new Dictionary<char, ICommand>();

        public void BindKey(char key, ICommand command) { _keyBindings[key] = command; }

        public void PressKey(char key)
        {
            if (_keyBindings.TryGetValue(key, out ICommand command))
            {
                Console.WriteLine($"\nPressed key '{key}': {command.GetType().Name}");
                command.Execute();
                _commandHistory.Push(command);
            }
            else { Console.WriteLine($"No command bound to key '{key}'"); }
        }

        public void UndoLastCommand()
        {
            if (_commandHistory.Count > 0)
            {
                var command = _commandHistory.Pop();
                command.Undo();
            }
            else { Console.WriteLine("No commands to undo"); }
        }

        public void ShowKeyBindings()
        {
            Console.WriteLine("\nCurrent Key Bindings");
            foreach (var binding in _keyBindings) { Console.WriteLine($"Key '{binding.Key}': {binding.Value.GetType().Name}"); }
        }
    }
}