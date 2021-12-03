using System;
using System.Data;

using AspNetMicroservices.Auth.Domain.Models.Database.Users;
using AspNetMicroservices.Shared.Utils;

using FluentMigrator;

namespace AspNetMicroservices.Auth.DataAccess.Migrations
{
	/// <summary>
	/// Create users table migration.
	/// </summary>
	[Migration(20211127001)]
	public class Create_Table_Users_20211127001 : Migration
	{
		private const string FK_UserDetails_UserId_User_Id = "FK_UserDetails_UserId_User_Id";

		private readonly string _usersTableName = Utils.GetNameFromTableAttribute<UserModel>();

		private readonly string _userDetailsTableName = Utils.GetNameFromTableAttribute<UserDetailModel>();

		/// <inheritdoc cref="MigrationBase"/>
		public override void Up()
		{
			string userId = Utils.GetNameFromColumnAttribute<UserModel>(nameof(UserModel.Id));
			string foreign_user_Id = Utils.GetNameFromColumnAttribute<UserDetailModel>(nameof(UserDetailModel.UserId));

			Create.Table(_usersTableName)
				.WithColumn(userId).AsInt32().PrimaryKey().Identity()
				.WithColumn(Utils.GetNameFromColumnAttribute<UserModel>(nameof(UserModel.FirstName))).AsString(100).NotNullable()
				.WithColumn(Utils.GetNameFromColumnAttribute<UserModel>(nameof(UserModel.LastName))).AsString(100).NotNullable()
				.WithColumn(Utils.GetNameFromColumnAttribute<UserModel>(nameof(UserModel.Email))).AsString(100).NotNullable()
				.WithColumn(Utils.GetNameFromColumnAttribute<UserModel>(nameof(UserModel.Password))).AsString(50).NotNullable()
				.WithColumn(Utils.GetNameFromColumnAttribute<UserModel>(nameof(UserModel.CreatedAt))).AsDateTime().NotNullable().WithDefault(SystemMethods.CurrentDateTime)
				.WithColumn(Utils.GetNameFromColumnAttribute<UserModel>(nameof(UserModel.UpdatedAt))).AsDateTime().NotNullable().WithDefault(SystemMethods.CurrentDateTime);

			Create.Table(_userDetailsTableName)
				.WithColumn(Utils.GetNameFromColumnAttribute<UserDetailModel>(nameof(UserDetailModel.Id))).AsInt32().PrimaryKey().Identity()
				.WithColumn(foreign_user_Id).AsInt32().Nullable()
				.WithColumn(Utils.GetNameFromColumnAttribute<UserDetailModel>(nameof(UserDetailModel.Address))).AsString(200)
				.WithColumn(Utils.GetNameFromColumnAttribute<UserDetailModel>(nameof(UserDetailModel.PhoneNumber))).AsString(50)
				.WithColumn(Utils.GetNameFromColumnAttribute<UserDetailModel>(nameof(UserDetailModel.CreatedAt))).AsDateTime().NotNullable().WithDefault(SystemMethods.CurrentDateTime)
				.WithColumn(Utils.GetNameFromColumnAttribute<UserDetailModel>(nameof(UserDetailModel.UpdatedAt))).AsDateTime().NotNullable().WithDefault(SystemMethods.CurrentDateTime);

			Create.ForeignKey(FK_UserDetails_UserId_User_Id)
				.FromTable(_userDetailsTableName).ForeignColumn(foreign_user_Id)
				.ToTable(_usersTableName).PrimaryColumn(userId).OnDeleteOrUpdate(Rule.Cascade);
		}

		/// <inheritdoc cref="MigrationBase"/>
		public override void Down()
		{
			Delete.ForeignKey(FK_UserDetails_UserId_User_Id);

			Delete.Table(_usersTableName);
			Delete.Table(_userDetailsTableName);
		}
	}
}