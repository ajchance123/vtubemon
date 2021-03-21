
using System;

namespace VTubeMon.API.Core.CommandResults
{
    public struct CommandResult
    {
        public CommandResult(CommandResultType resultType, string error = null)
        {
            Error = error;
            ResultType = resultType;
        }

        public string Error { get; }
        public CommandResultType ResultType { get; }

        public static implicit operator CommandResult(Exception ex) => new CommandResult(CommandResultType.Failure, ex.Message);
    }

    public enum CommandResultType
    {
        Success,
        Failure,
        Duplicate
    }
}
