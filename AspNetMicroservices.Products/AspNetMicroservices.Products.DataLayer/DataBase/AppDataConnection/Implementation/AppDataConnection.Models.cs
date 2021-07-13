using AspNetMicroservices.Products.DataLayer.Entities.Product;
using LinqToDB;

namespace AspNetMicroservices.Products.DataLayer.DataBase.AppDataConnection.Implementation
{
    public sealed partial class AppDataConnection : IAppDataConnection
    {
        public ITable<ProductEntity> Products => GetTable<ProductEntity>();
    }
}