using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace SqlStringBuilder.Internal.Constants
{
	/// <summary>
	/// Comparison operator literals.
	/// </summary>
	public static class ComparisonOperators
	{
		public const string Equal = "=";

		public const string NotEqual = "!=";

		public const string NotEqualDb = "<>";

		public const string NotEqual2 = "<=>";

		public const string MoreThen = ">";

		public const string MoreThenOrEqual = ">=";

		public const string LessThen = "<";

		public const string LessThenOrEqual = "<=";

		public static List<string> ToList() => ListedConstants.ToList();
	}

	/// <summary>
	/// Database comparison operator literals.
	/// </summary>
	internal static class DbComparisonOperators
	{
		public const string Like = "like";

		public const string NotLike = "not like";

		public const string ILike = "ilike";

		public const string NotILike = "not ilike";

		public static List<string> ToList() => ListedConstants.ToList();
	}

	internal static class ListedConstants
	{
		public static List<string> ToList()
		{
			return typeof(ComparisonOperators).GetFields(BindingFlags.Public | BindingFlags.Static |
			                                             BindingFlags.FlattenHierarchy)
				.Where(fi => fi.IsLiteral && !fi.IsInitOnly).Select(x => (string)x.GetValue(null)).ToList();
		}
	}
}