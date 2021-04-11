using VTubeMon.Common;

namespace VTubeMon.Data.Commands
{
    public class WhereStatement
    {
        public Equality Equality { get; set; }
        public string Target { get; set; }
        public string Value { get; set; }
        public bool UseQuotes { get; set; }
        public string Statement => $"{Target} {Equality.Display()} {(UseQuotes ? ("\'" + Value + "\'") : Value)}";
    }
}
