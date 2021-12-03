using System.Linq;
using System.Threading.Tasks;

using AspNetMicroservices.Auth.DataAccess.Context;
using AspNetMicroservices.Auth.Domain.Models.Database.Users;
using AspNetMicroservices.Auth.Domain.Repositories;
using AspNetMicroservices.Shared.Models.Pagination;
using AspNetMicroservices.Shared.Models.QueryFilter.Implementation;
using AspNetMicroservices.Shared.Utils;

using Dapper;
using Dapper.Mapper;

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
			var sqlStringBuilder = new SqlStringBuilder(UserModel.BaseQuery);
			sqlStringBuilder.AppendSorting(
				new SqlBuilderOrder
				{
					ColumnName = "u.created_at",
					Orientation = "DESC",
				});
			sqlStringBuilder.AppendPaginationQuery();

			await using var connection = _connectionFactory.GetDbConnection();

			var users = await connection.QueryAsync<UserModel>(
				sqlStringBuilder.ToString(),
				new SqlStringBuilderParameters
				{
					Page = SqlStringBuilder.CreateOffset(filter.Page, filter.PageSize),
					PageSize = filter.PageSize,
				});

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
			var sqlStringBuilder = new SqlStringBuilder("SELECT u.id, u.first_name, u.last_name, u.email, u.created_at, u.updated_at, ud.* FROM users u ");
			sqlStringBuilder.AppendLeftJoinQuery("user_details ud", "u.id", "ud.user_id");
			sqlStringBuilder.AppendQuery("WHERE u.id = @Id ");

			await using var connection = _connectionFactory.GetDbConnection();

			var users = await connection.QueryAsync<UserModel, UserDetailModel>(
				sqlStringBuilder.ToString(), param: new { Id = id });

			return users.FirstOrDefault();
		}

		/// <inheritdoc cref="IUsersRepository.Create"/>.
		public async Task<UserModel> Create(UserModel entity)
		{
			await using var connection = _connectionFactory.GetDbConnection();

			// Вынести в конфиг названия таблиц
			var sqlStringBuilder = new SqlStringBuilder("INSERT INTO users (first_name, last_name, email, password, created_at, updated_at) ");
			sqlStringBuilder.AppendQuery(
				"VALUES (@FirstName, @LastName, @Email, @Password, @CreatedAt, @UpdatedAt)");
			sqlStringBuilder.AppendReturningIdentity(connection);

			entity.Id = await connection.ExecuteScalarAsync<int>(sqlStringBuilder.ToString(), entity);

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

			var sqlStringBuilder = new SqlStringBuilder("INSERT INTO user_details (address, user_id, phone_number, created_at, updated_at) ");
			sqlStringBuilder.AppendQuery("VALUES (@Address, @UserId, @PhoneвыNumber, @CreatedAt, @UpdatedAt)");
			sqlStringBuilder.AppendReturningIdentity(connection);

			entity.Id = await connection.ExecuteScalarAsync<int>(sqlStringBuilder.ToString(), entity);

			return entity;
		}
	}
}