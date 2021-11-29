using System;

using FluentMigrator;

namespace AspNetMicroservices.Auth.DataAccess.Migrations
{
	/// <summary>
	/// Create users table migration.
	/// </summary>
	[Migration(20211127001)]
	public class Create_Table_Users_20211127001 : Migration
	{
		/// <inheritdoc cref="MigrationBase"/>
		public override void Up()
		{
			Create.Table("users")
				.WithColumn("id").AsInt32().PrimaryKey().Identity()
				.WithColumn("first_name").AsString().NotNullable()
				.WithColumn("last_name").AsString().NotNullable()
				.WithColumn("email").AsString().NotNullable()
				.WithColumn("created_at").AsDateTime().NotNullable().WithDefault(SystemMethods.CurrentDateTime)
				.WithColumn("updated_at").AsDateTime().NotNullable().WithDefault(SystemMethods.CurrentDateTime);

			Create.Table("user_details")
				.WithColumn("id").AsInt32().PrimaryKey().Identity()
				.WithColumn("address").AsString()
				.WithColumn("phone_number").AsString();
		}

		/// <inheritdoc cref="MigrationBase"/>
		public override void Down()
		{
			Delete.Table("users");
			Delete.Table("user_details");
		}
	}
}