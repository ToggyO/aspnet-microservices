using System;
using System.Linq;
using System.Threading.Tasks;

using AspNetMicroservices.Auth.DataAccess.Context;
using AspNetMicroservices.Auth.DataAccess.Extensions;
using AspNetMicroservices.Auth.Domain.Models.Database.Users;
using AspNetMicroservices.Auth.Domain.Repositories;
using AspNetMicroservices.Shared.Models.Pagination;
using AspNetMicroservices.Shared.Models.QueryFilter.Implementation;
using AspNetMicroservices.Shared.Utils;

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

		/// <inheritdoc cref="IUsersRepository.GetById"/>.
		public async Task<UserModel> GetById(int id)
		{
			//var sqlStringBuilder = new SqlStringBuilder("SELECT u.id, u.first_name, u.last_name, u.email, u.created_at, u.updated_at, ud.* FROM users u ");
			//sqlStringBuilder.AppendLeftJoinQuery("user_details ud", "u.id", "ud.user_id");
			//sqlStringBuilder.AppendQuery("WHERE u.id = @Id ");

			//await using var connection = _connectionFactory.GetDbConnection();

			//var users = await connection.QueryAsync<UserModel, UserDetailModel>(
			//	sqlStringBuilder.ToString(), param: new { Id = id });

			//return users.FirstOrDefault();
			return new UserModel();
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
	}
}