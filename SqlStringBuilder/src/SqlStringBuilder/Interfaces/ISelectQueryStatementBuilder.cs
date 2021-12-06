using SqlStringBuilder.Common;

namespace SqlStringBuilder.Interfaces
{
    public interface ISelectQueryStatementBuilder : IBaseQueryStatementBuilder
    {
        ISelectQueryStatementBuilder FromTable(string tableName);
        
        ISelectQueryStatementBuilder FromTable(FromTableParameter parameter);

        ISelectQueryStatementBuilder WithColumns(string[] columns);
        
        // ISelectQueryStatementBuilder 
    }
}