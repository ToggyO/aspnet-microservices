using SqlStringBuilder.Interfaces;

namespace SqlStringBuilder.Internal
{
    internal sealed class UpdateQueryStatementBuilder : IUpdateQueryStatementBuilder
    {
        private UpdateQueryStatementBuilder() {}

        public static IUpdateQueryStatementBuilder Create()
        {
            return new UpdateQueryStatementBuilder();
        }
    }
}