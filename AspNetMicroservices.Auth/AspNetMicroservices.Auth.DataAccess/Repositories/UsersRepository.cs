using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using AspNetMicroservices.Auth.DataAccess.Context;
using AspNetMicroservices.Auth.Domain.Models.Database.Users;
using AspNetMicroservices.Auth.Domain.Repositories;
using AspNetMicroservices.Shared.Models.Pagination;
using AspNetMicroservices.Shared.Models.QueryFilter.Implementation;

using Dapper;

namespace AspNetMicroservices.Auth.DataAccess.Repositories
{
	public class UsersRepository : IUsersRepository
	{
		private readonly AuthServiceDbContext _connectionFactory;

		public UsersRepository(AuthServiceDbContext context)
		{
			_connectionFactory = context;
		}

		public async Task<PaginationModel<UserModel>> GetList(QueryFilterModel filter)
		{
			string query = UserModel.BaseQuery;
			query += "SKIP @Page TAKE @PageSize ";

			await using var connection = _connectionFactory.GetDbConnection();

			var users = await connection.QueryAsync<UserModel>(
				query,
				new
				{
					filter.Page,
					filter.PageSize,
				});

			var result = users.ToList();
			return new PaginationModel<UserModel>
			{
				Items = result,
				Page = filter.Page,
				PageSize = filter.PageSize,
				Total = result.Count(),
			};
		}

		public async Task<UserModel> GetById(int id)
		{
			string query = UserModel.BaseQuery;
			query += "LEFT JOIN user_details ud ON u.id = ud.user_id ";
			query += "WHERE u.id = @Id";

			await using var connection = _connectionFactory.GetDbConnection();
			var users = await connection.QueryAsync<UserModel, UserDetailModel, UserModel>(query,
				(u, d) =>
				{
					u.Details = d;
					return u;
				},
				splitOn: "user_id",
				param: new { Id = id });

			return users.FirstOrDefault();
		}

		public async Task<UserModel> Create(UserModel entity)
		{
			// Вынести в конфиг названия таблиц
			string command = "INSERT INTO Users (id, first_name, last_name, email, password, created_at, updated_at)" +
			"VALUES (null, @FirstName, @LastName, @Email, @Password, CURRENT_TIMESTAMP(), CURRENT_TIMESTAMP())";

			await using var connection = _connectionFactory.GetDbConnection();
			int userId = await connection.ExecuteAsync(command, entity);

			entity.Id = userId;
			return entity;
		}

		public Task<UserModel> Update(UserModel entity)
		{
			throw new System.NotImplementedException();
		}

		public Task<UserModel> Delete(int id)
		{
			throw new System.NotImplementedException();
		}
	}
}