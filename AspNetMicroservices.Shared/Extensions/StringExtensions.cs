using System.Linq;

namespace AspNetMicroservices.Shared.Extensions
{
	public static class StringExtensions
	{
		/// <summary>
		/// Converts first letter to upper case.
		/// </summary>
		/// <param name="str">Input string.</param>
		/// <returns>String with first capital letter.</returns>
		public static string FirstLetterToUpper(this string str)
			=> string.IsNullOrEmpty(str) ? string.Empty : str.First().ToString().ToUpper() + str.Substring(1).ToLower();

	}
}