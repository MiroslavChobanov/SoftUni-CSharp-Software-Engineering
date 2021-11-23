﻿

namespace CommandPattern.Core.Models.CommandModels
{
    using CommandPattern.Core.Contracts;
    public class HelloCommand : ICommand
    {
        

        public string Execute(string[] args)
        {
            return $"Hello, {args[0]}";
        }
    }
}