
namespace VTubeMon.Core
{
    public enum Equality
    {
        EqualTo,
        NotEqualTo,
        GreaterThan,
        GreaterThanOrEqualTo,
        LessThan,
        LessThanOrEqualTo,
    }

    public static class EqualityExt
    {
        public static string Display(this Equality equality)
        {
            switch(equality)
            {
                case Equality.EqualTo:
                    return "=";
                case Equality.NotEqualTo:
                    return "!=";
                case Equality.GreaterThan:
                    return ">";
                case Equality.GreaterThanOrEqualTo:
                    return ">=";
                case Equality.LessThan:
                    return "<";
                case Equality.LessThanOrEqualTo:
                    return "<=";
            }

            throw new System.ArgumentException("Not a valid value for equality type", nameof(equality));
        }
    }
}
