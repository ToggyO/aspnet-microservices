using AspNetMicroservices.Products.DataLayer.Entities;
using AspNetMicroservices.Products.DataLayer.Entities.Product;

using FluentMigrator;

namespace AspNetMicroservices.Products.DataLayer.Migrations
{
	/// <summary>
	/// Create product table migration.
	/// </summary>
	[Migration(20210711001)]
	public class Create_Table_Products_20210711001 : Migration
	{
		/// <summary>
		/// Products table name.
		/// </summary>
		private readonly string _productsTableName = EntityHelpers.ExtractTableName<ProductEntity>();

		/// <inheritdoc cref="MigrationBase"/>
		public override void Up()
		{
			Create.Table(_productsTableName)
				.WithColumn(
					EntityHelpers.ExtractTableColumnName<ProductEntity>(nameof(ProductEntity.Id))).AsInt32().PrimaryKey().Identity()
				.WithColumn(
					EntityHelpers.ExtractTableColumnName<ProductEntity>(nameof(ProductEntity.Name))).AsString().NotNullable()
				.WithColumn
					(EntityHelpers.ExtractTableColumnName<ProductEntity>(nameof(ProductEntity.Description))).AsString()
				.WithColumn(
					EntityHelpers.ExtractTableColumnName<ProductEntity>(nameof(ProductEntity.Price))).AsInt64().NotNullable()
				.WithColumn(
					EntityHelpers.ExtractTableColumnName<ProductEntity>(nameof(ProductEntity.CreatedAt))).AsDateTime().NotNullable()
				.WithColumn(
					EntityHelpers.ExtractTableColumnName<ProductEntity>(nameof(ProductEntity.UpdatedAt))).AsDateTime().NotNullable();
		}

		/// <inheritdoc cref="MigrationBase"/>
		public override void Down()
		{
			Delete.Table(_productsTableName);
		}
	}
}