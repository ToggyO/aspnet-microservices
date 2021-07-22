using AspNetMicroservices.Products.DataLayer.Entities.Product;
using LinqToDB;

namespace AspNetMicroservices.Products.DataLayer.DataBase.AppDataConnection
{
    public sealed partial class AppDataConnection
    {
        public ITable<ProductEntity> Products => GetTable<ProductEntity>();
    }
}