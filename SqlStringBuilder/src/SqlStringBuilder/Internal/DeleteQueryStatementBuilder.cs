using SqlStringBuilder.Interfaces;

namespace SqlStringBuilder.Internal
{
    internal sealed class DeleteQueryStatementBuilder : IDeleteQueryStatementBuilder
    {
        private DeleteQueryStatementBuilder() {}

        public static IDeleteQueryStatementBuilder Create()
        {
            return new DeleteQueryStatementBuilder();
        }
    }
}