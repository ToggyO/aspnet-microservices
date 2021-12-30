using AspNetMicroservices.Auth.DataAccess.Helpers;
using AspNetMicroservices.Auth.Domain.Models.Database.Users;

namespace AspNetMicroservices.Auth.DataAccess.Mapping
{
	/// <summary>
	/// Maps C# classes into database tables.
	/// </summary>
	public static class DbModelsMapping
	{
		public static void Initialize()
		{
			DapperHelpers.SetTypeMap<UserModel>();
			DapperHelpers.SetTypeMap<UserDetailModel>();
		}
	}
}