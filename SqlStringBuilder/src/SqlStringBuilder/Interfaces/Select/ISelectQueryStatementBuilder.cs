using SqlStringBuilder.Interfaces.Common;

namespace SqlStringBuilder.Interfaces.Select
{
    public interface ISelectQueryStatementBuilder : IBaseQueryStatementBuilder
    {
        ISelectQueryStatementBuilder From(string tableName, string alias = null);

        ISelectQueryStatementBuilder Select(params string[] columns);
    }

    // TODO: check
    // public interface IAsSelectQueryStatementBuilder : ISelectQueryStatementBuilder
    // {
    //     ISelectQueryStatementBuilder As(string alias);
    // }
}