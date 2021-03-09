using VTubeMon.Core;

namespace VTubeMon.Data.Commands
{
    public class WhereStatement
    {
        public Equality Equality { get; }
        public string Target { get; }
        public string Value { get; }
        public bool UseQuotes { get; }
        public string Statement => $"{Target} {Equality.Display()} {(UseQuotes ? ("\'" + Value + "\'") : Value)}";
    }
}
