using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AspNetMicroservices.Shared.Utils
{
	// TODO: add description
	public class SqlStringBuilder
	{
		private readonly StringBuilder _builder;

		private bool _isPaginationAppended = false;

		private string _defaultPageNumberName = "Page";

		private string _defaultPageSizeName = "PageSize";

		private Dictionary<SqlJoinTypes, string> _joinDictionary = new()
		{
			{ SqlJoinTypes.Inner, "INNER" },
			{ SqlJoinTypes.OuterLeft, "LEFT OUTER" },
			{ SqlJoinTypes.OuterRight, "RIGHT OUTER" },
		};

		// TODO: add ctor with sb capacity
		public SqlStringBuilder()
		{
			_builder = new StringBuilder();
		}

		public SqlStringBuilder(string value)
		{
			_builder = new StringBuilder(value);
		}

		public SqlStringBuilder AppendQuery(string value)
		{
			_builder.Append(value);
			return this;
		}

		public SqlStringBuilder AppendLeftJoinQuery(string tableToJoin,
			string firstEqualityCondition, string secondEqualityCondition)
		{
			AppendJoinQuery(tableToJoin,
				firstEqualityCondition, secondEqualityCondition, _joinDictionary[SqlJoinTypes.OuterLeft]);
			return this;
		}

		public SqlStringBuilder AppendRightJoinQuery(string tableToJoin,
			string firstEqualityCondition, string secondEqualityCondition)
		{
			AppendJoinQuery(tableToJoin,
				firstEqualityCondition, secondEqualityCondition, _joinDictionary[SqlJoinTypes.OuterRight]);
			return this;
		}

		public SqlStringBuilder AppendJoinQuery(string tableToJoin,
			string firstEqualityCondition, string secondEqualityCondition, string joinType = "")
		{
			AppendQuery($"{joinType} JOIN {tableToJoin} ON {firstEqualityCondition} = {secondEqualityCondition} ");
			return this;
		}

		public SqlStringBuilder AppendPaginationQuery(string pageParamName = null, string pageSizeParamName = null)
		{
			if (!_isPaginationAppended)
			{
				string page = pageParamName ?? _defaultPageNumberName;
				string pageSize = pageSizeParamName ?? _defaultPageSizeName;
				AppendQuery($"OFFSET @{page} LIMIT @{pageSize} ");
				_isPaginationAppended = true;
			}
			return this;
		}

		public SqlStringBuilder AppendSorting(params SqlBuilderOrder[] orderConditions)
		{
			var columns = orderConditions.Select(x => x.Concat());
			AppendQuery($"ORDER BY {string.Join(", ", columns)} ");
			return this;
		}

		// TODO: add semicolon at the end of the SQL statement
		public override string ToString() => _builder.ToString();

		public void Clear() => _builder.Clear();

		/// <summary>
		/// Check if page index is valid.
		/// </summary>
		/// <param name="page">Page number.</param>
		/// <param name="pageSize">Page size.</param>
		/// <returns>Page index.</returns>
		public static int CreateOffset(int page, int pageSize)
		{
			int queryPage = (page - 1) * pageSize;
			return queryPage < 0 ? 0 : queryPage;
		}
	}

	public class SqlBuilderOrder
	{
		public string ColumnName { get; set; }

		public string Orientation { get; set; } = "ASC";

		public string Concat() => $"{ColumnName} {Orientation}";
	}

	public class SqlStringBuilderParameters
	{
		public int Page { get; set; }

		public int PageSize { get; set; }
	}

	public class SqlGroupStatementBuilder
	{
		// public SqlGroupStatementBuilder WithHaving
	}

	// TODO: вынести в отдельный файл
	public enum SqlJoinTypes
	{
		Inner,
		OuterLeft,
		OuterRight,
	}
}