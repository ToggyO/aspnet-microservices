using SqlStringBuilder.Interfaces;

namespace SqlStringBuilder.Internal
{
    internal sealed class SelectQueryStatementBuilder : ISelectQueryStatementBuilder
    {
        private SelectQueryStatementBuilder() {}

        public static ISelectQueryStatementBuilder Create()
        {
            return new SelectQueryStatementBuilder();
        }
    }
}