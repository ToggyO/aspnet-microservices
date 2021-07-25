using AspNetMicroservices.Products.DataLayer.Entities.Product;
using LinqToDB;

namespace AspNetMicroservices.Products.DataLayer.DataBase.AppDataConnection
{
	// TODO: add summary
    public sealed partial class AppDataConnection
    {
        public ITable<ProductEntity> Products => GetTable<ProductEntity>();
    }
}