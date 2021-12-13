using SqlStringBuilder.Interfaces.Select;
using SqlStringBuilder.Internal.BaseQuery;
using SqlStringBuilder.Internal.Components;
using SqlStringBuilder.Internal.Constants;
using SqlStringBuilder.Internal.Enums;

namespace SqlStringBuilder.Internal.Select
{
	/// <inheritdoc cref="ISelectQueryStatementBuilder"/>.
    internal sealed class SelectQueryStatementBuilder
        : BaseQueryStatementBuilder<SelectQueryStatementBuilder>, ISelectQueryStatementBuilder
    {
	    /// <inheritdoc cref="ISelectQueryStatementBuilder.IsDistinct"/>.
	    public bool IsDistinct { get; private set; } = false;

	    /// <summary>
	    /// Initialize new instance of <see cref="SelectQueryStatementBuilder"/>.
	    /// </summary>
	    private SelectQueryStatementBuilder() => QueryType = SqlStatementTypes.Select;

	    /// <summary>
	    /// Creates a new instance of <see cref="SelectQueryStatementBuilder"/>.
	    /// </summary>
	    /// <returns></returns>
        public static ISelectQueryStatementBuilder Create()
        {
            return new SelectQueryStatementBuilder();
        }

	    /// <inheritdoc cref="ISelectQueryStatementBuilder.From"/>.
        public ISelectQueryStatementBuilder From(string tableName, string alias = null)
            => AddOrReplaceComponent(ComponentTypes.From, new FromComponent
            {
                Table = tableName,
                Alias = alias,
            });

	    /// <inheritdoc cref="ISelectQueryStatementBuilder.Select"/>.
        public ISelectQueryStatementBuilder Select(string[] columns)
        {
            foreach (string column in columns)
			{
                AddComponent(ComponentTypes.Select, new Column
                {
                    Name = column,
                });
			}
            return this;
        }

	    /// <inheritdoc cref="ISelectQueryStatementBuilder.Distinct"/>.
        public ISelectQueryStatementBuilder Distinct()
        {
	        IsDistinct = true;
	        return this;
        }
    }
}