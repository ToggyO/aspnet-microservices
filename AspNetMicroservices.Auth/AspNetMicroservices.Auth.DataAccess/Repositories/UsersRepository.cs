using System.Linq;
using System.Threading.Tasks;

using AspNetMicroservices.Abstractions.Models.Pagination;
using AspNetMicroservices.Abstractions.Models.QueryFilter.Implementation;
using AspNetMicroservices.Auth.DataAccess.Context;
using AspNetMicroservices.Auth.DataAccess.Extensions;
using AspNetMicroservices.Auth.Domain.Models.Database.Users;
using AspNetMicroservices.Auth.Domain.Repositories;
using AspNetMicroservices.Data;

using Dapper;

using SqlKata;
using SqlKata.Compilers;

namespace AspNetMicroservices.Auth.DataAccess.Repositories
{
	/// <inheritdoc cref="IUsersRepository"/>.
	public class UsersRepository : IUsersRepository
	{
		/// <summary>
		/// Instance of <see cref="AuthServiceDbContext"/>.
		/// </summary>
		private readonly AuthServiceDbContext _connectionFactory;

		/// <summary>
		/// Initialize new instance of <see cref="UsersRepository"/>.
		/// </summary>
		/// <param name="context">Instance of <see cref="AuthServiceDbContext"/>.</param>
		public UsersRepository(AuthServiceDbContext context)
		{
			_connectionFactory = context;
		}

		/// <inheritdoc cref="IUsersRepository.GetList"/>.
		public async Task<PaginationModel<UserModel>> GetList(QueryFilterModel filter)
		{
			var sql = new Query("users as u")
				.Select("u.id", "u.first_name", "u.last_name", "u.email", "u.created_at", "u.updated_at")
				.Offset(SqlHelpers.CreateOffset(filter.Page, filter.PageSize))
				.Limit(filter.PageSize)
				.OrderByWithMapping(filter.OrderBy, filter.IsDesc);

			if (filter.Search is not null)
				sql.WhereContains("u.first_name", filter.Search).OrWhereContains("u.last_name", filter.Search);

			var compiled = new PostgresCompiler().Compile(sql);

			await using var connection = _connectionFactory.GetDbConnection();
			var users = await connection.QueryAsync<UserModel>(compiled.Sql, compiled.NamedBindings);

			var result = users.ToList();
			return new PaginationModel<UserModel>
			{
				Items = result,
				Page = filter.Page,
				PageSize = filter.PageSize,
				Total = result.Count,
			};
		}

		/// <inheritdoc cref="IUsersRepository.GetByEmail"/>.
		public async Task<UserModel> GetByEmail(string email)
		{
			var sql = GetBaseUserQuery().WhereLike("u.email", email);
			return await GetUser(sql);
		}

		/// <inheritdoc cref="IUsersRepository.GetById"/>.
		public async Task<UserModel> GetById(int id)
		{
			var sql = GetBaseUserQuery().Where("u.id", id);
			return await GetUser(sql);
		}

		/// <inheritdoc cref="IUsersRepository.Create"/>.
		public async Task<UserModel> Create(UserModel entity)
		{
			await using var connection = _connectionFactory.GetDbConnection();

			var sql = new Query("users").AsInsertWithMapping(entity);
			var compiled = new PostgresCompiler().Compile(sql);
			compiled.Sql = SqlHelpers.Adapter.AppendReturningIdentity(connection, compiled.Sql);

			entity.Id = await connection.ExecuteScalarAsync<int>(compiled.Sql, compiled.NamedBindings);
			return entity;
		}

		/// <inheritdoc cref="IUsersRepository.Update"/>.
		public Task<UserModel> Update(UserModel entity)
		{
			throw new System.NotImplementedException();
		}

		/// <inheritdoc cref="IUsersRepository.Delete"/>.
		public Task<UserModel> Delete(int id)
		{
			throw new System.NotImplementedException();
		}

		/// <inheritdoc cref="IUsersRepository.CreateDetails"/>.
		public async Task<UserDetailModel> CreateDetails(UserDetailModel entity)
		{
			await using var connection = _connectionFactory.GetDbConnection();

			var sql = new Query("user_details").AsInsertWithMapping(entity);
			var compiled = new PostgresCompiler().Compile(sql);
			compiled.Sql = SqlHelpers.Adapter.AppendReturningIdentity(connection, compiled.Sql);

			entity.Id = await connection.ExecuteScalarAsync<int>(compiled.Sql, compiled.NamedBindings);
			return entity;
		}


		private async Task<UserModel> GetUser(Query query)
		{
			var compiled = new PostgresCompiler().Compile(query);

			await using var connection = _connectionFactory.GetDbConnection();
			var users = await connection.QueryAsync<UserModel, UserDetailModel, UserModel>(
				compiled.Sql,
				(u, d) =>
				{
					u.Details = d;
					return u;
				},
				param: compiled.NamedBindings,
				splitOn: "id");

			return users.FirstOrDefault();
		}

		private Query GetBaseUserQuery()
			=> new Query("users as u")
				.Select("u.id", "u.first_name", "u.last_name", "u.email", "u.salt", "u.hash", "u.created_at", "u.updated_at")
				.Join("user_details as ud", "u.id", "ud.user_id")
				.Select("ud.*");
	}
}