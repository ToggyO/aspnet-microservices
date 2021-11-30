﻿using System;
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
		private readonly AuthServiceDbContext _connectionFactory;

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
			var sqlStringBuilder = new SqlStringBuilder(UserModel.BaseQuery);
			sqlStringBuilder.AppendLeftJoinQuery("user_details ud", "u.id", "ud.user_id");
			sqlStringBuilder.AppendQuery("WHERE u.id = @Id ");

			await using var connection = _connectionFactory.GetDbConnection();
			// TODO: delete
			// var users = await connection.QueryAsync<UserModel, UserDetailModel, UserModel>(
			// 	sqlStringBuilder.ToString(),
			// 	(u, d) =>
			// 	{
			// 		u.Details = d;
			// 		return u;
			// 	},
			// 	splitOn: "user_id",
			// 	param: new { Id = id });

			var users = await connection.QueryAsync<UserModel, UserDetailModel>(
				sqlStringBuilder.ToString(), param: new { Id = id });

			return users.FirstOrDefault();
		}

		/// <inheritdoc cref="IUsersRepository.Create"/>.
		public async Task<UserModel> Create(UserModel entity)
		{
			// Вынести в конфиг названия таблиц
			var sqlStringBuilder = new SqlStringBuilder("INSERT INTO users (first_name, last_name, email, password, created_at, updated_at) ");
			sqlStringBuilder.AppendQuery(
				"VALUES (@FirstName, @LastName, @Email, @Password, @CreatedAt, @UpdatedAt)");

			await using var connection = _connectionFactory.GetDbConnection();
			await using var t = await connection.BeginTransactionAsync();

			try
			{
				entity.Id = await connection.ExecuteAsync(sqlStringBuilder.ToString(), entity);
				sqlStringBuilder.Clear();

				if (entity.Details is not null)
				{
					sqlStringBuilder.AppendQuery("INSERT INTO user_details (address, phone_number, created_at, updated_at) ");
					sqlStringBuilder.AppendQuery("VALUES (@Address, @PhoneNumber, @CreatedAt, @UpdatedAt)");
					entity.Details.Id = await connection.ExecuteAsync(sqlStringBuilder.ToString(), entity.Details);
				}

				await t.CommitAsync();
				return entity;
			}
			catch (Exception e)
			{
				await t.RollbackAsync();
				return default;
			}
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
	}
}