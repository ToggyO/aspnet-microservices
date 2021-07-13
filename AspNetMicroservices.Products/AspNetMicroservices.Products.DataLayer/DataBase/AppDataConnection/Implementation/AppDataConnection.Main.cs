using LinqToDB.Configuration;
using LinqToDB.Data;

namespace AspNetMicroservices.Products.DataLayer.DataBase.AppDataConnection.Implementation
{
    public sealed partial class AppDataConnection : DataConnection
    {
        public AppDataConnection(LinqToDbConnectionOptions<AppDataConnection> options)
            : base(options)
        {}
    }
}