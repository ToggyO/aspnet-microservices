using SqlStringBuilder.Interfaces.Common;

namespace SqlStringBuilder.Interfaces.Select
{
	// TODO: add description.
    public interface ISelectQueryStatementBuilder : IBaseQueryStatementBuilder
    {
        ISelectQueryStatementBuilder From(string tableName, string alias = null);

        ISelectQueryStatementBuilder Select(params string[] columns);
    }
}