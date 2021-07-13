using AspNetMicroservices.Products.DataLayer.Entities.Product;
using LinqToDB;

namespace AspNetMicroservices.Products.DataLayer.DataBase.AppDataConnection
{
    public interface IAppDataConnection : IDataContext
    {
        ITable<ProductEntity> Products { get; }
    }
}