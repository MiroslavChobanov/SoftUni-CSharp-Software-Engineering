

namespace CommandPattern.Core.Models.CommandModels
{
    using CommandPattern.Core.Contracts;
    using System;

    public class ExitCommand : ICommand
    {
        

        public string Execute(string[] args)
        {
            return null;
        }
    }
}