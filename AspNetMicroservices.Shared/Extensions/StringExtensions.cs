using System.Linq;

namespace AspNetMicroservices.Shared.Extensions
{
	public static class StringExtensions
	{
		/// <summary>
		/// Converts first letter to upper case.
		/// </summary>
		/// <param name="str">Input string.</param>
		/// <param name="toLower">Indicates whether to transform the rest of result string to lower case.</param>
		/// <returns>String with first capital letter.</returns>
		public static string FirstLetterToUpper(this string str, bool toLower = false)
		{
			if (string.IsNullOrEmpty(str))
				return string.Empty;

			var capital = str.First().ToString().ToUpper();
			var rest = str.Substring(1);

			if (toLower)
				return capital + rest.ToLower();

			return capital + rest;
		}
	}
}