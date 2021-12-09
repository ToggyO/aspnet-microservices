using SqlStringBuilder.Interfaces;
using SqlStringBuilder.Interfaces.Common;

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