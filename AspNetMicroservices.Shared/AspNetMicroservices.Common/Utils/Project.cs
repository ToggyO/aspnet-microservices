using System.Reflection;
using System.Runtime.CompilerServices;

namespace AspNetMicroservices.Common.Utils
{
	/// <summary>
	/// Represents solution and project management.
	/// </summary>
	public static class Project
	{
		/// <summary>
		/// Get solution name for current executed project.
		/// </summary>
		/// <returns></returns>
		public static string GetCurrentSolutionName()
			=> TryGetSolutionSolutionFileName(Directory.GetCurrentDirectory())
				.Name.Replace(".sln", "");

		/// <summary>
		/// Get name of current executed project.
		/// </summary>
		/// <param name="assembly">
		///	Represents an assembly, which is a reusable, versionable,
		/// and self-describing building block of a common language runtime application.
		/// </param>
		/// <returns></returns>
		public static string GetCurrentProjectName(Assembly assembly)
			=> assembly.GetName().FullName?.Split(',')[0] ?? "";

		/// <summary>
		/// Get current executed method name.
		/// </summary>
		/// <param name="name">Method name injects by the compiler.</param>
		/// <returns></returns>
		public static string GetCurrentMethodName([CallerMemberName] string name = "") => name;

		/// <summary>
		/// Try to get solution file information.
		/// </summary>
		/// <param name="currentPath">Provided path.</param>
		/// <returns></returns>
		/// <exception cref="DirectoryNotFoundException"></exception>
		private static FileInfo TryGetSolutionSolutionFileName(string? currentPath)
		{
			var directory = new DirectoryInfo(currentPath ?? Directory.GetCurrentDirectory());
			FileInfo fileInfo = null;

			while (directory != null && fileInfo is null)
			{
				fileInfo = directory.GetFiles("*.sln").FirstOrDefault();
				if (fileInfo is not null)
					break;
				directory = directory.Parent;
			}

			if (directory is null || fileInfo is null)
				throw new DirectoryNotFoundException("There is no solution file");

			return fileInfo;
		}
	}
}