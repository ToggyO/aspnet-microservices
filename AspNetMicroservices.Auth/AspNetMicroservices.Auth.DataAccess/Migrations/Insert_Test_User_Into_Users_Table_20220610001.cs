using AspNetMicroservices.Auth.Domain.Models.Database.Users;
using AspNetMicroservices.Common.Utils;

using FluentMigrator;

namespace AspNetMicroservices.Auth.DataAccess.Migrations
{
	/// <summary>
	/// Seed:
	/// Insert test user into users table.
	/// </summary>
	[Migration(20220610001)]
	public class Isert_Test_User_Into_Users_Table_20220610001 : Migration
	{
		private readonly string _usersTableName = Utils.GetNameFromTableAttribute<UserModel>();

		private readonly string _userDetailsTableName = Utils.GetNameFromTableAttribute<UserDetailModel>();

		/// <inheritdoc cref="MigrationBase"/>
		public override void Up()
		{
			Insert.IntoTable(_usersTableName).Row(new
			{
				first_name = "Test",
				last_name = "Testo",
				email = "qwe@qwe.com",
				salt = "HD5lOmTNAcUtCbVoH87Yqw==",
				hash = "zl/MRTwtulDvJYbrbP18NxODxnKv+SQ/mtFzD3DmyZ8="
			});

			Insert.IntoTable(_userDetailsTableName).Row(new
			{
				user_id = 1,
				address = "address",
				phone_number = "98439586969696"
			});
		}

		/// <inheritdoc cref="MigrationBase"/>
		public override void Down()
		{
			Delete.FromTable(_usersTableName).Row(new { email = "qwe@qwe.com" });
			Delete.FromTable(_usersTableName).Row(new { phone_number = "98439586969696" });
		}
	}
}