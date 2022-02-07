using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace AspNetMicroservices.Shared.Utils
{
	// TODO: complete class members description.
	/// <summary>
	/// Represents functionality to programmatically create SQL statements.
	/// </summary>
	public class SqlStringBuilder
	{
		private readonly StringBuilder _builder;

		private bool _isPaginationAppended = false;

		private bool _isOrderAppended = false;

		private bool _isReturningIdentityAppended = false;

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

		/// <summary>
		/// Appends provided string to resulting SQL statement.
		/// </summary>
		/// <param name="value">String with SQL statement.</param>
		/// <returns>Instance of <see cref="SqlStringBuilder"/>.</returns>
		public SqlStringBuilder AppendQuery(string value)
		{
			_builder.Append(value);
			return this;
		}

		/// <summary>
		/// Appends left join to resulting SQL statement.
		/// </summary>
		/// <param name="tableToJoin">Name of table to join to (with aliases).</param>
		/// <param name="firstEqualityCondition">Column to be matched values in both tables.</param>
		/// <param name="secondEqualityCondition">Column to be matched values in both tables.</param>
		/// <returns>Instance of <see cref="SqlStringBuilder"/>.</returns>
		public SqlStringBuilder AppendLeftJoinQuery(string tableToJoin,
			string firstEqualityCondition, string secondEqualityCondition)
		{
			AppendJoinQuery(tableToJoin,
				firstEqualityCondition, secondEqualityCondition, _joinDictionary[SqlJoinTypes.OuterLeft]);
			return this;
		}

		/// <summary>
		/// Appends right join to resulting SQL statement.
		/// </summary>
		/// <param name="tableToJoin">Name of table to join to (with aliases).</param>
		/// <param name="firstEqualityCondition">Column to be matched values in both tables.</param>
		/// <param name="secondEqualityCondition">Column to be matched values in both tables.</param>
		/// <returns>Instance of <see cref="SqlStringBuilder"/>.</returns>
		public SqlStringBuilder AppendRightJoinQuery(string tableToJoin,
			string firstEqualityCondition, string secondEqualityCondition)
		{
			AppendJoinQuery(tableToJoin,
				firstEqualityCondition, secondEqualityCondition, _joinDictionary[SqlJoinTypes.OuterRight]);
			return this;
		}

		/// <summary>
		/// Appends join to resulting SQL statement.
		/// </summary>
		/// <param name="tableToJoin">Name of table to join to (with aliases).</param>
		/// <param name="firstEqualityCondition">Column to be matched values in both tables.</param>
		/// <param name="secondEqualityCondition">Column to be matched values in both tables.</param>
		/// <param name="joinType">Type of join.</param>
		/// <returns>Instance of <see cref="SqlStringBuilder"/>.</returns>
		public SqlStringBuilder AppendJoinQuery(string tableToJoin,
			string firstEqualityCondition, string secondEqualityCondition, string joinType = "")
		{
			AppendQuery($"{joinType} JOIN {tableToJoin} ON {firstEqualityCondition} = {secondEqualityCondition} ");
			return this;
		}

		/// <summary>
		/// Appends offset and limit to resulting SQL statement.
		/// </summary>
		/// <param name="pageParamName">Name of the parameter, represents offset.</param>
		/// <param name="pageSizeParamName">Name of the parameter, represents limit.</param>
		/// <returns>Instance of <see cref="SqlStringBuilder"/>.</returns>
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

		/// <summary>
		/// Appends order to resulting SQL statement.
		/// </summary>
		/// <param name="orderConditions">Collection of order conditions.</param>
		/// <returns>Instance of <see cref="SqlStringBuilder"/>.</returns>
		public SqlStringBuilder AppendSorting(params SqlBuilderOrder[] orderConditions)
		{
			if (!_isOrderAppended)
			{
				var columns = orderConditions.Select(x => x.Concat());
				AppendQuery($"ORDER BY {string.Join(", ", columns)} ");
				_isOrderAppended = true;
			}
			return this;
		}

		/// <summary>
		/// Appends query to resulting SQL statement, that returns the last inserted id.
		/// </summary>
		/// <param name="connection">Database connection.</param>
		/// <param name="pkColumnName">Name of column in table, represents primary key.</param>
		/// <returns>Instance of <see cref="SqlStringBuilder"/>.</returns>
		public SqlStringBuilder AppendReturningIdentity(IDbConnection connection, string pkColumnName = "id")
		{
			if (!_isReturningIdentityAppended)
			{
				var name = connection.GetType().Name.ToLower();
				SqlIdentityRetriever.AdapterDictionary.TryGetValue(name, out string query);
				if (query is not null)
					_builder.Append(query.Replace("@PkId", pkColumnName));
				_isReturningIdentityAppended = true;
			}
			return this;
		}

		// TODO: add semicolon at the end of the SQL statement
		/// <summary>
		/// Concatenates raw query string into ready SQL statement.
		/// </summary>
		/// <returns></returns>
		public override string ToString() => _builder.ToString();

		/// <summary>
		/// Clears raw query string.
		/// </summary>
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

		private static class SqlIdentityRetriever
		{
			// TODO: check
			internal static readonly Dictionary<string, string> AdapterDictionary
				= new (6)
				{
					["sqlconnection"] = "; SELECT SCOPE_IDENTITY() @PkId ",
					["sqlceconnection"] = "; SELECT @@IDENTITY @PkId ",
					["npgsqlconnection"] = " RETURNING @PkId",
					["sqliteconnection"] = "; SELECT last_insert_rowid() @PkId",
					["mysqlconnection"] = "SELECT LAST_INSERT_ID() @PkId",
					["fbconnection"] = ""
				};
		}

		private enum SqlJoinTypes
		{
			Inner,
			OuterLeft,
			OuterRight,
		}
	}

	/// <summary>
	/// Represents order SQL statement condition.
	/// </summary>
	public class SqlBuilderOrder
	{
		/// <summary>
		/// Name of column to be ordered by.
		/// </summary>
		public string ColumnName { get; set; }

		/// <summary>
		/// Order orientation.
		/// </summary>
		public string Orientation { get; set; } = "ASC";

		/// <summary>
		/// Concatenate column name and order orientation.
		/// </summary>
		/// <returns></returns>
		public string Concat() => $"{ColumnName} {Orientation}";
	}

	/// <summary>
	/// Represents offset and limit SQL statment condition.
	/// </summary>
	public class SqlStringBuilderParameters
	{
		/// <summary>
		/// Offset value.
		/// </summary>
		public int Page { get; set; }

		/// <summary>
		/// Limit value.
		/// </summary>
		public int PageSize { get; set; }
	}

	public class SqlGroupStatementBuilder
	{
		// public SqlGroupStatementBuilder WithHaving
	}
}